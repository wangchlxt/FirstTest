#pragma once
#include <string>
using namespace std;

// �ļ����ܽ���
class CFileCrypyo
{
public:
	CFileCrypyo();
	~CFileCrypyo();
	
	bool DesFile(string sourceFile, string targetFile);//�����ļ�
	bool MakePsw();//��׼������
	char FixByte(char SourceByte,int pswN);


	// �ļ����ܽ��ܵ�����
	string m_strPassword;
};

