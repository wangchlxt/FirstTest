#pragma once
#include "afxwin.h"
#include "afxcmn.h"


// CWebDownDlg �Ի���

class CWebDownDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CWebDownDlg)

public:
	CWebDownDlg(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CWebDownDlg();

// �Ի�������
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_DIALOG_WEB_DOWN };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

	DECLARE_MESSAGE_MAP()

public:
	afx_msg void OnBnClickedButtonDown();
	afx_msg void OnBnClickedButtonGetDir();
	afx_msg void OnWebDownOpen();

	virtual BOOL OnInitDialog();
	void InitListCtrl();
	void PushErrorDown(int row);

public:
	CEdit m_cEditDirUrl;
	CEdit m_cEditMsg;
	CListCtrl m_cListDown;

	CAtlString m_strSavePath;
	CAtlString m_strBookCfgFile;
	CAtlString m_strBookName;

};
