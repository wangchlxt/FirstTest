
// ToolsFairy.h : PROJECT_NAME Ӧ�ó������ͷ�ļ�
//

#pragma once

#ifndef __AFXWIN_H__
	#error "�ڰ������ļ�֮ǰ������stdafx.h�������� PCH �ļ�"
#endif

#include "resource.h"		// ������


// CToolsFairyApp: 
// �йش����ʵ�֣������ ToolsFairy.cpp
//

class CToolsFairyApp : public CWinApp
{
public:
	CToolsFairyApp();

// ��д
public:
	virtual BOOL InitInstance();

// ʵ��

	DECLARE_MESSAGE_MAP()
};

extern CToolsFairyApp theApp;