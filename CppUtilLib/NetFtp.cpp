#include "stdafx.h"
#include "NetFtp.h"
#include "StringUtil.h"
#include <atlconv.h>
#pragma warning(disable:4996)

CNetFtp::CNetFtp()
{
}

CNetFtp::~CNetFtp()
{
}

//工作者线程处理函数
DWORD WINAPI ProcessTreadIO(LPVOID lpParameter)
{
	//g_logFile.write("ftp 服务线程启动");

	CNetFtp* pFtp = (CNetFtp*)lpParameter;
	DWORD dwFlags;
	LPSOCKET_INF pSI;
	DWORD dwBytesTransferred;
	DWORD i;

	//处理异步的WSASend, WSARecv等请求等
	while (TRUE)
	{
		if ((pFtp->g_index = WSAWaitForMultipleEvents(pFtp->g_dwEventTotal, pFtp->g_events, FALSE,
			WSA_INFINITE, FALSE)) == WSA_WAIT_FAILED)
		{
			printf("错误：WSAWaitForMultipleEvents failed %d\n", WSAGetLastError());
			return 0;
		}

		if ((pFtp->g_index - WSA_WAIT_EVENT_0) == 0)
		{
			WSAResetEvent(pFtp->g_events[0]);
			continue;
		}

		pSI = pFtp->g_sockets[pFtp->g_index - WSA_WAIT_EVENT_0];
		WSAResetEvent(pFtp->g_events[pFtp->g_index - WSA_WAIT_EVENT_0]);

		if (WSAGetOverlappedResult(pSI->s, &(pSI->o), &dwBytesTransferred,
			FALSE, &dwFlags) == FALSE || dwBytesTransferred == 0)
		{
			printf("Closing socket %d\n", pSI->s);

			if (closesocket(pSI->s) == SOCKET_ERROR)
			{
				printf("错误：closesocket() failed with error %d\n", WSAGetLastError());
			}

			GlobalFree(pSI);
			WSACloseEvent(pFtp->g_events[pFtp->g_index - WSA_WAIT_EVENT_0]);

			// Cleanup g_sockets and g_events by removing the socket event handle
			// and socket information structure if they are not at the end of the
			// arrays.

			EnterCriticalSection(&pFtp->g_cs);

			if ((pFtp->g_index - WSA_WAIT_EVENT_0) + 1 != pFtp->g_dwEventTotal)
				for (i = pFtp->g_index - WSA_WAIT_EVENT_0; i < pFtp->g_dwEventTotal; i++)
				{
					pFtp->g_events[i] = pFtp->g_events[i + 1];
					pFtp->g_sockets[i] = pFtp->g_sockets[i + 1];
				}

			pFtp->g_dwEventTotal--;

			LeaveCriticalSection(&pFtp->g_cs);

			continue;
		}

		// 已经有数据传递
		if (pSI->nStatus == WSA_RECV)
		{
			memcpy(&pSI->buffRecv[pSI->dwBytesRecv], pSI->wsaBuf.buf, dwBytesTransferred);
			pSI->dwBytesRecv += dwBytesTransferred;
			printf("接受:%s\n", pSI->buffRecv);
			if (pSI->buffRecv[pSI->dwBytesRecv - 2] == '\r'      // 要保证最后是\r\n
				&& pSI->buffRecv[pSI->dwBytesRecv - 1] == '\n'
				&& pSI->dwBytesRecv > 2)
			{
				if (!pFtp->g_bLoggedIn)
				{
					if (pFtp->LoginIn(pSI) == LOGGED_IN)
						pFtp->g_bLoggedIn = TRUE;
				}
				else
				{
					if (pFtp->DealCommand(pSI) == FTP_QUIT)
						continue;
				}
				// 缓冲区清除
				memset(pSI->buffRecv, 0, sizeof(pSI->buffRecv));
				pSI->dwBytesRecv = 0;
			}
		}
		else
		{
			pSI->dwBytesSend += dwBytesTransferred;
		}

		// 继续接收以后到来的数据
		if (pFtp->RecvReq(pSI) == -1)
			return -1;
	}
	return 0;
}


void CNetFtp::Init(CAtlString user, CAtlString pass, CAtlString dir)
{
	USES_CONVERSION;

	m_strUser = W2A(user);
	m_strPass = W2A(pass);
	m_strDefaultDir = W2A(dir);
}

// 由于只是简单的出现一个登录信息，直接用send就可以了
int CNetFtp::SendRes(LPSOCKET_INF pSI)
{
	static DWORD dwSendBytes = 0;
	pSI->nStatus = WSA_SEND;
	memset(&(pSI->o), 0, sizeof(WSAOVERLAPPED));
	pSI->o.hEvent = g_events[g_index - WSA_WAIT_EVENT_0];
	pSI->wsaBuf.buf = pSI->buffSend + pSI->dwBytesSend;
	pSI->wsaBuf.len = strlen(pSI->buffSend) - pSI->dwBytesSend;
	if (WSASend(pSI->s, &(pSI->wsaBuf), 1, &dwSendBytes,
		0, &(pSI->o), NULL) == SOCKET_ERROR)
	{
		if (WSAGetLastError() != ERROR_IO_PENDING)
		{
			printf("WSASend() failed with error %d\n", WSAGetLastError());
			return -1;
		}
	}
	return 0;
}
//接受数据
int CNetFtp::RecvReq(LPSOCKET_INF pSI)
{
	static DWORD dwRecvBytes = 0;
	pSI->nStatus = WSA_RECV;

	DWORD dwFlags = 0;
	memset(&(pSI->o), 0, sizeof(WSAOVERLAPPED));
	pSI->o.hEvent = g_events[g_index - WSA_WAIT_EVENT_0];
	pSI->wsaBuf.len = DATA_BUFSIZE;

	if (WSARecv(pSI->s, &(pSI->wsaBuf), 1, &dwRecvBytes,
		&dwFlags, &(pSI->o), NULL) == SOCKET_ERROR)
	{
		if (WSAGetLastError() != ERROR_IO_PENDING)
		{
			printf("WSARecv() failed with error %d\n", WSAGetLastError());
			return -1;
		}
	}
	return 0;
}

