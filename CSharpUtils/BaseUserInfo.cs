//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;

namespace CSharpUtils
{
	/// <summary>
    /// BaseUserInfo
    /// 操作员类
	/// 
	/// 修改纪录
    /// 
    ///		2008.08.26 JiRiGaLa 版本：1.2 整理主键。
    ///		2006.05.03 JiRiGaLa 版本：1.1 添加到工程项目中。
    ///		2006.01.21 JiRiGaLa 版本：1.0 远程传递参数用属性才可以。
	///		
	/// 版本：1.2
	///
	/// <author>

    ///		<date>2008.08.26</date>
	/// </author> 
	/// </summary>
	[Serializable]
	public class BaseUserInfo
	{
        public BaseUserInfo()
        {
            this.GetUserInfo();
        }

        /// <summary>
        /// 单点登录唯一识别标识
        /// </summary>
        private string openId = string.Empty;
        public string OpenId
        {
            get
            {
                return this.openId;
            }
            set
            {
                this.openId = value;
            }
        }

        /// <summary>
        /// 目标用户
        /// </summary>
        private string targetUserId = string.Empty;
        public string TargetUserId
        {
            get
            {
                return this.targetUserId;
            }
            set
            {
                this.targetUserId = value;
            }
        }

        /// <summary>
        /// 操作员主键
        /// </summary>
        private string id = string.Empty;
        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        /// <summary>
        /// 职员主键
        /// </summary>
        private string staffId = string.Empty;
        public string StaffId
        {
            get
            {
                return this.staffId;
            }
            set
            {
                this.staffId = value;
            }
        }
        
        /// <summary>
        /// 操作员用户名
        /// </summary>
        private string userName = string.Empty;
        public string Username
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }
        
        /// <summary>
        /// 操作员姓名
        /// </summary>
        private string realname = string.Empty;
        public string Realname
        {
            get
            {
                return this.realname;
            }
            set
            {
                this.realname = value;
            }
        }

        /// <summary>
        /// 编号
        /// </summary>
        private string code = string.Empty;
        public string Code
        {
            get
            {
                return this.code;
            }
            set
            {
                this.code = value;
            }
        }

        /// <summary>
        /// 当前的组织结构公司主键
        /// </summary>
        private int? companyId = null;
        public int? CompanyId
        {
            get
            {
                return this.companyId;
            }
            set
            {
                this.companyId = value;
            }
        }
        
        /// <summary>
        /// 当前的组织结构公司编号
        /// </summary>
        private string companyCode = string.Empty;
        public string CompanyCode
        {
            get
            {
                return this.companyCode;
            }
            set
            {
                this.companyCode = value;
            }
        }
        
        /// <summary>
        /// 当前的组织结构公司名称
        /// </summary>
        private string companyFullName = string.Empty;
        public string CompanyFullName
        {
            get
            {
                return this.companyFullName;
            }
            set
            {
                this.companyFullName = value;
            }
        }

        /// <summary>
        /// 当前的组织结构部门主键
        /// </summary>
        private int? departmentId = null;
        public int? DepartmentId
        {
            get
            {
                return this.departmentId;
            }
            set
            {
                this.departmentId = value;
            }
        }

        /// <summary>
        /// 当前的组织结构部门编号
        /// </summary>
        private string departmentCode = string.Empty;
        public string DepartmentCode
        {
            get
            {
                return this.departmentCode;
            }
            set
            {
                this.departmentCode = value;
            }
        }

        /// <summary>
        /// 当前的组织结构部门名称
        /// </summary>
        private string departmentFullName = string.Empty;
        public string DepartmentFullName
        {
            get
            {
                return this.departmentFullName;
            }
            set
            {
                this.departmentFullName = value;
            }
        }

        /// <summary>
        /// 当前的组织结构工作组主键
        /// </summary>
        private int? workgroupId = null;
        public int? WorkgroupId
        {
            get
            {
                return this.workgroupId;
            }
            set
            {
                this.workgroupId = value;
            }
        }

        /// <summary>
        /// 当前的组织结构工作组编号
        /// </summary>
        private string workgroupCode = string.Empty;
        public string WorkgroupCode
        {
            get
            {
                return this.workgroupCode;
            }
            set
            {
                this.workgroupCode = value;
            }
        }

        /// <summary>
        /// 当前的组织结构工作组名称
        /// </summary>
        private string workgroupFullName = string.Empty;
        public string WorkgroupFullName
        {
            get
            {
                return this.workgroupFullName;
            }
            set
            {
                this.workgroupFullName = value;
            }
        }       

        /// <summary>
        /// 默认角色
        /// </summary>
        private int? roleId = null;
        public int? RoleId
        {
            get
            {
                return this.roleId;
            }
            set
            {
                this.roleId = value;
            }
        }

        /// <summary>
        /// 默认角色名称
        /// </summary>
        private string roleName = string.Empty;
        public string RoleName
        {
            get
            {
                return this.roleName;
            }
            set
            {
                this.roleName = value;
            }
        }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        private bool isAdministrator = false;
        public bool IsAdministrator
        {
            get
            {
                return this.isAdministrator;
            }
            set
            {
                this.isAdministrator = value;
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        private string password = string.Empty;
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        /// <summary>
        /// IP地址
        /// </summary>
        private string ipAddress = string.Empty;
        public string IPAddress
        {
            get
            {
                return this.ipAddress;
            }
            set
            {
                this.ipAddress = value;
            }
        }
        
        /// <summary>
        /// MAC地址
        /// </summary>
        private string macAddress = string.Empty;
        public string MACAddress
        {
            get
            {
                return this.macAddress;
            }
            set
            {
                this.macAddress = value;
            }
        }

        /// <summary>
        /// 当前语言选择
        /// </summary>
        private string currentLanguage = string.Empty;
        public string CurrentLanguage
        {
            get
            {
                return this.currentLanguage;
            }
            set
            {
                this.currentLanguage = value;
            }
        }

        /// <summary>
        /// 当前布局风格选择
        /// </summary>
        private string themes = string.Empty;
        public string Themes
        {
            get
            {
                return this.themes;
            }
            set
            {
                this.themes = value;
            }
        }
        
        /// <summary>
        /// 描述
        /// </summary>
        private string description = string.Empty;
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        /// <summary>
        /// WebService 用户名
        /// </summary>
        private string webServiceUsername = "Jirisoft";
        public string WebServiceUsername
        {
            get
            {
                return this.webServiceUsername;
            }
            set
            {
                this.webServiceUsername = value;
            }
        }

        /// <summary>
        /// WebService 密码
        /// </summary>
        private string webServicePassword = "Jirisoft";
        public string WebServicePassword
        {
            get
            {
                return this.webServicePassword;
            }
            set
            {
                this.webServicePassword = value;
            }
        }

        #region public void GetUserInfo()
        /// <summary>
        /// 获取信息
        /// </summary>
        public void GetUserInfo()
		{
            this.WebServiceUsername = BaseSystemInfo.WebServiceUsername;
            this.WebServicePassword = BaseSystemInfo.WebServicePassword;
            this.CurrentLanguage = BaseSystemInfo.CurrentLanguage;
            this.Themes = BaseSystemInfo.Themes;
        }
		#endregion
	}
}