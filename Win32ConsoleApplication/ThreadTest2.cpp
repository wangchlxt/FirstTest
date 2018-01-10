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
	//TestEvent2();
	TestMutex();
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

/**************************************************************************************************************************************/

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
  ���û�н� WaitForSingleObject �ȴ��� event ����Ϊ���źţ���ʹ�ر� event ��������������Ϊ NULL��WaitForSingleObject ��Ȼ�ᴦ������
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
 �ر� event ���ǰ�����������Ϊ���źţ����Է�ֹ�����������̱߳�Ϊ��ʬ�߳�
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

/**************************************************************************************************************************************/

UINT __stdcall TestMutexThread1(LPVOID lpParam)
{
	CThreadTest2* pThis = (CThreadTest2*)lpParam;
	
	WaitForSingleObject(pThis->m_hMutex, INFINITE);
	cout << "���� TestMutexThread1" << endl;

	Sleep(6000);
	ReleaseMutex(pThis->m_hMutex);

	cout << "�˳� TestMutexThread1" << endl;
	return 0;
}

UINT __stdcall TestMutexThread2(LPVOID lpParam)
{
	CThreadTest2* pThis = (CThreadTest2*)lpParam;
	
	WaitForSingleObject(pThis->m_hMutex, INFINITE);
	cout << "���� TestMutexThread2" << endl;

	Sleep(3000);
	ReleaseMutex(pThis->m_hMutex);

	cout << "�˳� TestMutexThread2" << endl;
	return 0;
}

/*
 mutex �� event ��ͬ������Ҫ ReleaseMutex��CloseHandle ��WaitForSingleObject ��������
*/
void CThreadTest2::TestMutex()
{
	m_hMutex = CreateMutex(NULL, FALSE, NULL);

	UINT tid = 0;
	_beginthreadex(NULL, 0, TestMutexThread1, this, 0, &tid);
	_beginthreadex(NULL, 0, TestMutexThread2, this, 0, &tid);

	CloseHandle(m_hMutex);
	m_hMutex = NULL;
}
