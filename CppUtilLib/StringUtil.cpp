#include "stdafx.h"
#include "StringUtil.h"


CStringUtil::CStringUtil()
{
}


CStringUtil::~CStringUtil()
{
}

void CStringUtil::SplitA(CAtlStringA str, vector<CAtlStringA>& vtList, CAtlStringA sep)
{
	while (1)
	{
		int idx = str.Find(sep, 0);
		if (idx > 0)
		{
			vtList.push_back(str.Left(idx));
			str = str.Right(str.GetLength() - idx - sep.GetLength());
		}
		else
		{
			if (!str.IsEmpty())
			{
				vtList.push_back(str);
			}
			break;
		}
	}
}

void CStringUtil::Split(CAtlString str, vector<CAtlString>& vtList, CAtlString sep)
{
	while (1)
	{
		int idx = str.Find(sep, 0);
		if (idx > 0)
		{
			vtList.push_back(str.Left(idx));
			str = str.Right(str.GetLength() - idx - sep.GetLength());
		}
		else
		{
			if (!str.IsEmpty())
			{
				vtList.push_back(str);
			}
			break;
		}
	}
}

CAtlString CStringUtil::Cut(CAtlString str, CAtlString sep, int count, _STRING_OPERATOR_DIRECTION direction)
{
	for (int i = 0; i < count; i++)
	{
		if (direction == STRING_OPERATOR_DIRECTION_LEFT)
		{
			str = str.Left(str.ReverseFind(sep.GetAt(0)));
		}
		else if (direction == STRING_OPERATOR_DIRECTION_RIGHT)
		{
			str = str.Right(str.GetLength() - str.Find(sep) - 1);
		}
	}

	return str;
}

int CStringUtil::FindSepCount(CAtlString str, CAtlString sep, int startIdx)
{
	int i = 0;
	int idx = startIdx;

	while (1)
	{
		idx = str.Find(sep, idx);
		if (idx == -1)
		{
			break;
		}
		i++;
		idx++;
	}

	return i;
}

TCHAR CStringUtil::GetNextChar(TCHAR ch)
{
	switch (ch)
	{
	case 57:
		return 97;
	case 122:
		return 48;
	}
	int ic = ch;
	return ++ic;
}

CAtlString CStringUtil::GetNextString(CAtlString str)
{
	if (str.IsEmpty())
	{
		return _T("0");
	}

	str = str.MakeLower();

	for (int i = str.GetLength() - 1; i >= 0; i--)
	{
		TCHAR ch = GetNextChar(str.GetAt(i));
		str.SetAt(i, ch);

		if (ch == '0')
		{
			// ½øÎ»
			if (i == 0)
			{
				str += '0';
			}
		}
		else
		{
			break;
		}
	}

	return str;
}

CAtlString CStringUtil::UTF8ToUnicode(CAtlStringA str)
{
	int len = 0;
	len = str.GetLength();
	int unicodeLen = ::MultiByteToWideChar(CP_UTF8,
		0,
		str.GetBuffer(),
		-1,
		NULL,
		0);

	wchar_t * pUnicode;
	pUnicode = new wchar_t[unicodeLen + 1];
	memset(pUnicode, 0, (unicodeLen + 1) * sizeof(wchar_t));

	::MultiByteToWideChar(CP_UTF8,
		0,
		str.GetBuffer(),
		-1,
		(LPWSTR)pUnicode,
		unicodeLen);

	CAtlString strText;
	strText = (wchar_t*)pUnicode;
	delete pUnicode;

	return strText;
}


CAtlStringA CStringUtil::UnicodeToUTF8(CAtlString str)
{
	char* pElementText;
	int iTextLen;

	// wide char to multi char
	iTextLen = WideCharToMultiByte(CP_UTF8,
		0,
		str.GetBuffer(),
		-1,
		NULL,
		0,
		NULL,
		NULL);

	pElementText = new char[iTextLen + 1];
	memset((void*)pElementText, 0, sizeof(char) * (iTextLen + 1));

	::WideCharToMultiByte(CP_UTF8,
		0,
		str.GetBuffer(),
		-1,
		pElementText,
		iTextLen,
		NULL,
		NULL);

	CAtlStringA strText = pElementText;
	delete[] pElementText;

	return strText;
}

