#pragma once

#include "NetSocket.h"
#include <stdio.h>
#include <ws2tcpip.h>
#include <stdlib.h>

#define WSA_RECV         0
#define WSA_SEND         1

#define DATA_BUFSIZE    8192
#define MAX_NAME_LEN    128
#define MAX_PWD_LEN     128
#define MAX_RESP_LEN    1024
#define MAX_REQ_LEN     256
#define MAX_ADDR_LEN    80

#define FTP_PORT        21     // FTP ���ƶ˿�
#define DATA_FTP_PORT   20     // FTP ���ݶ˿�

#define USER_OK         331
#define LOGGED_IN       230
#define LOGIN_FAILED    530
#define CMD_OK          200
#define OPENING_AMODE   150
#define TRANS_COMPLETE  226
#define CANNOT_FIND     550
#define FTP_QUIT        221
#define CURR_DIR        257
#define DIR_CHANGED     250
#define OS_TYPE         215
#define REPLY_MARKER    504
#define PASSIVE_MODE    227

#define MAX_FILE_NUM        1024

#define MODE_PORT       0
#define MODE_PASV       1

#define PORT_BIND   1821

typedef struct {
	CHAR   buffRecv[DATA_BUFSIZE];
	CHAR   buffSend[DATA_BUFSIZE];
	WSABUF wsaBuf;
	SOCKET s;
	WSAOVERLAPPED o;
	DWORD dwBytesSend;
	DWORD dwBytesRecv;
	int   nStatus;
} SOCKET_INF, *LPSOCKET_INF;

typedef struct {
	CHAR    szFileName[MAX_PATH];
	DWORD    dwFileAttributes;
	FILETIME ftCreationTime;
	FILETIME ftLastAccessTime;
	FILETIME ftLastWriteTime;
	DWORD    nFileSizeHigh;
	DWORD    nFileSizeLow;
} FILE_INF, *LPFILE_INF;

DWORD WINAPI ProcessTreadIO(LPVOID lpParameter);

class CNetFtp : public CNetSocket
{
public:
	CNetFtp();
	~CNetFtp();

	// ���� ftp ��������������
	//		user:��������¼�û���
	//		pass:��������¼����
	//		dir:������Ĭ��·��
	void Init(CAtlString user, CAtlString pass, CAtlString dir);

	int SendRes(LPSOCKET_INF pSI);
	int RecvReq(LPSOCKET_INF pSI);
	BOOL WelcomeInfo(SOCKET s);
	int LoginIn(LPSOCKET_INF pSocketInfo);
	char* ConvertCommaAddress(char* szAddress, WORD wPort);
	int ConvertDotAddress(char* szAddress, LPDWORD pdwIpAddr, LPWORD pwPort);
	UINT FileListToString(char* buff, UINT nBuffSize, BOOL bDetails);
	DWORD ReadFileToBuffer(const char* szFile, char *buff, DWORD nFileSize);
	DWORD WriteToFile(SOCKET s, const char* szFile);
	int CombindFileNameSize(const char* szFileName, char* szFileNS);
	int	DataConn(SOCKET& s, DWORD dwIp, WORD wPort, int nMode);
	int DataSend(SOCKET s, char* buff, int nBufSize);
	int DataRecv(SOCKET s, const char* szFileName);
	SOCKET DataAccept(SOCKET& s);
	int DealCommand(LPSOCKET_INF pSI);
	char* GetLocalAddress();
	int GetFileList(LPFILE_INF pFI, UINT nArraySize, const char* szPath);
	char* HostToNet(char* szPath);
	char* NetToHost(char* szPath);
	char* RelativeDirectory(char* szDir);
	char* AbsoluteDirectory(char* szDir);

	// ���� ftp ����
	int RunServer(void);


	int PasvToSockAddr(CAtlStringA recvData, sockaddr_in& addr);

	// ftp ����ģʽ�ļ��ϴ�
	//		return: 0 ����
	//			-1 ��ʼ�� socket ����ʧ��
	//			-2 socket ��汾����
	//			-3 ���� ftp ������ʧ��
	//			-4 ftp �û�������
	//			-5 ftp �������
	//			-6 ���ñ���ģʽʧ��
	//			-7 ���ӱ���ģʽ���ݶ˿�ʧ��
	//			-8 ���� ftp �����·��ʧ��
	//			-9 �� ftp ������ļ�ʧ�� 
	//			-10 ��Ҫ�ϴ����ļ�ʧ��
	// int ret = FtpPasvFileUpload("117.79.229.199", 21, "yxtt888", "C75947BE4E4201", "/yxtt888/web/send/jwku/", "a.txt", "C:/a.txt");
	int FtpPasvFileUpload(CAtlStringA ftpHost, int ftpPort, CAtlStringA ftpUser, CAtlStringA ftpPass, CAtlStringA ftpDir, CAtlStringA ftpFileName, CAtlStringA fileName);

	DWORD g_dwEventTotal;
	DWORD g_index;
	WSAEVENT g_events[WSA_MAXIMUM_WAIT_EVENTS];
	LPSOCKET_INF g_sockets[WSA_MAXIMUM_WAIT_EVENTS];
	CRITICAL_SECTION g_cs;
	char  g_szLocalAddr[MAX_ADDR_LEN];
	BOOL  g_bLoggedIn;

	// ftp ��������¼�û��� 
	CAtlStringA m_strUser;

	// ftp ��������¼����
	CAtlStringA m_strPass;

	// ftp ������Ĭ��·��
	CAtlStringA m_strDefaultDir;
};

