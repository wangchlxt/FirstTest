#pragma once
#include "afxwin.h"
#include "afxcmn.h"


// CWebDownDlg 对话框

class CWebDownDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CWebDownDlg)

public:
	CWebDownDlg(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CWebDownDlg();

// 对话框数据
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_DIALOG_WEB_DOWN };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedButtonDown();
	CEdit m_cEditDirUrl;
	virtual BOOL OnInitDialog();
	void InitListCtrl();
	CListCtrl m_cListDown;
	afx_msg void OnBnClickedButtonGetDir();
	afx_msg void OnWebDownOpen();
	CAtlString m_strSavePath;
	CAtlString m_strBookCfgFile;
	CAtlString m_strBookName;
};
