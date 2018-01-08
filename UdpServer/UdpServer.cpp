// UdpServer.cpp : �������̨Ӧ�ó������ڵ㡣
//

#include "stdafx.h"
#include <stdio.h>
#include <iostream>
using namespace std;

#include <Winsock2.h>
#pragma comment (lib, "ws2_32.lib")

int main()
{
	cout << "������˿ںţ�";

	char pszTmp[10] = {0};
	cin.getline(pszTmp, 10);

	int port = atoi(pszTmp);

	SOCKET uiFdSocket;
	WSADATA wsaData;
	char szbuffer[1024] = { 0 };

	struct sockaddr_in stServerAddr;
	struct sockaddr_in stClientAddr;

	int iAddrlen = sizeof(sockaddr_in);

	/* ���� Windows Sockets DLL,�ɹ������ʹ�� socket ϵ�к��������Զ�ε��� */
	if (0 != WSAStartup(MAKEWORD(2, 1), &wsaData))
	{
		printf("Winsock init failed!\r\n");
		WSACleanup();
		return -1;
	}

	memset(&stServerAddr, 0, sizeof(stServerAddr));
	memset(&stClientAddr, 0, sizeof(stClientAddr));

	/* ��������ַ */
	stServerAddr.sin_family = AF_INET;

	/* �����˿� */
	stServerAddr.sin_port = htons(port);

	stServerAddr.sin_addr.s_addr = INADDR_ANY;

	/* �������˴���socket, ����ģʽ(UDP)*/
	uiFdSocket = socket(AF_INET, SOCK_DGRAM, 0);

	/* �󶨶˿ں� */
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

	system("pause");
    return 0;
}
