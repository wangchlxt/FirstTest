#pragma once
#include "afxwin.h"


// CGetHtmlDlg �Ի���

class CGetHtmlDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CGetHtmlDlg)

public:
	CGetHtmlDlg(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CGetHtmlDlg();

// �Ի�������
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_DIALOG_GET_HTML };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

	DECLARE_MESSAGE_MAP()
public:
	CEdit m_cEditUrl;
	CEdit m_cEditContent;
	afx_msg void OnBnClickedButtonGet();
};
