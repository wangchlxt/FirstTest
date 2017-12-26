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

	cout << "函数退出" << endl;
}

void CThreadPoolTest::TestWait()
{
	// 创建一个线程池，初始化为2个线程   
	pool tp(2);

	// 调用4个线程函数
	tp.schedule(&first_task); // 不带参数的执行函数
	tp.wait(); //等待前面的线程函数执行结束才继续执行后面的线程

	tp.schedule(&second_task);
	tp.wait();

	tp.schedule(&third_task);
	tp.schedule(boost::bind(task_with_parameter, 8, "hello")); // 带两个参数的执行函数   
	tp.schedule(&third_task);

	// 这儿函数会等到4个线程全部执行结束才会退出
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
