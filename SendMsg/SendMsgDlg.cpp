
// SendMsgDlg.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "SendMsg.h"
#include "SendMsgDlg.h"
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


// CSendMsgDlg �Ի���



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
END_MESSAGE_MAP()


// CSendMsgDlg ��Ϣ�������

BOOL CSendMsgDlg::OnInitDialog()
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

// �����Ի��������С����ť������Ҫ����Ĵ���
//  �����Ƹ�ͼ�ꡣ  ����ʹ���ĵ�/��ͼģ�͵� MFC Ӧ�ó���
//  �⽫�ɿ���Զ���ɡ�

void CSendMsgDlg::OnPaint()
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

	int ret = m_tcp.SendMessA2(ipa.GetBuffer(), iPort, msga.GetBuffer());
	
	CString sRet;
	sRet.Format(_T("���ͽ�� %d"), ret);
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
	sRet.Format(_T("���ͽ���� %d"), ret);
	m_cStaticRet.SetWindowTextW(sRet);
}
