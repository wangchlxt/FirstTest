#pragma once
#include "afxwin.h"


// COpenProcessDlg �Ի���

class COpenProcessDlg : public CDialogEx
{
	DECLARE_DYNAMIC(COpenProcessDlg)

public:
	COpenProcessDlg(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~COpenProcessDlg();

// �Ի�������
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_DIALOG_OPEN_PROCESS };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

	DECLARE_MESSAGE_MAP()
public:
	CEdit m_cEditFile;
	afx_msg void OnBnClickedButtonOpen();
	afx_msg void OnBnClickedButtonOpenProcess2();
};
