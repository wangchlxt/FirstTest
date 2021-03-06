#include "stdafx.h"
#include "UdpServerTest.h"

#include <stdio.h>
#include <iostream>
using namespace std;

#include <Winsock2.h>
#pragma comment (lib, "ws2_32.lib")

CUdpServerTest::CUdpServerTest()
{
}


CUdpServerTest::~CUdpServerTest()
{
}

void CUdpServerTest::Test()
{
	cout << "请输入端口号：";

	char pszTmp[10] = { 0 };
	cin.getline(pszTmp, 10);

	int port = atoi(pszTmp);

	SOCKET uiFdSocket;
	WSADATA wsaData;
	char szbuffer[1024] = { 0 };

	struct sockaddr_in stServerAddr;
	struct sockaddr_in stClientAddr;

	int iAddrlen = sizeof(sockaddr_in);

	/* 调用 Windows Sockets DLL,成功后才能使用 socket 系列函数，可以多次调用 */
	if (0 != WSAStartup(MAKEWORD(2, 1), &wsaData))
	{
		printf("Winsock init failed!\r\n");
		WSACleanup();
		return;
	}

	memset(&stServerAddr, 0, sizeof(stServerAddr));
	memset(&stClientAddr, 0, sizeof(stClientAddr));

	/* 服务器地址 */
	stServerAddr.sin_family = AF_INET;

	/* 监听端口 */
	stServerAddr.sin_port = htons(port);

	stServerAddr.sin_addr.s_addr = INADDR_ANY;

	/* 服务器端创建socket, 报文模式(UDP)*/
	uiFdSocket = socket(AF_INET, SOCK_DGRAM, 0);

	/* 绑定端口号 */
	bind(uiFdSocket, (struct sockaddr*)&stServerAddr, iAddrlen);

	while (true)
	{
		printf("waiting client send msg now...\r\n");
		int ret = recvfrom(uiFdSocket, szbuffer, sizeof(szbuffer), 0, (struct  sockaddr*)&stClientAddr, &iAddrlen);
		cout << "recvfrom data len " << ret << endl;

		if (SOCKET_ERROR != ret)
		{
			printf("Received datagram from %s--%s\n", inet_ntoa(stClientAddr.sin_addr), szbuffer);

			sendto(uiFdSocket, szbuffer, sizeof(szbuffer), 0, (struct sockaddr*)&stClientAddr, iAddrlen);
		}
	}

	closesocket(uiFdSocket);
}
