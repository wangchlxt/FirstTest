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
	int SendMessA(CStringA ip, int port, CStringA mess);
	int SendMessA2(char* ip, int port, char* mess);

	// �� tcp ����˷�����Ϣ����Ϣ������ȶϿ�����
	int SendMess(CAtlString ip, int port, CAtlString mess);

	// �� tcp ����˷�����Ϣ����Ϣ������ȶϿ�����
	CStringA SendMessRecvA(CStringA ip, int port, CStringA mess);

	char* SendMessRecvA2(char* ip, int port, char* mess);

private:
	friend UINT _stdcall NetTcpServerThread(LPVOID lpParam);

	bool m_bIsRecv;
	bool m_bIsRun;
	int m_iPort;
	ITcpServer* m_pRecv;
};

