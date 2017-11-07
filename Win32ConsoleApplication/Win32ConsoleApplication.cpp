// Win32ConsoleApplication.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"

#include "TimeTest.h"
#include "MapTest.h"

int main()
{

	/*CTimeTest timeTest;
	timeTest.Test();*/

	CMapTest mapTest;
	mapTest.Test();

	system("pause");

    return 0;
}

