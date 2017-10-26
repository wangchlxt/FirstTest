#include "stdafx.h"
#include "NetUdp.h"


CNetUdp::CNetUdp() :m_bIsRecv(false)
{
}


CNetUdp::~CNetUdp()
{
}

UINT _stdcall NetUdpServerThread(LPVOID lpParam)
{
	CNetUdp* pThis = (CNetUdp*)lpParam;

	SOCKET uiFdSocket;
	const int len = 10240;
	char szBuffer[len] = { 0 };

	struct sockaddr_in stServerAddr;
	struct sockaddr_in stClientAddr;
	memset(&stServerAddr, 0, sizeof(stServerAddr));
	memset(&stClientAddr, 0, sizeof(stClientAddr));

	int iAddrlen = sizeof(sockaddr_in);

	/* 服务器地址 */
	stServerAddr.sin_family = AF_INET;
	stServerAddr.sin_port = htons(pThis->m_iPort);
	stServerAddr.sin_addr.s_addr = INADDR_ANY;

	/* 服务器端创建socket, 报文模式(UDP)*/
	uiFdSocket = socket(AF_INET, SOCK_DGRAM, 0);

	/* 绑定端口号 */
	bind(uiFdSocket, (struct sockaddr*)&stServerAddr, sizeof(sockaddr_in));

	while (true)
	{
		memset(szBuffer, 0, len);
		int ret = recvfrom(uiFdSocket, szBuffer, len, 0, (struct  sockaddr*)&stClientAddr, &iAddrlen);
		if (ret == SOCKET_ERROR)
		{
			CAtlString clientIp = CA2W(inet_ntoa(stClientAddr.sin_addr));
			ret = pThis->m_pRecv->TcpServerRecvData(clientIp, ret, (BYTE*)szBuffer);

			if (ret == -1)
			{
				break;
			}
		}
	}

	closesocket(uiFdSocket);

	return 0;
}

int CNetUdp::RunServer(int port, ITcpServer* pRecv)
{
	UINT uID = 0;

	if (!m_bIsRecv)
	{
		m_iPort = port;
		m_pRecv = pRecv;

		_beginthreadex(NULL, 0, NetUdpServerThread, this, 0, &uID);
		m_bIsRecv = true;
	}

	return uID;
}


int CNetUdp::SendMess(CAtlString ip, int port, CAtlString mess)
{
	SOCKET uiFdsocket;
	CAtlStringA ipa = CW2A(ip);

	struct sockaddr_in stServerAddr;
	memset(&stServerAddr, 0, sizeof(stServerAddr));

	int iAddrlen = sizeof(sockaddr_in);

	const int len = 1024;
	char szBuffer[len] = { 0 };

	/* 服务器监听的端口和地址 */
	stServerAddr.sin_family = AF_INET;
	stServerAddr.sin_port = htons(port);
	stServerAddr.sin_addr.s_addr = inet_addr(ipa);

	//inet_pton(AF_INET, "117.79.229.199", &stServerAddr.sin_addr.s_addr );

	uiFdsocket = socket(AF_INET, SOCK_DGRAM, 0);

	int ret = sendto(uiFdsocket, szBuffer, len, 0, (struct  sockaddr*)&stServerAddr, iAddrlen);
	
	closesocket(uiFdsocket);

	return ret;
}
