#pragma once
#include "afxeditbrowsectrl.h"
#include "afxwin.h"


// CFileCryptoDlg 对话框

class CFileCryptoDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CFileCryptoDlg)

public:
	CFileCryptoDlg(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CFileCryptoDlg();

// 对话框数据
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_DIALOG_FILE_CRYPTO };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedButtonCrypto();
	CMFCEditBrowseCtrl m_cMfcEditBrowseFile;
	CEdit m_cEditPwd;
};
