#include "stdafx.h"
#include "ThreadTest.h"
#include <iostream>
using namespace std;
using namespace boost;

CThreadTest::CThreadTest()
{
}


CThreadTest::~CThreadTest()
{
}

UINT TestThread1(const char* lpParam)
{
	cout << lpParam << endl;
	return 0;
}

void TestThread2(int num)
{
	cout << num << endl;
}

void CThreadTest::Test()
{
	thread t1(TestThread1,"abc");
	thread t2(TestThread2, 1223);
}
