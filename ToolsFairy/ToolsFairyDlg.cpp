
// ToolsFairyDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "ToolsFairy.h"
#include "ToolsFairyDlg.h"
#include "afxdialogex.h"
#include "NetHttp.h"
#include "AppSet.h"
#include "SysPath.h"

#include "GetHtmlDlg.h"
#include "SetWndDlg.h"
#include "WebDownDlg.h"
#include "CodingDlg.h"
#include "FileCryptoDlg.h"

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


// CToolsFairyDlg 对话框



CToolsFairyDlg::CToolsFairyDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_TOOLSFAIRY_DIALOG, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CToolsFairyDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_STATIC_MSG, m_cStaticMsg);
}

BEGIN_MESSAGE_MAP(CToolsFairyDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON_GET_HTML_CONTENT, &CToolsFairyDlg::OnBnClickedButtonGetHtmlContent)
	ON_BN_CLICKED(IDC_BUTTON_SET_WND, &CToolsFairyDlg::OnBnClickedButtonSetWnd)
	ON_BN_CLICKED(IDC_BUTTON_WEB_DOWN, &CToolsFairyDlg::OnBnClickedButtonWebDown)
	ON_BN_CLICKED(IDC_BUTTON_CODING, &CToolsFairyDlg::OnBnClickedButtonCoding)
	ON_BN_CLICKED(IDC_BUTTON_FILE_CRYPTO, &CToolsFairyDlg::OnBnClickedButtonFileCrypto)
END_MESSAGE_MAP()


// CToolsFairyDlg 消息处理程序

BOOL CToolsFairyDlg::OnInitDialog()
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

	CNetHttp netHttp;
	CAtlStringA ip = netHttp.GetHostNetIp();
	CAppSet::netIp = CA2W(ip);
	CAppSet::hostIp = netHttp.GetLocalIp();
	
	CAtlString msg;
	msg.Format(_T("外网 ip：%s  局域网 ip：%s"), CAppSet::netIp, CAppSet::hostIp);
	m_cStaticMsg.SetWindowTextW(msg);

	CSysPath sysPath;
	CAppSet::currentPath = sysPath.GetCurrentDir();

	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

void CToolsFairyDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CToolsFairyDlg::OnPaint()
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
HCURSOR CToolsFairyDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}



void CToolsFairyDlg::OnBnClickedButtonGetHtmlContent()
{
	CGetHtmlDlg* pWnd = new CGetHtmlDlg();
	pWnd->Create(IDD_DIALOG_GET_HTML);
	pWnd->ShowWindow(SW_SHOWNORMAL);
}


void CToolsFairyDlg::OnBnClickedButtonSetWnd()
{
	CSetWndDlg* pWnd = new CSetWndDlg();
	pWnd->Create(IDD_DIALOG_SET_WND);
	pWnd->ShowWindow(SW_SHOWNORMAL);
	
}


void CToolsFairyDlg::OnBnClickedButtonWebDown()
{
	CWebDownDlg* pWnd = new CWebDownDlg();
	pWnd->Create(IDD_DIALOG_WEB_DOWN);
	pWnd->ShowWindow(SW_SHOWNORMAL);
}


void CToolsFairyDlg::OnBnClickedButtonCoding()
{
	CCodingDlg* pWnd = new CCodingDlg();
	pWnd->Create(IDD_DIALOG_CODING);
	pWnd->ShowWindow(SW_SHOWNORMAL);
}


void CToolsFairyDlg::OnBnClickedButtonFileCrypto()
{
	CFileCryptoDlg* pWnd = new CFileCryptoDlg();
	pWnd->Create(IDD_DIALOG_FILE_CRYPTO);
	pWnd->ShowWindow(SW_SHOWNORMAL);
}
