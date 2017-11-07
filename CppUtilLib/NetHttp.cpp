#include "stdafx.h"
#include "NetHttp.h"
#include <atlconv.h>
#include <atlconv.h>
#include "SysPath.h"
#include "StringUtil.h"

#include <WinInet.h>
#pragma comment (lib,"Wininet.lib")

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

CAtlStringA CNetHttp::GetFileContent(CAtlStringA url)
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

	HINTERNET hInternet = InternetOpenUrlA(m_hi, url, NULL, 0, INTERNET_FLAG_RELOAD, 0);
	DWORD err = GetLastError();
	if (hInternet == INVALID_HANDLE_VALUE)
	{
		InternetCloseHandle(m_hi);
		return "";
	}

	char retBuf[10] = { 0 };
	DWORD bufLen = sizeof(retBuf);
	if (!HttpQueryInfoA(hInternet, HTTP_QUERY_STATUS_CODE, retBuf, &bufLen, NULL))
	{
		DWORD err = GetLastError();
		strErr.Format("error in HttpQueryInfoA code %d",err);

		ClearInternet();
		return strErr.GetBuffer();
	}

	CAtlStringA html;
	
	const DWORD tmpLen = 10240;
	char szTmp[tmpLen] = { 0 };
	DWORD rlen = 0;
	DWORD readPoint = 0;

	while (InternetReadFile(hInternet, szTmp, tmpLen, &rlen))
	{
		html += szTmp;
		memset(szTmp, 0, tmpLen);

		DWORD moveLen = InternetSetFilePointer(hInternet, readPoint, NULL, FILE_BEGIN, 0);
		if (moveLen == INVALID_SET_FILE_POINTER)
		{
			break;
		}

		readPoint += moveLen;
	}

	ClearInternet();

	return html;
}

CAtlStringA CNetHttp::GetHostA(CAtlStringA strUrl)
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

CAtlStringA CNetHttp::GetParamA(CAtlStringA strUrl)
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

hostent* CNetHttp::GetHostentA(CAtlStringA strUrl)
{
	CStringA host = GetHostA(strUrl);

	return gethostbyname(host);
}

CAtlStringA CNetHttp::GetHttpHeadA(CAtlStringA method, CAtlStringA param, CAtlStringA host, CAtlStringA other)
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

CAtlString CNetHttp::GetHttpHeadW(CAtlString method, CAtlString param, CAtlString host, CAtlString other)
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

CAtlStringA CNetHttp::GetHtmlA(CAtlStringA strUrl)
{
	CAtlStringA html;
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
	return html;
}

CAtlStringA CNetHttp::GetHtmlByDownFileA(CAtlStringA url)
{
	CAtlStringA html;

	// 清理缓存
	DeleteUrlCacheEntryA(url);

	CSysPath sysPath;
	CAtlString tmpFile = sysPath.GetTempFileNameStr();
	CAtlStringA tmpFileA = CW2A(tmpFile);

	if (S_OK == URLDownloadToFileA(NULL, url, tmpFileA, NULL, NULL))
	{
		HANDLE hFile = CreateFileA(tmpFileA, GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
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
	}

	DeleteFileA(tmpFileA);
	return html;
}

CAtlStringA CNetHttp::GetHostNetIp()
{
	CAtlStringA html = GetHtmlByDownFileA("http://cwx.quectel-service.com/Tool/WechatUtilTest2");

	//html = html.Right(html.GetLength() - html.Find("<center>您的IP是：[") - 1);
	//html = html.Left(html.Find("]"));
	
	return html;
}

// 获取本机外网 ip，从百度获取
CAtlStringA CNetHttp::GetHostNetIpFormBaidu()
{
	return "";
}

// 获取本机外网 ip，从 ip138 获取
CAtlStringA CNetHttp::GetHostNetIpFormIP138()
{
	return "";
}

// 获取本机外网 ip 和城市，从 ip138 获取
CAtlStringA CNetHttp::GetHostNetIpAndCityFormIP138()
{
	return "";
}

