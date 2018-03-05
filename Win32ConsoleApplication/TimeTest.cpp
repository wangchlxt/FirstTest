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
 ��ȡ��ǰ��ϵͳʱ�䣬���صĽ����һ��time_t���ͣ���ʵ����һ������������ֵ��ʾ�� CUT��Coordinated Universal Time������Э��ʱ�� 1970��1��1��00:00:00
 ����Ϊ UNIX ϵͳ�� Epoch ʱ�䣩����ǰʱ�̵�������Ȼ����� localtime �� time_t ����ʾ�� CUT ʱ��ת��Ϊ����ʱ�䣨������ + 8������ CUT �� 8 ��Сʱ��
  ��ת�� struct tm ���ͣ������͵ĸ����ݳ�Ա�ֱ��ʾ������ʱ���롣
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
	cout << "time() �����ľ��ȵ��� :" << endl;
	for (int i = 0;i < 100;i++)
	{
		// time() �����ľ��ȵ���
		time_t tt;
		tm st;
		time(&tt);
		localtime_s(&st, &tt);

		CAtlStringA strTime;
		strTime.Format("%d%d%d%d%d%d", st.tm_year + 1900, st.tm_mon + 1, st.tm_mday, st.tm_hour, st.tm_min, st.tm_sec);

		cout << strTime << endl;
	}


	cout << "GetSystemTime() ���ȵ� 3 λ���� :" << endl;
	for (int i = 0;i < 100;i++)
	{
		// GetSystemTime() ���ȵ� 3 λ����
		SYSTEMTIME m_time;
		GetSystemTime(&m_time);

		CAtlStringA strTime;
		strTime.Format("%d%.02d%.02d%.02d%.02d%.02d%.03d", m_time.wYear, m_time.wMonth, m_time.wDay, m_time.wHour + 8, m_time.wMinute, m_time.wSecond, m_time.wMilliseconds);

		cout << strTime << endl;
	}

	cout << "GetSystemTime() Sleep(1) :" << endl;
	for (int i = 0;i < 100;i++)
	{
		// GetSystemTime() ���ȵ� 3 λ����
		SYSTEMTIME m_time;
		GetSystemTime(&m_time);

		

		CAtlStringA strTime;
		strTime.Format("%d%.02d%.02d%.02d%.02d%.02d%.03d", m_time.wYear, m_time.wMonth, m_time.wDay, m_time.wHour + 8, m_time.wMinute, m_time.wSecond, m_time.wMilliseconds);

		cout << strTime << endl;
		Sleep(1);
	}
}
