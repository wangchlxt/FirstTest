
// TcpServerDlg.h : ͷ�ļ�
//

#pragma once
#include "afxwin.h"
#include "NetTcp.h"
#include "ITcpServer.h"
#include "afxcmn.h"

// CTcpServerDlg �Ի���
class CTcpServerDlg : public CDialogEx,ITcpServer
{
// ����
public:
	CTcpServerDlg(CWnd* pParent = NULL);	// ��׼���캯��

// �Ի�������
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_TCPSERVER_DIALOG };
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
