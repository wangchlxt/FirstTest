// Log.cpp : 定义 DLL 应用程序的导出函数。
//
#include "stdafx.h"
#include "Log.h"
#include "Base64Coding.h"
#include <atlconv.h>

// 这是已导出类的构造函数。
// 有关类定义的信息，请参阅 Log.h
CLog::CLog() :m_hFile(NULL), m_eEncoding(LOG_NO_ENCODING)
{
	m_hMutex = CreateMutex(NULL, FALSE, _T("g_log_mutex"));
}

DWORD CLog::Open(CAtlString file)
{
	m_hFile = CreateFile(file, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, NULL, OPEN_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);

	if (m_hFile == INVALID_HANDLE_VALUE)
	{
		return GetLastError();
	}

	return ERROR_SUCCESS;
}

DWORD CLog::Close()
{
	if (!CloseHandle(m_hFile))
	{
		return GetLastError();
	}

	return ERROR_SUCCESS;
}

void CLog::SetLogEncoding(_LOG_ENCODING encoding)
{
	m_eEncoding = encoding;
}

DWORD CLog::WriteA(CAtlStringA content)
{
	USES_CONVERSION;

	SYSTEMTIME sys;
	GetLocalTime(&sys);
	CAtlStringA sTime;
	sTime.Format("[%4d/%02d/%02d %02d:%02d:%02d.%03d] ", sys.wYear, sys.wMonth, sys.wDay, sys.wHour, sys.wMinute, sys.wSecond, sys.wMilliseconds);

	CAtlStringA logText = sTime;
	logText.Append(content);

	if (m_eEncoding == LOG_BASE64_ENCODING)
	{
		int len = logText.GetLength() * 2 + 1;
		unsigned char *pszBase64 = new unsigned char[len];

		stringcoding::Base64Decode(pszBase64, (unsigned char*)logText.GetBuffer());
		logText = pszBase64;

		delete[] pszBase64;
	}
	logText.Append("\r\n");

	SetFilePointer(m_hFile, 0, 0, FILE_END);
	DWORD writeSize = 0;
	if (!WriteFile(m_hFile, logText, logText.GetLength(), &writeSize, NULL))
	{
		return GetLastError();
	}
	if (!FlushFileBuffers(m_hFile))
	{
		return GetLastError();
	}

	return ERROR_SUCCESS;
}

DWORD CLog::WriteW(CAtlStringW content)
{
	SYSTEMTIME sys;
	GetLocalTime(&sys);
	CString sTime;
	sTime.Format(_T("[%4d/%02d/%02d %02d:%02d:%02d.%03d] "), sys.wYear, sys.wMonth, sys.wDay, sys.wHour, sys.wMinute, sys.wSecond, sys.wMilliseconds);

	CString logText = sTime;
	logText.Append(content);

	if (m_eEncoding == LOG_BASE64_ENCODING)
	{
		logText = stringcoding::StringBase64Encode(logText);
	}
	logText.Append(_T("\r\n"));

	USES_CONVERSION;
	CAtlStringA logTextA = W2A(logText);

	SetFilePointer(m_hFile, 0, 0, FILE_END);
	DWORD writeSize = 0;
	if (!WriteFile(m_hFile, logTextA, logTextA.GetLength(), &writeSize, NULL))
	{
		return GetLastError();
	}
	if (!FlushFileBuffers(m_hFile))
	{
		return GetLastError();
	}

	return ERROR_SUCCESS;
}

DWORD CLog::FullFileWrite(CAtlStringA file, CAtlStringA content)
{
	WaitForSingleObject(m_hMutex, INFINITE);

	HANDLE hFile = CreateFileA(file, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, NULL, OPEN_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);
	if (hFile == INVALID_HANDLE_VALUE)
	{
		return 1;
	}

	try
	{
		content += "\r\n";

		SetFilePointer(hFile, 0, 0, FILE_END);

		DWORD wlen = 0;
		WriteFile(hFile, content, content.GetLength(), &wlen, NULL);
	}
	catch (...)
	{

	}

	CloseHandle(hFile);

	ReleaseMutex(m_hMutex);

	return 0;
}

DWORD CLog::FullFileWrite(CAtlStringW file, CAtlStringW content)
{
	CAtlStringA filea = CW2A(file);
	CAtlStringA contenta = CW2A(content);

	FullFileWrite(filea, contenta);

	return 0;
}
