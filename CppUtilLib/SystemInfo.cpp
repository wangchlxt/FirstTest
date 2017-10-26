#include "stdafx.h"
#include "SystemInfo.h"

#pragma warning(disable:4996)

CSystemInfo::CSystemInfo()
{
}


CSystemInfo::~CSystemInfo()
{
}

// ��ȫ��ȡ����ʵϵͳ��Ϣ   
VOID CSystemInfo::SafeGetNativeSystemInfo(__out LPSYSTEM_INFO lpSystemInfo)
{
	if (NULL == lpSystemInfo)
		return;
	typedef VOID(WINAPI *LPFN_GetNativeSystemInfo)(LPSYSTEM_INFO lpSystemInfo);
	LPFN_GetNativeSystemInfo nsInfo =
		(LPFN_GetNativeSystemInfo)GetProcAddress(GetModuleHandle(_T("kernel32")), "GetNativeSystemInfo");;
	if (NULL != nsInfo)
	{
		nsInfo(lpSystemInfo);
	}
	else
	{
		GetSystemInfo(lpSystemInfo);
	}
}

// ��ȡ����ϵͳλ��   
int CSystemInfo::GetSystemBits()
{
	SYSTEM_INFO si;
	SafeGetNativeSystemInfo(&si);
	if (si.wProcessorArchitecture == PROCESSOR_ARCHITECTURE_AMD64 ||
		si.wProcessorArchitecture == PROCESSOR_ARCHITECTURE_IA64)
	{
		return 64;
	}
	return 32;
}

// ��ȡ����ϵͳ������
void CSystemInfo::GetSystemName(CAtlString& osname)
{
	SYSTEM_INFO info;        //��SYSTEM_INFO�ṹ�ж�64λAMD������ 
	GetSystemInfo(&info);    //����GetSystemInfo�������ṹ 
	OSVERSIONINFOEX os;
	os.dwOSVersionInfoSize = sizeof(OSVERSIONINFOEX);

	osname = _T("unknown OperatingSystem.");

	if (GetVersionEx((OSVERSIONINFO *)&os))
	{
		//������ݰ汾��Ϣ�жϲ���ϵͳ���� 
		switch (os.dwMajorVersion)//�ж����汾��
		{
		case 4:
			switch (os.dwMinorVersion)//�жϴΰ汾�� 
			{
			case 0:
				if (os.dwPlatformId == VER_PLATFORM_WIN32_NT)
					osname = _T("Microsoft Windows NT 4.0"); //1996��7�·��� 
				else if (os.dwPlatformId == VER_PLATFORM_WIN32_WINDOWS)
					osname = _T("Microsoft Windows 95");
				break;
			case 10:
				osname = _T("Microsoft Windows 98");
				break;
			case 90:
				osname = _T("Microsoft Windows Me");
				break;
			}
			break;

		case 5:
			switch (os.dwMinorVersion)	//�ٱȽ�dwMinorVersion��ֵ
			{
			case 0:
				osname = _T("Microsoft Windows 2000");//1999��12�·���
				break;

			case 1:
				osname = _T("Microsoft Windows XP");//2001��8�·���
				break;

			case 2:
				if (os.wProductType == VER_NT_WORKSTATION
					&& info.wProcessorArchitecture == PROCESSOR_ARCHITECTURE_AMD64)
				{
					osname = _T("Microsoft Windows XP Professional x64 Edition");
				}
				else if (GetSystemMetrics(SM_SERVERR2) == 0)
					osname = _T("Microsoft Windows Server 2003");//2003��3�·��� 
				else if (GetSystemMetrics(SM_SERVERR2) != 0)
					osname = _T("Microsoft Windows Server 2003 R2");
				break;
			}
			break;

		case 6:
			switch (os.dwMinorVersion)
			{
			case 0:
				if (os.wProductType == VER_NT_WORKSTATION)
					osname = _T("Microsoft Windows Vista");
				else
					osname = _T("Microsoft Windows Server 2008");//�������汾 
				break;
			case 1:
				if (os.wProductType == VER_NT_WORKSTATION)
					osname = _T("Microsoft Windows 7");
				else
					osname = _T("Microsoft Windows Server 2008 R2");
				break;
			}
			break;
		}
	}//if(GetVersionEx((OSVERSIONINFO *)&os))

}

// ��ȡ����ϵͳ�İ汾����
void CSystemInfo::GetVersionMark(CAtlString& vmark)
{
	OSVERSIONINFOEX os;
	os.dwOSVersionInfoSize = sizeof(OSVERSIONINFOEX);
	vmark = _T("");

	if (GetVersionEx((OSVERSIONINFO*)&os))
	{
		switch (os.dwMajorVersion)
		{                //���жϲ���ϵͳ�汾 
		case 5:
			switch (os.dwMinorVersion)
			{
			case 0:                  //Windows 2000 
				if (os.wSuiteMask == VER_SUITE_ENTERPRISE)
					vmark = _T("Advanced Server");
				break;
			case 1:                  //Windows XP 
				if (os.wSuiteMask == VER_SUITE_EMBEDDEDNT)
					vmark = _T("Embedded");
				else if (os.wSuiteMask == VER_SUITE_PERSONAL)
					vmark = _T("Home Edition");
				else
					vmark = _T("Professional");
				break;
			case 2:
				if (GetSystemMetrics(SM_SERVERR2) == 0
					&& os.wSuiteMask == VER_SUITE_BLADE)  //Windows Server 2003 
					vmark = _T("Web Edition");
				else if (GetSystemMetrics(SM_SERVERR2) == 0
					&& os.wSuiteMask == VER_SUITE_COMPUTE_SERVER)
					vmark = _T("Compute Cluster Edition");
				else if (GetSystemMetrics(SM_SERVERR2) == 0
					&& os.wSuiteMask == VER_SUITE_STORAGE_SERVER)
					vmark = _T("Storage Server");
				else if (GetSystemMetrics(SM_SERVERR2) == 0
					&& os.wSuiteMask == VER_SUITE_DATACENTER)
					vmark = _T("Datacenter Edition");
				else if (GetSystemMetrics(SM_SERVERR2) == 0
					&& os.wSuiteMask == VER_SUITE_ENTERPRISE)
					vmark = _T("Enterprise Edition");
				else if (GetSystemMetrics(SM_SERVERR2) != 0
					&& os.wSuiteMask == VER_SUITE_STORAGE_SERVER)
					vmark = _T("Storage Server");
				break;
			}
			break;

		case 6:
			switch (os.dwMinorVersion)
			{
			case 0:
				if (os.wProductType != VER_NT_WORKSTATION
					&& os.wSuiteMask == VER_SUITE_DATACENTER)
					vmark = _T("Datacenter Server");
				else if (os.wProductType != VER_NT_WORKSTATION
					&& os.wSuiteMask == VER_SUITE_ENTERPRISE)
					vmark = _T("Enterprise");
				else if (os.wProductType == VER_NT_WORKSTATION
					&& os.wSuiteMask == VER_SUITE_PERSONAL)  //Windows Vista
					vmark = _T("Home");
				break;
			}
			break;
		}
	}
}
