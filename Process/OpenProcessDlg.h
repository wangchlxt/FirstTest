#pragma once
#include "afxwin.h"


// COpenProcessDlg 对话框

class COpenProcessDlg : public CDialogEx
{
	DECLARE_DYNAMIC(COpenProcessDlg)

public:
	COpenProcessDlg(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~COpenProcessDlg();

// 对话框数据
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_DIALOG_OPEN_PROCESS };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	CEdit m_cEditFile;
	afx_msg void OnBnClickedButtonOpen();
	afx_msg void OnBnClickedButtonOpenProcess2();
};
