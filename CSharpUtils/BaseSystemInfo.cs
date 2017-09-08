//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using CSharpUtils.Configuration;
using System;
using System.Management;
using System.Net;

namespace CSharpUtils
{
    /// <summary>
    /// BUBaseSystemInfo 的定义部分
    /// 这部分其实是系统的核心部分
    /// 
    /// 修改记录
    /// 
    ///		2010.09.19 版本：2.0 JiRiGaLa   彻底进行重构。
    ///		2007.04.02 版本：1.2 JiRiGaLa   进行主键优化。
    ///		2007.01.02 版本：1.1 JiRiGaLa   进行主键优化。
    ///		2006.02.05 版本：1.1 JiRiGaLa	重新调整主键的规范化。
    ///		2004.11.19 版本：1.0 JiRiGaLa	主键创建。
    ///		
    /// 版本：3.0
    /// 
    /// <author>

    ///		<date>2010.09.19</date>
    /// </author>
    /// </summary>
    public class BaseSystemInfo
    {
        /// <summary>
        /// RegistryKey、Configuration、UserConfig 注册表或者配置文件读取参数
        /// </summary>
        public static ConfigurationCategory ConfigurationFrom = ConfigurationCategory.Configuration;

        /// <summary>
        /// 当前用户名
        /// </summary>
        public static string CurrentUsername = string.Empty;

        /// <summary>
        /// 当前密码
        /// </summary>
        public static string CurrentPassword = string.Empty;

        /// <summary>
        /// 启用分级管理范围权限设置
        /// </summary>
        public static bool UsePermissionScope = false;

        /// <summary>
        /// 动态加载组织机构树
        /// </summary>
        public static bool OrganizeDynamicLoading = true;

        /// <summary>
        /// 启用逐级授权
        /// </summary>
        public static bool UseLicensedPermission = false;

        /// <summary>
        /// 模块菜单权限
        /// </summary>
        public static bool UseModulePermission = false;

        /// <summary>
        /// 是否采用了数据表的列权限
        /// </summary>
        public static bool UseTableColumnsPermission = false;

        /// <summary>
        /// 当前客户选择的语言
        /// </summary>
        public static string CurrentLanguage = "zh-CHS";

        /// <summary>
        /// 当前语言
        /// </summary>
        public static string Themes = string.Empty;

        /// <summary>
        /// 远程服务器地址
        /// </summary>
        public static string WebService = string.Empty;

        /// <summary>
        /// WebService用户名
        /// </summary>
        public static string WebServiceUsername = "Jirisoft";
 
        /// <summary>
        /// WebService密码
        /// </summary>
        public static string WebServicePassword = "Jirisoft";

        /// <summary>
        /// 当前软件ID
        /// </summary>
        public static string SoftName = string.Empty;

        /// <summary>
        /// 软件的名称
        /// </summary>
        public static string SoftFullName = string.Empty;

        /// <summary>
        /// Local、WebService、Remoting 连接方式
        /// </summary>
        public static RunMode RunMode = RunMode.Local;

        /// <summary>
        /// 根菜单编号
        /// </summary>
        public static string RootMenuCode = string.Empty;

        /// <summary>
        /// 当前版本
        /// </summary>
        public static string Version = "3.0";

        /// <summary>
        /// 数据库类别
        /// </summary>
        public static DataBaseType DataBaseType = DataBaseType.Sqlserver;

        public static string ServerName = string.Empty;             // 服务器名称
        public static string DataBase = string.Empty;             // 连接的数据库名称
        public static bool TrustLink = true;                     // 是否为信任的连接
        public static string RegisterKey = string.Empty;             // 注册码
        public static string StartupPath = string.Empty;             // 当前网站的安装地址

        /// <summary>
        /// 数据库连接的字符串
        /// </summary>
        public static string UserCenterDbConnection = string.Empty;

        /// <summary>
        /// 业务数据库
        /// </summary>
        public static string BusinessDbConnection = string.Empty;

        public static string ServiceFactory = "ServiceFactory";
        public static string ServicePath = "DotNet.Service";
        public static string DbHelperClass = "DotNet.DbUtilities.SqlHelper";
        public static string DbHelperAssmely = "DotNet.DbUtilities";

        public static string LoginAssembly = "DotNet.WinForm.Login";
        public static string LoginForm = "FrmLogin";
        public static string MainForm = "MainForm";

        public static bool LoadUser = true;
        public static bool AllowUserRegister = true;

        public static string RegisterException = "请您联系： 浙江省杭州市 吉日嘎拉 手机：13858163011 电子邮件：JiRiGaLa_Bao@Hotmail.com 对软件进行注册。";

        public static bool ShowInformation         = true;         // 每个操作是否进行信息提示。

        // 当前客户公司名称
        public static string CustomerCompanyName = string.Empty;
       
        public static string CustomerPhone       = "0574-88083168";			    // 当前客户公司电话
        public static string CompanyFullName     = "宁波吉日软件开发有限公司";	// 公司名称
        public static string CompanyPhone        = "13858163011";			    // 公司电话

        public static string Copyright                  = "Copyright 2003-2010 Jirisoft Corporation";
        public static string BugFeedback                = @"mailto:JiRiGaLa@HotMail.com?subject=对费用报销管理的反馈&body=这里输入您宝贵的反馈";
        public static string IEDownloadUrl              = @"http://download.microsoft.com/download/ie6sp1/finrel/6_sp1/W98NT42KMeXP/CN/ie6setup.exe";
        public static string UploadDirectory            = "Document/";
        public static bool   NeedRegister               = false;				    // 是否需要注册
        public static string TimeFormat                 = "HH:mm:ss";			    // 时间格式
        public static string DateFormat                 = "yyyy-MM-dd";			    // 日期短格式
        public static string DateTimeFormat             = "yyyy-MM-dd HH:mm:ss";    // 日期长格式
        public static string LastUpdate                 = "2010.09.19";				// 最后更新日期

