#pragma once
#include <atlstr.h>
#include <vector>
using namespace std;

enum _STRING_OPERATOR_DIRECTION
{
	STRING_OPERATOR_DIRECTION_LEFT,
	STRING_OPERATOR_DIRECTION_RIGHT
};

// 字符串工具类
class CStringUtil
{
public:
	CStringUtil();
	~CStringUtil();

	// 通过 sep 拆分 str 到 vtList
	void SplitA(CAtlStringA str, vector<CAtlStringA>& vtList, CAtlStringA sep);
	void Split(CAtlString str, vector<CAtlString>& vtList, CAtlString sep);

	// 截取字符串,STRING_OPERATOR_DIRECTION_RIGHT 时，sep为字符
	CAtlString Cut(CAtlString str, CAtlString sep, int count, _STRING_OPERATOR_DIRECTION direction);

	// 搜索 sep 在 str 中出现的次数
	int FindSepCount(CAtlString str, CAtlString sep,int startIdx);

	// 获取 ch 的下一个字符
	TCHAR GetNextChar(TCHAR ch);

	// 获取 str 的下一个字符串
	CAtlString GetNextString(CAtlString str);

	CAtlString UTF8ToUnicode(CAtlStringA str);
	CAtlStringA UnicodeToUTF8(CAtlString str);
};

