#include "stdafx.h"
#include "FileUtil.h"

CFileUtil::CFileUtil()
{
}


CFileUtil::~CFileUtil()
{
}

BOOL CFileUtil::IsExistFile(wchar_t* lpstFileName)
{
	HANDLE hFile = CreateFile(
		lpstFileName, // Ҫ�жϵ��ļ����ļ���
		0, // ����ֻ��Ҫ��͵�Ȩ�޼���
		FILE_SHARE_DELETE | FILE_SHARE_READ | FILE_SHARE_WRITE, // ����Ҳ������ļ����κ�ռ��
		NULL, // ��ȫ���ԣ����ǲ�����
		OPEN_EXISTING, // ֻ���Ѵ��ڵ��ļ�����Ҳ�����ǵ�Ŀ��
		FILE_ATTRIBUTE_NORMAL, // Ĭ��
		NULL // ��
	);

	if (INVALID_HANDLE_VALUE == hFile && (ERROR_ACCESS_DENIED != GetLastError()))
	{
		return FALSE;
	}

	if (INVALID_HANDLE_VALUE != hFile)
	{
		CloseHandle(hFile); // ����һ��Ҫ�ر��ļ��������Ȼ�ᵼ���ļ�ռ�ú���Դй©��
		hFile = INVALID_HANDLE_VALUE;
	}
	return TRUE;
}

BOOL CFileUtil::IsExistFile2(wchar_t* lpstFileName)
{
	int nRet = _taccess(lpstFileName, 0);
	return 0 == nRet || EACCES == nRet;

}

DWORD CFileUtil::FileToOld(wchar_t* lpstFileName)
{
	CStringW fileName = lpstFileName;

	SYSTEMTIME m_time;
	GetSystemTime(&m_time);

	CAtlString strTime;
	strTime.Format(_T("%d%.02d%.02d%.02d%.02d%.02d%.03d"), m_time.wYear, m_time.wMonth, m_time.wDay, m_time.wHour + 8, m_time.wMinute, m_time.wSecond, m_time.wMilliseconds);

	CString oldFileName = fileName + _T(".old_");
	oldFileName += strTime;

	if (!MoveFile(fileName, oldFileName))
	{
		return GetLastError();
	}

	return 0;
}

CAtlStringA CFileUtil::GetFileTextA(CAtlStringA fileName)
{
	CAtlStringA html;
	HANDLE hFile = CreateFileA(fileName, GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
	if (hFile != INVALID_HANDLE_VALUE)
	{
		LARGE_INTEGER fileSize;
		GetFileSizeEx(hFile, &fileSize);

		char* szTmp = new char[fileSize.QuadPart + 2];
		memset(szTmp, 0, fileSize.QuadPart + 2);

		DWORD rlen = 0;
		ReadFile(hFile, szTmp, fileSize.QuadPart, &rlen, NULL);

		html = szTmp;
		delete[] szTmp;
	}

	CloseHandle(hFile);
	return html;
}
