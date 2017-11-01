#pragma once
#include <vector>
using namespace std;

class CHardDisk
{
public:
	CHardDisk();
	~CHardDisk();

	// 函数描述：函数名GetDeviceInfo 
	// 功能：取驱动器信息 
	// GetLogicalDriveStrings返回的格式为A:\0B:\0C:\0
	// 参数描述:     arrDrives存储所有盘符
	// 返回值描述：TRUE成功 FALSE失败 
	// 
	vector<CAtlString> GetDeviceInfo();

};

