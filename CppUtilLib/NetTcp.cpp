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

	/* ��������ַ */
	stServerAddr.sin_family = AF_INET;

	/* �����˿� */
	stServerAddr.sin_port = htons(pThis->m_iPort);

	stServerAddr.sin_addr.s_addr = INADDR_ANY;

	/* �������˴���socket, ��ģʽ(TCP)*/
	uiFdSerSocket = socket(AF_INET, SOCK_STREAM, 0);
	if (uiFdSerSocket == INVALID_SOCKET)
	{
		return WSAGetLastError();
	}

	/* �󶨶˿ں� */
	int nResult = bind(uiFdSerSocket, (struct sockaddr*)&stServerAddr, sizeof(sockaddr_in));
	if (nResult == SOCKET_ERROR)
	{
		return WSAGetLastError();
	}


	/* ���������� */
	nResult = listen(uiFdSerSocket, 5);
	if (nResult == SOCKET_ERROR)
	{
		return WSAGetLastError();
	}

	while (true)
	{
		memset(szRecvbuffer, 0, CNetTcp::MAX_BUF_LEN);

		/* ���ܿͻ�������,��ȡ�ͻ��˵�ip��ַ */
		uiFdConnectSocket = accept(uiFdSerSocket, (SOCKADDR*)&stClientAddr, &iAddrlen);
		if (uiFdConnectSocket == INVALID_SOCKET)
		{
			closesocket(uiFdConnectSocket);
			continue;
		}

		/* ���ͻ���IP��ַ����ת��Ϊ�ַ��� */
		CAtlStringA clientIp;
		clientIp.Format("%s", inet_ntoa(stClientAddr.sin_addr));

		/* ���տͻ�����Ϣ */
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

		/* ������Ϣ���ͻ��ˣ���ʾ��Ϣ���ճɹ� */
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

	/* �����������Ķ˿ں͵�ַ */
	memset(&stServerAddr, 0, sizeof(stServerAddr));

	stServerAddr.sin_family = AF_INET;
	stServerAddr.sin_port = htons(port);
	stServerAddr.sin_addr.s_addr = inet_addr(ipa);

	/* ����SOCKET */
	uiFdClientsocket = socket(AF_INET, SOCK_STREAM, 0);

	/* ���ӷ����� */
	int nResult = connect(uiFdClientsocket, (SOCKADDR*)&stServerAddr, sizeof(sockaddr_in));
	if (nResult == SOCKET_ERROR)
	{
		return WSAGetLastError();
	}

	/* ��������˷������� */
	nResult = send(uiFdClientsocket, messa, messa.GetLength(), 0);
	if (nResult == SOCKET_ERROR)
	{
		return WSAGetLastError();
	}

	closesocket(uiFdClientsocket);

	return 0;
}
