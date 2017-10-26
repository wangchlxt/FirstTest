#pragma once
#include <atlstr.h>

class IBroadcastServer
{
public:
	virtual int BroadcastServerRecvData(CAtlString strClientIp, DWORD dwLen, BYTE* pData) = 0;
};
