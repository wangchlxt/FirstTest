
// ToolsFairyDlg.cpp : ʵ���ļ�
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


// CToolsFairyDlg �Ի���



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


// CToolsFairyDlg ��Ϣ�������

BOOL CToolsFairyDlg::OnInitDialog()
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

	CNetHttp netHttp;
	CAtlStringA ip = netHttp.GetHostNetIp();
	CAppSet::netIp = CA2W(ip);
	CAppSet::hostIp = netHttp.GetLocalIp();
	
	CAtlString msg;
	msg.Format(_T("���� ip��%s  ������ ip��%s"), CAppSet::netIp, CAppSet::hostIp);
	m_cStaticMsg.SetWindowTextW(msg);

	CSysPath sysPath;
	CAppSet::currentPath = sysPath.GetCurrentDir();

	return TRUE;  // ���ǽ��������õ��ؼ������򷵻� TRUE
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

// �����Ի��������С����ť������Ҫ����Ĵ���
//  �����Ƹ�ͼ�ꡣ  ����ʹ���ĵ�/��ͼģ�͵� MFC Ӧ�ó���
//  �⽫�ɿ���Զ���ɡ�

void CToolsFairyDlg::OnPaint()
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
