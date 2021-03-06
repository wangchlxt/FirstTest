
// ToolsFairyDlg.h : 头文件
//

#pragma once
#include "afxwin.h"


// CToolsFairyDlg 对话框
class CToolsFairyDlg : public CDialogEx
{
// 构造
public:
	CToolsFairyDlg(CWnd* pParent = NULL);	// 标准构造函数

// 对话框数据
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_TOOLSFAIRY_DIALOG };
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
	afx_msg void OnBnClickedButtonGetHtmlContent();
	CStatic m_cStaticMsg;
	afx_msg void OnBnClickedButtonSetWnd();
	afx_msg void OnBnClickedButtonWebDown();
	afx_msg void OnBnClickedButtonCoding();
	afx_msg void OnBnClickedButtonFileCrypto();
};
