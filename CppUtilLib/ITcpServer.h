#pragma once
#include <atlstr.h>

class ITcpServer
{
public:
	// ���� recv ���ݣ����� -1 ���˳� recv ѭ�����������ֵ��Ϊ�գ��򷵻ص�������Ҫ������У�����˻� delete[]
	virtual CAtlStringA TcpServerRecvData(CAtlStringA ip,DWORD dwLen,BYTE* pData) = 0;
};
