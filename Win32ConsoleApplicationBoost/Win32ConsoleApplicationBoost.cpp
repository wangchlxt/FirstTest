// Win32ConsoleApplicationBoost.cpp : �������̨Ӧ�ó������ڵ㡣
//

#include "stdafx.h"
#include <iostream>
using namespace std;

#include "ThreadTest.h"
#include "RegexTest.h"
#include "AsioTest.h"
#include "AsioTcpTest.h"

int main()
{
	/*CThreadTest threadTest;
	threadTest.Test();*/

	/*CRegexTest regexTest;
	regexTest.Test();*/

	/*CAsioTest asioTest;
	asioTest.Test();*/

	CAsioTcpTest asioTcpTest;
	asioTcpTest.Test("");

	system("pause");
    return 0;
}

