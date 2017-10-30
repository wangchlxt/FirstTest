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

	// 关闭 tcp 服务
	void ClostServer();

	// 向 tcp 服务端发送消息，消息发送完既断开连接
	int SendMessA(CStringA ip, int port, CStringA mess);
	int SendMessA2(char* ip, int port, char* mess);

	// 向 tcp 服务端发送消息，消息发送完既断开连接
	int SendMess(CAtlString ip, int port, CAtlString mess);

	// 向 tcp 服务端发送消息，消息发送完既断开连接
	CStringA SendMessRecvA(CStringA ip, int port, CStringA mess);

	char* SendMessRecvA2(char* ip, int port, char* mess);

private:
	friend UINT _stdcall NetTcpServerThread(LPVOID lpParam);

	bool m_bIsRecv;
	bool m_bIsRun;
	int m_iPort;
	ITcpServer* m_pRecv;
};

