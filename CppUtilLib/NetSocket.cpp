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

	// 获取主机名  
	char hostname[256];
	int ret = gethostname(hostname, sizeof(hostname));
	if (ret == SOCKET_ERROR)
	{
		return _T("");
	}

	// 获取主机ip  
	HOSTENT* host = gethostbyname(hostname);
	if (host == NULL)
	{
		return _T("");
	}

	// 转化为char*并拷贝返回
	localIp.Format("%s", inet_ntoa(*(in_addr*)*host->h_addr_list));
	USES_CONVERSION;
	CAtlString localIpw = A2W(localIp);

	return localIpw;
}

bool CNetSocket::GetLocalIPs(IPInfo ips[], int maxCnt, int* cnt)
{
	// 1.获取主机名
	char hostname[256] = { 0 };
	int ret = gethostname(hostname, sizeof(hostname));
	if (ret == SOCKET_ERROR)
	{
		return false;
	}

	// 2.获取主机ip
	HOSTENT* host = gethostbyname(hostname);
	if (host == NULL)
	{
		return false;
	}

	// 3.逐个转化为char*并拷贝返回
	*cnt = host->h_length<maxCnt ? host->h_length : maxCnt;
	for (int i = 0; i<*cnt; i++)
	{
		in_addr* addr = (in_addr*)*host->h_addr_list;
		strcpy(ips[i].ip, inet_ntoa(addr[i]));
	}
	return true;
}
