// CodingDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "ToolsFairy.h"
#include "CodingDlg.h"
#include "afxdialogex.h"
#include "AESEncryptor.h"

using namespace std;

// CCodingDlg 对话框

IMPLEMENT_DYNAMIC(CCodingDlg, CDialogEx)

CCodingDlg::CCodingDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_DIALOG_CODING, pParent)
{

}

CCodingDlg::~CCodingDlg()
{
}

void CCodingDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT_PWD, m_cEditPwd);
	DDX_Control(pDX, IDC_EDIT_TEXT_IN, m_cEditTextIn);
	DDX_Control(pDX, IDC_EDIT_TEXT_OUT, m_cEditTextOut);
	DDX_Control(pDX, IDC_MFCEDITBROWSE_FILE, m_cMfcEditBrowserFile);
}


BEGIN_MESSAGE_MAP(CCodingDlg, CDialogEx)
	ON_BN_CLICKED(IDC_BUTTON_PROCESS_CODE_TEXT, &CCodingDlg::OnBnClickedButtonProcessCodeText)
//	ON_BN_CLICKED(IDC_BUTTON_, &CCodingDlg::OnBnClickedButton)
ON_BN_CLICKED(IDC_BUTTON_PROCESS_CODE_FILE, &CCodingDlg::OnBnClickedButtonProcessCodeFile)
ON_BN_CLICKED(IDC_BUTTON_ENCODE_TEXT, &CCodingDlg::OnBnClickedButtonEncodeText)
ON_BN_CLICKED(IDC_BUTTON_ENCODE_FILE, &CCodingDlg::OnBnClickedButtonEncodeFile)
END_MESSAGE_MAP()


// CCodingDlg 消息处理程序


void CCodingDlg::OnBnClickedButtonProcessCodeText()
{
	CString pwd;
	m_cEditPwd.GetWindowTextW(pwd);
	string pwda = CW2A(pwd);
	CAESEncryptor aes((unsigned char*)pwda.c_str());

	CString txt;
	m_cEditTextOut.GetWindowTextW(txt);
	CAtlStringA txta = CW2A(txt);

	std::string out = aes.DecryptString(txta.GetBuffer());
	CString outTxt = CA2W(out.c_str());

	m_cEditTextIn.SetWindowTextW(outTxt);
}


//void CCodingDlg::OnBnClickedButton()
//{
//	// TODO: 在此添加控件通知处理程序代码
//}


void CCodingDlg::OnBnClickedButtonProcessCodeFile()
{
	CString file;
	m_cMfcEditBrowserFile.GetWindowTextW(file);
	if (file.IsEmpty())
	{
		MessageBox(_T("请选择文件"));
		return;
	}

	if (file.Find(_T(".mi")) < 0)
	{
		MessageBox(_T("选择的文件格式不正确"));
		return;
	}

	CString pwd;
	m_cEditPwd.GetWindowTextW(pwd);
	CAtlStringA pwda = CW2A(pwd);

	CAtlStringA fileOld = CW2A(file);
	CAtlStringA fileNew = fileOld;
	fileNew.Replace(".mi", "");

	CAESEncryptor aes((unsigned char*)pwda.GetBuffer());
	aes.EncryptTxtFile(fileOld, fileNew);
}


void CCodingDlg::OnBnClickedButtonEncodeText()
{
	CString pwd;
	m_cEditPwd.GetWindowTextW(pwd);
	string pwda = CW2A(pwd);
	CAESEncryptor aes((unsigned char*)pwda.c_str());

	CString txt;
	m_cEditTextIn.GetWindowTextW(txt);
	CAtlStringA txta = CW2A(txt);

	
	std::string out = aes.EncryptString(txta.GetBuffer());
	CString outTxt = CA2W(out.c_str());

	m_cEditTextOut.SetWindowTextW(outTxt);
}


void CCodingDlg::OnBnClickedButtonEncodeFile()
{
	CString file;
	m_cMfcEditBrowserFile.GetWindowTextW(file);
	if (file.IsEmpty())
	{
		MessageBox(_T("请选择文件"));
		return;
	}

	CString pwd;
	m_cEditPwd.GetWindowTextW(pwd);
	CAtlStringA pwda = CW2A(pwd);

	CAtlStringA filea = CW2A(file);

	CAESEncryptor aes((unsigned char*)pwda.GetBuffer());
	aes.EncryptTxtFile(filea, filea + ".mi");
}
