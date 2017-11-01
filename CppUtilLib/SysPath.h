#pragma once
#include <atlstr.h>

// ϵͳ·����
class CSysPath
{
public:
	CSysPath();
	~CSysPath();

	// ��ȡ windows Ŀ¼
	CAtlString GetWinDir();

	// ��ȡ system32 Ŀ¼
	CAtlString GetSystem32Dir();

	// ��ȡ��ǰĿ¼
	CAtlString GetCurrentDir();

	// ��ȡ��ǰģ������
	CAtlString GetCurrentModelName();

	// ��ȡ��ʱĿ¼
	CAtlString GetTempDir();

	// ��ȡϵͳ��ʱĿ¼
	CAtlString GetTempPathStr();

	// ��Ŀ¼��������ʱ�ļ��������ش�·�����ļ���
	CAtlString GetTempFileNameStr();
	CAtlString GetTempFileNameStr(CAtlString strPath);
	CAtlString GetTempFileNameStr(CAtlString strPath, CAtlString strPrefix);

	// ����·��ת��Ϊ����·��
	BOOL DosPathToNtPath(LPTSTR pszDosPath, LPTSTR pszNtPath);
};

