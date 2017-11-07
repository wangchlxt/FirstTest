#pragma once
class CTimeUtil
{
public:
	CTimeUtil();
	~CTimeUtil();

	// 获取当前时间字符串，默认格式 %d%.02d%.02d%.02d%.02d%.02d%.03d
	CAtlStringA GetTimeStringA(CAtlStringA fmt = "%d%.02d%.02d%.02d%.02d%.02d%.03d");

	// 获取当前时间字符串，默认格式 %d%.02d%.02d%.02d%.02d%.02d%.03d
	CAtlString GetTimeString(CAtlString fmt = _T("%d%.02d%.02d%.02d%.02d%.02d%.03d"));
};

