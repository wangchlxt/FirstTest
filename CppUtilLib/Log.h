#pragma once
#include <atlstr.h>

enum _LOG_ENCODING
{
	LOG_NO_ENCODING,
	LOG_BASE64_ENCODING
};

// ��־��
class CLog {
public:
	CLog(void);

	// ����־�ļ�
	DWORD Open(CAtlString file);

	// �ر���־�ļ�
	DWORD Close();

	// ������־���ܷ�ʽ
	void SetLogEncoding(_LOG_ENCODING encoding);

	// д��־
	DWORD WriteA(CAtlStringA content);
	DWORD WriteW(CAtlStringW content);

	DWORD FullFileWrite(CAtlStringA file, CAtlStringA content);
	DWORD FullFileWrite(CAtlStringW file, CAtlStringW content);

private:
	// ��־�ļ����
	HANDLE m_hFile;

	// ��־���ܷ�ʽ
	_LOG_ENCODING m_eEncoding;

	HANDLE m_hMutex;
};

