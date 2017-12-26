// FileCryptoDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "ToolsFairy.h"
#include "FileCryptoDlg.h"
#include "afxdialogex.h"
#include "FileCrypto.h"

// CFileCryptoDlg 对话框

IMPLEMENT_DYNAMIC(CFileCryptoDlg, CDialogEx)

CFileCryptoDlg::CFileCryptoDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_DIALOG_FILE_CRYPTO, pParent)
{

}

CFileCryptoDlg::~CFileCryptoDlg()
{
}

void CFileCryptoDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_MFCEDITBROWSE_FILE, m_cMfcEditBrowseFile);
	DDX_Control(pDX, IDC_EDIT_PWD, m_cEditPwd);
}


BEGIN_MESSAGE_MAP(CFileCryptoDlg, CDialogEx)
	ON_BN_CLICKED(IDC_BUTTON_CRYPTO, &CFileCryptoDlg::OnBnClickedButtonCrypto)
END_MESSAGE_MAP()


// CFileCryptoDlg 消息处理程序


void CFileCryptoDlg::OnBnClickedButtonCrypto()
{
	CString file;
	m_cMfcEditBrowseFile.GetWindowTextW(file);
	if (file.IsEmpty())
	{
		MessageBox(_T("请选择要加密解密的文件"));
		return;
	}

	CString pwd;
	m_cEditPwd.GetWindowTextW(pwd);
	
	CString targetFile = file;
	targetFile.Insert(targetFile.ReverseFind('.'), _T("_new"));

	string oldFile = CW2A(file);
	string newFile = CW2A(targetFile);

	CFileCrypto fileCrypto;
	fileCrypto.m_strPassword = CW2A(pwd);
	if(fileCrypto.DesFile(oldFile, newFile))
	{
		MessageBox(_T("文件加密解密完成"));
	}
	else
	{
		MessageBox(_T("文件加密解密失败"));
	}
}