//显示欢迎消息
BOOL CNetFtp::WelcomeInfo(SOCKET s)
{
	//char* szWelcomeInfo = "220 欢迎您登录到Mini FtpServer...\r\n";
	char* szWelcomeInfo = "220 Mini FtpServer...\r\n";
	if (send(s, szWelcomeInfo, strlen(szWelcomeInfo), 0) == SOCKET_ERROR)
	{
		printf("Ftp client error:%d\n", WSAGetLastError());
		return FALSE;
	}
	// 刚进来，还没连接，故设置初始状态为false
	g_bLoggedIn = FALSE;
	return TRUE;
}
//登录函数
int CNetFtp::LoginIn(LPSOCKET_INF pSocketInfo)
{
	const char* szUserOK = "331 User name okay, need password.\r\n";
	const char* szLoggedIn = "230 User logged in, proceed.\r\n";

	int  nRetVal = 0;
	static char szUser[MAX_NAME_LEN], szPwd[MAX_PWD_LEN];
	LPSOCKET_INF pSI = pSocketInfo;
	// 取得登录用户名
	if (strstr(strupr(pSI->buffRecv), "USER"))
	{
		sprintf(szUser, "%s", pSI->buffRecv + strlen("USER") + 1);
		strtok(szUser, "\r\n");
		// 响应信息
		sprintf(pSI->buffSend, "%s", szUserOK);
		if (SendRes(pSI) == -1) return -1;
		return USER_OK;
	}
	if (strstr(strupr(pSI->buffRecv), "PASS") || strstr(pSI->buffRecv, "pass"))
	{
		sprintf(szPwd, "%s", pSI->buffRecv + strlen("PASS") + 1);
		strtok(szPwd, "\r\n");
		// 判断用户名跟口令正确性
		if (stricmp(szPwd, m_strUser) || stricmp(szUser, m_strPass))
		{
			sprintf(pSI->buffSend, "530 User %s cannot log in.\r\n", szUser);
			printf("User %s cannot log in\n", szUser);
			nRetVal = LOGIN_FAILED;
		}
		else
		{
			sprintf(pSI->buffSend, "%s", szLoggedIn);
			printf("User %s logged in\n", szUser);
			nRetVal = LOGGED_IN;
		}
		if (SendRes(pSI) == -1)
			return -1;
	}
	return nRetVal;
}

char* CNetFtp::ConvertCommaAddress(char* szAddress, WORD wPort)
{
	char szPort[10];
	sprintf(szPort, "%d,%d", wPort / 256, wPort % 256);
	char szIpAddr[20];
	sprintf(szIpAddr, "%s,", szAddress);
	int idx = 0;
	while (szIpAddr[idx])
	{
		if (szIpAddr[idx] == '.')
			szIpAddr[idx] = ',';
		idx++;
	}
	sprintf(szAddress, "%s%s", szIpAddr, szPort);
	return szAddress;
}

int CNetFtp::ConvertDotAddress(char* szAddress, LPDWORD pdwIpAddr, LPWORD pwPort)
{
	int  idx = 0, i = 0, iCount = 0;
	char szIpAddr[MAX_ADDR_LEN]; memset(szIpAddr, 0, sizeof(szIpAddr));
	char szPort[MAX_ADDR_LEN];   memset(szPort, 0, sizeof(szPort));

	*pdwIpAddr = 0; *pwPort = 0;

	while (szAddress[idx])
	{
		if (szAddress[idx] == ',')
		{
			iCount++;
			szAddress[idx] = '.';
		}
		if (iCount < 4)
			szIpAddr[idx] = szAddress[idx];
		else
			szPort[i++] = szAddress[idx];
		idx++;
	}

	if (iCount != 5) return -1;
	*pdwIpAddr = inet_addr(szIpAddr);
	if (*pdwIpAddr == INADDR_NONE) return -1;
	char *pToken = strtok(szPort + 1, ".");
	if (pToken == NULL) return -1;
	*pwPort = (WORD)(atoi(pToken) * 256);
	pToken = strtok(NULL, ".");
	if (pToken == NULL) return -1;
	*pwPort += (WORD)atoi(pToken);

	return 0;
}

UINT CNetFtp::FileListToString(char* buff, UINT nBuffSize, BOOL bDetails)
{
	FILE_INF   fi[MAX_FILE_NUM];
	int nFiles = GetFileList(fi, MAX_FILE_NUM, "*.*");
	char szTemp[128];
	sprintf(buff, "%s", "");
	if (bDetails) {
		for (int i = 0; i<nFiles; i++) {
			if (strlen(buff)>nBuffSize - 128)   break;
			if (!strcmp(fi[i].szFileName, "."))  continue;
			if (!strcmp(fi[i].szFileName, "..")) continue;
			// 时间
			SYSTEMTIME st;
			FileTimeToSystemTime(&(fi[i].ftLastWriteTime), &st);
			char  *szNoon = "AM";
			if (st.wHour > 12)
			{
				st.wHour -= 12;
				szNoon = "PM";
			}
			if (st.wYear >= 2000)
				st.wYear -= 2000;
			else st.wYear -= 1900;
			sprintf(szTemp, "%02u-%02u-%02u  %02u:%02u%s       ",
				st.wMonth, st.wDay, st.wYear, st.wHour, st.wMonth, szNoon);
			strcat(buff, szTemp);
			if (fi[i].dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)
			{
				strcat(buff, "<DIR>");
				strcat(buff, "          ");
			}
			else
			{
				strcat(buff, "     ");
				// 文件大小
				sprintf(szTemp, "% 9d ", fi[i].nFileSizeLow);
				strcat(buff, szTemp);
			}
			// 文件名
			strcat(buff, fi[i].szFileName);
			strcat(buff, "\r\n");
		}
	}
	else
	{
		for (int i = 0; i < nFiles; i++)
		{
			if (strlen(buff) + strlen(fi[i].szFileName) + 2 < nBuffSize)
			{
				strcat(buff, fi[i].szFileName);
				strcat(buff, "\r\n");
			}
			else
				break;
		}
	}
	return strlen(buff);
}

