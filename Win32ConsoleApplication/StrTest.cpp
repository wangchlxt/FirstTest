#include "stdafx.h"
#include "StrTest.h"


CStrTest::CStrTest()
{
}


CStrTest::~CStrTest()
{
}

void CStrTest::Test()
{
	CAtlStringA html = "<li><a href=\"/71/71063/22931410.html\">第九十三章 再临</a></li>";
	int idx = html.Find("\">");
	CAtlStringA url = html.Left(idx);
	url = url.Right(url.GetLength() - url.ReverseFind('\"') -1);

	CAtlStringA title = html.Right(html.GetLength() - idx -2);
	title = title.Left(title.Find("<"));

	int i = 0;
}
