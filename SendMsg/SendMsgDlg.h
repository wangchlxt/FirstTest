
// SendMsgDlg.h : ͷ�ļ�
//

#pragma once
#include "afxwin.h"
#include "NetTcp.h"
#include "NetUdp.h"

// CSendMsgDlg �Ի���
class CSendMsgDlg : public CDialogEx
{
// ����
public:
	CSendMsgDlg(CWnd* pParent = NULL);	// ��׼���캯��

// �Ի�������
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_SENDMSG_DIALOG };
#endif

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV ֧��


// ʵ��
protected:
	HICON m_hIcon;

	// ���ɵ���Ϣӳ�亯��
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
