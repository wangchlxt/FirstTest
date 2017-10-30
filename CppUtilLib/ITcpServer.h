#pragma once
#include <atlstr.h>

class ITcpServer
{
public:
	// 处理 recv 数据，返回 -1 则退出 recv 循环
	virtual void TcpServerRecvData(char* pIp,DWORD dwLen,BYTE* pData) = 0;
};
