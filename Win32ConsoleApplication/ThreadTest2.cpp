#include "stdafx.h"
#include "ThreadTest2.h"
#include <process.h>
#include <iostream>
using namespace std;

CThreadTest2::CThreadTest2()
{
}

CThreadTest2::~CThreadTest2()
{
}

UINT __stdcall Thread1(LPVOID lpParam)
{
	Sleep(3000);
	cout << "Thread1 end" << endl;
	return 0;
}

UINT __stdcall Thread2(LPVOID lpParam)
{
	Sleep(1000);
	cout << "Thread2 end" << endl;
	return 0;
}

void CThreadTest2::Test()
{
	WaitForMultipleObjectsTest();
}

void CThreadTest2::WaitForMultipleObjectsTest()
{
	UINT tid = 0;
	HANDLE hThreads[2] = { 0 };
	hThreads[0] = (HANDLE)_beginthreadex(NULL, 0, Thread1, this, 0, &tid);
	hThreads[1] = (HANDLE)_beginthreadex(NULL, 0, Thread2, this, 0, &tid);

	WaitForMultipleObjects(2, hThreads, TRUE, INFINITE);
	cout << "exec thread end" << endl;
}
