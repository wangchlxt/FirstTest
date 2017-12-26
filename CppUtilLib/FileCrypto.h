#pragma once
#include <string>
using namespace std;

// 文件加密解密-对等加密
class CFileCrypto
{
public:
	CFileCrypto();
	~CFileCrypto();
	
	// 文件对等加密解密
	bool DesFile(string sourceFile, string targetFile);

	//标准化密码
	bool MakePsw();
	char FixByte(char SourceByte,int pswN);


	// 文件加密解密的密码
	string m_strPassword;
};

