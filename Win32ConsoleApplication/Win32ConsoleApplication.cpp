// Win32ConsoleApplication.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"

#include "TimeTest.h"
#include "MapTest.h"
#include "DeskTest.h"
#include "SystemTest.h"
#include "PortTest.h"
#include "StrTest.h"

#include <iostream>
using namespace std;

int main()
{
	CStrTest str;
	str.Test();

	//system("wmic process > c:/process33.txt");

	/*CPortTest port;
	port.Test();*/


	/*CSystemTest sys;
	sys.Test();

	auto i = 10;
	cout << i << endl;

	auto s = "abc";
	cout << s << endl;*/

	/*CTimeTest timeTest;
	timeTest.Test();*/

	/*CMapTest mapTest;
	mapTest.Test();*/

	/*CDeskTest deskTest;
	deskTest.Test();*/

	system("pause");

    return 0;
}


