
// ToolsFairyDlg.h : ͷ�ļ�
//

#pragma once
#include "afxwin.h"


// CToolsFairyDlg �Ի���
class CToolsFairyDlg : public CDialogEx
{
// ����
public:
	CToolsFairyDlg(CWnd* pParent = NULL);	// ��׼���캯��

// �Ի�������
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_TOOLSFAIRY_DIALOG };
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
	afx_msg void OnBnClickedButtonGetHtmlContent();
	CStatic m_cStaticMsg;
	afx_msg void OnBnClickedButtonSetWnd();
	afx_msg void OnBnClickedButtonWebDown();
	afx_msg void OnBnClickedButtonCoding();
};
