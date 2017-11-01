
// ProcessDlg.cpp : ʵ���ļ�
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


// ����Ӧ�ó��򡰹��ڡ��˵���� CAboutDlg �Ի���

class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// �Ի�������
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_ABOUTBOX };
#endif

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

// ʵ��
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


// CProcessDlg �Ի���



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


// CProcessDlg ��Ϣ�������

BOOL CProcessDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// ��������...���˵�����ӵ�ϵͳ�˵��С�

	// IDM_ABOUTBOX ������ϵͳ���Χ�ڡ�
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

	// ���ô˶Ի����ͼ�ꡣ  ��Ӧ�ó��������ڲ��ǶԻ���ʱ����ܽ��Զ�
	//  ִ�д˲���
	SetIcon(m_hIcon, TRUE);			// ���ô�ͼ��
	SetIcon(m_hIcon, FALSE);		// ����Сͼ��

	InitListCtrl();

	SetTimer(1, 10000, NULL);

	return TRUE;  // ���ǽ��������õ��ؼ������򷵻� TRUE
}

void CProcessDlg::InitListCtrl()
{
	// ͨ�� GetWindowLong ����ȡ CListCtrl ���е���ʽ 
	DWORD dwStyle = GetWindowLong(m_cListProcess.m_hWnd, GWL_STYLE);

	// ��ԭ����ʽ�Ļ����ϣ���� LVS_REPORT ��չ��ʽ 
	SetWindowLong(m_cListProcess.m_hWnd, GWL_STYLE, dwStyle | LVS_REPORT);

	// ��ȡ���е���չ��ʽ 
	DWORD dwStyles = m_cListProcess.GetExStyle();

	// ȡ����ѡ����ʽ 
	dwStyles &= ~LVS_EX_CHECKBOXES;

	// �������ѡ��ͱ������չ��ʽ 
	m_cListProcess.SetExtendedStyle(dwStyles | LVS_EX_FULLROWSELECT | LVS_EX_GRIDLINES);

	// ������
	m_cListProcess.InsertColumn(0, _T("����ID"), LVCFMT_LEFT, 80);
	m_cListProcess.InsertColumn(1, _T("������ID"), LVCFMT_LEFT, 80);
	m_cListProcess.InsertColumn(2, _T("��������"), LVCFMT_LEFT, 500);
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

// �����Ի��������С����ť������Ҫ����Ĵ���
//  �����Ƹ�ͼ�ꡣ  ����ʹ���ĵ�/��ͼģ�͵� MFC Ӧ�ó���
//  �⽫�ɿ���Զ���ɡ�

void CProcessDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // ���ڻ��Ƶ��豸������

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// ʹͼ���ڹ����������о���
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// ����ͼ��
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

//���û��϶���С������ʱϵͳ���ô˺���ȡ�ù��
//��ʾ��
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
