#pragma once
class CFileUtil
{
public:
	CFileUtil();
	~CFileUtil();

	// 检查配置文件是否存在
	BOOL IsExistFile(wchar_t* lpstFileName);

	// 检查配置文件是否存在
	BOOL IsExistFile2(wchar_t* lpstFileName);

	// 将文件重命名为旧文件，文件扩展名改为 .old_[日期字符串]。成功返回 0，失败返回错误码
	DWORD FileToOld(wchar_t* lpstFileName);

	CAtlStringA GetFileTextA(CAtlStringA fileName);
};

