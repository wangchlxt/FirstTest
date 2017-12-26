#include "stdafx.h"
#include "Task.h"
#include "TimeUtil.h"

#include <iostream>
using namespace std;

CTask::CTask()
{
}


CTask::~CTask()
{
}

void CTask::PrintTime(int idx)
{
	CTimeUtil timeUtil;

	for (int i = 0;i < 3;i++)
	{
		cout << idx << "   "  << i << "   " << timeUtil.GetTimeStringA() << endl;
		Sleep(1000);
	}
}
