
// SendMsgDlg.h : 头文件
//

#pragma once
#include "afxwin.h"
#include "NetTcp.h"
#include "NetUdp.h"

// CSendMsgDlg 对话框
class CSendMsgDlg : public CDialogEx
{
// 构造
public:
	CSendMsgDlg(CWnd* pParent = NULL);	// 标准构造函数

// 对话框数据
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_SENDMSG_DIALOG };
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
	CNetTcp m_tcp;
	CNetUdp m_udp;

	CEdit m_cEditIp;
	CEdit m_cEditPort;
	afx_msg void OnBnClickedButtonSendTcp();
	CEdit m_cEditMsg;
	afx_msg void OnBnClickedButtonSendUdp();
	CStatic m_cStaticRet;
};
