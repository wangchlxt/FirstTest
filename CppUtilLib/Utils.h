#pragma once

#include <atlstr.h>
#include <vector>
#include <string>
using namespace std;

// ������
class CUtils
{
public:
	CUtils(void);

	// ��ȡ�ļ� MD5
	CAtlString GetFileMD5(CAtlString fileName);

	// �ַ��� md5 ����
	CAtlStringA StrEncodeMD5A(CAtlStringA str);
	CAtlString StrEncodeMD5W(CAtlString str);
	
	// ɾ����������ļ�
	BOOL DelSelfProcFile();
};
