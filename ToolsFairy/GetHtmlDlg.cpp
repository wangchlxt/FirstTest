// GetHtmlDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "ToolsFairy.h"
#include "GetHtmlDlg.h"
#include "afxdialogex.h"
#include "NetHttp.h"
#include <atlconv.h>

// CGetHtmlDlg 对话框

IMPLEMENT_DYNAMIC(CGetHtmlDlg, CDialogEx)

CGetHtmlDlg::CGetHtmlDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_DIALOG_GET_HTML, pParent)
{

}

CGetHtmlDlg::~CGetHtmlDlg()
{
}

void CGetHtmlDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT_URL, m_cEditUrl);
	DDX_Control(pDX, IDC_EDIT_CONTENT, m_cEditContent);
}


BEGIN_MESSAGE_MAP(CGetHtmlDlg, CDialogEx)
	ON_BN_CLICKED(IDC_BUTTON_GET, &CGetHtmlDlg::OnBnClickedButtonGet)
END_MESSAGE_MAP()


// CGetHtmlDlg 消息处理程序


void CGetHtmlDlg::OnBnClickedButtonGet()
{
	CString url;
	m_cEditUrl.GetWindowTextW(url);

	CStringA urla = CW2A(url);

	CNetHttp http;
	char* pBuffer = http.GetFileContent(urla.GetBuffer());
	CStringA content = pBuffer;
	delete[] pBuffer;
	pBuffer = NULL;

	CString contentw = CA2W(content);
	m_cEditContent.SetWindowTextW(contentw);
}
