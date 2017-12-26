#include "stdafx.h"
#include "ThreadPoolTest.h"
#include "Task.h"

#include <iostream>
#include <boost\bind.hpp>
#include <boost\threadpool.hpp>
using namespace std;
using namespace boost::threadpool;

CThreadPoolTest::CThreadPoolTest()
{
}


CThreadPoolTest::~CThreadPoolTest()
{
}

void first_task()
{
	for (int i = 0; i < 30; ++i)
		cout << "first" << i << endl;
}
void second_task()
{
	for (int i = 0; i < 30; ++i)
		cout << "second" << i << endl;
}
void third_task()
{
	for (int i = 0; i < 30; ++i)
		cout << "third" << i << endl;
}
void task_with_parameter(int value, string str)
{
	printf("task_with_parameter with int=(%d).\n", value);
	printf("task_with_parameter with string=(%s).\n", str.c_str());
}



void CThreadPoolTest::Test()
{
	//TestWait();
	//TestExecutionOrder();
	TestOtherClassFunction();

	cout << "�����˳�" << endl;
}

void CThreadPoolTest::TestWait()
{
	// ����һ���̳߳أ���ʼ��Ϊ2���߳�   
	pool tp(2);

	// ����4���̺߳���
	tp.schedule(&first_task); // ����������ִ�к���
	tp.wait(); //�ȴ�ǰ����̺߳���ִ�н����ż���ִ�к�����߳�

	tp.schedule(&second_task);
	tp.wait();

	tp.schedule(&third_task);
	tp.schedule(boost::bind(task_with_parameter, 8, "hello")); // ������������ִ�к���   
	tp.schedule(&third_task);

	// ���������ȵ�4���߳�ȫ��ִ�н����Ż��˳�
	cout << "TestWait bottom" << endl;
}


void thread_task(int idx)
{
	for (int i = 0; i < 3; ++i)
	{
		cout << idx << "  " << i << endl;
		Sleep(1000);
	}
}

void CThreadPoolTest::TestExecutionOrder()
{
	pool tp(5);

	for (int i = 0;i < 30;++i)
	{
		tp.schedule(boost::bind(thread_task, i));
	}

	cout << "TestExecutionOrder bottom" << endl;
}

void other_class_funciton(int idx)
{
	CTask task;
	task.PrintTime(idx);
}

void CThreadPoolTest::TestOtherClassFunction()
{
	pool tp(5);

	for (int i = 0;i < 30;++i)
	{
		tp.schedule(boost::bind(other_class_funciton, i));
	}
}
