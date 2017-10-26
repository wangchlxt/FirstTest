#pragma once
#include "ITcpServer.h"
#include "NetSocket.h"

class CNetTcp : public CNetSocket
{
public:
	CNetTcp();
	~CNetTcp();

	// 运行 tcp 服务，会线程阻塞，使用时可以开线程
	int RunServer(int port, ITcpServer* pRecv);

	// 向 tcp 服务端发送消息，消息发送完既断开连接
	int SendMess(CAtlString ip, int port, CAtlString mess);

private:
	friend UINT _stdcall NetTcpServerThread(LPVOID lpParam);

	bool m_bIsRecv;
	int m_iPort;
	ITcpServer* m_pRecv;
};

