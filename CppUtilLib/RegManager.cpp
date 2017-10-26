#include "stdafx.h"
#include "RegManager.h"


CRegManager::CRegManager(HKEY hKey) :m_hKey(hKey)
{

}


CRegManager::~CRegManager()
{
	Close();
}

BOOL CRegManager::CreateKey(LPCTSTR lpSubKey)
{
	HKEY hKey;
	DWORD dw;
	long lReturn = RegCreateKeyEx(m_hKey, lpSubKey, 0L, NULL, REG_OPTION_VOLATILE, KEY_ALL_ACCESS, NULL, &hKey, &dw);

	if (lReturn == ERROR_SUCCESS)
	{
		m_hKey = hKey;
		return TRUE;
	}

	return FALSE;

}

BOOL CRegManager::Open(LPCTSTR lpSubKey)
{
	HKEY hKey;
	long lReturn = RegOpenKeyEx(m_hKey, lpSubKey, 0L, KEY_ALL_ACCESS, &hKey);

	if (lReturn == ERROR_SUCCESS)
	{
		m_hKey = hKey;
		return TRUE;
	}
	return FALSE;

}

void CRegManager::Close()
{
	if (m_hKey)
	{
		RegCloseKey(m_hKey);
		m_hKey = NULL;
	}

}

BOOL CRegManager::DeleteValue(LPCTSTR lpValueName)
{
	long lReturn = RegDeleteValue(m_hKey, lpValueName);

	if (lReturn == ERROR_SUCCESS)
		return TRUE;
	return FALSE;

}

BOOL CRegManager::DeleteKey(HKEY hKey, LPCTSTR lpSubKey)
{
	long lReturn = RegDeleteValue(hKey, lpSubKey);

	if (lReturn == ERROR_SUCCESS)
		return TRUE;
	return FALSE;

}

BOOL CRegManager::Write(LPCTSTR lpSubKey, int nVal)
{
	DWORD dwValue;
	dwValue = (DWORD)nVal;

	long lReturn = RegSetValueEx(m_hKey, lpSubKey, 0L, REG_DWORD, (const BYTE *)&dwValue, sizeof(DWORD));

	if (lReturn == ERROR_SUCCESS)
		return TRUE;

	return FALSE;

}

BOOL CRegManager::Write(LPCTSTR lpSubKey, DWORD dwVal)
{
	long lReturn = RegSetValueEx(m_hKey, lpSubKey, 0L, REG_DWORD, (const BYTE *)&dwVal, sizeof(DWORD));

	if (lReturn == ERROR_SUCCESS)
		return TRUE;

	return FALSE;

}

BOOL CRegManager::Write(CAtlString strSubKey, CAtlString strVal)
{
	long lReturn = RegSetValueEx(m_hKey, strSubKey, 0L, REG_SZ, (const BYTE *)strVal.GetBuffer(), strVal.GetLength() * 2);

	if (lReturn == ERROR_SUCCESS)
		return TRUE;

	return FALSE;
}

BOOL CRegManager::Read(LPCTSTR lpValueName, int* pnVal)
{
	DWORD dwType;
	DWORD dwSize = sizeof(DWORD);
	DWORD dwDest;
	long lReturn = RegQueryValueEx(m_hKey, lpValueName, NULL, &dwType, (BYTE *)&dwDest, &dwSize);

	if (lReturn == ERROR_SUCCESS)
	{
		*pnVal = (int)dwDest;
		return TRUE;
	}
	return FALSE;

}

BOOL CRegManager::Read(LPCTSTR lpValueName, DWORD* pdwVal)
{
	DWORD dwType;
	DWORD dwSize = sizeof(DWORD);
	DWORD dwDest;
	long lReturn = RegQueryValueEx(m_hKey, lpValueName, NULL, &dwType, (BYTE *)&dwDest, &dwSize);

	if (lReturn == ERROR_SUCCESS)
	{
		*pdwVal = dwDest;
		return TRUE;
	}
	return FALSE;

}




BOOL CRegManager::RestoreKey(LPCTSTR lpFileName)
{
	long lReturn = RegRestoreKey(m_hKey, lpFileName, REG_WHOLE_HIVE_VOLATILE);

	if (lReturn == ERROR_SUCCESS)
		return TRUE;

	return FALSE;
}

BOOL CRegManager::SaveKey(LPCTSTR lpFileName)
{
	long lReturn = RegSaveKey(m_hKey, lpFileName, NULL);

	if (lReturn == ERROR_SUCCESS)
		return TRUE;

	return FALSE;
}

BOOL CRegManager::Read(CAtlString strValueName, CAtlString& strVal)
{
	if (strValueName.IsEmpty())
	{
		return FALSE;
	}

	DWORD dwType;
	DWORD dwSize = 2550;
	TCHAR szString[2550] = { 0 };

	long lReturn = RegQueryValueEx(m_hKey, strValueName, NULL, &dwType, (BYTE *)szString, &dwSize);

	if (lReturn == ERROR_SUCCESS)
	{
		strVal = szString;
		return TRUE;
	}
	return FALSE;
}

