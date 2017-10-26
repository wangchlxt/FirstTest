#pragma once
#include <WinSock2.h>
#include <vector>
#include <atlstr.h>

#pragma comment(lib,"Ws2_32.lib")

using namespace std;

// ��� ip
typedef struct tagIPInfo
{
	char ip[30];
}IPInfo;

class CNetSocket
{
public:
	CNetSocket();
	~CNetSocket();

	// ��ȡ����ip
	CAtlString GetLocalIp();

	// ��ȡ���������� ip
	// ips[]: ��ȡ���ı��� ip �б�IPInfo ���� tagIPInfo pIp[maxCnt] = { 0 };
	// maxCnt: ips[] ����Ĵ�С
	// cnt: ��ȡ���� ip ����
	bool GetLocalIPs(IPInfo ips[], int maxCnt, int* cnt);

	// �������ڽ������ݵ��ڴ��С
	static const int MAX_BUF_LEN = 3 * 1024;
};

