
// MFCDialogApplication.h : PROJECT_NAME Ӧ�ó������ͷ�ļ�
//

#pragma once

#ifndef __AFXWIN_H__
	#error "�ڰ������ļ�֮ǰ������stdafx.h�������� PCH �ļ�"
#endif

#include "resource.h"		// ������


// CMFCDialogApplicationApp: 
// �йش����ʵ�֣������ MFCDialogApplication.cpp
//

class CMFCDialogApplicationApp : public CWinApp
{
public:
	CMFCDialogApplicationApp();

// ��д
public:
	virtual BOOL InitInstance();

// ʵ��

	DECLARE_MESSAGE_MAP()
};

extern CMFCDialogApplicationApp theApp;