using CSharpUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolFairy.Config
{
    /// <summary>
    /// 必应配置类
    /// </summary>
    [Serializable]
    public class BingConfig
    {
        public BingConfig()
        {
            IsSwitch = true;
            DownHomeImageHouse = 9;
            DownHomeIamgeTime = new DateTime();
            SaveHomeHtmlPath = AppConfig.AppPath + "Bing\\Home\\Html\\";
            SaveHomeImagePath = AppConfig.AppPath + "Bing\\Home\\Image\\";
        }

        /// <summary>
        /// 功能开关
        /// </summary>
        public bool IsSwitch { get; set; }

        /// <summary>
        /// 首页图片下载时间，整点小时
        /// </summary>
        public int DownHomeImageHouse { get; set; }
        
        /// <summary>
        /// 首页图片下载时间
        /// </summary>
        public DateTime DownHomeIamgeTime { get; set; }

        /// <summary>
        /// 首页 html 保存路径
        /// </summary>
        public string SaveHomeHtmlPath { get; set; }

        /// <summary>
        /// 首页图片保存路径
        /// </summary>
        public string SaveHomeImagePath { get; set; }
    }
}
