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
	//TestWaitForMultipleObjects();
	//TestEvent();
	TestEvent2();
}

void CThreadTest2::TestWaitForMultipleObjects()
{
	UINT tid = 0;
	HANDLE hThreads[2] = { 0 };
	hThreads[0] = (HANDLE)_beginthreadex(NULL, 0, Thread1, this, 0, &tid);
	hThreads[1] = (HANDLE)_beginthreadex(NULL, 0, Thread2, this, 0, &tid);

	WaitForMultipleObjects(2, hThreads, TRUE, INFINITE);
	cout << "exec thread end" << endl;
}


UINT __stdcall TestEventThread(LPVOID lpParam)
{
	Sleep(3000);
	
	CThreadTest2*  pThis = (CThreadTest2*)lpParam;
	CloseHandle(pThis->m_hEvent);
	pThis->m_hEvent = NULL;
	
	cout << "close event" << endl;

	return 0;
}

/*
  如果没有将 WaitForSingleObject 等待的 event 设置为有信号，即使关闭 event 句柄，将句柄设置为 NULL，WaitForSingleObject 依然会处于阻塞
*/
void CThreadTest2::TestEvent()
{
	m_hEvent = CreateEvent(NULL, TRUE, FALSE, NULL);
	
	UINT tid = 0;
	_beginthreadex(NULL, 0, TestEventThread, this, 0, &tid);

	WaitForSingleObject(m_hEvent,INFINITE);
	cout << "Event end" << endl;
}

/*
 关闭 event 句柄前，将句柄设置为有信号，可以防止处于阻塞的线程变为僵尸线程
*/
UINT __stdcall TestEventThread2(LPVOID lpParam)
{
	Sleep(3000);

	CThreadTest2*  pThis = (CThreadTest2*)lpParam;
	SetEvent(pThis->m_hEvent2);
	CloseHandle(pThis->m_hEvent2);
	pThis->m_hEvent2 = NULL;

	cout << "close event" << endl;

	return 0;
}

void CThreadTest2::TestEvent2()
{
	m_hEvent2 = CreateEvent(NULL, TRUE, FALSE, NULL);

	UINT tid = 0;
	_beginthreadex(NULL, 0, TestEventThread2, this, 0, &tid);

	WaitForSingleObject(m_hEvent2, INFINITE);
	cout << "Event end" << endl;
}

