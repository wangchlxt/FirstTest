
// ProcessDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "Process.h"
#include "ProcessDlg.h"
#include "afxdialogex.h"
#include <ctime>

#include <windows.h>
#include <tlhelp32.h>

#include <atlconv.h>
#include <process.h>

#include "OpenProcessDlg.h"

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


// CProcessDlg 对话框



CProcessDlg::CProcessDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_PROCESS_DIALOG, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CProcessDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST_PROCESS, m_cListProcess);
}

BEGIN_MESSAGE_MAP(CProcessDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_TIMER()
	ON_COMMAND(ID_FLASH_PROCESS, &CProcessDlg::OnFlashProcess)
	ON_COMMAND(ID_OPEN_PROCESS, &CProcessDlg::OnOpenProcess)
	ON_COMMAND(ID_MENU_EXIT, &CProcessDlg::OnMenuExit)
END_MESSAGE_MAP()


// CProcessDlg 消息处理程序

BOOL CProcessDlg::OnInitDialog()
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

	InitListCtrl();

	SetTimer(1, 10000, NULL);

	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

void CProcessDlg::InitListCtrl()
{
	// 通过 GetWindowLong 来获取 CListCtrl 已有的样式 
	DWORD dwStyle = GetWindowLong(m_cListProcess.m_hWnd, GWL_STYLE);

	// 在原有样式的基本上，添加 LVS_REPORT 扩展样式 
	SetWindowLong(m_cListProcess.m_hWnd, GWL_STYLE, dwStyle | LVS_REPORT);

	// 获取已有的扩展样式 
	DWORD dwStyles = m_cListProcess.GetExStyle();

	// 取消复选框样式 
	dwStyles &= ~LVS_EX_CHECKBOXES;

	// 添加整行选择和表格线扩展样式 
	m_cListProcess.SetExtendedStyle(dwStyles | LVS_EX_FULLROWSELECT | LVS_EX_GRIDLINES);

	// 设置列
	m_cListProcess.InsertColumn(0, _T("进程ID"), LVCFMT_LEFT, 80);
	m_cListProcess.InsertColumn(1, _T("父进程ID"), LVCFMT_LEFT, 80);
	m_cListProcess.InsertColumn(2, _T("进程名称"), LVCFMT_LEFT, 500);
}

void CProcessDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CProcessDlg::OnPaint()
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
HCURSOR CProcessDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}



int CProcessDlg::FlashProcess()
{
	m_cListProcess.DeleteAllItems();

	HANDLE hsnap = ::CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
	if (INVALID_HANDLE_VALUE == hsnap)
	{
		MessageBox(_T("sorry create snap of the processes failed!"));
		return -1;
	}

	PROCESSENTRY32 pe;
	pe.dwSize = sizeof(PROCESSENTRY32);
	int b = ::Process32First(hsnap, &pe);
	int i = 0;
	const int len = 50;
	while (b)
	{
		wchar_t szTmp[len] = { 0 };
		_itow_s(pe.th32ProcessID, szTmp, 10);
		m_cListProcess.InsertItem(i, szTmp);

		memset(szTmp, 0, len);
		_itow_s(pe.th32ParentProcessID, szTmp, 10);
		m_cListProcess.SetItemText(i, 1, szTmp);

		m_cListProcess.SetItemText(i, 2, pe.szExeFile);

		i++;
		b = ::Process32Next(hsnap, &pe);
	}

	::CloseHandle(hsnap);

	return 0;
}

UINT __stdcall LoopFlashProcessThread(LPVOID lpParam)
{
	CProcessDlg* pThis = (CProcessDlg*)lpParam;

	HANDLE hsnap = ::CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
	if (INVALID_HANDLE_VALUE == hsnap)
	{
		MessageBox(NULL,_T("sorry create snap of the processes failed!"),_T("Error"),MB_OK);
		return -1;
	}

	while (true)
	{
		pThis->m_cListProcess.DeleteAllItems();

		PROCESSENTRY32 pe;
		pe.dwSize = sizeof(PROCESSENTRY32);
		int b = ::Process32First(hsnap, &pe);
		int i = 0;
		const int len = 50;
		while (b)
		{
			wchar_t szTmp[len] = { 0 };
			_itow_s(pe.th32ProcessID, szTmp, 10);
			pThis->m_cListProcess.InsertItem(i, szTmp);

			memset(szTmp, 0, len);
			_itow_s(pe.th32ParentProcessID, szTmp, 10);
			pThis->m_cListProcess.SetItemText(i, 1, szTmp);

			pThis->m_cListProcess.SetItemText(i, 2, pe.szExeFile);

			i++;
			b = ::Process32Next(hsnap, &pe);
		}

		Sleep(1000);
	}

	::CloseHandle(hsnap);

	return 0;
}

int CProcessDlg::LoopFlashProcess()
{
	UINT tid = 0;
	_beginthreadex(NULL, 0, LoopFlashProcessThread, this, 0, &tid);

	return 0;
}

void CProcessDlg::OnTimer(UINT_PTR nIDEvent)
{
	CDialogEx::OnTimer(nIDEvent);
}


void CProcessDlg::OnFlashProcess()
{
	FlashProcess();
}


void CProcessDlg::OnOpenProcess()
{
	COpenProcessDlg wnd;
	wnd.DoModal();
}


void CProcessDlg::OnMenuExit()
{
	exit(1);
}
