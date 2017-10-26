#pragma once
#include <WinSock2.h>
#include <vector>
#include <atlstr.h>

#pragma comment(lib,"Ws2_32.lib")

using namespace std;

// 存放 ip
typedef struct tagIPInfo
{
	char ip[30];
}IPInfo;

class CNetSocket
{
public:
	CNetSocket();
	~CNetSocket();

	// 获取本机ip
	CAtlString GetLocalIp();

	// 获取本机的所有 ip
	// ips[]: 获取到的本机 ip 列表，IPInfo 数组 tagIPInfo pIp[maxCnt] = { 0 };
	// maxCnt: ips[] 数组的大小
	// cnt: 获取到的 ip 个数
	bool GetLocalIPs(IPInfo ips[], int maxCnt, int* cnt);

	// 定义用于接收数据的内存大小
	static const int MAX_BUF_LEN = 3 * 1024;
};

