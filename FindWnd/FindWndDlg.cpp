
// FindWndDlg.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "FindWnd.h"
#include "FindWndDlg.h"
#include "afxdialogex.h"

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


// CFindWndDlg �Ի���



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


// CFindWndDlg ��Ϣ�������

BOOL CFindWndDlg::OnInitDialog()
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

	// TODO: �ڴ���Ӷ���ĳ�ʼ������

	return TRUE;  // ���ǽ��������õ��ؼ������򷵻� TRUE
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

// �����Ի��������С����ť������Ҫ����Ĵ���
//  �����Ƹ�ͼ�ꡣ  ����ʹ���ĵ�/��ͼģ�͵� MFC Ӧ�ó���
//  �⽫�ɿ���Զ���ɡ�

void CFindWndDlg::OnPaint()
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
		MessageBox(_T("���Ȳ��ҵ�����"));
		return;
	}

	m_pFindWnd->ShowWindow(SW_HIDE);
}


void CFindWndDlg::OnBnClickedButtonShow()
{
	if (m_pFindWnd == NULL)
	{
		MessageBox(_T("���Ȳ��ҵ�����"));
		return;
	}

	m_pFindWnd->ShowWindow(SW_SHOWNORMAL);
}


void CFindWndDlg::OnBnClickedButtonClose()
{
	if (m_pFindWnd == NULL)
	{
		MessageBox(_T("���Ȳ��ҵ�����"));
		return;
	}

	::PostMessage(m_pFindWnd->m_hWnd, WM_CLOSE, 0, 0);
}


void CFindWndDlg::OnBnClickedButtonTopShow()
{
	if (m_pFindWnd == NULL)
	{
		MessageBox(_T("���Ȳ��ҵ�����"));
		return;
	}

	m_pFindWnd->ShowWindow(SW_SHOWNORMAL);
}


void CFindWndDlg::OnBnClickedButtonExit()
{
	if (m_pFindWnd == NULL)
	{
		MessageBox(_T("���Ȳ��ҵ�����"));
		return;
	}

	DWORD nProcessID = 0;
	::GetWindowThreadProcessId(m_pFindWnd->m_hWnd, &nProcessID);

	HANDLE hProcessHandle = ::OpenProcess(PROCESS_TERMINATE, FALSE,nProcessID);
	::TerminateProcess(hProcessHandle, 4);
}
