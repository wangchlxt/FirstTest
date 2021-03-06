
// ProcessDlg.h : 头文件
//

#pragma once
#include "afxcmn.h"


// CProcessDlg 对话框
class CProcessDlg : public CDialogEx
{
// 构造
public:
	CProcessDlg(CWnd* pParent = NULL);	// 标准构造函数

// 对话框数据
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_PROCESS_DIALOG };
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

private:
	// 初始化 list ctrl
	void InitListCtrl();

public:
	CListCtrl m_cListProcess;

	int FlashProcess();
	int LoopFlashProcess();

	afx_msg void OnTimer(UINT_PTR nIDEvent);
	afx_msg void OnFlashProcess();
	afx_msg void OnOpenProcess();
	afx_msg void OnMenuExit();
};
