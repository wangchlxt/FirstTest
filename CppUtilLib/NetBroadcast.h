#pragma once
#include "IBroadcastServer.h"
#include "NetSocket.h"

class CNetBroadcast : public CNetSocket
{
public:
	CNetBroadcast();
	~CNetBroadcast();

	// 运行发布广播线程
	int RunSend();

	// 运行接收广播线程
	int RunRecv(int port, IBroadcastServer* pRecv);

	// 发布广播
	void SendMess(CAtlString message);

private:
	friend UINT _stdcall NetBroadcastRecvThread(LPVOID lpParam);
	friend UINT _stdcall NetBroadcastSendThread(LPVOID lpParam);

	bool m_bIsRecv;
	bool m_bIsSend;
	int m_iPort;
	IBroadcastServer* m_pRecv;
	HANDLE m_hSendEvent;
	vector<CAtlString> m_vtSendMess;
};

