// Network.cpp : 定义 DLL 应用程序的导出函数。
//
#include "stdafx.h"
#include "Network.h"
#include "NetHttp.h"

// 这是已导出类的构造函数。
// 有关类定义的信息，请参阅 Network.h
CNetwork::CNetwork()
{
	return;
}

int CNetwork::Init()
{
	WORD wVersionRequested;
	WSADATA wsaData;

	// 启动socket api  
	wVersionRequested = MAKEWORD(2, 2);
	int err = WSAStartup(wVersionRequested, &wsaData);
	if (err != 0)
	{
		return -1;
	}

	if (LOBYTE(wsaData.wVersion) != 2 || HIBYTE(wsaData.wVersion) != 2)
	{
		WSACleanup();
		return -2;
	}

	return err;
}

int CNetwork::UnInit()
{
	return WSACleanup();
}

CAtlString CNetwork::GetFileContent(CAtlString url)
{
	CNetHttp http;
	return http.GetFileContent(url);
}

void CNetwork::RunFtpServer()
{

}
