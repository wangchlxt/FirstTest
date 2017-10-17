using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wechat.Util.Messages.WechatApi;
using Wechat.Util.Messages.WechatApi.Custom;
using CSharpUtils;
using Redis;

namespace Wechat.Util
{
    public static class WechatApi
    {
        private static object locker = new object();

        private static string AccessToken { get; set; }

        #region HttpGet
        /// <summary>
        /// 发送 GET 请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string HttpGet( string url )
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create( url );

            request.Method = "GET";

            using( HttpWebResponse response = (HttpWebResponse)request.GetResponse() )
            {
                using( Stream stream = response.GetResponseStream() )
                {
                    using( StreamReader sr = new StreamReader( stream ) )
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
        #endregion

        #region HttpPost
        /// <summary>
        /// 发送 HTTP POST 请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        private static string HttpPost( string url, string body )
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create( url );

            request.ContentType = "application/json";
            request.Method = "POST";

            byte[] buffer = Encoding.UTF8.GetBytes( body );

            request.ContentLength = buffer.Length;

            using( Stream stream = request.GetRequestStream() )
            {
                stream.Write( buffer, 0, buffer.Length );
            }

            using( HttpWebResponse response = (HttpWebResponse)request.GetResponse() )
            {
                using( Stream stream = response.GetResponseStream() )
                {
                    using( StreamReader sr = new StreamReader( stream ) )
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
        #endregion

        #region GetAccessTokenFromWechat
        /// <summary>
        /// 从微信服务器获取票据凭证
        /// </summary>
        /// <returns></returns>
        public static AccessTokenResponseMessage GetAccessTokenFromWechat()
        {
            string url = string.Format( "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}",
                ConfigurationManager.AppSettings["AppId"],
                ConfigurationManager.AppSettings["AppSecret"] );

            LogHelper.Info( "正在调用接口获取票据凭证..." );

            AccessTokenResponseMessage response = WechatApi.GetWechatApi<AccessTokenResponseMessage>( url );

            LogHelper.Info( "已获取票据凭证。" );

            if(  response.ErrorCode.HasValue && response.ErrorCode.Value != 0  )
            {
                throw new WechatApiInvokeException( response.ErrorCode.Value, response.ErrorMessage, url );
            }

            return response;
        }
        #endregion

        #region GetAccessTokenFromDb
        /// <summary>
        /// 从数据库中获取缓存的票据凭证
        /// </summary>
        /// <returns></returns>
        private static WechatAccessToken GetAccessTokenFromDb()
        {
            RedisHelper redis = new RedisHelper();
            return redis.StringGet<WechatAccessToken>(ConfigurationManager.AppSettings["AppId"]);
        }
        #endregion

        #region GetAccessToken
        /// <summary>
        /// 获取票据凭证
        /// </summary>
        /// <returns></returns>
        public static string GetAccessToken()
        {
            lock( locker )
            {
                if( string.IsNullOrEmpty( WechatApi.AccessToken ) )
                {
                    WechatAccessToken token = WechatApi.GetAccessTokenFromDb();

                    if( token == null )
                    {
                        AccessTokenResponseMessage response = WechatApi.GetAccessTokenFromWechat();

                        token = new WechatAccessToken();

                        token.AppId = ConfigurationManager.AppSettings["AppId"];
                        token.AccessToken = response.AccessToken;
                        token.ExpiresIn = response.ExpiresIn;
                        token.UpdateTime = DateTime.Now;
                        
                        RedisHelper redis = new RedisHelper();
                        redis.StringSet<WechatAccessToken>(token.AppId, token);

                        WechatApi.AccessToken = token.AccessToken;
                    }
                    else
                    {
                        WechatApi.AccessToken = token.AccessToken;
                    }
                }

                return WechatApi.AccessToken;
            }
        }

        public static string GetAccessTokenAndRefresh()
        {
            lock(locker)
            {
                RedisHelper redis = new RedisHelper();
                WechatAccessToken token = redis.StringGet<WechatAccessToken>("WechatAccessToken");
                if(token == null)
                {
                    token = new WechatAccessToken();
                    AccessTokenResponseMessage message = GetAccessTokenFromWechat();

                    token.AccessToken = message.AccessToken;
                    token.ExpiresIn = message.ExpiresIn;
                    token.UpdateTime = DateTime.Now;

                    redis.StringSet<WechatAccessToken>("WechatAccessToken", token);
                }
                else
                {
                    TimeSpan ts = DateTime.Now - token.UpdateTime;
                    if(ts.TotalMinutes > 90)
                    {
                        AccessTokenResponseMessage message = GetAccessTokenFromWechat();

                        token.AccessToken = message.AccessToken;
                        token.ExpiresIn = message.ExpiresIn;
                        token.UpdateTime = DateTime.Now;

                        redis.StringSet<WechatAccessToken>("WechatAccessToken", token);
                    }
                }
                
                return token.AccessToken;
            }
        }
        #endregion

        #region RefreshAccessToken
        /// <summary>
        /// 刷新票据凭证
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static string RefreshAccessToken( string accessToken )
        {
            lock( locker )
            {
                if( WechatApi.AccessToken != accessToken )
                {
                    return WechatApi.AccessToken;
                }

                AccessTokenResponseMessage response = WechatApi.GetAccessTokenFromWechat();

                WechatAccessToken token = new WechatAccessToken();

                token.AppId = ConfigurationManager.AppSettings["AppId"];
                token.AccessToken = response.AccessToken;
                token.ExpiresIn = response.ExpiresIn;
                token.UpdateTime = DateTime.Now;
                
                RedisHelper redis = new RedisHelper();
                redis.StringSet<WechatAccessToken>(token.AppId, token);

                WechatApi.AccessToken = token.AccessToken;

                return WechatApi.AccessToken;
            }
        }
        #endregion

        #region GetUserAccessToken
        /// <summary>
        /// 获取微信用户 Access Token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static UserAccessTokenResponseMessage GetUserAccessToken( string code )
        {
            string url = string.Format( "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code",
                ConfigurationManager.AppSettings["AppId"],
                ConfigurationManager.AppSettings["AppSecret"],
                code );

            UserAccessTokenResponseMessage response = WechatApi.GetWechatApi<UserAccessTokenResponseMessage>( url );

            if( response.ErrorCode.HasValue && response.ErrorCode.Value != 0)
            {
                throw new WechatApiInvokeException( response.ErrorCode.Value, response.ErrorMessage, url );
            }

            return response;
        }
        #endregion

        #region GetWechatApi
        /// <summary>
        /// 通过 GET 方式调用微信 API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        private static T GetWechatApi<T>( string rawUrl ) where T : WechatApiResponseMessage
        {
            string responseBody = WechatApi.HttpGet( rawUrl );

            return JsonConvert.DeserializeObject<T>( responseBody );
        }
        #endregion

        #region GetWechatApiWithToken
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        private static T GetWechatApiWithToken<T>( string rawUrl ) where T: WechatApiResponseMessage
        {
            string accessToken = WechatApi.GetAccessToken();

            string url = rawUrl + "access_token=" + accessToken;

            T response = WechatApi.GetWechatApi<T>( url );

            if( response.ErrorCode.HasValue )
            {
                switch( response.ErrorCode.Value )
                {
                    case 0:
                        break;
                    // 如果是票据过期问题，则重新获取票据
                    case 40001:
                    case 40014:
                    case 42001:
                        LogHelper.Info( string.Format( "调用微信接口时票据凭证无效或过期, errcode : {0}, errmsg : {1}, interface : {2}", response.ErrorCode, response.ErrorMessage, url ) );

                        // 重新获取票据凭证
                        accessToken = WechatApi.RefreshAccessToken( accessToken );

                        url = rawUrl + "access_token=" + accessToken;

                        response = WechatApi.GetWechatApi<T>( url );

                        if( response.ErrorCode.HasValue && response.ErrorCode.Value != 0 )
                        {
                            throw new WechatApiInvokeException( response.ErrorCode.Value, response.ErrorMessage, url );
                        }
                        break;
                    default:
                        throw new WechatApiInvokeException( response.ErrorCode.Value, response.ErrorMessage, url );
                }
            }

            return response;
        }
        #endregion

        #region PostWechatApi
        /// <summary>
        /// 通过 POST 方式调用微信 API
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        private static T PostWechatApi<T>( string url, WechatApiRequestMessage request ) where T : WechatApiResponseMessage
        {
            string body = WechatApi.HttpPost( url, JsonConvert.SerializeObject( request ) );

            return JsonConvert.DeserializeObject<T>( body );
        }
        private static T PostWechatApi<T>(string url, string request) where T : WechatApiResponseMessage
        {
            string body = WechatApi.HttpPost(url, request);

            return JsonConvert.DeserializeObject<T>(body);
        }
        #endregion

        #region PostWechatApiWithToken
        /// <summary>
        /// 使用票据凭证以 POST 方式调用微信 API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rawUrl"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private static T PostWechatApiWithToken<T>( string rawUrl, WechatApiRequestMessage request ) where T : WechatApiResponseMessage
        {
            string accessToken = WechatApi.GetAccessToken();

            string url = rawUrl + "access_token=" + accessToken;

            T response = WechatApi.PostWechatApi<T>( url, request );

            if( response.ErrorCode.HasValue )
            {
                switch( response.ErrorCode.Value )
                {
                    case 0:
                        break;
                    // 如果是票据过期问题，则重新获取票据
                    case 40001:
                    case 40014:
                    case 42001:
                        LogHelper.Info( string.Format( "调用微信接口时票据凭证无效或过期, errcode : {0}, errmsg : {1}, interface : {2}", response.ErrorCode, response.ErrorMessage, url ) );

                        // 重新获取票据凭证
                        accessToken = WechatApi.RefreshAccessToken( accessToken );

                        url = rawUrl + "access_token=" + accessToken;

                        response = WechatApi.PostWechatApi<T>( url, request );

                        if( response.ErrorCode.HasValue && response.ErrorCode.Value != 0 )
                        {
                            throw new WechatApiInvokeException( response.ErrorCode.Value, response.ErrorMessage, url );
                        }
                        break;
                    default:
                        throw new WechatApiInvokeException( response.ErrorCode.Value, response.ErrorMessage, url );
                }
            }

            return response;
        }
        private static T PostWechatApiWithToken<T>(string rawUrl, string request) where T : WechatApiResponseMessage
        {
            string accessToken = WechatApi.GetAccessToken();

            string url = rawUrl + "access_token=" + accessToken;

            T response = WechatApi.PostWechatApi<T>(url, request);

            if (response.ErrorCode.HasValue)
            {
                switch (response.ErrorCode.Value)
                {
                    case 0:
                        break;
                    // 如果是票据过期问题，则重新获取票据
                    case 40001:
                    case 40014:
                    case 42001:
                        LogHelper.Info(string.Format("调用微信接口时票据凭证无效或过期, errcode : {0}, errmsg : {1}, interface : {2}", response.ErrorCode, response.ErrorMessage, url));

                        // 重新获取票据凭证
                        accessToken = WechatApi.RefreshAccessToken(accessToken);

                        url = rawUrl + "access_token=" + accessToken;

                        response = WechatApi.PostWechatApi<T>(url, request);

                        if (response.ErrorCode.HasValue && response.ErrorCode.Value != 0)
                        {
                            throw new WechatApiInvokeException(response.ErrorCode.Value, response.ErrorMessage, url);
                        }
                        break;
                    default:
                        throw new WechatApiInvokeException(response.ErrorCode.Value, response.ErrorMessage, url);
                }
            }

            return response;
        }
        #endregion

        #region SendCustomMessage
        /// <summary>
        /// 调用客服接口向用户推送消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void SendCustomMessage( CustomMessage message )
        {
            string rawUrl = "https://api.weixin.qq.com/cgi-bin/message/custom/send?";

            WechatApi.PostWechatApiWithToken<WechatApiResponseMessage>( rawUrl, message );
        }
        #endregion
        
        /// <summary>
        /// 调用模板接口向用户推送消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void SendCustomTemplateMessage( CustomMessage message )
        {
            string rawUrl = "https://api.weixin.qq.com/cgi-bin/message/template/send?";

            WechatApi.PostWechatApiWithToken<WechatApiTemplateResponseMessage>(rawUrl, message);
        }

        public static void SendCustomTemplateMessage(string message)
        {
            string rawUrl = "https://api.weixin.qq.com/cgi-bin/message/template/send?";

            WechatApi.PostWechatApiWithToken<WechatApiTemplateResponseMessage>(rawUrl, message);
        }

        #region SendNewsMessage
        /// <summary>
        /// 发送多图文消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void SendTextImageMessage( CustomTextImageMessage message )
        {
            WechatApi.SendCustomMessage( message );
        }
        #endregion

        #region SendTextMessage
        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void SendTextMessage( CustomTextMessage message )
        {
            WechatApi.SendCustomMessage( message );
        }
        #endregion

        #region GetCallbackIp
        /// <summary>
        /// 获取微信服务器的IP地址
        /// </summary>
        /// <returns></returns>
        public static CallbackIpResponseMessage GetCallbackIp()
        {
            string rawUrl = "https://api.weixin.qq.com/cgi-bin/getcallbackip?";

            return WechatApi.GetWechatApiWithToken<CallbackIpResponseMessage>( rawUrl );
        }
        #endregion

        #region GetTicket
        public static string GetTicket()
        {
            string accessToken = WechatApi.GetAccessToken();
            string url1 = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + accessToken + "&type=jsapi";
            string responseInfo = WechatApi.HttpGet( url1 );
            WechatTicketResponse response = JsonConvert.DeserializeObject<WechatTicketResponse>( responseInfo );
            if( response.errcode == 42001 )
            {
                WechatApi.RefreshAccessToken( accessToken );
                accessToken = WechatApi.GetAccessToken();
                url1 = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + accessToken + "&type=jsapi";
                responseInfo = WechatApi.HttpGet( url1 );
                WechatTicketResponse responseLast = JsonConvert.DeserializeObject<WechatTicketResponse>( responseInfo );
                return responseLast.ticket;
            }
            else
            {
                string ticket = response.ticket;
                return ticket;
            }
        }
        #endregion
    }
}
