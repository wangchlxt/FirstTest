#pragma once
#include "IUdpServer.h"
#include "NetSocket.h"

class CNetUdp : public CNetSocket
{
public:
	CNetUdp();
	~CNetUdp();

	// ���� udp ���񣬻��߳�������ʹ��ʱ���Կ��߳�
	int RunServer(int port, IUdpServer* pRecv);

	// �ر� udp ����
	void ClostServer();

	// �� tcp ����˷�����Ϣ����Ϣ������ȶϿ�����
	int SendMessA(CAtlStringA ip, int port, CAtlStringA mess);
	int SendMessA2(char* ip, int port, char* mess);

	// �� tcp ����˷�����Ϣ����Ϣ������ȶϿ�����
	int SendMess(CAtlString ip, int port, CAtlString mess);

private:
	friend UINT _stdcall NetUdpServerThread(LPVOID lpParam);

	bool m_bIsRecv;
	bool m_bIsRun;
	int m_iPort;
	IUdpServer* m_pRecv;
};

