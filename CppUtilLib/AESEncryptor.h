#pragma once
#include "AES.h"
#include <string>

// AES ���ܽ���
class CAESEncryptor
{
public:
	CAESEncryptor(unsigned char* key);
	~CAESEncryptor();

public:
	// �ַ�������
	std::string EncryptString(std::string strInfor);

	// �ַ�������
	std::string DecryptString(std::string strMessage);

	// �ļ�����
	int EncryptTxtFile(const char* inputFileName, const char* outputFileName);

	// �ļ�����
	int DecryptTxtFile(const char* inputFileName, const char* outputFileName);

private:
	void Byte2Hex(const unsigned char* src, int len, char* dest);
	void Hex2Byte(const char* src, int len, unsigned char* dest);
	int  Char2Int(char c);

private:
	CAES* m_pEncryptor;
};
