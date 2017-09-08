//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;

namespace CSharpUtils
{
	/// <summary>
    /// BaseUserInfo
    /// ����Ա��
	/// 
	/// �޸ļ�¼
    /// 
    ///		2008.08.26 JiRiGaLa �汾��1.2 ����������
    ///		2006.05.03 JiRiGaLa �汾��1.1 ��ӵ�������Ŀ�С�
    ///		2006.01.21 JiRiGaLa �汾��1.0 Զ�̴��ݲ��������Բſ��ԡ�
	///		
	/// �汾��1.2
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
        /// �����¼Ψһʶ���ʶ
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
        /// Ŀ���û�
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
        /// ����Ա����
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
        /// ְԱ����
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
        /// ����Ա�û���
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
        /// ����Ա����
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
        /// ���
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
        /// ��ǰ����֯�ṹ��˾����
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
        /// ��ǰ����֯�ṹ��˾���
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
        /// ��ǰ����֯�ṹ��˾����
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
        /// ��ǰ����֯�ṹ��������
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
        /// ��ǰ����֯�ṹ���ű��
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
        /// ��ǰ����֯�ṹ��������
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
        /// ��ǰ����֯�ṹ����������
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
        /// ��ǰ����֯�ṹ��������
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
        /// ��ǰ����֯�ṹ����������
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
        /// Ĭ�Ͻ�ɫ
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
        /// Ĭ�Ͻ�ɫ����
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
        /// �Ƿ񳬼�����Ա
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
        /// ����
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
        /// IP��ַ
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
        /// MAC��ַ
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
        /// ��ǰ����ѡ��
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
        /// ��ǰ���ַ��ѡ��
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
        /// ����
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
        /// WebService �û���
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
        /// WebService ����
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
        /// ��ȡ��Ϣ
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