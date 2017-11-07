#include "stdafx.h"
#include "ClipboardUtil.h"


CClipboardUtil::CClipboardUtil()
{
}


CClipboardUtil::~CClipboardUtil()
{
}

bool CClipboardUtil::SaveString(CAtlStringA str)
{
	if (OpenClipboard(NULL))
	{
		HGLOBAL clipbuffer;
		char* buffer = NULL;

		EmptyClipboard();

		clipbuffer = GlobalAlloc(GMEM_DDESHARE, str.GetLength() + 1);
		
		buffer = (char*)GlobalLock(clipbuffer);
		memset(buffer, 0, str.GetLength() + 1);
		//strcpy_s(buffer,str.GetLength(), str.GetBuffer());
		memcpy(buffer, str.GetBuffer(), str.GetLength());
		GlobalUnlock(clipbuffer);
		SetClipboardData(CF_TEXT, clipbuffer);

		CloseClipboard();
		return true;
	}
	return false;
}
