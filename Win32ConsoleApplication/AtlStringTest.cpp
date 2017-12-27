#include "stdafx.h"
#include "AtlStringTest.h"

#include <iostream>
using namespace std;

CAtlStringTest::CAtlStringTest()
{
}


CAtlStringTest::~CAtlStringTest()
{
}

void CAtlStringTest::Test()
{
	TestSub();
}

void CAtlStringTest::TestSub()
{
	CAtlStringA file = "E:\\work\\QLGU_NB-IOT_UDP_SERVER\\QLGUUdpSvr\\QLGUUdpSvr\\resource.h";
	cout << file << endl;

	// left(从 0 开始，到要截取的字符下标)
	cout << file.Left(file.ReverseFind('\\') + 1) << endl;

	// right(从 0 开始，开始截取的字符下标)
	cout << file.Right(file.GetLength() - file.ReverseFind('\\') - 1) << endl;
}
