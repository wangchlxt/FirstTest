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

	// left(�� 0 ��ʼ����Ҫ��ȡ���ַ��±�)
	cout << file.Left(file.ReverseFind('\\') + 1) << endl;

	// right(�� 0 ��ʼ����ʼ��ȡ���ַ��±�)
	cout << file.Right(file.GetLength() - file.ReverseFind('\\') - 1) << endl;
}
