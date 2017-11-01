#include "stdafx.h"
#include "HardDisk.h"


CHardDisk::CHardDisk()
{
}


CHardDisk::~CHardDisk()
{
}

// 函数描述：函数名GetDeviceInfo 
// 功能：取驱动器信息 
// GetLogicalDriveStrings返回的格式为A:\0B:\0C:\0
// 参数描述:     arrDrives存储所有盘符
// 返回值描述：TRUE成功 FALSE失败 
// 
vector<CAtlString> CHardDisk::GetDeviceInfo()
{
	vector<CString> vtList;

	DWORD dw = ::GetLogicalDriveStrings(0, NULL);
	TCHAR* pAllDrivers = new TCHAR[dw];
	::GetLogicalDriveStrings(dw, pAllDrivers);

	LPCTSTR pDriver = pAllDrivers;
	DWORD DriverNum = 0;
	while (pDriver[0] != 0)
	{
		vtList.push_back(pDriver);
		DriverNum++;
		pDriver = _tcschr(pDriver, 0) + 1;
	}
	delete[] pAllDrivers;
	return vtList;
}


