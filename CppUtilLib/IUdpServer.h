#pragma once
#include <atlstr.h>

class IUdpServer
{
public:
	// ���� recv ���ݣ����� -1 ���˳� recv ѭ��
	virtual void UdpServerRecvData(char* pIp, DWORD dwLen, BYTE* pData) = 0;
};
