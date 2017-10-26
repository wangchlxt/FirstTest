
// QQLoginDlg.h : 头文件
//

#pragma once
#include "afxwin.h"


// CQQLoginDlg 对话框
class CQQLoginDlg : public CDialogEx
{
// 构造
public:
	CQQLoginDlg(CWnd* pParent = NULL);	// 标准构造函数

// 对话框数据
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_QQLOGIN_DIALOG };
#endif

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV 支持


public:
	CWnd* m_pQQWnd;
	RECT m_rcQQWnd;
	CStatic m_cStaticMsg;

private:


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

	// 删除 QQ 号
	void BackspaceQQNumber();

	// 输入 QQ 号
	void InputQQNumber();


	// 输入 QQ 密码
	void InputQQPwd();

	// 点击登陆按钮
	void ClickLogin();

	afx_msg void OnBnClickedButtonGetWnd();
	afx_msg void OnBnClickedButtonShow();
	afx_msg void OnBnClickedButtonHide();
	afx_msg void OnBnClickedButtonLogin();
	
	afx_msg void OnBnClickedButtonLibTest();
};
