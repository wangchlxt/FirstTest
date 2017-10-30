#pragma once
#include <atlstr.h>

class IBroadcastServer
{
public:
	virtual int BroadcastServerRecvData(CString strClientIp, DWORD dwLen, BYTE* pData) = 0;
};
