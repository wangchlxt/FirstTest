
// MFCDialogApplicationDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "MFCDialogApplication.h"
#include "MFCDialogApplicationDlg.h"
#include "afxdialogex.h"

#pragma warning(disable:4996)
#include <process.h>
#include <Winsock2.h>
#pragma comment (lib, "ws2_32.lib")

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// 用于应用程序“关于”菜单项的 CAboutDlg 对话框

class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// 对话框数据
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_ABOUTBOX };
#endif

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

// 实现
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialogEx(IDD_ABOUTBOX)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
END_MESSAGE_MAP()


// CMFCDialogApplicationDlg 对话框



CMFCDialogApplicationDlg::CMFCDialogApplicationDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_MFCDIALOGAPPLICATION_DIALOG, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CMFCDialogApplicationDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT_MSG, m_cEditMsg);
	DDX_Control(pDX, IDC_EDIT_PORT, m_cEditPort);
}

BEGIN_MESSAGE_MAP(CMFCDialogApplicationDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON_RUN, &CMFCDialogApplicationDlg::OnBnClickedButtonRun)
END_MESSAGE_MAP()


// CMFCDialogApplicationDlg 消息处理程序

BOOL CMFCDialogApplicationDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// 将“关于...”菜单项添加到系统菜单中。

	// IDM_ABOUTBOX 必须在系统命令范围内。
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// 设置此对话框的图标。  当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	// TODO: 在此添加额外的初始化代码

	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

void CMFCDialogApplicationDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialogEx::OnSysCommand(nID, lParam);
	}
}

// 如果向对话框添加最小化按钮，则需要下面的代码
//  来绘制该图标。  对于使用文档/视图模型的 MFC 应用程序，
//  这将由框架自动完成。

void CMFCDialogApplicationDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // 用于绘制的设备上下文

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// 使图标在工作区矩形中居中
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// 绘制图标
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

//当用户拖动最小化窗口时系统调用此函数取得光标
//显示。
HCURSOR CMFCDialogApplicationDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

UINT _stdcall UdpServerThread(LPVOID lpParam)
{
	CMFCDialogApplicationDlg* pThis = (CMFCDialogApplicationDlg*)lpParam;

	CString strPort;
	pThis->m_cEditPort.GetWindowText(strPort);
	int port = _wtoi(strPort);

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
		return -1;
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

	CString msg,txt;

	while (true)
	{
		printf("waiting client send msg now...\r\n");
		int ret = recvfrom(uiFdSocket, szbuffer, sizeof(szbuffer), 0, (struct  sockaddr*)&stClientAddr, &iAddrlen);
		
		msg.Format(_T("\r\nrecvfrom data len %d"), ret);
		pThis->m_cEditMsg.GetWindowTextW(txt);
		
		txt += msg;
		pThis->m_cEditMsg.SetWindowTextW(txt);

		if (SOCKET_ERROR != ret)
		{
			CStringA data;
			data.Format("\r\nReceived datagram from %s--%s", inet_ntoa(stClientAddr.sin_addr), szbuffer);
			
			txt += data;
			pThis->m_cEditMsg.SetWindowTextW(txt);

			sendto(uiFdSocket, szbuffer, sizeof(szbuffer), 0, (struct sockaddr*)&stClientAddr, iAddrlen);
		}
	}

	closesocket(uiFdSocket);

	return 0;
}

void CMFCDialogApplicationDlg::OnBnClickedButtonRun()
{
	UINT tid = 0;
	_beginthreadex(NULL, 0, UdpServerThread, this, 0, &tid);
}
