
// FindWndDlg.h : ͷ�ļ�
//

#pragma once
#include "afxwin.h"


// CFindWndDlg �Ի���
class CFindWndDlg : public CDialogEx
{
// ����
public:
	CFindWndDlg(CWnd* pParent = NULL);	// ��׼���캯��

// �Ի�������
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_FINDWND_DIALOG };
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