        public static bool Logined                  = false;    // 是否已经登录
        public static bool RecordLoginLog           = true;     // 登录历史纪录
        
        /// <summary>
        /// 是否进行日志记录
        /// </summary>
        public static bool RecordLog = true;

        public static bool EncryptPassword      = false;    // 是否加密登录密码
        public static bool AllowNullPassword    = false;    // 允许空密码
        public static bool RememberPassword         = true;     // 登录是否保存密码
        public static bool LogException         = true;     // 是否登记异常
        public static bool LogSQL               = false;    // 是否登记数据库操作
        public static bool EventLog             = false;    // 是否登记到 Windows 系统异常中

        public static bool MatchCase        = true;     // 是否区分大小写
        public static bool UpdateVisit      = true;     // 是否更新访问日期信息
        public static int OnLineTime0ut     = 200;      // 检查周期3分钟内不在线的，就认为是已经没在线了，生命周期检查
        public static int OnLineCheck       = 60;       // 每过1分钟，检查一次在线状态
        public static int OnLineLimit       = 0;        // 同时在线用户数量限制
        public static bool CheckOnLine      = false;    // 只允许登录一次
        public static bool CheckMACAddress  = false;    // 是否检查用户的网卡MAC地址
        public static bool CheckIPAddress   = false;    // 是否检查用户IP地址
        public static bool CheckOnLineLimit = false;    // 是否检查同时在线用户数量

        public static string HelpNamespace  = string.Empty;
        
        public static string AppIco     = string.Empty; // 系统图标文件
        public static string FormIco    = string.Empty; // 系统图标文件
        public static bool ClientCache  = false;        // 是否采用客户端缓存
        public static bool ServerCache  = false;        // 是否采用服务器端缓存

        public static int    TopLimit       = 200;

        public static int   LockNoWaitCount             = 5;    // 锁不住记录时的循环次数
        public static int   LockNoWaitTickMilliSeconds  = 30;   // 锁不住记录时的等待时间

        // 错误报告相关
        public static string ErrorReportTo          = "JiRiGaLa_Bao@Hotmail.com";   // 发送给谁，用,;符号隔开
        public static string ErrorReportMailServer  = "";                // 邮件服务器地址
        public static string ErrorReportMailUsername = "";           // 用户名
        public static string ErrorReportMailPassword = "";           // 密码

        /// <summary>
        /// 主应用程序集名
        /// </summary>
        public static string MainAssembly = string.Empty;

        public static void Login(BaseUserInfo userInfo)
        {
            BaseSystemInfo.UserInfo = userInfo;
        }

        private static BaseUserInfo userInfo = new BaseUserInfo();
        
        /// <summary>
        /// 当前操作员信息
        /// </summary>
        public static BaseUserInfo UserInfo
        {
            get
            {
                // IP地址
                if (String.IsNullOrEmpty(userInfo.IPAddress))
                {
                    userInfo.IPAddress = GetIPAddress();
                }                
                // 主键
                if (String.IsNullOrEmpty(userInfo.Id))
                {
                    userInfo.Id = GetIPAddress();
                }
                // 用户名
                if (String.IsNullOrEmpty(userInfo.Username))
                {
                    userInfo.Username = System.Environment.MachineName;
                }
                // 真实姓名
                if (String.IsNullOrEmpty(userInfo.Realname))
                {
                    userInfo.Realname = System.Environment.UserName;
                }
                return userInfo;
            }
            set
            {
                userInfo = value;
            }
        }

        public static string GetIPAddress()
        {
            string ipAddress = string.Empty;
            System.Net.IPHostEntry ipHostEntrys = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (System.Net.IPAddress ip in ipHostEntrys.AddressList)
            {
                ipAddress = ip.ToString();
            }
            return ipAddress;
        }

        public static string GetCPUSerialNo()
        {
            string cpuSerialNo = string.Empty;
            ManagementClass managementClass = new ManagementClass("Win32_Processor");
            ManagementObjectCollection managementObjectCollection = managementClass.GetInstances();
            foreach (ManagementObject managementObject in managementObjectCollection)
            {
                // 可能是有多个
                cpuSerialNo = managementObject.Properties["ProcessorId"].Value.ToString();
                break;
            }
            return cpuSerialNo;
        }

        public static string GetHardDiskInfo()
        {
            string hardDisk = string.Empty;
            ManagementClass managementClass = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection managementObjectCollection = managementClass.GetInstances();
            foreach (ManagementObject managementObject in managementObjectCollection)
            {
                // 可能是有多个
                hardDisk = (string)managementObject.Properties["Model"].Value;
                break;
            }
            return hardDisk;
        }

        public static string GetMacAddress()
        {
            string macAddress = string.Empty;
            ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection managementObjectCollection = managementClass.GetInstances();
            foreach (ManagementObject managementObject in managementObjectCollection)
            {
                // 可能是有多个
                if ((bool)managementObject["IPEnabled"] == true)
                {
                    macAddress = managementObject["MacAddress"].ToString();
                    break;
                }
            }
            return macAddress;
        }
    }
}