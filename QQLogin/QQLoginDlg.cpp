
// QQLoginDlg.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "QQLogin.h"
#include "QQLoginDlg.h"
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


// CQQLoginDlg �Ի���



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


// CQQLoginDlg ��Ϣ�������

BOOL CQQLoginDlg::OnInitDialog()
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

// �����Ի��������С����ť������Ҫ����Ĵ���
//  �����Ƹ�ͼ�ꡣ  ����ʹ���ĵ�/��ͼģ�͵� MFC Ӧ�ó���
//  �⽫�ɿ���Զ���ɡ�

void CQQLoginDlg::OnPaint()
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
		m_cStaticMsg.SetWindowTextW(_T("û���ҵ�����"));
		return;
	}
	
	CString strHWnd;
	strHWnd.Format(_T("%X"), m_pQQWnd->m_hWnd);
	m_cStaticMsg.SetWindowTextW(_T("�ҵ����ڣ�" + strHWnd));

	m_pQQWnd->GetWindowRect(&m_rcQQWnd);
}


void CQQLoginDlg::OnBnClickedButtonShow()
{
	if (m_pQQWnd == NULL)
	{
		m_cStaticMsg.SetWindowTextW(_T("���Ȳ��Ҵ���"));
		return;
	}

	m_pQQWnd->ShowWindow(SW_SHOW);
}


void CQQLoginDlg::OnBnClickedButtonHide()
{
	if (m_pQQWnd == NULL)
	{
		m_cStaticMsg.SetWindowTextW(_T("���Ȳ��Ҵ���"));
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
	// TODO: �ڴ���ӿؼ�֪ͨ����������
}
