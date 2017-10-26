#pragma once
#include "ITcpServer.h"
#include "NetSocket.h"

class CNetTcp : public CNetSocket
{
public:
	CNetTcp();
	~CNetTcp();

	// ���� tcp ���񣬻��߳�������ʹ��ʱ���Կ��߳�
	int RunServer(int port, ITcpServer* pRecv);

	// �� tcp ����˷�����Ϣ����Ϣ������ȶϿ�����
	int SendMess(CAtlString ip, int port, CAtlString mess);

private:
	friend UINT _stdcall NetTcpServerThread(LPVOID lpParam);

	bool m_bIsRecv;
	int m_iPort;
	ITcpServer* m_pRecv;
};

