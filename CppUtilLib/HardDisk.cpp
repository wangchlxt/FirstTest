#include "stdafx.h"
#include "HardDisk.h"


CHardDisk::CHardDisk()
{
}


CHardDisk::~CHardDisk()
{
}

// ����������������GetDeviceInfo 
// ���ܣ�ȡ��������Ϣ 
// GetLogicalDriveStrings���صĸ�ʽΪA:\0B:\0C:\0
// ��������:     arrDrives�洢�����̷�
// ����ֵ������TRUE�ɹ� FALSEʧ�� 
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


