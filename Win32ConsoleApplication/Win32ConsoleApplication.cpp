// Win32ConsoleApplication.cpp : �������̨Ӧ�ó������ڵ㡣
//

#include "stdafx.h"

#include "TimeTest.h"
#include "MapTest.h"
#include "DeskTest.h"
#include "SystemTest.h"

#include <iostream>
using namespace std;

int main()
{
	CSystemTest sys;
	sys.Test();


	auto i = 10;
	cout << i << endl;

	auto s = "abc";
	cout << s << endl;

	/*CTimeTest timeTest;
	timeTest.Test();*/

	/*CMapTest mapTest;
	mapTest.Test();*/

	CDeskTest deskTest;
	deskTest.Test();

	system("pause");

    return 0;
}

