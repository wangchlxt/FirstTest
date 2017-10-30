#pragma once
#include <atlstr.h>

class IUdpServer
{
public:
	// 处理 recv 数据，返回 -1 则退出 recv 循环
	virtual void UdpServerRecvData(char* pIp, DWORD dwLen, BYTE* pData) = 0;
};