DWORD CNetFtp::ReadFileToBuffer(const char* szFile, char *buff, DWORD nFileSize)
{
	DWORD  idx = 0;
	DWORD  dwBytesLeft = nFileSize;
	DWORD  dwNumOfBytesRead = 0;
	char lpFileName[MAX_PATH];
	GetCurrentDirectoryA(MAX_PATH, lpFileName);
	strcat(lpFileName, "\\");
	strcat(lpFileName, szFile);
	HANDLE hFile = CreateFileA(lpFileName,
		GENERIC_READ,
		FILE_SHARE_READ,
		NULL,
		OPEN_EXISTING,
		FILE_ATTRIBUTE_NORMAL,
		NULL);
	if (hFile != INVALID_HANDLE_VALUE)
	{
		while (dwBytesLeft > 0)
		{
			if (!ReadFile(hFile, &buff[idx], dwBytesLeft, &dwNumOfBytesRead, NULL))
			{
				printf("读文件出错.\n");
				CloseHandle(hFile);
				return 0;
			}
			idx += dwNumOfBytesRead;
			dwBytesLeft -= dwNumOfBytesRead;
		}
		CloseHandle(hFile);
	}
	return idx;
}

DWORD CNetFtp::WriteToFile(SOCKET s, const char* szFile)
{
	DWORD  idx = 0;
	DWORD  dwNumOfBytesWritten = 0;
	DWORD  nBytesLeft = DATA_BUFSIZE;
	char   buf[DATA_BUFSIZE];
	char   lpFileName[MAX_PATH];
	GetCurrentDirectoryA(MAX_PATH, lpFileName);
	strcat(lpFileName, "\\");
	strcat(lpFileName, szFile);
	HANDLE hFile = CreateFileA(lpFileName,
		GENERIC_WRITE,
		FILE_SHARE_WRITE,
		NULL,
		OPEN_ALWAYS,
		FILE_ATTRIBUTE_NORMAL,
		NULL);
	if (hFile == INVALID_HANDLE_VALUE)
	{
		printf("打开文件出错.\n");
		return 0;
	}

	while (TRUE)
	{
		int nBytesRecv = 0;
		idx = 0; nBytesLeft = DATA_BUFSIZE;
		while (nBytesLeft > 0)
		{
			nBytesRecv = recv(s, &buf[idx], nBytesLeft, 0);
			if (nBytesRecv == SOCKET_ERROR)
			{
				printf("Failed to send buffer to socket %d\n", WSAGetLastError());
				return -1;
			}
			if (nBytesRecv == 0) break;

			idx += nBytesRecv;
			nBytesLeft -= nBytesRecv;
		}
		nBytesLeft = idx;   // 要写入文件中的字节数
		idx = 0;			// 索引清0,指向开始位置
		while (nBytesLeft > 0)
		{
			// 移动文件指针到文件末尾
			if (!SetEndOfFile(hFile)) return 0;
			if (!WriteFile(hFile, &buf[idx], nBytesLeft, &dwNumOfBytesWritten, NULL))
			{
				printf("写文件出错.\n");
				CloseHandle(hFile);
				return 0;
			}
			idx += dwNumOfBytesWritten;
			nBytesLeft -= dwNumOfBytesWritten;
		}
		// 如果没有数据可接收，退出循环
		if (nBytesRecv == 0) break;
	}

	CloseHandle(hFile);
	return idx;
}
int CNetFtp::CombindFileNameSize(const char* szFileName, char* szFileNS)
{
	// 假定文件的大小不超过4GB,只处理低位
	int nFileSize = -1;
	FILE_INF fi[1];
	int nFiles = GetFileList(fi, 1, szFileName);
	if (nFiles != 1) return -1;
	sprintf(szFileNS, "%s<%d bytes>", szFileName, fi[0].nFileSizeLow);
	nFileSize = fi[0].nFileSizeLow;
	return nFileSize;
}

int	CNetFtp::DataConn(SOCKET& s, DWORD dwIp, WORD wPort, int nMode)
{
	// 创建一个socket
	s = socket(AF_INET, SOCK_STREAM, 0);
	if (s == INVALID_SOCKET)
	{
		printf("Failed to get a socket %d\n", WSAGetLastError());
		return -1;
	}

	struct sockaddr_in inetAddr;
	inetAddr.sin_family = AF_INET;
	if (nMode == MODE_PASV)
	{
		inetAddr.sin_port = htons(wPort);
		inetAddr.sin_addr.s_addr = dwIp;
	}
	else
	{
		inetAddr.sin_port = htons(DATA_FTP_PORT);
		inetAddr.sin_addr.s_addr = inet_addr(GetLocalAddress());
	}

	BOOL optval = TRUE;
	if (setsockopt(s, SOL_SOCKET, SO_REUSEADDR,
		(char*)&optval, sizeof(optval)) == SOCKET_ERROR)
	{
		printf("Failed to setsockopt %d.\n", WSAGetLastError());
		closesocket(s);
		return -1;
	}

	if (bind(s, (struct sockaddr*)&inetAddr, sizeof(inetAddr)) == SOCKET_ERROR)
	{
		printf("Failed to bind a socket %d.\n", WSAGetLastError());
		closesocket(s);
		return -1;
	}

	if (MODE_PASV == nMode)
	{
		if (listen(s, SOMAXCONN) == SOCKET_ERROR)
		{
			printf("Failed to listen a socket %d.\n", WSAGetLastError());
			closesocket(s);
			return -1;
		}
	}
	else if (MODE_PORT == nMode)
	{
		struct sockaddr_in addr;
		addr.sin_family = AF_INET;
		addr.sin_port = htons(wPort);
		addr.sin_addr.s_addr = dwIp;
		if (connect(s, (const sockaddr*)&addr, sizeof(addr)) == SOCKET_ERROR)
		{
			printf("Failed to connect a socket %d\n", WSAGetLastError());
			closesocket(s);
			return -1;
		}
	}
	return 0;
}

