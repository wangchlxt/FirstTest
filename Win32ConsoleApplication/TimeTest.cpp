#include "stdafx.h"
#include "TimeTest.h"

#include <ctime>
#include <iostream>
using namespace std;

CTimeTest::CTimeTest()
{
}


CTimeTest::~CTimeTest()
{
}

void CTimeTest::Test()
{
	//TestTime();
	TestTimeString();
}


/*
 获取当前的系统时间，返回的结果是一个time_t类型，其实就是一个大整数，其值表示从 CUT（Coordinated Universal Time）世界协调时间 1970年1月1日00:00:00
 （称为 UNIX 系统的 Epoch 时间）到当前时刻的秒数。然后调用 localtime 将 time_t 所表示的 CUT 时间转换为本地时间（我们是 + 8区，比 CUT 多 8 个小时）
  并转成 struct tm 类型，该类型的各数据成员分别表示年月日时分秒。
*/
void CTimeTest::TestTime()
{
	time_t t1;
	
	for (int i = 0;i < 10;i++)
	{
		time(&t1);
		cout << t1 << endl;
		Sleep(1000);
	}
}

void CTimeTest::TestTimeString()
{
	cout << "time() 函数的精度到秒 :" << endl;
	for (int i = 0;i < 100;i++)
	{
		// time() 函数的精度到秒
		time_t tt;
		tm st;
		time(&tt);
		localtime_s(&st, &tt);

		CAtlStringA strTime;
		strTime.Format("%d%d%d%d%d%d", st.tm_year + 1900, st.tm_mon + 1, st.tm_mday, st.tm_hour, st.tm_min, st.tm_sec);

		cout << strTime << endl;
	}


	cout << "GetSystemTime() 精度到 3 位毫秒 :" << endl;
	for (int i = 0;i < 100;i++)
	{
		// GetSystemTime() 精度到 3 位毫秒
		SYSTEMTIME m_time;
		GetSystemTime(&m_time);

		CAtlStringA strTime;
		strTime.Format("%d%.02d%.02d%.02d%.02d%.02d%.03d", m_time.wYear, m_time.wMonth, m_time.wDay, m_time.wHour + 8, m_time.wMinute, m_time.wSecond, m_time.wMilliseconds);

		cout << strTime << endl;
	}

	cout << "GetSystemTime() Sleep(1) :" << endl;
	for (int i = 0;i < 100;i++)
	{
		// GetSystemTime() 精度到 3 位毫秒
		SYSTEMTIME m_time;
		GetSystemTime(&m_time);

		

		CAtlStringA strTime;
		strTime.Format("%d%.02d%.02d%.02d%.02d%.02d%.03d", m_time.wYear, m_time.wMonth, m_time.wDay, m_time.wHour + 8, m_time.wMinute, m_time.wSecond, m_time.wMilliseconds);

		cout << strTime << endl;
		Sleep(1);
	}
}
