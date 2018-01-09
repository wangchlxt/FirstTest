#include "stdafx.h"
#include "TryTest.h"
#include <process.h>
#include <iostream>
using namespace std;

CTryTest::CTryTest()
{
}


CTryTest::~CTryTest()
{
}

UINT __stdcall TryTestThread(LPVOID lpParam)
{
	try
	{
		int a = 1;
		int b = 0;
		int i = a / b;

		/*char* pszTmp="abc";
		delete[] pszTmp;*/
	}
	catch (const std::exception& e)
	{
		cout << "exception£º" << e.what() << endl;
	}
	catch (...)
	{
		cout << "exception ..." << endl;
	}

	cout << "TryTestThread end" << endl;

	return 0;
}

void CTryTest::Test()
{
	UINT tid = 0;
	_beginthreadex(NULL, 0, TryTestThread, this, 0, &tid);
}
