#pragma once
class CTimeUtil
{
public:
	CTimeUtil();
	~CTimeUtil();

	// ��ȡ��ǰʱ���ַ�����Ĭ�ϸ�ʽ %d%.02d%.02d%.02d%.02d%.02d%.03d
	CAtlStringA GetTimeStringA(CAtlStringA fmt = "%d%.02d%.02d%.02d%.02d%.02d%.03d");

	// ��ȡ��ǰʱ���ַ�����Ĭ�ϸ�ʽ %d%.02d%.02d%.02d%.02d%.02d%.03d
	CAtlString GetTimeString(CAtlString fmt = _T("%d%.02d%.02d%.02d%.02d%.02d%.03d"));
};

