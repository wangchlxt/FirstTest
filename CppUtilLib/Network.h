#pragma once
#include <atlstr.h>

// �����Ǵ� Network.dll ������
class CNetwork {
public:
	CNetwork(void);
	
	// ��ʼ�����绷��
	static int Init();

	// �������绷��
	static int UnInit();

	// ���� ftp ����
	void RunFtpServer();
};

