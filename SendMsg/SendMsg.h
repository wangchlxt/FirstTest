
// SendMsg.h : PROJECT_NAME Ӧ�ó������ͷ�ļ�
//

#pragma once

#ifndef __AFXWIN_H__
	#error "�ڰ������ļ�֮ǰ������stdafx.h�������� PCH �ļ�"
#endif

#include "resource.h"		// ������


// CSendMsgApp: 
// �йش����ʵ�֣������ SendMsg.cpp
//

class CSendMsgApp : public CWinApp
{
public:
	CSendMsgApp();

// ��д
public:
	virtual BOOL InitInstance();

// ʵ��

	DECLARE_MESSAGE_MAP()
};

extern CSendMsgApp theApp;