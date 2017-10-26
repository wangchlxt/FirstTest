// Network.cpp : ���� DLL Ӧ�ó���ĵ���������
//
#include "stdafx.h"
#include "Network.h"
#include "NetHttp.h"

// �����ѵ�����Ĺ��캯����
// �й��ඨ�����Ϣ������� Network.h
CNetwork::CNetwork()
{
	return;
}

int CNetwork::Init()
{
	WORD wVersionRequested;
	WSADATA wsaData;

	// ����socket api  
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
