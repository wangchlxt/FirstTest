#pragma once
class CThreadTest2
{
public:
	CThreadTest2();
	~CThreadTest2();

	void Test();

	void TestWaitForMultipleObjects();

	void TestEvent();
	void TestEvent2();

	void TestMutex();


public:
	HANDLE m_hEvent;
	HANDLE m_hEvent2;

	HANDLE m_hMutex;
};

