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

	// �ر� tcp ����
	void ClostServer();

	// �� tcp ����˷�����Ϣ����Ϣ������ȶϿ�����
	int SendMessA(CAtlStringA ip, int port, CAtlStringA mess);

	// �� tcp ����˷�����Ϣ����Ϣ������ȶϿ�����
	int SendMess(CAtlString ip, int port, CAtlString mess);

	// �� tcp ����˷�����Ϣ����Ϣ������ȶϿ�����
	CAtlStringA SendMessRecvA(CAtlStringA ip, int port, CAtlStringA mess);

private:
	friend UINT _stdcall NetTcpServerThread(LPVOID lpParam);

	bool m_bIsRecv;
	bool m_bIsRun;
	int m_iPort;
	ITcpServer* m_pRecv;
};

