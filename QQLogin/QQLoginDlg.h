
// QQLoginDlg.h : ͷ�ļ�
//

#pragma once
#include "afxwin.h"


// CQQLoginDlg �Ի���
class CQQLoginDlg : public CDialogEx
{
// ����
public:
	CQQLoginDlg(CWnd* pParent = NULL);	// ��׼���캯��

// �Ի�������
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_QQLOGIN_DIALOG };
#endif

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV ֧��


public:
	CWnd* m_pQQWnd;
	RECT m_rcQQWnd;
	CStatic m_cStaticMsg;

private:


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

	// ɾ�� QQ ��
	void BackspaceQQNumber();

	// ���� QQ ��
	void InputQQNumber();


	// ���� QQ ����
	void InputQQPwd();

	// �����½��ť
	void ClickLogin();

	afx_msg void OnBnClickedButtonGetWnd();
	afx_msg void OnBnClickedButtonShow();
	afx_msg void OnBnClickedButtonHide();
	afx_msg void OnBnClickedButtonLogin();
	
	afx_msg void OnBnClickedButtonLibTest();
};
