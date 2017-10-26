#pragma once
#include <atlstr.h>

enum _LOG_ENCODING
{
	LOG_NO_ENCODING,
	LOG_BASE64_ENCODING
};

// 日志类
class CLog {
public:
	CLog(void);

	// 打开日志文件
	DWORD Open(CAtlString file);

	// 关闭日志文件
	DWORD Close();

	// 设置日志加密方式
	void SetLogEncoding(_LOG_ENCODING encoding);

	// 写日志
	DWORD WriteA(CAtlStringA content);
	DWORD WriteW(CAtlStringW content);

	DWORD FullFileWrite(CAtlStringA file, CAtlStringA content);
	DWORD FullFileWrite(CAtlStringW file, CAtlStringW content);

private:
	// 日志文件句柄
	HANDLE m_hFile;

	// 日志加密方式
	_LOG_ENCODING m_eEncoding;

	HANDLE m_hMutex;
};

