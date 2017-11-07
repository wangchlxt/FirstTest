#include "stdafx.h"
#include "MapTest.h"

#include <atlstr.h>
#include <iostream>
#include <map>
using namespace std;

CMapTest::CMapTest()
{
}


CMapTest::~CMapTest()
{
}

void CMapTest::Test()
{
	cout << "------ map test ------" << endl;
	
	map<CAtlStringA, bool> myMap;
	myMap.insert(map<CAtlStringA, bool>::value_type("中国", true));
	myMap.insert(map<CAtlStringA, bool>::value_type("人民", true));
	myMap.insert(map<CAtlStringA, bool>::value_type("共和", true));
	myMap.insert(map<CAtlStringA, bool>::value_type("洪荒", true));
	
	cout << "人民" << myMap.find("人民")->second << endl;

	cout << "共和：" << myMap.at("共和") << endl;

	cout << "李白" << myMap["李白"] << endl;
	cout << "李白" << myMap["李白"] << endl;

	myMap["李白"] = true;
	cout << "李白" << myMap["李白"] << endl;

	myMap[""] = true;

	cout << endl;

	// 循环添加
	for (int i = 0;i < 10000;i++)
	{
		char szTmp[10] = { 0 };
		_itoa_s(i, szTmp, 10, 10);
		myMap[szTmp] = true;
	}

	cout << "map 数量：" << myMap.size() << endl;;

	// map[] 下标访问 10000 次
	DWORD begin = GetTickCount();
	cout << "开始时间：" << begin << endl;

	for (int i = 0;i < 10000;i++)
	{
		bool tmp = myMap["李白"];
	}

	DWORD end = GetTickCount();
	cout << "结束时间：" << end << endl;

	cout << "map[] 下标访问 10000 次消耗时间 ：" << end - begin << " ms" << endl << endl;

	// clean
	myMap.clear();
	cout << "clean() 后 size() = " << myMap.size() << endl << endl;


	// 字符串比较时间
	CAtlStringA strTmp = "加适量的房间";
	begin = GetTickCount();
	cout << "开始时间：" << begin << endl;

	for (int i = 0;i < 10000;i++)
	{
		if ("中了四六级" == strTmp)
		{

		}
	}

	end = GetTickCount();
	cout << "结束时间：" << end << endl;

	cout << "字符串比较 10000 次消耗时间 ：" << end - begin << " ms" << endl;
}
