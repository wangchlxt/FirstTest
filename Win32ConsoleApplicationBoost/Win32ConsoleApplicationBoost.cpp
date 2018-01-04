// Win32ConsoleApplicationBoost.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
using namespace std;

#include "ThreadTest.h"
#include "RegexTest.h"
#include "AsioTest.h"
#include "AsioTcpTest.h"
#include "ThreadPoolTest.h"
#include "StringTest.h"
#include "LogTest.h"
#include "UdpServerTest.h"

int main()
{
	CUdpServerTest udpServerTest;
	udpServerTest.Test();

	/*CLogTest logTest;
	logTest.Test();*/

	/*CStringTest stringTest;
	stringTest.Test();*/

	/*CThreadPoolTest threadPoolTest;
	threadPoolTest.Test();*/

	/*CThreadTest threadTest;
	threadTest.Test();*/

	/*CRegexTest regexTest;
	regexTest.Test();*/

	/*CAsioTest asioTest;
	asioTest.Test();*/

	/*CAsioTcpTest asioTcpTest;
	asioTcpTest.Test("");*/

	system("pause");
    return 0;
}

