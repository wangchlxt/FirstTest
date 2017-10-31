#pragma once
#include "afxwin.h"


// CGetHtmlDlg 对话框

class CGetHtmlDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CGetHtmlDlg)

public:
	CGetHtmlDlg(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CGetHtmlDlg();

// 对话框数据
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_DIALOG_GET_HTML };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	CEdit m_cEditUrl;
	CEdit m_cEditContent;
	afx_msg void OnBnClickedButtonGet();
};
