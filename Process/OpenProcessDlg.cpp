// OpenProcessDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "Process.h"
#include "OpenProcessDlg.h"
#include "afxdialogex.h"


#include <Shellapi.h>
#pragma comment(lib,"Shell32.lib")
// COpenProcessDlg 对话框

IMPLEMENT_DYNAMIC(COpenProcessDlg, CDialogEx)

COpenProcessDlg::COpenProcessDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_DIALOG_OPEN_PROCESS, pParent)
{

}

COpenProcessDlg::~COpenProcessDlg()
{
}

void COpenProcessDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT_FILE, m_cEditFile);
}


BEGIN_MESSAGE_MAP(COpenProcessDlg, CDialogEx)
	ON_BN_CLICKED(IDC_BUTTON_OPEN, &COpenProcessDlg::OnBnClickedButtonOpen)
	ON_BN_CLICKED(IDC_BUTTON_OPEN_PROCESS2, &COpenProcessDlg::OnBnClickedButtonOpenProcess2)
END_MESSAGE_MAP()


// COpenProcessDlg 消息处理程序


void COpenProcessDlg::OnBnClickedButtonOpen()
{
	CString file;
	m_cEditFile.GetWindowTextW(file);

	CStringA filea = CW2A(file);

	ShellExecuteA(NULL, "open", filea.GetBuffer(), NULL, NULL, SW_SHOWNORMAL);

}


void COpenProcessDlg::OnBnClickedButtonOpenProcess2()
{
	CString file;
	m_cEditFile.GetWindowTextW(file);

	CStringA filea = CW2A(file);

	WinExec(filea.GetBuffer(), SW_SHOWNORMAL);
}
