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

	cout << "�̷��б�" << endl;
	for (vector<CAtlString>::iterator it = vtDisk.begin();it != vtDisk.end();it++)
	{
		CAtlStringA str = CW2A(*it);
		cout << str << endl;
	}

	// дӲ��
	HANDLE hFile = CreateFile(_T("\\\\.\\G:"), GENERIC_WRITE|GENERIC_WRITE, FILE_SHARE_READ |FILE_SHARE_WRITE, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
	if (hFile == INVALID_HANDLE_VALUE)
	{
		cout << "�� G ��ʧ��" << endl;
	}
	else
	{
		cout << "�� G �̳ɹ�" << endl;

		LARGE_INTEGER size = { 0 };
		GetFileSizeEx(hFile, &size);

		cout << "�ļ���С��" << size.QuadPart << endl;

		SetFilePointer(hFile, 0, NULL, FILE_BEGIN);
		CAtlStringA str = "�й� test --";
		DWORD wlen = 0;
		if (WriteFile(hFile, str.GetBuffer(), str.GetLength(), &wlen, NULL))
		{
			cout << "����д��ɹ�" << endl;
		}
		else
		{
			cout << "����д��ʧ�ܣ������룺" << GetLastError() << endl;
		}

		CloseHandle(hFile);
		hFile = NULL;
	}
}
