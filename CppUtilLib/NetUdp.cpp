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

		CStringA clientIp = inet_ntoa(stClientAddr.sin_addr);

		if (ret == SOCKET_ERROR)
		{
			int err = WSAGetLastError();
			CStringA strErr;
			strErr.Format("recvfrom error:%d", err);

			pThis->m_pRecv->UdpServerRecvData(clientIp.GetBuffer(), ret, (BYTE*)strErr.GetBuffer());
		}
		else
		{
			pThis->m_pRecv->UdpServerRecvData(clientIp.GetBuffer(), ret, (BYTE*)szBuffer);
		}

		if (!pThis->m_bIsRecv)
		{
			break;
		}
	}

	closesocket(uiFdSocket);

	return 0;
}

int CNetUdp::RunServer(int port, IUdpServer* pRecv)
{
	UINT uID = 0;
	m_bIsRun = true;

	if (!m_bIsRecv)
	{
		m_iPort = port;
		m_pRecv = pRecv;

		_beginthreadex(NULL, 0, NetUdpServerThread, this, 0, &uID);
		m_bIsRecv = true;
	}

	return uID;
}

void CNetUdp::ClostServer()
{
	m_bIsRun = false;
}

int CNetUdp::SendMessA(CAtlStringA ip, int port, CAtlStringA mess)
{
	SOCKET uiFdsocket;

	struct sockaddr_in stServerAddr;
	memset(&stServerAddr, 0, sizeof(stServerAddr));

	int iAddrlen = sizeof(sockaddr_in);

	/* 服务器监听的端口和地址 */
	stServerAddr.sin_family = AF_INET;
	stServerAddr.sin_port = htons(port);
	stServerAddr.sin_addr.s_addr = inet_addr(ip);

	uiFdsocket = socket(AF_INET, SOCK_DGRAM, 0);

	int ret = sendto(uiFdsocket, mess.GetBuffer(), mess.GetLength(), 0, (struct  sockaddr*)&stServerAddr, iAddrlen);

	closesocket(uiFdsocket);

	return ret;
}

int CNetUdp::SendMessA2(char* ip, int port, char* mess)
{
	return SendMessA(ip, port, mess);
}

int CNetUdp::SendMess(CAtlString ip, int port, CAtlString mess)
{
	SOCKET uiFdsocket;
	CAtlStringA ipa = CW2A(ip);

	struct sockaddr_in stServerAddr;
	memset(&stServerAddr, 0, sizeof(stServerAddr));

	int iAddrlen = sizeof(sockaddr_in);

	/* 服务器监听的端口和地址 */
	stServerAddr.sin_family = AF_INET;
	stServerAddr.sin_port = htons(port);
	stServerAddr.sin_addr.s_addr = inet_addr(ipa);

	uiFdsocket = socket(AF_INET, SOCK_DGRAM, 0);

	CStringA msa = CW2A(mess);
	int ret = sendto(uiFdsocket, msa.GetBuffer(), msa.GetLength(), 0, (struct  sockaddr*)&stServerAddr, iAddrlen);
	
	closesocket(uiFdsocket);

	return ret;
}

int CNetUdp::SendData(CAtlStringA ip, int port, char* pData, int dataLen)
{
	SOCKET uiFdsocket;

	struct sockaddr_in stServerAddr;
	memset(&stServerAddr, 0, sizeof(stServerAddr));

	int iAddrlen = sizeof(sockaddr_in);

	/* 服务器监听的端口和地址 */
	stServerAddr.sin_family = AF_INET;
	stServerAddr.sin_port = htons(port);
	stServerAddr.sin_addr.s_addr = inet_addr(ip);

	uiFdsocket = socket(AF_INET, SOCK_DGRAM, 0);

	int ret = sendto(uiFdsocket, pData, dataLen, 0, (struct  sockaddr*)&stServerAddr, iAddrlen);

	closesocket(uiFdsocket);

	return ret;
}
