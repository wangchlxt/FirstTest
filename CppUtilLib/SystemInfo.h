#pragma once
class CSystemInfo
{
public:
	CSystemInfo();
	~CSystemInfo();

	// 安全的取得真实系统信息   
	VOID SafeGetNativeSystemInfo(__out LPSYSTEM_INFO lpSystemInfo);

	// 获取操作系统位数   
	int GetSystemBits();

	// 获取操作系统的名称
	void GetSystemName(CAtlString& osname);

	// 获取操作系统的版本名称
	void GetVersionMark(CAtlString& vmark);
};

