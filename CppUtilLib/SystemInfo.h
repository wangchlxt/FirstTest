#pragma once
class CSystemInfo
{
public:
	CSystemInfo();
	~CSystemInfo();

	// ��ȫ��ȡ����ʵϵͳ��Ϣ   
	VOID SafeGetNativeSystemInfo(__out LPSYSTEM_INFO lpSystemInfo);

	// ��ȡ����ϵͳλ��   
	int GetSystemBits();

	// ��ȡ����ϵͳ������
	void GetSystemName(CAtlString& osname);

	// ��ȡ����ϵͳ�İ汾����
	void GetVersionMark(CAtlString& vmark);
};

