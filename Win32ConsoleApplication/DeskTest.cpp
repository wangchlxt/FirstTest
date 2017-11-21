#include "stdafx.h"
#include "DeskTest.h"
#include "HardDisk.h"

#include <iostream>
#include <atlstr.h>
#include <atlconv.h>
#include <vector>
using namespace std;

CDeskTest::CDeskTest()
{
}


CDeskTest::~CDeskTest()
{
}

void CDeskTest::Test()
{
	vector<CAtlString> vtDisk;

	CHardDisk hardDisk;
	vtDisk = hardDisk.GetDeviceInfo();

	cout << "盘符列表：" << endl;
	for (vector<CAtlString>::iterator it = vtDisk.begin();it != vtDisk.end();it++)
	{
		CAtlStringA str = CW2A(*it);
		cout << str << endl;
	}

	// 写硬盘
	HANDLE hFile = CreateFile(_T("\\\\.\\G:"), GENERIC_WRITE|GENERIC_WRITE, FILE_SHARE_READ |FILE_SHARE_WRITE, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
	if (hFile == INVALID_HANDLE_VALUE)
	{
		cout << "打开 G 盘失败" << endl;
	}
	else
	{
		cout << "打开 G 盘成功" << endl;

		LARGE_INTEGER size = { 0 };
		GetFileSizeEx(hFile, &size);

		cout << "文件大小：" << size.QuadPart << endl;

		SetFilePointer(hFile, 0, NULL, FILE_BEGIN);
		CAtlStringA str = "中国 test --";
		DWORD wlen = 0;
		if (WriteFile(hFile, str.GetBuffer(), str.GetLength(), &wlen, NULL))
		{
			cout << "内容写入成功" << endl;
		}
		else
		{
			cout << "内容写入失败，错误码：" << GetLastError() << endl;
		}

		CloseHandle(hFile);
		hFile = NULL;
	}
}