int CNetFtp::DataSend(SOCKET s, char* buff, int nBufSize)
{
	int nBytesLeft = nBufSize;
	int idx = 0, nBytes = 0;
	while (nBytesLeft > 0) {
		nBytes = send(s, &buff[idx], nBytesLeft, 0);
		if (nBytes == SOCKET_ERROR)
		{
			printf("Failed to send buffer to socket %d\n", WSAGetLastError());
			closesocket(s);
			return -1;
		}
		nBytesLeft -= nBytes;
		idx += nBytes;
	}
	return idx;
}
int CNetFtp::DataRecv(SOCKET s, const char* szFileName)
{
	return WriteToFile(s, szFileName);
}

SOCKET CNetFtp::DataAccept(SOCKET& s)
{
	SOCKET sAccept = accept(s, NULL, NULL);
	if (sAccept != INVALID_SOCKET)
	{
		closesocket(s);
	}
	return sAccept;
}
int CNetFtp::DealCommand(LPSOCKET_INF pSI)
{
	int nRetVal = 0;
	static SOCKET sAccept = INVALID_SOCKET;
	static SOCKET s = INVALID_SOCKET;
	static BOOL   bPasv = FALSE;

	char  szCmd[MAX_REQ_LEN];
	char  szCurrDir[MAX_PATH];
	strcpy(szCmd, pSI->buffRecv);
	if (strtok(szCmd, " \r\n") == NULL) return -1;
	strupr(szCmd);

	const char*  szOpeningAMode = "150 Opening ASCII mode data connection for ";
	static DWORD  dwIpAddr = 0;
	static WORD   wPort = 0;
	// ?PORT n1,n2,n3,n4,n5,n6
	if (strstr(szCmd, "PORT"))
	{
		if (ConvertDotAddress(pSI->buffRecv + strlen("PORT") + 1, &dwIpAddr, &wPort) == -1)
			return -1;
		const char*  szPortCmdOK = "200 PORT Command successful.\r\n";
		sprintf(pSI->buffSend, "%s", szPortCmdOK);
		if (SendRes(pSI) == -1) return -1;
		bPasv = FALSE;
		return CMD_OK;
	}
	if (strstr(szCmd, "PASV"))
	{
		if (DataConn(s, htonl(INADDR_ANY), PORT_BIND, MODE_PASV) == -1)
			return -1;
		char *szCommaAddress = ConvertCommaAddress(GetLocalAddress(), PORT_BIND);
		sprintf(pSI->buffSend, "227 Entering Passive Mode (%s).\r\n", szCommaAddress);
		if (SendRes(pSI) == -1)
			return -1;
		bPasv = TRUE;
		return PASSIVE_MODE;
	}
	if (strstr(szCmd, "NLST") || strstr(szCmd, "LIST"))
	{
		if (bPasv) sAccept = DataAccept(s);
		if (!bPasv)
			sprintf(pSI->buffSend, "%s/bin/ls.\r\n", szOpeningAMode);
		else
			strcpy(pSI->buffSend, "125 Data connection already open; Transfer starting.\r\n");

		if (SendRes(pSI) == -1)
			return -1;
		// 取得文件列表信息，并转换成字符串
		BOOL bDetails = strstr(szCmd, "LIST") ? TRUE : FALSE;
		char buff[DATA_BUFSIZE];
		UINT nStrLen = FileListToString(buff, sizeof(buff), bDetails);
		if (!bPasv)
		{
			if (DataConn(s, dwIpAddr, wPort, MODE_PORT) == -1)
				return -1;
			if (DataSend(s, buff, nStrLen) == -1)
				return -1;
			closesocket(s);
		}
		else
		{
			DataSend(sAccept, buff, nStrLen);
			closesocket(sAccept);
		}
		sprintf(pSI->buffSend, "%s", "226 Transfer complete.\r\n");
		if (SendRes(pSI) == -1)
			return -1;

		return TRANS_COMPLETE;
	}
	if (strstr(szCmd, "RETR"))
	{
		if (bPasv) sAccept = DataAccept(s);
		char szFileNS[MAX_PATH];
		char *szFile = strtok(NULL, " \r\n");
		int nFileSize = CombindFileNameSize(szFile, szFileNS);
		if (nFileSize == -1)
		{
			sprintf(pSI->buffSend, "550 %s: 系统找不到指定的文件.\r\n", szFile);
			if (SendRes(pSI) == -1)
				return -1;
			if (!bPasv) closesocket(sAccept);
			else closesocket(s);

			return CANNOT_FIND;
		}
		else
			sprintf(pSI->buffSend, "%s%s.\r\n", szOpeningAMode, szFileNS);

		if (SendRes(pSI) == -1)
			return -1;

		char* buff = new char[nFileSize];
		if (NULL == buff)
		{
			printf("Not enough memory error!\n");
			return -1;
		}
		if (ReadFileToBuffer(szFile, buff, nFileSize) == (DWORD)nFileSize)
		{
			// 处理Data FTP连接
			Sleep(10);
			if (bPasv)
			{
				DataSend(sAccept, buff, nFileSize);
				closesocket(sAccept);
			}
			else
			{
				if (DataConn(s, dwIpAddr, wPort, MODE_PORT) == -1)
					return -1;
				DataSend(s, buff, nFileSize);
				closesocket(s);
			}
		}
		if (buff != NULL)
			delete[] buff;

		sprintf(pSI->buffSend, "%s", "226 Transfer complete.\r\n");
		if (SendRes(pSI) == -1)
			return -1;


		return TRANS_COMPLETE;
	}
	if (strstr(szCmd, "STOR"))
	{
		if (bPasv) sAccept = DataAccept(s);
		char *szFile = strtok(NULL, " \r\n");
		if (NULL == szFile) return -1;
		sprintf(pSI->buffSend, "%s%s.\r\n", szOpeningAMode, szFile);
		if (SendRes(pSI) == -1)
			return -1;

		// 处理Data FTP连接
		if (bPasv)
			DataRecv(sAccept, szFile);
		else
		{
			if (DataConn(s, dwIpAddr, wPort, MODE_PORT) == -1)
				return -1;
			DataRecv(s, szFile);
		}

		sprintf(pSI->buffSend, "%s", "226 Transfer complete.\r\n");
		if (SendRes(pSI) == -1)
			return -1;

		return TRANS_COMPLETE;
	}
	if (strstr(szCmd, "QUIT"))
	{
		sprintf(pSI->buffSend, "%s", "221 Good bye,欢迎下次再来.\r\n");
		if (SendRes(pSI) == -1)
			return -1;

		return FTP_QUIT;
	}
	if (strstr(szCmd, "XPWD") || strstr(szCmd, "PWD"))
	{
		GetCurrentDirectoryA(MAX_PATH, szCurrDir);
		sprintf(pSI->buffSend, "257 \"%s\" is current directory.\r\n",
			RelativeDirectory(szCurrDir));
		if (SendRes(pSI) == -1) return -1;

		return CURR_DIR;
	}
	if (strstr(szCmd, "CWD") || strstr(szCmd, "CDUP"))
	{
		char *szDir = strtok(NULL, "\r\n");
		if (szDir == NULL) szDir = "\\";
		char szSetDir[MAX_PATH];
		if (strstr(szCmd, "CDUP"))
			strcpy(szSetDir, "..");
		else
			strcpy(szSetDir, AbsoluteDirectory(szDir));
		if (!SetCurrentDirectoryA(szSetDir))
		{
			sprintf(szCurrDir, "\\%s", szSetDir);
			sprintf(pSI->buffSend, "550 %s No such file or Directory.\r\n",
				RelativeDirectory(szCurrDir));
			nRetVal = CANNOT_FIND;
		}
		else
		{
			GetCurrentDirectoryA(MAX_PATH, szCurrDir);
			sprintf(pSI->buffSend, "250 Directory changed to /%s.\r\n",
				RelativeDirectory(szCurrDir));
			nRetVal = DIR_CHANGED;
		}
		if (SendRes(pSI) == -1) return -1;

		return nRetVal;
	}
	if (strstr(szCmd, "SYST"))
	{
		sprintf(pSI->buffSend, "%s", "215 Windows_NT Version 4.0\r\n");
		if (SendRes(pSI) == -1) return -1;
		return OS_TYPE;
	}
	if (strstr(szCmd, "TYPE"))
	{
		char *szType = strtok(NULL, "\r\n");
		if (szType == NULL) szType = "A";
		sprintf(pSI->buffSend, "200 Type set to %s.\r\n", szType);
		if (SendRes(pSI) == -1)
			return -1;
		return CMD_OK;
	}
	if (strstr(szCmd, "REST"))
	{
		sprintf(pSI->buffSend, "504 Reply marker must be 0.\r\n");
		if (SendRes(pSI) == -1)
			return -1;
		return REPLY_MARKER;
	}
	if (strstr(szCmd, "NOOP"))
	{
		sprintf(pSI->buffSend, "200 NOOP command successful.\r\n");
		if (SendRes(pSI) == -1)
			return -1;
		return CMD_OK;
	}
	//其余都是无效的命令
	sprintf(pSI->buffSend, "500 '%s' command not understand.\r\n", szCmd);
	if (SendRes(pSI) == -1) return -1;
	return nRetVal;
}

