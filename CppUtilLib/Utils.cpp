// Utils.cpp : ���� DLL Ӧ�ó���ĵ���������
//
#include "stdafx.h"
#include "Utils.h"
#include "StringUtil.h"
#include "MD5.h"
#include <atlconv.h>
#include <shellapi.h>
#include <ShlObj.h>
#pragma comment(lib,"Shell32.lib")

// �����ѵ�����Ĺ��캯����
// �й��ඨ�����Ϣ������� Utils.h
CUtils::CUtils()
{
	return;
}

CAtlString CUtils::GetFileMD5(CAtlString fileName)
{
	const int READ_BUF_SIZE = 1024;
	MD5 md5;
	md5.reset();

	int uReadSize = 0;
	char szReadBuf[READ_BUF_SIZE] = "";
	unsigned int uRetCRCValue = 0;
	TCHAR* pszTest = fileName.GetBuffer(MAX_PATH);

	//�ļ���С
	int nFileSize = 0;
	FILE *pFile;
	_wfopen_s(&pFile, pszTest, _T("rb"));

	if (pFile == NULL)
	{
		return _T("");
	}

	while (TRUE)
	{
		::ZeroMemory(szReadBuf, sizeof(szReadBuf) * sizeof(char));
		uReadSize = (int)fread(szReadBuf, sizeof(char), READ_BUF_SIZE, pFile);

		if (uReadSize == 0)
		{
			break;
		}

		md5.update(szReadBuf, uReadSize);
		nFileSize += uReadSize;
	}

	CString strmd5(md5.toString().c_str());
	fclose(pFile);

	return strmd5;
}

CAtlStringA CUtils::StrEncodeMD5A(CAtlStringA str)
{
	string strTmp = str;
	MD5 md5;
	md5.reset();
	md5.update(strTmp);
	string strMd5 = md5.toString();

	return strMd5.c_str();
}

CAtlString CUtils::StrEncodeMD5W(CAtlString str)
{
	USES_CONVERSION;
	return A2W(StrEncodeMD5A(W2A(str)));
}

BOOL CUtils::DelSelfProcFile()
{
	SHELLEXECUTEINFO sei;
	TCHAR szModule[MAX_PATH], szComspec[MAX_PATH], szParams[MAX_PATH];

	// ����ļ���.  
	if ((GetModuleFileName(0, szModule, MAX_PATH) != 0) &&
		(GetShortPathName(szModule, szModule, MAX_PATH) != 0) &&
		(GetEnvironmentVariable(_T("COMSPEC"), szComspec, MAX_PATH) != 0))
	{
		// �����������.  
		lstrcpy(szParams, _T("/c del "));
		lstrcat(szParams, szModule);
		lstrcat(szParams, _T(" > nul"));

		// ���ýṹ��Ա.  
		sei.cbSize = sizeof(sei);
		sei.hwnd = 0;
		sei.lpVerb = _T("Open");
		sei.lpFile = szComspec;
		sei.lpParameters = szParams;
		sei.lpDirectory = 0;
		sei.nShow = SW_HIDE;
		sei.fMask = SEE_MASK_NOCLOSEPROCESS;
		// ִ��shell����.  
		if (ShellExecuteEx(&sei))
		{
			// ���������н��̵�ִ�м���Ϊ����ִ��,ʹ���������㹻��ʱ����ڴ����˳�.   
			SetPriorityClass(sei.hProcess, IDLE_PRIORITY_CLASS);
			SetPriorityClass(GetCurrentProcess(), REALTIME_PRIORITY_CLASS);
			SetThreadPriority(GetCurrentThread(), THREAD_PRIORITY_TIME_CRITICAL);

			// ֪ͨWindows��Դ�����,�������ļ��Ѿ���ɾ��.  
			SHChangeNotify(SHCNE_DELETE, SHCNF_PATH, szModule, 0);
			return TRUE;
		}
	}
	return FALSE;
}
