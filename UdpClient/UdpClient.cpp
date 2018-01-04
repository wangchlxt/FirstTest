// UdpClient.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
#include <stdio.h>
#include <Winsock2.h>
#pragma comment (lib, "ws2_32.lib")
#pragma warning(disable:4996)

using namespace std;

int main()
{
	char pszIp[50] = { 0 };
	cout << "请输入服务器地址：";
	cin.getline(pszIp, 50);

	char pszPort[6] = { 0 };
	cout << "请输入服务器端口号：";
	cin.getline(pszPort, 6);
	int port = atoi(pszPort);

	SOCKET uiFdsocket;
	WSADATA wsaData;

	struct sockaddr_in stServerAddr;
	int iAddrlen = sizeof(sockaddr_in);

	char szbuffer[1024] = {0};

	if (0 != WSAStartup(MAKEWORD(2, 1), &wsaData))
	{
		printf("Winsock init faied!\r\n");
		WSACleanup();
		system("pause");
		return -1;
	}

	/* 服务器监听的端口和地址 */
	memset(&stServerAddr, 0, sizeof(stServerAddr));

	stServerAddr.sin_family = AF_INET;
	stServerAddr.sin_port = htons(port);
	stServerAddr.sin_addr.s_addr = inet_addr(pszIp);

	printf("Now connecting the server...\r\n");

	uiFdsocket = socket(AF_INET, SOCK_DGRAM, 0);

	while (true)
	{
		printf("input message here...\r\n");
		scanf("%s", szbuffer);

		if (strcmp(szbuffer, "bye") == 0)
		{
			printf("exit\r\n");
			Sleep(100);
			closesocket(uiFdsocket);
			break;
		}

		if (SOCKET_ERROR != sendto(uiFdsocket, szbuffer, sizeof(szbuffer), 0, (struct  sockaddr*)&stServerAddr, iAddrlen))
		{
			Sleep(100);

			if (SOCKET_ERROR != recvfrom(uiFdsocket, szbuffer, sizeof(szbuffer), 0, (struct sockaddr*)&stServerAddr, &iAddrlen))
			{
				printf("recive from server:%s\r\n", szbuffer);
			}
		}
	}

	closesocket(uiFdsocket);

	system("pause");
    return 0;
}

