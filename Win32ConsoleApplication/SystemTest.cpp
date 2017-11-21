#include "stdafx.h"
#include "SystemTest.h"
#include <process.h>
#include <iostream>
using namespace std;

CSystemTest::CSystemTest()
{
}


CSystemTest::~CSystemTest()
{
}

UINT __stdcall SystemThread(LPVOID lpParam)
{
	int ret = system("ping www.baidu.com");
	cout << "system return:" << ret << endl;

	return 0;
}

void CSystemTest::Test()
{
	UINT tid = 0;
	for (int i = 0;i < 100;i++)
	{
		_beginthreadex(NULL, 0, SystemThread, this, 0, &tid);
		cout << "Thread Run:" << tid << endl;
	}
}
