#pragma once
#include <string>
using namespace std;

// �ļ����ܽ���-�Եȼ���
class CFileCrypto
{
public:
	CFileCrypto();
	~CFileCrypto();
	
	// �ļ��Եȼ��ܽ���
	bool DesFile(string sourceFile, string targetFile);

	//��׼������
	bool MakePsw();
	char FixByte(char SourceByte,int pswN);


	// �ļ����ܽ��ܵ�����
	string m_strPassword;
};

