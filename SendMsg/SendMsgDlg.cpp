
// SendMsgDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "SendMsg.h"
#include "SendMsgDlg.h"
#include "afxdialogex.h"

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


// CSendMsgDlg 对话框



CSendMsgDlg::CSendMsgDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_SENDMSG_DIALOG, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CSendMsgDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT_IP, m_cEditIp);
	DDX_Control(pDX, IDC_EDIT_PORT, m_cEditPort);
	DDX_Control(pDX, IDC_EDIT_MSG, m_cEditMsg);
	DDX_Control(pDX, IDC_STATIC_RET, m_cStaticRet);
}

BEGIN_MESSAGE_MAP(CSendMsgDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON_SEND_TCP, &CSendMsgDlg::OnBnClickedButtonSendTcp)
	ON_BN_CLICKED(IDC_BUTTON_SEND_UDP, &CSendMsgDlg::OnBnClickedButtonSendUdp)
	ON_BN_CLICKED(IDC_BUTTON_SEND_RECV, &CSendMsgDlg::OnBnClickedButtonSendRecv)
END_MESSAGE_MAP()


// CSendMsgDlg 消息处理程序

BOOL CSendMsgDlg::OnInitDialog()
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

void CSendMsgDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CSendMsgDlg::OnPaint()
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
HCURSOR CSendMsgDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}



void CSendMsgDlg::OnBnClickedButtonSendTcp()
{
	CString ip, port, msg;
	m_cEditIp.GetWindowTextW(ip);
	m_cEditPort.GetWindowTextW(port);
	m_cEditMsg.GetWindowTextW(msg);

	CStringA ipa,msga;
	ipa = CW2A(ip);
	msga = CW2A(msg);

	int iPort = _wtoi(port.GetBuffer());

	int ret = m_tcp.SendMessA(ipa.GetBuffer(), iPort, msga.GetBuffer());
	
	CString sRet;
	sRet.Format(_T("发送结果 %d"), ret);
	m_cStaticRet.SetWindowTextW(sRet);
}


void CSendMsgDlg::OnBnClickedButtonSendUdp()
{
	CString ip, port, msg;
	m_cEditIp.GetWindowTextW(ip);
	m_cEditPort.GetWindowTextW(port);
	m_cEditMsg.GetWindowTextW(msg);

	int iPort = _wtoi(port.GetBuffer());

	int ret = m_udp.SendMess(ip, iPort, msg);

	CString sRet;
	sRet.Format(_T("发送结果码 %d"), ret);
	m_cStaticRet.SetWindowTextW(sRet);
}


void CSendMsgDlg::OnBnClickedButtonSendRecv()
{
	CString ip, port, msg;
	m_cEditIp.GetWindowTextW(ip);
	m_cEditPort.GetWindowTextW(port);
	m_cEditMsg.GetWindowTextW(msg);

	CStringA ipa, msga;
	ipa = CW2A(ip);
	msga = CW2A(msg);

	int iPort = _wtoi(port.GetBuffer());

	CStringA ret = m_tcp.SendMessRecvA(ipa.GetBuffer(), iPort, msga.GetBuffer());
	CString retw = CA2W(ret);

	CString sRet;
	sRet.Format(_T("发送结果 %s"), retw.GetBuffer());
	m_cStaticRet.SetWindowTextW(sRet);
}
