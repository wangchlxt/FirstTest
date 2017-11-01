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

	// �� udp ����˷�����Ϣ����Ϣ������ȶϿ�����
	int SendMessA(CAtlStringA ip, int port, CAtlStringA mess);
	int SendMessA2(char* ip, int port, char* mess);

	// �� udp ����˷�����Ϣ����Ϣ������ȶϿ�����
	int SendMess(CAtlString ip, int port, CAtlString mess);

	// �� udp ����˷������ݣ���Ϣ������ȶϿ�����
	int SendData(CAtlStringA ip, int port, char* pData,int dataLen);

private:
	friend UINT _stdcall NetUdpServerThread(LPVOID lpParam);

	bool m_bIsRecv;
	bool m_bIsRun;
	int m_iPort;
	IUdpServer* m_pRecv;
};

