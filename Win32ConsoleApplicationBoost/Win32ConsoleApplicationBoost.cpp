// Win32ConsoleApplicationBoost.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
using namespace std;

//#include "ThreadTest.h"
#include "RegexTest.h"

int main()
{
	/*CThreadTest t;
	t.Test();*/

	CRegexTest reg;
	reg.Test();

	system("pause");
    return 0;
}

