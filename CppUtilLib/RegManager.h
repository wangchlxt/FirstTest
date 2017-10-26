#pragma once

// 注册表管理类
// 64 位系统中，HKEY_LOCAL_MACHINE\SOFTWARE\ 键下的值会写到 HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\ 下
class CRegManager
{
public:
	CRegManager(HKEY hKey = HKEY_LOCAL_MACHINE);
	~CRegManager();

	BOOL SaveKey(LPCTSTR lpFileName);
	BOOL RestoreKey(LPCTSTR lpFileName);
	BOOL Read(CAtlString strValueName, CAtlString& strVal);
	BOOL Read(LPCTSTR lpValueName, DWORD* pdwVal);
	BOOL Read(LPCTSTR lpValueName, int* pnVal);
	BOOL Write(CAtlString strSubKey, CAtlString strVal);
	BOOL Write(LPCTSTR lpSubKey, DWORD dwVal);
	BOOL Write(LPCTSTR lpSubKey, int nVal);
	BOOL DeleteKey(HKEY hKey, LPCTSTR lpSubKey);
	BOOL DeleteValue(LPCTSTR lpValueName);
	void Close();
	BOOL Open(LPCTSTR lpSubKey);
	BOOL CreateKey(LPCTSTR lpSubKey);

	HKEY m_hKey;
};

