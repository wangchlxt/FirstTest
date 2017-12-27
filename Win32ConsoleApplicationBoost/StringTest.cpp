#include "stdafx.h"
#include "StringTest.h"

#include <iostream>
#include <vector>
#include <boost\format.hpp>
#include <boost\tokenizer.hpp>
#include <boost\algorithm\string.hpp>

using namespace std;
using namespace boost;

CStringTest::CStringTest()
{
}


CStringTest::~CStringTest()
{
}

void CStringTest::Test()
{
	string flx = "QRequestBlock^^";
	string txt = "QRequestBlock^^1,10,19,22,25,77,90";
	
	txt.replace(0,flx.length(), "");
	cout << txt << endl;

	/*vector<string> vtList;
	boost::split(vtList, txt, boost::is_any_of(","));

	for (int i = 0;i < vtList.size();i++)
	{
		cout << vtList.at(i) << endl;
	}*/

}
