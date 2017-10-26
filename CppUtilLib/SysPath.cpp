#include "stdafx.h"
#include "SysPath.h"


CSysPath::CSysPath()
{
}


CSysPath::~CSysPath()
{
}

CAtlString CSysPath::GetWinDir()
{
	TCHAR szSysPath[MAX_PATH] = { 0 };
	GetWindowsDirectory(szSysPath, MAX_PATH);

	CAtlString sysPath = szSysPath;
	if (sysPath.IsEmpty())
	{
		sysPath = _T("C:\\Windows");
	}

	sysPath += '\\';

	return sysPath;
}

CAtlString CSysPath::GetSystem32Dir()
{
	TCHAR szPath[MAX_PATH] = { 0 };
	GetSystemDirectory(szPath, MAX_PATH);

	CAtlString path = szPath;
	if (path.IsEmpty())
	{
		path = _T("C:\\Windows\\system32");
	}
	path += '\\';

	return path;
}

CAtlString CSysPath::GetCurrentDir()
{
	TCHAR szPath[MAX_PATH] = { 0 };
	GetModuleFileName(NULL, szPath, MAX_PATH);

	CAtlString path = szPath;
	path = path.Left(path.ReverseFind('\\') + 1);

	return path;
}

CAtlString CSysPath::GetTempDir()
{
	TCHAR szTempPath[MAX_PATH] = { 0 };
	GetTempPath(MAX_PATH, szTempPath);

	CAtlString path = szTempPath;
	if (path.IsEmpty())
	{
		path = _T("C:\\Users\\Administrator\\AppData\\Local\\Temp\\");
	}

	return path;
}

CAtlString CSysPath::GetTempPathStr()
{
	TCHAR szTempPath[MAX_PATH] = { 0 };
	GetTempPath(MAX_PATH, szTempPath);

	return szTempPath;
}

CAtlString CSysPath::GetTempFileNameStr()
{
	return GetTempFileNameStr(GetTempPathStr());
}

CAtlString CSysPath::GetTempFileNameStr(CAtlString strPath)
{
	return GetTempFileNameStr(strPath, _T("tmp"));
}

CAtlString CSysPath::GetTempFileNameStr(CAtlString strPath, CAtlString strPrefix)
{
	TCHAR szTempFile[MAX_PATH * 2] = { 0 };

	if (strPrefix.IsEmpty())
	{
		GetTempFileName(strPath, NULL, 0, szTempFile);
	}
	else
	{
		GetTempFileName(strPath, strPrefix, 0, szTempFile);
	}

	return szTempFile;
}

BOOL CSysPath::DosPathToNtPath(LPTSTR pszDosPath, LPTSTR pszNtPath)
{
	TCHAR szDriveStr[500] = { 0 };
	TCHAR szDrive[3] = { 0 };
	TCHAR szDevName[100] = { 0 };
	INT cchDevName = 0;
	INT i = 0;

	//检查参数
	if (!pszDosPath || !pszNtPath)
		return FALSE;

	//获取本地磁盘字符串
	if (GetLogicalDriveStrings(sizeof(szDriveStr), szDriveStr))
	{
		for (i = 0; szDriveStr[i]; i += 4)
		{
			if (!lstrcmpi(&(szDriveStr[i]), _T("A:\\")) || !lstrcmpi(&(szDriveStr[i]), _T("B:\\")))
				continue;

			szDrive[0] = szDriveStr[i];
			szDrive[1] = szDriveStr[i + 1];
			szDrive[2] = '\0';
			if (!QueryDosDevice(szDrive, szDevName, 100))//查询 Dos 设备名
				return FALSE;

			cchDevName = lstrlen(szDevName);
			if (_tcsnicmp(pszDosPath, szDevName, cchDevName) == 0)//命中
			{
				lstrcpy(pszNtPath, szDrive);//复制驱动器
				lstrcat(pszNtPath, pszDosPath + cchDevName);//复制路径

				return TRUE;
			}
		}
	}

	lstrcpy(pszNtPath, pszDosPath);

	return FALSE;
}

