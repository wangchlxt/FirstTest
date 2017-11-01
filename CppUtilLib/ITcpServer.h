#pragma once
#include <atlstr.h>

class ITcpServer
{
public:
	// 处理 recv 数据，返回 -1 则退出 recv 循环，如果返回值不为空，则返回的内容需要放入堆中，服务端会 delete[]
	virtual CAtlStringA TcpServerRecvData(CAtlStringA ip,DWORD dwLen,BYTE* pData) = 0;
};
