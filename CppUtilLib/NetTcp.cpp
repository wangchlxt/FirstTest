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
	CStringA strErr;

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
		int err = WSAGetLastError();
		strErr.Format("socket err:%d create error" + err);
		pThis->m_pRecv->TcpServerRecvData("", strErr.GetLength(), (BYTE*)strErr.GetBuffer());

		return -1;
	}

	/* �󶨶˿ں� */
	int nResult = bind(uiFdSerSocket, (struct sockaddr*)&stServerAddr, sizeof(sockaddr_in));
	if (nResult == SOCKET_ERROR)
	{
		int err = WSAGetLastError();
		strErr.Format("socket err:%d bind error", err);
		pThis->m_pRecv->TcpServerRecvData("", strErr.GetLength(), (BYTE*)strErr.GetBuffer());

		closesocket(uiFdSerSocket);
		return -1;
	}


	/* ���������� */
	nResult = listen(uiFdSerSocket, 5);
	if (nResult == SOCKET_ERROR)
	{
		int err = WSAGetLastError();
		strErr.Format("socket err:%d listen error" + err);
		pThis->m_pRecv->TcpServerRecvData("", strErr.GetLength(), (BYTE*)strErr.GetBuffer());

		closesocket(uiFdSerSocket);
		return -1;
	}

	while (true)
	{
		memset(szRecvbuffer, 0, CNetTcp::MAX_BUF_LEN);

		/* ���ܿͻ�������,��ȡ�ͻ��˵�ip��ַ */
		uiFdConnectSocket = accept(uiFdSerSocket, (SOCKADDR*)&stClientAddr, &iAddrlen);
		if (uiFdConnectSocket == INVALID_SOCKET)
		{
			int err = WSAGetLastError();
			strErr.Format("socket err:%d INVALID_SOCKET" + err);
			pThis->m_pRecv->TcpServerRecvData("", strErr.GetLength(), (BYTE*)strErr.GetBuffer());

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
			int err = WSAGetLastError();
			strErr.Format("socket err:%d recv error" + err);

			pThis->m_pRecv->TcpServerRecvData("", strErr.GetLength(), (BYTE*)strErr.GetBuffer());
			
			closesocket(uiFdConnectSocket);
			continue;
		}

		CAtlStringA retSendData = pThis->m_pRecv->TcpServerRecvData(clientIp.GetBuffer(), nResult, (BYTE*)szRecvbuffer);
		if (!retSendData.IsEmpty())
		{
			send(uiFdConnectSocket, retSendData.GetBuffer(), retSendData.GetLength(), 0);
		}

		closesocket(uiFdConnectSocket);

		if (!pThis->m_bIsRun)
		{
			break;
		}
	}

	closesocket(uiFdSerSocket);

	return 0;
}

int CNetTcp::RunServer(int port, ITcpServer* pRecv)
{
	UINT uID = 0;
	m_bIsRun = true;

	if (!m_bIsRecv)
	{
		m_iPort = port;
		m_pRecv = pRecv;

		_beginthreadex(NULL, 0, NetTcpServerThread, this, 0, &uID);
		m_bIsRecv = true;
	}

	return uID;
}

void CNetTcp::ClostServer()
{
	m_bIsRun = false;
}

int CNetTcp::SendMessA(CAtlStringA ip, int port, CAtlStringA mess)
{
	SOCKET uiFdClientsocket;

	struct sockaddr_in stServerAddr;
	int iAddrlen = sizeof(sockaddr_in);

	/* �����������Ķ˿ں͵�ַ */
	memset(&stServerAddr, 0, sizeof(stServerAddr));

	stServerAddr.sin_family = AF_INET;
	stServerAddr.sin_port = htons(port);
	stServerAddr.sin_addr.s_addr = inet_addr(ip);

	/* ����SOCKET */
	uiFdClientsocket = socket(AF_INET, SOCK_STREAM, 0);

	/* ���ӷ����� */
	int nResult = connect(uiFdClientsocket, (SOCKADDR*)&stServerAddr, sizeof(sockaddr_in));
	if (nResult == SOCKET_ERROR)
	{
		return WSAGetLastError();
	}

	/* ��������˷������� */
	nResult = send(uiFdClientsocket, mess, mess.GetLength(), 0);
	if (nResult == SOCKET_ERROR)
	{
		return WSAGetLastError();
	}

	closesocket(uiFdClientsocket);

	return 0;
}

int CNetTcp::SendMess(CAtlString ip, int port, CAtlString mess)
{
	CAtlStringA ipa = CW2A(ip);
	CAtlStringA messa = CW2A(mess);

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

CAtlStringA CNetTcp::SendMessRecvA(CAtlStringA ip, int port, CAtlStringA mess)
{
	CAtlStringA strErr;
	SOCKET uiFdClientsocket;

	struct sockaddr_in stServerAddr;
	int iAddrlen = sizeof(sockaddr_in);

	/* �����������Ķ˿ں͵�ַ */
	memset(&stServerAddr, 0, sizeof(stServerAddr));

	stServerAddr.sin_family = AF_INET;
	stServerAddr.sin_port = htons(port);
	stServerAddr.sin_addr.s_addr = inet_addr(ip);

	/* ����SOCKET */
	uiFdClientsocket = socket(AF_INET, SOCK_STREAM, 0);

	/* ���ӷ����� */
	int nResult = connect(uiFdClientsocket, (SOCKADDR*)&stServerAddr, sizeof(sockaddr_in));
	if (nResult == SOCKET_ERROR)
	{
		int err = WSAGetLastError();
		strErr.Format("socket error by connect code %d",err);
		return strErr;
	}

	/* ��������˷������� */
	nResult = send(uiFdClientsocket, mess, mess.GetLength(), 0);
	if (nResult == SOCKET_ERROR)
	{
		int err = WSAGetLastError();
		strErr.Format("socket error by send code %d", err);
		
		closesocket(uiFdClientsocket);
		return strErr;
	}

	/* ���տͻ�����Ϣ */
	char szRecvbuffer[10240] = { 0 };
	recv(uiFdClientsocket, szRecvbuffer, sizeof(szRecvbuffer), 0);

	closesocket(uiFdClientsocket);

	return szRecvbuffer;
}
