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

	// ��ȡ������ʶ��
	char* GetNICCodeA();

	// ��ȡ������ʶ��
	wchar_t* GetNICCodeW();

	// ���������ʶ��
	bool CheckNICCodeA(char* lpszCode);

	// ���������ʶ��
	bool CheckNICCodeW(wchar_t* lpszCode);
};

