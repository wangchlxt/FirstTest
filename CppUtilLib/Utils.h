#pragma once

#include <atlstr.h>
#include <vector>
#include <string>
using namespace std;

// 工具类
class CUtils
{
public:
	CUtils(void);

	// 获取文件 MD5
	CAtlString GetFileMD5(CAtlString fileName);

	// 字符串 md5 加密
	CAtlStringA StrEncodeMD5A(CAtlStringA str);
	CAtlString StrEncodeMD5W(CAtlString str);
	
	// 删除自身进程文件
	BOOL DelSelfProcFile();
};
