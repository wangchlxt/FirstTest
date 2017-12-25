#pragma once
#include "afxwin.h"
#include "afxeditbrowsectrl.h"


// CCodingDlg 对话框

class CCodingDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CCodingDlg)

public:
	CCodingDlg(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CCodingDlg();

// 对话框数据
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_DIALOG_CODING };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedButtonProcessCodeText();
//	afx_msg void OnBnClickedButton();
	afx_msg void OnBnClickedButtonProcessCodeFile();
	CEdit m_cEditPwd;
	CEdit m_cEditTextIn;
	CEdit m_cEditTextOut;
	CMFCEditBrowseCtrl m_cMfcEditBrowserFile;
	afx_msg void OnBnClickedButtonEncodeText();
	afx_msg void OnBnClickedButtonEncodeFile();
};
