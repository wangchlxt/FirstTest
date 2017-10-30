
// FindWndDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "FindWnd.h"
#include "FindWndDlg.h"
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


// CFindWndDlg 对话框



CFindWndDlg::CFindWndDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_FINDWND_DIALOG, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CFindWndDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT_TITLE, m_cEditTitle);
	DDX_Control(pDX, IDC_EDIT_CLASS, m_cEditClass);
	DDX_Control(pDX, IDC_EDIT_MSG, m_cEditMsg);
}

BEGIN_MESSAGE_MAP(CFindWndDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON_FIND, &CFindWndDlg::OnBnClickedButtonFind)
	ON_BN_CLICKED(IDC_BUTTON_HIDE, &CFindWndDlg::OnBnClickedButtonHide)
	ON_BN_CLICKED(IDC_BUTTON_SHOW, &CFindWndDlg::OnBnClickedButtonShow)
	ON_BN_CLICKED(IDC_BUTTON_CLOSE, &CFindWndDlg::OnBnClickedButtonClose)
	ON_BN_CLICKED(IDC_BUTTON_TOP_SHOW, &CFindWndDlg::OnBnClickedButtonTopShow)
	ON_BN_CLICKED(IDC_BUTTON_EXIT, &CFindWndDlg::OnBnClickedButtonExit)
END_MESSAGE_MAP()


// CFindWndDlg 消息处理程序

BOOL CFindWndDlg::OnInitDialog()
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

void CFindWndDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CFindWndDlg::OnPaint()
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
HCURSOR CFindWndDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}



void CFindWndDlg::OnBnClickedButtonFind()
{
	CString title, cls;
	m_cEditTitle.GetWindowTextW(title);
	m_cEditClass.GetWindowTextW(cls);

	wchar_t* lpszTitle = NULL;
	wchar_t* lpszCls = NULL;

	if (!title.IsEmpty())
	{
		lpszTitle = title.GetBuffer();
	}

	if (!cls.IsEmpty())
	{
		lpszCls = cls.GetBuffer();
	}

	m_pFindWnd = FindWindow(lpszCls, lpszTitle);

	if (m_pFindWnd == NULL)
	{
		m_cEditMsg.SetWindowTextW(_T("not find"));
	}
	else
	{
		CString msg;
		msg.Format(_T("%X"), m_pFindWnd->m_hWnd);
		m_cEditMsg.SetWindowTextW(msg);
	}
}


void CFindWndDlg::OnBnClickedButtonHide()
{
	if (m_pFindWnd == NULL)
	{
		MessageBox(_T("请先查找到窗口"));
		return;
	}

	m_pFindWnd->ShowWindow(SW_HIDE);
}


void CFindWndDlg::OnBnClickedButtonShow()
{
	if (m_pFindWnd == NULL)
	{
		MessageBox(_T("请先查找到窗口"));
		return;
	}

	m_pFindWnd->ShowWindow(SW_SHOWNORMAL);
}


void CFindWndDlg::OnBnClickedButtonClose()
{
	if (m_pFindWnd == NULL)
	{
		MessageBox(_T("请先查找到窗口"));
		return;
	}

	::PostMessage(m_pFindWnd->m_hWnd, WM_CLOSE, 0, 0);
}


void CFindWndDlg::OnBnClickedButtonTopShow()
{
	if (m_pFindWnd == NULL)
	{
		MessageBox(_T("请先查找到窗口"));
		return;
	}

	m_pFindWnd->ShowWindow(SW_SHOWNORMAL);
}


void CFindWndDlg::OnBnClickedButtonExit()
{
	if (m_pFindWnd == NULL)
	{
		MessageBox(_T("请先查找到窗口"));
		return;
	}

	DWORD nProcessID = 0;
	::GetWindowThreadProcessId(m_pFindWnd->m_hWnd, &nProcessID);

	HANDLE hProcessHandle = ::OpenProcess(PROCESS_TERMINATE, FALSE,nProcessID);
	::TerminateProcess(hProcessHandle, 4);
}
