using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpUtils;
using CSharpUtils.Net;

namespace Wechat.Util
{
    public class WechatCall
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="toWeixinOpenId">要发送给的微信用户的openId</param>
        /// <param name="fromWeixinOpenId">发出人的openid</param>
        /// <param name="bussinessId">业务主键</param>
        public static void SendTemplatMessage(MsgType msgType, string toWeixinOpenId, string fromWeixinOpenId, string bussinessId)
        {
            string createTime=GetNowTicks().ToString();
            string nonce=Guid.NewGuid().ToString();
            string signature=WechatUtility.Sign(createTime,nonce);
            string url=string.Format("{0}?signature={1}&timestamp={2}&nonce={3}",
                ConfigurationManager.AppSettings["WeixinSiteUrl"],
                signature,
                createTime,
                nonce);
            string postDataStr=string.Format(@"<xml>
                <ToUserName></ToUserName>
                <FromUserName></FromUserName>
                <CreateTime></CreateTime>
                <MsgType></MsgType>
                <EventKey></EventKey>
                <BussinessId></BussinessId>
                </xml>
                ", toWeixinOpenId, fromWeixinOpenId, createTime, msgType.ToString(), bussinessId);
            string result = HttpUtil.HttpPost(url, postDataStr);
            
        }

        private static long GetNowTicks()
        {
            DateTime dt=new DateTime(1970,1,1);
            TimeSpan d=DateTime.Now-dt;
            return d.Ticks/10000000;
        }

    }

    /// <summary>
    /// 信息类型，必须在微信项目的appsettings中设置对应模板
    /// </summary>
    public enum MsgType
    {

        /// <summary>
        /// 申威狮星线路申请模板消息
        /// </summary>
        LineApply
    }

}
