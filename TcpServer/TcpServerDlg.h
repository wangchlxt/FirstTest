
// TcpServerDlg.h : 头文件
//

#pragma once
#include "afxwin.h"
#include "NetTcp.h"
#include "ITcpServer.h"
#include "afxcmn.h"

// CTcpServerDlg 对话框
class CTcpServerDlg : public CDialogEx,ITcpServer
{
// 构造
public:
	CTcpServerDlg(CWnd* pParent = NULL);	// 标准构造函数

// 对话框数据
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_TCPSERVER_DIALOG };
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

	CAtlStringA TcpServerRecvData(CAtlStringA ip, DWORD dwLen, BYTE* pData);
	afx_msg void OnBnClickedButtonStart();

	CNetTcp m_tcp;
	bool m_bIsRun;
	CEdit m_cEditPort;
	CEdit m_cEditRet;
	CEdit m_cEditMsg;
	CButton m_cBtnStart;
	CRichEditCtrl m_cRichEdit2Msg;
};
