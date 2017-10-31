using CSharpUtils;
using CSharpUtils.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToolFairy.Config;

namespace ToolFairy
{
    public class Bing
    {
        public string ConfigFile { get; set; }
        public BingConfig Config { get; set; }

        public Bing()
        {
            ConfigFile = AppConfig.ConfigPath + "bing.cfg";
            LoadConfig();
        }

        public void DownHomeImg()
        {
            LogHelper.Info("开始下载必应首页图片");

            string html = HttpUtil.HttpGet("http://cn.bing.com/", "");

            // 写入 html 文件
            Directory.CreateDirectory(Config.SaveHomeHtmlPath);
            File.WriteAllText(Config.SaveHomeHtmlPath + DateTimeUtil.GetDateStr() + ".html", html);

            // 从 html 中找到背景图片地址
            string startTxt = "g_img={url: \"";
            int start = html.IndexOf(startTxt);
            html = html.Substring(start + startTxt.Length);

            int end = html.IndexOf("\"");
            html = html.Substring(0, end);

            string url = "http://cn.bing.com/" + html;

            // 下载背景图片
            Directory.CreateDirectory(Config.SaveHomeImagePath);

            string imgFileName = html.Substring(html.LastIndexOf("/") + 1);
            if(HttpUtil.DownloadFile(url, Config.SaveHomeImagePath + DateTimeUtil.GetDateStr() + imgFileName))
            {
                LogHelper.Info("必应首页图片下载成功");

                // 保存状态
                Config.DownHomeIamgeTime = DateTime.Now;
                SaveConfig();
            }
            else
            {
                LogHelper.Info("必应首页图片下载失败");
            }
        }

        public void LoadConfig()
        {
            if (File.Exists(ConfigFile))
            {
                Config = SerializationUtil.DeserializationBinary<BingConfig>(ConfigFile);
            }
            else
            {
                Config = new BingConfig();
                SaveConfig();
            }
        }

        public void SaveConfig()
        {
            if(!Directory.Exists(AppConfig.ConfigPath))
            {
                Directory.CreateDirectory(AppConfig.ConfigPath);
            }
            
            SerializationUtil.SerializationBinary<BingConfig>(Config, ConfigFile);
        }

        public static void WorkThread()
        {
            while (true)
            {
                try
                {
                    Bing bing = new Bing();
                    if (bing.Config.IsSwitch)
                    {
                        if (DateTime.Now.Hour >= bing.Config.DownHomeImageHouse)
                        {
                            if (bing.Config.DownHomeIamgeTime.ToShortDateString() != DateTime.Now.ToShortDateString())
                            {
                                bing.DownHomeImg();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error("必应工作线程异常", ex);
                }
                
                Thread.Sleep(60 * 60 * 1000);
            }
        }
    }
}
