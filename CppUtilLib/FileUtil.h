#pragma once
class CFileUtil
{
public:
	CFileUtil();
	~CFileUtil();

	// ��������ļ��Ƿ����
	BOOL IsExistFile(wchar_t* lpstFileName);

	// ��������ļ��Ƿ����
	BOOL IsExistFile2(wchar_t* lpstFileName);

	// ���ļ�������Ϊ���ļ����ļ���չ����Ϊ .old_[�����ַ���]���ɹ����� 0��ʧ�ܷ��ش�����
	DWORD FileToOld(wchar_t* lpstFileName);

	CAtlStringA GetFileTextA(CAtlStringA fileName);
};

