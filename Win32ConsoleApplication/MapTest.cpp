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
	myMap.insert(map<CAtlStringA, bool>::value_type("�й�", true));
	myMap.insert(map<CAtlStringA, bool>::value_type("����", true));
	myMap.insert(map<CAtlStringA, bool>::value_type("����", true));
	myMap.insert(map<CAtlStringA, bool>::value_type("���", true));
	
	cout << "����" << myMap.find("����")->second << endl;

	cout << "���ͣ�" << myMap.at("����") << endl;

	cout << "���" << myMap["���"] << endl;
	cout << "���" << myMap["���"] << endl;

	myMap["���"] = true;
	cout << "���" << myMap["���"] << endl;

	myMap[""] = true;

	cout << endl;

	// ѭ�����
	for (int i = 0;i < 10000;i++)
	{
		char szTmp[10] = { 0 };
		_itoa_s(i, szTmp, 10, 10);
		myMap[szTmp] = true;
	}

	cout << "map ������" << myMap.size() << endl;;

	// map[] �±���� 10000 ��
	DWORD begin = GetTickCount();
	cout << "��ʼʱ�䣺" << begin << endl;

	for (int i = 0;i < 10000;i++)
	{
		bool tmp = myMap["���"];
	}

	DWORD end = GetTickCount();
	cout << "����ʱ�䣺" << end << endl;

	cout << "map[] �±���� 10000 ������ʱ�� ��" << end - begin << " ms" << endl << endl;

	// clean
	myMap.clear();
	cout << "clean() �� size() = " << myMap.size() << endl << endl;


	// �ַ����Ƚ�ʱ��
	CAtlStringA strTmp = "�������ķ���";
	begin = GetTickCount();
	cout << "��ʼʱ�䣺" << begin << endl;

	for (int i = 0;i < 10000;i++)
	{
		if ("����������" == strTmp)
		{

		}
	}

	end = GetTickCount();
	cout << "����ʱ�䣺" << end << endl;

	cout << "�ַ����Ƚ� 10000 ������ʱ�� ��" << end - begin << " ms" << endl;
}
