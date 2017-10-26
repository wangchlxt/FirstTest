#include "stdafx.h"
#include "NetBroadcast.h"
#include <atlconv.h>
#include <process.h>

CNetBroadcast::CNetBroadcast() :m_bIsRecv(false), m_bIsSend(false), m_pRecv(NULL)
{
	m_hSendEvent = CreateEvent(NULL, TRUE, FALSE, _T("event_send_broadcast_thread"));
}


CNetBroadcast::~CNetBroadcast()
{
	CloseHandle(m_hSendEvent);
}

// ���չ㲥�߳�
UINT _stdcall NetBroadcastRecvThread(LPVOID lpParam)
{
	CNetBroadcast* pThis = (CNetBroadcast*)lpParam;
	int err = 0;
	CAtlString error;

	// ����socket  
	SOCKET connect_socket;
	connect_socket = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
	if (INVALID_SOCKET == connect_socket)
	{
		error.Format(_T("\"socket \" error! error code is %d"), WSAGetLastError());
		return -1;
	}

	// �������׽���
	SOCKADDR_IN sin;
	sin.sin_family = AF_INET;
	sin.sin_port = htons(pThis->m_iPort);
	sin.sin_addr.s_addr = 0;

	// �����������ϵĹ㲥��ַ��������
	SOCKADDR_IN sin_from;
	sin_from.sin_family = AF_INET;
	sin_from.sin_port = htons(pThis->m_iPort);
	sin_from.sin_addr.s_addr = INADDR_BROADCAST;

	//���ø��׽���Ϊ�㲥����
	bool bOpt = true;
	setsockopt(connect_socket, SOL_SOCKET, SO_BROADCAST, (char*)&bOpt, sizeof(bOpt));

	// ���׽���
	err = bind(connect_socket, (SOCKADDR*)&sin, sizeof(SOCKADDR));
	if (SOCKET_ERROR == err)
	{
		error.Format(_T("\"bind \" error! error code is %d"), WSAGetLastError());
		return -1;
	}

	int nAddrLen = sizeof(SOCKADDR);
	char buff[CNetBroadcast::MAX_BUF_LEN] = { 0 };
	int nLoop = 0;
	while (1)
	{
		memset(buff, 0, CNetBroadcast::MAX_BUF_LEN);
		// ��������
		int nSendSize = recvfrom(connect_socket, buff, CNetBroadcast::MAX_BUF_LEN, 0, (SOCKADDR*)&sin_from, &nAddrLen);
		if (SOCKET_ERROR == nSendSize)
		{
			error.Format(_T("\"recvfrom \" error! error code is %d"), WSAGetLastError());
			return -1;
		}

		pThis->m_pRecv->BroadcastServerRecvData(_T(""), nSendSize, (BYTE*)buff);
	}

	closesocket(connect_socket);
	return 0;
}

// �����㲥�߳�
UINT _stdcall NetBroadcastSendThread(LPVOID lpParam)
{
	CNetBroadcast* pThis = (CNetBroadcast*)lpParam;
	CAtlString errText;

	// ����socket  
	SOCKET connect_socket;
	connect_socket = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
	if (INVALID_SOCKET == connect_socket)
	{
		errText.Format(_T("\"socket \" error! error code is %d"), WSAGetLastError());
		return -1;
	}

	SOCKADDR_IN sin;
	sin.sin_family = AF_INET;
	sin.sin_port = htons(pThis->m_iPort);
	sin.sin_addr.s_addr = INADDR_BROADCAST;

	bool bOpt = true;
	//���ø��׽���Ϊ�㲥����  
	setsockopt(connect_socket, SOL_SOCKET, SO_BROADCAST, (char*)&bOpt, sizeof(bOpt));

	int nAddrLen = sizeof(SOCKADDR);

	char buff[CNetBroadcast::MAX_BUF_LEN] = { 0 };
	USES_CONVERSION;

	while (1)
	{
		WaitForSingleObject(pThis->m_hSendEvent, INFINITE);

		if (pThis->m_vtSendMess.size() == 0)
		{
			ResetEvent(pThis->m_hSendEvent);
		}
		else
		{
			CAtlStringA strMess = W2A(pThis->m_vtSendMess.at(0));
			int ret = sendto(connect_socket, strMess, strMess.GetLength(), 0, (SOCKADDR*)&sin, nAddrLen);
			if (SOCKET_ERROR == ret)
			{
				errText.Format(_T("\"sendto \" error! error code is %d"), WSAGetLastError());
				return -1;
			}
			pThis->m_vtSendMess.erase(pThis->m_vtSendMess.begin());
		}
	}

	closesocket(connect_socket);
	return 0;
}

int CNetBroadcast::RunSend()
{
	UINT uID = 0;

	if (!m_bIsSend)
	{
		_beginthreadex(NULL, 0, NetBroadcastSendThread, this, 0, &uID);
		m_bIsSend = true;
	}

	return uID;
}

int CNetBroadcast::RunRecv(int port, IBroadcastServer* pRecv)
{
	if (port < 1024 || pRecv == NULL)
	{
		return -1;
	}

	UINT uID = 0;

	if (!m_bIsRecv)
	{
		m_iPort = port;
		m_pRecv = pRecv;

		_beginthreadex(NULL, 0, NetBroadcastRecvThread, this, 0, &uID);
		m_bIsRecv = true;
	}

	return uID;
}

void CNetBroadcast::SendMess(CAtlString message)
{
	SetEvent(m_hSendEvent);
	m_vtSendMess.push_back(message);
}
