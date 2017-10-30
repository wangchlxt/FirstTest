#pragma once
#include "AES.h"
#include <string>

// AES 加密解密
class CAESEncryptor
{
public:
	CAESEncryptor(unsigned char* key);
	~CAESEncryptor();

public:
	// 字符串加密
	std::string EncryptString(std::string strInfor);

	// 字符串解密
	std::string DecryptString(std::string strMessage);

	// 文件加密
	int EncryptTxtFile(const char* inputFileName, const char* outputFileName);

	// 文件解密
	int DecryptTxtFile(const char* inputFileName, const char* outputFileName);

private:
	void Byte2Hex(const unsigned char* src, int len, char* dest);
	void Hex2Byte(const char* src, int len, unsigned char* dest);
	int  Char2Int(char c);

private:
	CAES* m_pEncryptor;
};
