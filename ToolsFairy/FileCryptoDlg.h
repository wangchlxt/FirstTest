#pragma once
#include "afxeditbrowsectrl.h"
#include "afxwin.h"


// CFileCryptoDlg �Ի���

class CFileCryptoDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CFileCryptoDlg)

public:
	CFileCryptoDlg(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CFileCryptoDlg();

// �Ի�������
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_DIALOG_FILE_CRYPTO };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedButtonCrypto();
	CMFCEditBrowseCtrl m_cMfcEditBrowseFile;
	CEdit m_cEditPwd;
};
