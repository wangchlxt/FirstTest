#pragma once
#include "ITcpServer.h"
#include "NetSocket.h"

class CNetUdp : public CNetSocket
{
public:
	CNetUdp();
	~CNetUdp();

	// ���� tcp ���񣬻��߳�������ʹ��ʱ���Կ��߳�
	int RunServer(int port, ITcpServer* pRecv);

	// �� tcp ����˷�����Ϣ����Ϣ������ȶϿ�����
	int SendMess(CAtlString ip, int port, CAtlString mess);

private:
	friend UINT _stdcall NetUdpServerThread(LPVOID lpParam);

	bool m_bIsRecv;
	int m_iPort;
	ITcpServer* m_pRecv;
};

