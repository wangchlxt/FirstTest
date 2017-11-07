#pragma once
#include <atlstr.h>
#include <vector>
using namespace std;

enum _STRING_OPERATOR_DIRECTION
{
	STRING_OPERATOR_DIRECTION_LEFT,
	STRING_OPERATOR_DIRECTION_RIGHT
};

// �ַ���������
class CStringUtil
{
public:
	CStringUtil();
	~CStringUtil();

	// ͨ�� sep ��� str �� vtList
	void SplitA(CAtlStringA str, vector<CAtlStringA>& vtList, CAtlStringA sep);
	void Split(CAtlString str, vector<CAtlString>& vtList, CAtlString sep);

	// ��ȡ�ַ���,STRING_OPERATOR_DIRECTION_RIGHT ʱ��sepΪ�ַ�
	CAtlString Cut(CAtlString str, CAtlString sep, int count, _STRING_OPERATOR_DIRECTION direction);

	// ���� sep �� str �г��ֵĴ���
	int FindSepCount(CAtlString str, CAtlString sep,int startIdx);

	// ��ȡ ch ����һ���ַ�
	TCHAR GetNextChar(TCHAR ch);

	// ��ȡ str ����һ���ַ���
	CAtlString GetNextString(CAtlString str);

	CAtlString UTF8ToUnicode(CAtlStringA str);
	CAtlStringA UnicodeToUTF8(CAtlString str);
};

