
// QQLoginDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "QQLogin.h"
#include "QQLoginDlg.h"
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


// CQQLoginDlg 对话框



CQQLoginDlg::CQQLoginDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_QQLOGIN_DIALOG, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_pQQWnd = NULL;
}

void CQQLoginDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_STATIC_MSG, m_cStaticMsg);
}

BEGIN_MESSAGE_MAP(CQQLoginDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON_GET_WND, &CQQLoginDlg::OnBnClickedButtonGetWnd)
	ON_BN_CLICKED(IDC_BUTTON_SHOW, &CQQLoginDlg::OnBnClickedButtonShow)
	ON_BN_CLICKED(IDC_BUTTON_HIDE, &CQQLoginDlg::OnBnClickedButtonHide)
	ON_BN_CLICKED(IDC_BUTTON_LOGIN, &CQQLoginDlg::OnBnClickedButtonLogin)
	ON_BN_CLICKED(IDC_BUTTON_LIB_TEST, &CQQLoginDlg::OnBnClickedButtonLibTest)
END_MESSAGE_MAP()


// CQQLoginDlg 消息处理程序

BOOL CQQLoginDlg::OnInitDialog()
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

void CQQLoginDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CQQLoginDlg::OnPaint()
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
HCURSOR CQQLoginDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

void SendInput(char ch)
{
	LPINPUT key = new INPUT;

	key->type = INPUT_KEYBOARD;
	key->ki.wVk = ch;
	key->ki.time = 0;
	key->ki.wScan = 0;
	key->ki.dwFlags = 0;
	key->ki.dwExtraInfo = KEYEVENTF_KEYUP;
	::SendInput(1, key, sizeof(INPUT));
	::Sleep(1);
	delete key;
	key = NULL;
}


void CQQLoginDlg::OnBnClickedButtonGetWnd()
{
	m_pQQWnd = FindWindow(_T("TXGuiFoundation"), _T("QQ"));

	if (m_pQQWnd == NULL)
	{
		m_cStaticMsg.SetWindowTextW(_T("没有找到窗口"));
		return;
	}
	
	CString strHWnd;
	strHWnd.Format(_T("%X"), m_pQQWnd->m_hWnd);
	m_cStaticMsg.SetWindowTextW(_T("找到窗口：" + strHWnd));

	m_pQQWnd->GetWindowRect(&m_rcQQWnd);
}


void CQQLoginDlg::OnBnClickedButtonShow()
{
	if (m_pQQWnd == NULL)
	{
		m_cStaticMsg.SetWindowTextW(_T("请先查找窗口"));
		return;
	}

	m_pQQWnd->ShowWindow(SW_SHOW);
}


void CQQLoginDlg::OnBnClickedButtonHide()
{
	if (m_pQQWnd == NULL)
	{
		m_cStaticMsg.SetWindowTextW(_T("请先查找窗口"));
		return;
	}

	m_pQQWnd->ShowWindow(SW_HIDE);
}


void CQQLoginDlg::OnBnClickedButtonLogin()
{
	if (m_pQQWnd == NULL)
	{
		OnBnClickedButtonGetWnd();
	}

	if (m_pQQWnd == NULL)
	{
		return;
	}

	m_pQQWnd->ShowWindow(SW_HIDE);
	m_pQQWnd->ShowWindow(SW_SHOW);
	::SetActiveWindow(m_pQQWnd->m_hWnd);

	InputQQNumber();
	InputQQPwd();
	//ClickLogin();
}


void CQQLoginDlg::BackspaceQQNumber()
{
	int x = 300;
	int y = 270;

	Sleep(50);
	SetCursorPos(m_rcQQWnd.left + x, m_rcQQWnd.top + y);
	Sleep(50);
	m_pQQWnd->PostMessageW(WM_LBUTTONDOWN, MK_LBUTTON, MAKELPARAM(x, y));
	Sleep(50);
	m_pQQWnd->PostMessageW(WM_LBUTTONUP, MK_LBUTTON, MAKELPARAM(x, y));

	for (int i = 0;i < 20;i++)
	{
		Sleep(50);
		m_pQQWnd->PostMessageW(WM_KEYDOWN, VK_BACK);
	}
}

void CQQLoginDlg::InputQQNumber()
{
	BackspaceQQNumber();
	BackspaceQQNumber();

	Sleep(50);
	CStringA strQQ = "3339441830";
	for (int i = 0;i < strQQ.GetLength();i++)
	{
		m_pQQWnd->SendMessage(WM_CHAR, (WPARAM)strQQ.GetAt(i));

		if (i == 0)
		{
			Sleep(1000);
		}
		Sleep(50);
	}
}



void CQQLoginDlg::InputQQPwd()
{
	int x = 300;
	int y = 300;

	Sleep(50);
	SetCursorPos(m_rcQQWnd.left + x, m_rcQQWnd.top + y);
	Sleep(50);
	m_pQQWnd->PostMessageW(WM_LBUTTONDOWN, MK_LBUTTON, MAKELPARAM(x, y));
	Sleep(50);
	m_pQQWnd->PostMessageW(WM_LBUTTONUP, MK_LBUTTON, MAKELPARAM(x, y));

	CStringA strPwd = "abcd12345678";
	for (int i = 0;i < strPwd.GetLength();i++)
	{
		Sleep(50);
		SendInput(strPwd.GetAt(i));
	}
}

void CQQLoginDlg::ClickLogin()
{
	int x = 260;
	int y = 365;

	Sleep(50);
	SetCursorPos(m_rcQQWnd.left + x, m_rcQQWnd.top + y);
	Sleep(50);
	m_pQQWnd->PostMessageW(WM_LBUTTONDOWN, MK_LBUTTON, MAKELPARAM(x, y));
	Sleep(50);
	m_pQQWnd->PostMessageW(WM_LBUTTONUP, MK_LBUTTON, MAKELPARAM(x, y));
}


void CQQLoginDlg::OnBnClickedButtonLibTest()
{
	// TODO: 在此添加控件通知处理程序代码
}
