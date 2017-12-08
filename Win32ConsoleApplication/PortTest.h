#pragma once
#include <string>  
using namespace std;

class CPortTest
{
public:
	CPortTest();
	~CPortTest();

	bool GetProcNameByPort(int nPort, string &strResult);
	int Test();
};

