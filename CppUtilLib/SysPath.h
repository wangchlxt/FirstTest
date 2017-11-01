#pragma once
#include <atlstr.h>

// 系统路径类
class CSysPath
{
public:
	CSysPath();
	~CSysPath();

	// 获取 windows 目录
	CAtlString GetWinDir();

	// 获取 system32 目录
	CAtlString GetSystem32Dir();

	// 获取当前目录
	CAtlString GetCurrentDir();

	// 获取当前模块名称
	CAtlString GetCurrentModelName();

	// 获取临时目录
	CAtlString GetTempDir();

	// 获取系统临时目录
	CAtlString GetTempPathStr();

	// 在目录下生成临时文件名，返回带路径的文件名
	CAtlString GetTempFileNameStr();
	CAtlString GetTempFileNameStr(CAtlString strPath);
	CAtlString GetTempFileNameStr(CAtlString strPath, CAtlString strPrefix);

	// 磁盘路径转换为物理路径
	BOOL DosPathToNtPath(LPTSTR pszDosPath, LPTSTR pszNtPath);
};

