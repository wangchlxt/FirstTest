#pragma once
#include <atlstr.h>

class ITcpServer
{
public:
	// ���� recv ���ݣ����� -1 ���˳� recv ѭ��
	virtual int TcpServerRecvData(CAtlString strClientIp,DWORD dwLen,BYTE* pData) = 0;
};