///////////////////////////////////////////////////////////////////////////////////////////
//其他函数
char* CNetFtp::GetLocalAddress()
{
	struct in_addr *pinAddr;
	LPHOSTENT	lpHostEnt;
	int			nRet;
	int			nLen;
	char        szLocalAddr[80];
	memset(szLocalAddr, 0, sizeof(szLocalAddr));
	// Get our local name
	nRet = gethostname(szLocalAddr, sizeof(szLocalAddr));
	if (nRet == SOCKET_ERROR)
	{
		return NULL;
	}
	// "Lookup" the local name
	lpHostEnt = gethostbyname(szLocalAddr);
	if (NULL == lpHostEnt)
	{
		return NULL;
	}
	// Format first address in the list
	pinAddr = ((LPIN_ADDR)lpHostEnt->h_addr);
	nLen = strlen(inet_ntoa(*pinAddr));
	if ((DWORD)nLen > sizeof(szLocalAddr))
	{
		WSASetLastError(WSAEINVAL);
		return NULL;
	}
	return inet_ntoa(*pinAddr);
}

int CNetFtp::GetFileList(LPFILE_INF pFI, UINT nArraySize, const char* szPath)
{
	WIN32_FIND_DATAA  wfd;
	int idx = 0;
	CHAR lpFileName[MAX_PATH];
	GetCurrentDirectoryA(MAX_PATH, lpFileName);
	strcat(lpFileName, "\\");
	strcat(lpFileName, szPath);
	HANDLE hFile = FindFirstFileA(lpFileName, &wfd);
	if (hFile != INVALID_HANDLE_VALUE)
	{
		pFI[idx].dwFileAttributes = wfd.dwFileAttributes;
		lstrcpyA(pFI[idx].szFileName, wfd.cFileName);
		pFI[idx].ftCreationTime = wfd.ftCreationTime;
		pFI[idx].ftLastAccessTime = wfd.ftLastAccessTime;
		pFI[idx].ftLastWriteTime = wfd.ftLastWriteTime;
		pFI[idx].nFileSizeHigh = wfd.nFileSizeHigh;
		pFI[idx++].nFileSizeLow = wfd.nFileSizeLow;
		while (FindNextFileA(hFile, &wfd) && idx < (int)nArraySize)
		{
			pFI[idx].dwFileAttributes = wfd.dwFileAttributes;
			lstrcpyA(pFI[idx].szFileName, wfd.cFileName);
			pFI[idx].ftCreationTime = wfd.ftCreationTime;
			pFI[idx].ftLastAccessTime = wfd.ftLastAccessTime;
			pFI[idx].ftLastWriteTime = wfd.ftLastWriteTime;
			pFI[idx].nFileSizeHigh = wfd.nFileSizeHigh;
			pFI[idx++].nFileSizeLow = wfd.nFileSizeLow;
		}
		FindClose(hFile);
	}
	return idx;
}
char* CNetFtp::HostToNet(char* szPath)
{
	int idx = 0;
	if (NULL == szPath) return NULL;
	strlwr(szPath);
	while (szPath[idx])
	{
		if (szPath[idx] == '\\')
			szPath[idx] = '/';
		idx++;
	}
	return szPath;
}

