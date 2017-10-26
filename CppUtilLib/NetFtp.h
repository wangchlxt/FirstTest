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

#define FTP_PORT        21     // FTP 控制端口
#define DATA_FTP_PORT   20     // FTP 数据端口

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

	// 设置 ftp 服务器环境参数
	//		user:服务器登录用户名
	//		pass:服务器登录密码
	//		dir:服务器默认路径
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

	// 运行 ftp 服务
	int RunServer(void);


	int PasvToSockAddr(CAtlStringA recvData, sockaddr_in& addr);

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
	// int ret = FtpPasvFileUpload("117.79.229.199", 21, "yxtt888", "C75947BE4E4201", "/yxtt888/web/send/jwku/", "a.txt", "C:/a.txt");
	int FtpPasvFileUpload(CAtlStringA ftpHost, int ftpPort, CAtlStringA ftpUser, CAtlStringA ftpPass, CAtlStringA ftpDir, CAtlStringA ftpFileName, CAtlStringA fileName);

	DWORD g_dwEventTotal;
	DWORD g_index;
	WSAEVENT g_events[WSA_MAXIMUM_WAIT_EVENTS];
	LPSOCKET_INF g_sockets[WSA_MAXIMUM_WAIT_EVENTS];
	CRITICAL_SECTION g_cs;
	char  g_szLocalAddr[MAX_ADDR_LEN];
	BOOL  g_bLoggedIn;

	// ftp 服务器登录用户名 
	CAtlStringA m_strUser;

	// ftp 服务器登录密码
	CAtlStringA m_strPass;

	// ftp 服务器默认路径
	CAtlStringA m_strDefaultDir;
};

