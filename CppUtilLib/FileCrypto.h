#pragma once
#include <string>
using namespace std;

// 文件加密解密
class CFileCrypyo
{
public:
	CFileCrypyo();
	~CFileCrypyo();
	
	bool DesFile(string sourceFile, string targetFile);//解密文件
	bool MakePsw();//标准化密码
	char FixByte(char SourceByte,int pswN);


	// 文件加密解密的密码
	string m_strPassword;
};