char* CNetFtp::NetToHost(char* szPath)
{
	int idx = 0;
	if (NULL == szPath) return NULL;
	strlwr(szPath);
	while (szPath[idx])
	{
		if ('/' == szPath[idx])
			szPath[idx] = '\\';
		idx++;
	}
	return szPath;
}
char* CNetFtp::RelativeDirectory(char* szDir)
{
	int nStrLen = strlen(m_strDefaultDir);
	if (!strnicmp(szDir, m_strDefaultDir, nStrLen))
		szDir += nStrLen;

	if (szDir && szDir[0] == '\0') szDir = "/";

	return HostToNet(szDir);
}
char* CNetFtp::AbsoluteDirectory(char* szDir)
{
	char szTemp[MAX_PATH];
	strcpy(szTemp, m_strDefaultDir.GetBuffer() + 2);
	if (NULL == szDir) return NULL;
	if ('/' == szDir[0])
		strcat(szTemp, szDir);
	szDir = szTemp;
	return NetToHost(szDir);
}

int CNetFtp::RunServer(void)
{
	//g_logFile.write("启动 ftp server 线程...");

	CAtlStringA log;
	WSADATA wsaData;
	SOCKET sListen, sAccept;
	SOCKADDR_IN inetAddr;
	DWORD dwFlags;
	DWORD dwThreadId;
	DWORD dwRecvBytes;
	INT   nRet;

	InitializeCriticalSection(&g_cs);
	if ((nRet = WSAStartup(0x0202, &wsaData)) != 0) {
		log.Format("错误[ftp server RunServer]：WSAStartup failed with error %d", nRet);
		//g_logFile.write(log.GetBuffer());
		return -1;
	}

	// 先取得本地地址
	sprintf(g_szLocalAddr, "%s", GetLocalAddress());

	if ((sListen = WSASocket(AF_INET, SOCK_STREAM, 0, NULL, 0,
		WSA_FLAG_OVERLAPPED)) == INVALID_SOCKET)
	{
		log.Format("错误[ftp server RunServer]：Failed to get a socket %d", WSAGetLastError());
		//g_logFile.write(log.GetBuffer());
		WSACleanup();
		return -1;
	}

	inetAddr.sin_family = AF_INET;
	inetAddr.sin_addr.s_addr = htonl(INADDR_ANY);
	inetAddr.sin_port = htons(FTP_PORT);

	if (bind(sListen, (PSOCKADDR)&inetAddr, sizeof(inetAddr)) == SOCKET_ERROR)
	{
		log.Format("错误[ftp server RunServer]：bind() failed with error %d", WSAGetLastError());
		//g_logFile.write(log.GetBuffer());
		return -1;
	}

	if (listen(sListen, SOMAXCONN))
	{
		log.Format("错误[ftp server RunServer]：listen() failed with error %d", WSAGetLastError());
		//g_logFile.write(log.GetBuffer());
		return -1;
	}

	//g_logFile.write("ftp server 已启动");
	//g_logFile.write("ftp server 开始侦听");

	if ((sAccept = WSASocket(AF_INET, SOCK_STREAM, 0, NULL, 0,
		WSA_FLAG_OVERLAPPED)) == INVALID_SOCKET)
	{
		log.Format("错误[ftp server RunServer]：Failed to get a socket %d", WSAGetLastError());
		//g_logFile.write(log.GetBuffer());
		return -1;
	}

	//创建第一个手动重置对象 
	if ((g_events[0] = WSACreateEvent()) == WSA_INVALID_EVENT)
	{
		log.Format("错误[ftp server RunServer]：WSACreateEvent failed with error %d", WSAGetLastError());
		//g_logFile.write(log.GetBuffer());
		return -1;
	}

	// 创建一个线程处理请求
	if (CreateThread(NULL, 0, ProcessTreadIO, this, 0, &dwThreadId) == NULL)
	{
		log.Format("错误[ftp server RunServer]：CreateThread failed with error %d", WSAGetLastError());
		//g_logFile.write(log.GetBuffer());
		return -1;
	}

	g_dwEventTotal = 1;

	while (TRUE)
	{
		//处理入站连接
		if ((sAccept = accept(sListen, NULL, NULL)) == INVALID_SOCKET)
		{
			log.Format("错误[ftp server RunServer]：accept failed with error %d", WSAGetLastError());
			//g_logFile.write(log.GetBuffer());
			return -1;
		}

		//回传欢迎消息
		if (!WelcomeInfo(sAccept)) break;
		//设置ftp根目录
		if (!SetCurrentDirectoryA(m_strDefaultDir)) break;

		//操作临界区，防止出错
		EnterCriticalSection(&g_cs);
		//创建一个新的SOCKET_INF结构处理接受的数据socket.
		if ((g_sockets[g_dwEventTotal] = (LPSOCKET_INF)
			GlobalAlloc(GPTR, sizeof(SOCKET_INF))) == NULL)
		{
			log.Format("错误[ftp server RunServer]：GlobalAlloc() failed with error %d", WSAGetLastError());
			//g_logFile.write(log.GetBuffer());
			return -1;
		}

		//初始化新的SOCKET_INF结构
		char buff[DATA_BUFSIZE]; memset(buff, 0, DATA_BUFSIZE);
		g_sockets[g_dwEventTotal]->wsaBuf.buf = buff;
		g_sockets[g_dwEventTotal]->wsaBuf.len = DATA_BUFSIZE;
		g_sockets[g_dwEventTotal]->s = sAccept;
		memset(&(g_sockets[g_dwEventTotal]->o), 0, sizeof(OVERLAPPED));
		g_sockets[g_dwEventTotal]->dwBytesSend = 0;
		g_sockets[g_dwEventTotal]->dwBytesRecv = 0;
		g_sockets[g_dwEventTotal]->nStatus = WSA_RECV;    // 接收

		//创建事件
		if ((g_sockets[g_dwEventTotal]->o.hEvent = g_events[g_dwEventTotal] =
			WSACreateEvent()) == WSA_INVALID_EVENT)
		{
			log.Format("错误[ftp server RunServer]：WSACreateEvent() failed with error %d", WSAGetLastError());
			//g_logFile.write(log.GetBuffer());
			return -1;
		}

		//发出接受请求
		dwFlags = 0;
		dwRecvBytes = 0;
		if (WSARecv(g_sockets[g_dwEventTotal]->s,
			&(g_sockets[g_dwEventTotal]->wsaBuf), 1, &dwRecvBytes, &dwFlags,
			&(g_sockets[g_dwEventTotal]->o), NULL) == SOCKET_ERROR)
		{
			if (WSAGetLastError() != ERROR_IO_PENDING)
			{
				log.Format("错误[ftp server RunServer]：WSARecv() failed with error %d", WSAGetLastError());
				//g_logFile.write(log.GetBuffer());
				return -1;
			}
		}
		g_dwEventTotal++;

		//离开临界区
		LeaveCriticalSection(&g_cs);

		//使第一个事件有信号。使工作者线程处理其他的事件
		if (WSASetEvent(g_events[0]) == FALSE)
		{
			log.Format("错误[ftp server RunServer]：WSASetEvent failed with error %d", WSAGetLastError());
			//g_logFile.write(log.GetBuffer());
			return -1;
		}
	}
	return 0;
}

