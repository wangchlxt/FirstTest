
// ProcessDlg.h : ͷ�ļ�
//

#pragma once
#include "afxcmn.h"


// CProcessDlg �Ի���
class CProcessDlg : public CDialogEx
{
// ����
public:
	CProcessDlg(CWnd* pParent = NULL);	// ��׼���캯��

// �Ի�������
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_PROCESS_DIALOG };
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
	CListCtrl m_cListProcess;

	int FlashProcess();
	int LoopFlashProcess();

	afx_msg void OnTimer(UINT_PTR nIDEvent);
	afx_msg void OnFlashProcess();
	afx_msg void OnOpenProcess();
};
