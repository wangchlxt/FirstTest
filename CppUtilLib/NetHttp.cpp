#include "stdafx.h"
#include "NetHttp.h"
#include <atlconv.h>

CNetHttp::CNetHttp()
{
}

CNetHttp::~CNetHttp()
{
}

BOOL CNetHttp::CheckNetConnect()
{
	DWORD dwFlag = 0;
	return IsNetworkAlive(&dwFlag);
}

void CNetHttp::ClearInternet()
{
	InternetCloseHandle(m_hUrl);
	InternetCloseHandle(m_hi);
}

char* CNetHttp::GetFileContent(char* url)
{
	CStringA strErr;

	if (!CheckNetConnect())
	{
		return "";
	}

	char msgbuf[256] = { 0 };
	m_hi = InternetOpenA("http_down_dll", INTERNET_OPEN_TYPE_PRECONFIG, NULL, INTERNET_INVALID_PORT_NUMBER, 0);
	if (m_hi == NULL)
	{
		return "";
	}

	HINTERNET hUrl = InternetOpenUrlA(m_hi, url, NULL, 0, INTERNET_FLAG_RELOAD, 0);
	DWORD err = GetLastError();
	if (hUrl == INVALID_HANDLE_VALUE)
	{
		InternetCloseHandle(m_hi);
		return "";
	}

	TCHAR retBuf[10] = { 0 };
	DWORD bufLen = sizeof(retBuf);
	if (!HttpQueryInfoA(hUrl, HTTP_QUERY_STATUS_CODE, retBuf, &bufLen, NULL))
	{
		DWORD err = GetLastError();
		strErr.Format("error in HttpQueryInfoA code %d",err);

		ClearInternet();
		return strErr.GetBuffer();
	}

	
	const int readBufLen = 0x40000;
	BYTE* pBuffer = new BYTE[readBufLen];
	memset(pBuffer, 0, readBufLen);

	DWORD len = 0;
	InternetReadFile(hUrl, (LPVOID)pBuffer, readBufLen, &len);

	ClearInternet();
	return (char*)pBuffer;
}

char* CNetHttp::GetHostA(char* strUrl)
{
	CStringA strHost = strUrl;
	strHost = strHost.MakeLower();

	CStringA url = strUrl;
	if (url.Find("http://") == 0)
	{
		strHost = url.Right(url.GetLength() - 7);
	}

	int idx = strHost.Find("/");
	if (idx >= 0)
	{
		strHost = strHost.Left(idx);
	}

	return strHost.GetBuffer();
}

char* CNetHttp::GetParamA(char* strUrl)
{
	CAtlStringA strParam = strUrl;

	int idx = strParam.Find("://");
	if (idx > 0)
	{
		strParam = strParam.Right(strParam.GetLength() - idx - 3);
	}

	idx = strParam.Find("/");
	if (idx > 0)
	{
		strParam = strParam.Right(strParam.GetLength() - idx);
	}

	return strParam.GetBuffer();
}

hostent* CNetHttp::GetHostentA(char* strUrl)
{
	CStringA host = GetHostA(strUrl);

	return gethostbyname(host);
}

char* CNetHttp::GetHttpHeadA(char* method, char* param, char* host, char* other)
{
	CStringA query;
	query += method;
	query += " ";
	query += param;
	query += " HTTP/1.1\r\n";
	query += "Host: ";
	query += host;
	query += "\r\n";
	query += "Connection: keep-alive\r\n";
	query += "Cache-Control: no-cache\r\n";
	query += "Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8\r\n";
	query += "Pragma: no-cache\r\n";
	query += "User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Maxthon/4.4.1.5000 Chrome/30.0.1599.101 Safari/537.36\r\n";
	query += "DNT: 1\r\n";
	query += "Accept-Encoding: gzip,deflate\r\n";
	query += "Accept-Language: zh-CN\r\n";
	query += other;
	query += "\r\n\r\n";

	return query.GetBuffer();
}

wchar_t* CNetHttp::GetHttpHeadW(wchar_t* method, wchar_t* param, wchar_t* host, wchar_t* other)
{
	CAtlStringW query;
	query += method;
	query += _T(" ");
	query += param;
	query += _T(" HTTP/1.1\r\n");
	query += _T("Host: ");
	query += host;
	query += _T("\r\n");
	query += _T("Connection: keep-alive\r\n");
	query += _T("Cache-Control: no-cache\r\n");
	query += _T("Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8\r\n");
	query += _T("Pragma: no-cache\r\n");
	query += _T("User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Maxthon/4.4.1.5000 Chrome/30.0.1599.101 Safari/537.36\r\n");
	query += _T("DNT: 1\r\n");
	query += _T("Accept-Encoding: gzip,deflate\r\n");
	query += _T("Accept-Language: zh-CN\r\n");
	query += other;
	query += _T("\r\n\r\n");

	return query.GetBuffer();
}

char* CNetHttp::GetHtmlA(char* strUrl)
{
	CStringA html;
	hostent* ht = GetHostentA(strUrl);
	if (ht == NULL)
	{
		return "";
	}

	in_addr inAddr;
	memcpy(&inAddr, ht->h_addr, 4);

	sockaddr_in sa;
	sa.sin_family = AF_INET;
	sa.sin_port = htons(80);
	sa.sin_addr.s_addr = inet_addr(inet_ntoa(inAddr));

	SOCKET sk = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	int ret = connect(sk, (sockaddr*)&sa, sizeof(sa));

	if (ret == SOCKET_ERROR)
	{
		closesocket(sk);
		return "";
	}

	CAtlStringA query = GetHttpHeadA("GET", GetParamA(strUrl), GetHostA(strUrl), "");
	send(sk, query, query.GetLength(), 0);

	const int len = 1024;
	char* data = new char[len];
	ret = 1;

	while (ret > 0)
	{
		memset(data, 0, len);
		ret = recv(sk, data, len - 1, 0);
		html += data;
	}

	delete[] data;

	closesocket(sk);
	return html.GetBuffer();
}
