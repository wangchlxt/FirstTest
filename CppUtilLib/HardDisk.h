#pragma once
#include <vector>
using namespace std;

class CHardDisk
{
public:
	CHardDisk();
	~CHardDisk();

	// ����������������GetDeviceInfo 
	// ���ܣ�ȡ��������Ϣ 
	// GetLogicalDriveStrings���صĸ�ʽΪA:\0B:\0C:\0
	// ��������:     arrDrives�洢�����̷�
	// ����ֵ������TRUE�ɹ� FALSEʧ�� 
	// 
	vector<CAtlString> GetDeviceInfo();

};

