#pragma once
#include "afxwin.h"


// CSetWndDlg �Ի���

class CSetWndDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CSetWndDlg)

public:
	CSetWndDlg(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CSetWndDlg();

// �Ի�������
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_DIALOG_SET_WND };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

	DECLARE_MESSAGE_MAP()
public:
	CWnd* m_pWndFind;
	CEdit m_cEditTitle;
	CEdit m_cEditHeight;
	CEdit m_cEditWidth;
	CEdit m_cEditMsg;
	afx_msg void OnBnClickedButtonFind();
	afx_msg void OnBnClickedButtonSetsize();
};
