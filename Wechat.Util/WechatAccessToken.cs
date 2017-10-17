using System;

namespace Wechat.Util
{
    /// <summary>
    /// wechat_access_token
    /// </summary>
    [Serializable]
    public class WechatAccessToken
    {
        private string m_AppId;
        private string m_AccessToken;
        private int m_ExpiresIn;
        private DateTime m_UpdateTime;

        ///<summary>
        /// 应用ID    
        ///</summary>
        public string AppId
        {
            get
            {
                return this.m_AppId;
            }
            set
            {
                this.m_AppId = value;
                //this.OnPropertyChanged( "AppId", value );
            }
        }

        ///<summary>
        /// 凭证票据    
        ///</summary>
        public string AccessToken
        {
            get
            {
                return this.m_AccessToken;
            }
            set
            {
                this.m_AccessToken = value;
                //this.OnPropertyChanged( "AccessToken", value );
            }
        }

        ///<summary>
        /// 过期时间    
        ///</summary>
        public int ExpiresIn
        {
            get
            {
                return this.m_ExpiresIn;
            }
            set
            {
                this.m_ExpiresIn = value;
                //this.OnPropertyChanged( "ExpiresIn", value );
            }
        }

        ///<summary>
        /// 更新时间    
        ///</summary>
        public DateTime UpdateTime
        {
            get
            {
                return this.m_UpdateTime;
            }
            set
            {
                this.m_UpdateTime = value;
                //this.OnPropertyChanged( "UpdateTime", value );
            }
        }
    }
}

