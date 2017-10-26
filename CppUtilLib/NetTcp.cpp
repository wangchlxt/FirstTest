#include "stdafx.h"
#include "NetTcp.h"
#include <process.h>
#include <atlconv.h>

CNetTcp::CNetTcp() :m_bIsRecv(false)
{
}


CNetTcp::~CNetTcp()
{
}

UINT _stdcall NetTcpServerThread(LPVOID lpParam)
{
	CNetTcp* pThis = (CNetTcp*)lpParam;

	SOCKET uiFdSerSocket;
	SOCKET uiFdConnectSocket;

	char szRecvbuffer[CNetTcp::MAX_BUF_LEN] = { 0 };

	struct sockaddr_in stServerAddr;
	struct sockaddr_in stClientAddr;

	int iAddrlen = sizeof(sockaddr_in);

	memset(&stServerAddr, 0, sizeof(stServerAddr));
	memset(&stClientAddr, 0, sizeof(stClientAddr));

	/* 服务器地址 */
	stServerAddr.sin_family = AF_INET;

	/* 监听端口 */
	stServerAddr.sin_port = htons(pThis->m_iPort);

	stServerAddr.sin_addr.s_addr = INADDR_ANY;

	/* 服务器端创建socket, 流模式(TCP)*/
	uiFdSerSocket = socket(AF_INET, SOCK_STREAM, 0);
	if (uiFdSerSocket == INVALID_SOCKET)
	{
		return WSAGetLastError();
	}

	/* 绑定端口号 */
	int nResult = bind(uiFdSerSocket, (struct sockaddr*)&stServerAddr, sizeof(sockaddr_in));
	if (nResult == SOCKET_ERROR)
	{
		return WSAGetLastError();
	}


	/* 服务器监听 */
	nResult = listen(uiFdSerSocket, 5);
	if (nResult == SOCKET_ERROR)
	{
		return WSAGetLastError();
	}

	while (true)
	{
		memset(szRecvbuffer, 0, CNetTcp::MAX_BUF_LEN);

		/* 接受客户端连接,获取客户端的ip地址 */
		uiFdConnectSocket = accept(uiFdSerSocket, (SOCKADDR*)&stClientAddr, &iAddrlen);
		if (uiFdConnectSocket == INVALID_SOCKET)
		{
			closesocket(uiFdConnectSocket);
			continue;
		}

		/* 将客户端IP地址整形转化为字符串 */
		CAtlStringA clientIp;
		clientIp.Format("%s", inet_ntoa(stClientAddr.sin_addr));

		/* 接收客户端消息 */
		int nResult = recv(uiFdConnectSocket, szRecvbuffer, sizeof(szRecvbuffer), 0);
		if (nResult == SOCKET_ERROR)
		{
			closesocket(uiFdConnectSocket);
			continue;
		}

		USES_CONVERSION;
		nResult = pThis->m_pRecv->TcpServerRecvData(A2W(clientIp), nResult, (BYTE*)szRecvbuffer);
		if (nResult == -1)
		{
			closesocket(uiFdConnectSocket);
			break;
		}

		/* 发送消息到客户端，表示消息接收成功 */
		CAtlStringA success = "success";
		nResult = send(uiFdConnectSocket, success, success.GetLength(), 0);

		closesocket(uiFdConnectSocket);
	}

	closesocket(uiFdSerSocket);

	return 0;
}

int CNetTcp::RunServer(int port, ITcpServer* pRecv)
{
	UINT uID = 0;

	if (!m_bIsRecv)
	{
		m_iPort = port;
		m_pRecv = pRecv;

		_beginthreadex(NULL, 0, NetTcpServerThread, this, 0, &uID);
		m_bIsRecv = true;
	}

	return uID;
}

int CNetTcp::SendMess(CAtlString ip, int port, CAtlString mess)
{
	USES_CONVERSION;
	CAtlStringA ipa = W2A(ip);
	CAtlStringA messa = W2A(mess);

	SOCKET uiFdClientsocket;

	struct sockaddr_in stServerAddr;
	int iAddrlen = sizeof(sockaddr_in);

	/* 服务器监听的端口和地址 */
	memset(&stServerAddr, 0, sizeof(stServerAddr));

	stServerAddr.sin_family = AF_INET;
	stServerAddr.sin_port = htons(port);
	stServerAddr.sin_addr.s_addr = inet_addr(ipa);

	/* 创建SOCKET */
	uiFdClientsocket = socket(AF_INET, SOCK_STREAM, 0);

	/* 连接服务器 */
	int nResult = connect(uiFdClientsocket, (SOCKADDR*)&stServerAddr, sizeof(sockaddr_in));
	if (nResult == SOCKET_ERROR)
	{
		return WSAGetLastError();
	}

	/* 向服务器端发送数据 */
	nResult = send(uiFdClientsocket, messa, messa.GetLength(), 0);
	if (nResult == SOCKET_ERROR)
	{
		return WSAGetLastError();
	}

	closesocket(uiFdClientsocket);

	return 0;
}
