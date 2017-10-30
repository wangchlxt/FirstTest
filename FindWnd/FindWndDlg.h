
// FindWndDlg.h : 头文件
//

#pragma once
#include "afxwin.h"


// CFindWndDlg 对话框
class CFindWndDlg : public CDialogEx
{
// 构造
public:
	CFindWndDlg(CWnd* pParent = NULL);	// 标准构造函数

// 对话框数据
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_FINDWND_DIALOG };
#endif

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV 支持


// 实现
protected:
	HICON m_hIcon;

	// 生成的消息映射函数
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	CEdit m_cEditTitle;
	CEdit m_cEditClass;
	CEdit m_cEditMsg;
	CWnd* m_pFindWnd;

	afx_msg void OnBnClickedButtonFind();
	afx_msg void OnBnClickedButtonHide();
	afx_msg void OnBnClickedButtonShow();
	afx_msg void OnBnClickedButtonClose();
	afx_msg void OnBnClickedButtonTopShow();
	afx_msg void OnBnClickedButtonExit();
};