int CNetFtp::PasvToSockAddr(CAtlStringA recvData, sockaddr_in& addr)
{
	recvData = recvData.Right(recvData.GetLength() - recvData.Find('(') - 1);
	recvData = recvData.Left(recvData.Find(')'));

	vector<CAtlStringA> vtList;
	CStringUtil su;
	su.SplitA(recvData, vtList, ",");

	CAtlStringA ip;
	ip.Format("%s.%s.%s.%s", vtList.at(0), vtList.at(1), vtList.at(2), vtList.at(3));
	int port = atoi(vtList.at(4)) * 256 + atoi(vtList.at(5));

	memset(&addr, 0, sizeof(addr));
	addr.sin_family = AF_INET;
	addr.sin_port = htons(port);
	addr.sin_addr.s_addr = inet_addr(ip);
	//inet_pton(AF_INET, ip, &addr.sin_addr.s_addr);

	return 0;
}

// ftp 被动模式文件上传
//		return: 0 正常
//			-1 初始化 socket 环境失败
//			-2 socket 库版本不对
//			-3 连接 ftp 服务器失败
//			-4 ftp 用户名错误
//			-5 ftp 密码错误
//			-6 设置被动模式失败
//			-7 连接被动模式数据端口失败
//			-8 设置 ftp 服务端路径失败
//			-9 打开 ftp 服务端文件失败 
//			-10 打开要上传的文件失败
int CNetFtp::FtpPasvFileUpload(CAtlStringA ftpHost, int ftpPort, CAtlStringA ftpUser, CAtlStringA ftpPass, CAtlStringA ftpDir, CAtlStringA ftpFileName, CAtlStringA fileName)
{
	CAtlStringA msg;
	int ret = 0;

	SOCKET uiFdClientsocket;
	WSADATA wsaData;

	sockaddr_in stServerAddr;
	int iAddrlen = sizeof(sockaddr_in);

	const int len = 1024;
	char szRecvbuffer[len] = { 0 };

	if (0 != WSAStartup(MAKEWORD(2, 1), &wsaData))
	{
		WSACleanup();
		return -1;
	}

	if (LOBYTE(wsaData.wVersion) != 2 || HIBYTE(wsaData.wVersion) != 1)
	{
		WSACleanup();
		return -2;
	}

	/* 服务器监听的端口和地址 */
	memset(&stServerAddr, 0, sizeof(stServerAddr));

	stServerAddr.sin_family = AF_INET;
	stServerAddr.sin_port = htons(ftpPort);
	stServerAddr.sin_addr.s_addr = inet_addr(ftpHost);
	//inet_pton(AF_INET, "117.79.229.199", &stServerAddr.sin_addr.s_addr);

	/* 创建SOCKET */
	uiFdClientsocket = socket(AF_INET, SOCK_STREAM, 0);

	/* 连接服务器 */
	if (connect(uiFdClientsocket, (SOCKADDR*)&stServerAddr, sizeof(sockaddr_in)) == 0)
	{
		/* 接收服务器端数据 */
		recv(uiFdClientsocket, szRecvbuffer, len, 0);
		//MessageBoxA(NULL, szRecvbuffer, "recv", MB_OK);

		/* 登录 ftp */
		CAtlStringA USER;
		USER.Format("USER %s\r\n", ftpUser);
		send(uiFdClientsocket, USER, USER.GetLength(), 0);

		memset(szRecvbuffer, 0, len);
		recv(uiFdClientsocket, szRecvbuffer, len, 0);
		//MessageBoxA(NULL, szRecvbuffer, "recv", MB_OK);
		if (szRecvbuffer[0] == '3' && szRecvbuffer[1] == '3' && szRecvbuffer[2] == '1')
		{
			CAtlStringA PASS;
			PASS.Format("PASS %s\r\n", ftpPass);
			send(uiFdClientsocket, PASS, PASS.GetLength(), 0);

			memset(szRecvbuffer, 0, len);
			recv(uiFdClientsocket, szRecvbuffer, len, 0);
			//MessageBoxA(NULL, szRecvbuffer, "recv", MB_OK);
			if (szRecvbuffer[0] == '2' && szRecvbuffer[1] == '3' && szRecvbuffer[2] == '0')
			{
				// 进入被动模式
				CAtlStringA PASV = "PASV\r\n";
				send(uiFdClientsocket, PASV, PASV.GetLength(), 0);

				memset(szRecvbuffer, 0, len);
				recv(uiFdClientsocket, szRecvbuffer, len, 0);
				//MessageBoxA(NULL, szRecvbuffer, "recv", MB_OK);
				if (szRecvbuffer[0] == '2' && szRecvbuffer[1] == '2' && szRecvbuffer[2] == '7')
				{
					// 连接数据端口
					sockaddr_in stDataSockAddr;
					PasvToSockAddr(szRecvbuffer, stDataSockAddr);

					SOCKET dataSock = socket(AF_INET, SOCK_STREAM, 0);

					if (connect(dataSock, (SOCKADDR*)&stDataSockAddr, sizeof(stDataSockAddr)) == 0)
					{
						// 设置 ftp 服务端路径
						CAtlStringA CWD;
						CWD.Format("CWD %s\r\n", ftpDir);
						send(uiFdClientsocket, CWD, CWD.GetLength(), 0);

						memset(szRecvbuffer, 0, len);
						recv(uiFdClientsocket, szRecvbuffer, len, 0);
						//MessageBoxA(NULL, szRecvbuffer, "recv", MB_OK);
						if (szRecvbuffer[0] == '2' && szRecvbuffer[1] == '5' && szRecvbuffer[2] == '0')
						{
							// 打开 ftp 服务端文件
							CAtlStringA STOR;
							STOR.Format("STOR %s\r\n", ftpFileName);
							send(uiFdClientsocket, STOR, STOR.GetLength(), 0);

							memset(szRecvbuffer, 0, len);
							recv(uiFdClientsocket, szRecvbuffer, len, 0);
							//MessageBoxA(NULL, szRecvbuffer, "recv", MB_OK);
							if (szRecvbuffer[0] == '1' && szRecvbuffer[1] == '2' && szRecvbuffer[2] == '5')
							{
								// 写文件
								HANDLE hFile = CreateFileA(fileName, GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
								if (hFile != INVALID_HANDLE_VALUE)
								{
									LARGE_INTEGER fileSize;
									LARGE_INTEGER sendSize;
									fileSize.QuadPart = 0;
									sendSize.QuadPart = 0;

									GetFileSizeEx(hFile, &fileSize);

									const DWORD dwLen = 1024 * 10;
									while (1)
									{
										BYTE fileBuf[dwLen] = { 0 };

										DWORD rLen = 0;
										BOOL bRet = ReadFile(hFile, fileBuf, dwLen, &rLen, NULL);

										if (!bRet || sendSize.QuadPart >= fileSize.QuadPart)
										{
											break;
										}

										if (rLen < dwLen)
										{
											if (fileSize.QuadPart - sendSize.QuadPart == rLen)
											{
												send(dataSock, (char*)fileBuf, rLen, 0);
												Sleep(1000);
												break;
											}
										}

										send(dataSock, (char*)fileBuf, dwLen, 0);
										sendSize.QuadPart += dwLen;
										Sleep(10);
										SetFilePointer(hFile, sendSize.LowPart, &sendSize.HighPart, FILE_BEGIN);
									}

									CloseHandle(hFile);
									hFile = NULL;
								}
								else
								{
									ret = -10;
								}
							}
							else
							{
								ret = -9;
							}
						}
						else
						{
							return -8;
						}

						closesocket(dataSock);
					}
					else
					{
						//msg.Format("dataSock 连接失败:%d", WSAGetLastError());
						//MessageBoxA(NULL, msg, "dataSock recv", MB_OK);
						ret = -7;
					}
				}
				else
				{
					ret = -6;
				}
			}
			else
			{
				ret = -5;
			}
		}
		else
		{
			ret = -4;
		}
		closesocket(uiFdClientsocket);
	}
	else
	{
		//msg.Format("uiFdClientsocket 连接失败:%d", WSAGetLastError());
		//MessageBoxA(NULL, msg, "dataSock recv", MB_OK);
		ret = -3;
	}

	WSACleanup();
	return ret;
}
