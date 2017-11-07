// SetWndDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "ToolsFairy.h"
#include "SetWndDlg.h"
#include "afxdialogex.h"


// CSetWndDlg 对话框

IMPLEMENT_DYNAMIC(CSetWndDlg, CDialogEx)

CSetWndDlg::CSetWndDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_DIALOG_SET_WND, pParent),m_pWndFind(NULL)
{

}

CSetWndDlg::~CSetWndDlg()
{
}

void CSetWndDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT_TITLE, m_cEditTitle);
	DDX_Control(pDX, IDC_EDIT_HEIGHT, m_cEditHeight);
	DDX_Control(pDX, IDC_EDIT_WIDTH, m_cEditWidth);
	DDX_Control(pDX, IDC_EDIT_MSG, m_cEditMsg);
}


BEGIN_MESSAGE_MAP(CSetWndDlg, CDialogEx)
	ON_BN_CLICKED(IDC_BUTTON_FIND, &CSetWndDlg::OnBnClickedButtonFind)
	ON_BN_CLICKED(IDC_BUTTON_SETSIZE, &CSetWndDlg::OnBnClickedButtonSetsize)
END_MESSAGE_MAP()


// CSetWndDlg 消息处理程序


void CSetWndDlg::OnBnClickedButtonFind()
{
	CString title;
	m_cEditTitle.GetWindowTextW(title);

	m_pWndFind = FindWindow(NULL, title);
	if (m_pWndFind != NULL)
	{
		CString msg;
		msg.Format(_T("找到窗口 %X"), m_pWndFind->m_hWnd);
		m_cEditMsg.SetWindowTextW(msg);

		RECT rc;
		m_pWndFind->GetWindowRect(&rc);
		
		CString str;
		str.Format(_T("%d"), rc.bottom - rc.top);
		m_cEditHeight.SetWindowTextW(str);

		str.Format(_T("%d"), rc.right - rc.left);
		m_cEditWidth.SetWindowTextW(str);
	}
}


void CSetWndDlg::OnBnClickedButtonSetsize()
{
	if (m_pWndFind == NULL)
	{
		MessageBox(_T("请先查找窗口"));
		return;
	}

	CString strH, strW;
	m_cEditHeight.GetWindowTextW(strH);
	m_cEditWidth.GetWindowTextW(strW);

	int height = _wtoi(strH);
	int width = _wtoi(strW);

	::SetWindowPos(m_pWndFind->m_hWnd, HWND_TOP, 0, 0, width, height, SWP_NOMOVE | SWP_SHOWWINDOW);
}
