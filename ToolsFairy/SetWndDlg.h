#pragma once
#include "afxwin.h"


// CSetWndDlg 对话框

class CSetWndDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CSetWndDlg)

public:
	CSetWndDlg(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CSetWndDlg();

// 对话框数据
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_DIALOG_SET_WND };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	CWnd* m_pWndFind;
	CEdit m_cEditTitle;
	CEdit m_cEditHeight;
	CEdit m_cEditWidth;
	CEdit m_cEditMsg;
	afx_msg void OnBnClickedButtonFind();
	afx_msg void OnBnClickedButtonSetsize();
};
