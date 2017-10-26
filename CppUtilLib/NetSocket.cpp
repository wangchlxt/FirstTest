#include "stdafx.h"
#include "NetSocket.h"
#include <atlconv.h>
#pragma warning(disable:4996)

CNetSocket::CNetSocket()
{
}


CNetSocket::~CNetSocket()
{
}

CAtlString CNetSocket::GetLocalIp()
{
	CAtlStringA localIp;

	// ��ȡ������  
	char hostname[256];
	int ret = gethostname(hostname, sizeof(hostname));
	if (ret == SOCKET_ERROR)
	{
		return _T("");
	}

	// ��ȡ����ip  
	HOSTENT* host = gethostbyname(hostname);
	if (host == NULL)
	{
		return _T("");
	}

	// ת��Ϊchar*����������
	localIp.Format("%s", inet_ntoa(*(in_addr*)*host->h_addr_list));
	USES_CONVERSION;
	CAtlString localIpw = A2W(localIp);

	return localIpw;
}

bool CNetSocket::GetLocalIPs(IPInfo ips[], int maxCnt, int* cnt)
{
	// 1.��ȡ������
	char hostname[256] = { 0 };
	int ret = gethostname(hostname, sizeof(hostname));
	if (ret == SOCKET_ERROR)
	{
		return false;
	}

	// 2.��ȡ����ip
	HOSTENT* host = gethostbyname(hostname);
	if (host == NULL)
	{
		return false;
	}

	// 3.���ת��Ϊchar*����������
	*cnt = host->h_length<maxCnt ? host->h_length : maxCnt;
	for (int i = 0; i<*cnt; i++)
	{
		in_addr* addr = (in_addr*)*host->h_addr_list;
		strcpy(ips[i].ip, inet_ntoa(addr[i]));
	}
	return true;
}
