#pragma once
#include "IUdpServer.h"
#include "NetSocket.h"

class CNetUdp : public CNetSocket
{
public:
	CNetUdp();
	~CNetUdp();

	// 运行 udp 服务，会线程阻塞，使用时可以开线程
	int RunServer(int port, IUdpServer* pRecv);

	// 关闭 udp 服务
	void ClostServer();

	// 向 tcp 服务端发送消息，消息发送完既断开连接
	int SendMessA(CAtlStringA ip, int port, CAtlStringA mess);
	int SendMessA2(char* ip, int port, char* mess);

	// 向 tcp 服务端发送消息，消息发送完既断开连接
	int SendMess(CAtlString ip, int port, CAtlString mess);

private:
	friend UINT _stdcall NetUdpServerThread(LPVOID lpParam);

	bool m_bIsRecv;
	bool m_bIsRun;
	int m_iPort;
	IUdpServer* m_pRecv;
};

