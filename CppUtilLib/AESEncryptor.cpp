#include "stdafx.h"
#include "AESEncryptor.h"


#include <fstream>
using namespace std;

CAESEncryptor::CAESEncryptor(unsigned char* key)
{
	m_pEncryptor = new CAES(key);
}


CAESEncryptor::~CAESEncryptor()
{
	delete m_pEncryptor;
}

void CAESEncryptor::Byte2Hex(const unsigned char* src, int len, char* dest)
{
	for (int i = 0; i < len; ++i)
	{
		sprintf_s(dest + i * 2, 3, "%02X", src[i]);
	}
}

void CAESEncryptor::Hex2Byte(const char* src, int len, unsigned char* dest)
{
	int length = len / 2;
	for (int i = 0; i < length; ++i)
	{
		dest[i] = Char2Int(src[i * 2]) * 16 + Char2Int(src[i * 2 + 1]);
	}
}

int CAESEncryptor::Char2Int(char c)
{
	if ('0' <= c && c <= '9')
	{
		return (c - '0');
	}
	else if ('a' <= c && c <= 'f')
	{
		return (c - 'a' + 10);
	}
	else if ('A' <= c && c <= 'F')
	{
		return (c - 'A' + 10);
	}
	return -1;
}

string CAESEncryptor::EncryptString(string strInfor)
{
	int nLength = strInfor.length();
	int spaceLength = 16 - (nLength % 16);
	unsigned char* pBuffer = new unsigned char[nLength + spaceLength];
	memset(pBuffer, '\0', nLength + spaceLength);
	memcpy_s(pBuffer, nLength + spaceLength, strInfor.c_str(), nLength);
	m_pEncryptor->Cipher(pBuffer, nLength + spaceLength);

	// 这里需要把得到的字符数组转换成十六进制字符串 
	char* pOut = new char[2 * (nLength + spaceLength) + 10];
	memset(pOut, '\0', 2 * (nLength + spaceLength) + 10);
	Byte2Hex(pBuffer, nLength + spaceLength, pOut);

	CStringA retValue = pOut;
	delete[] pBuffer;
	delete[] pOut;
	return retValue.GetBuffer();
}

string CAESEncryptor::DecryptString(string strMessage)
{
	int nLength = strMessage.length() / 2;
	unsigned char* pBuffer = new unsigned char[nLength + 10];
	memset(pBuffer, '\0', nLength);
	Hex2Byte(strMessage.c_str(), strMessage.length(), pBuffer);

	m_pEncryptor->InvCipher(pBuffer, nLength);
	string retValue((char*)pBuffer);

	memset(pBuffer, '\0', nLength);
	delete[] pBuffer;
	return retValue;
}

int CAESEncryptor::EncryptTxtFile(const char* inputFileName, const char* outputFileName)
{
	ifstream ifs;

	// Open file:
	ifs.open(inputFileName);
	if (!ifs)
	{
		return -1;
	}

	// Read config data:
	string strInfor;
	string strLine;
	while (!ifs.eof())
	{
		char temp[1024];
		memset(temp, '\0', 1024);
		ifs.read(temp, 1000);
		strInfor += temp;
	}
	ifs.close();

	// Encrypt
	strLine = EncryptString(strInfor);

	// Writefile 
	ofstream ofs;
	ofs.open(outputFileName);
	if (!ofs)
	{
		return -2;
	}
	ofs << strLine;
	ofs.close();

	return 0;
}

int CAESEncryptor::DecryptTxtFile(const char* inputFile, const char* outputFile)
{
	ifstream ifs;

	// Open file:
	ifs.open(inputFile);
	if (!ifs) {
		return -1;
	}

	// Read config data:
	string strInfor;
	string strLine;
	while (!ifs.eof())
	{
		char temp[1024];
		memset(temp, '\0', 1024);
		ifs.read(temp, 1000);
		strInfor += temp;
	}
	ifs.close();

	// Encrypt
	strLine = DecryptString(strInfor);

	// Writefile 
	ofstream ofs;
	ofs.open(outputFile);
	if (!ofs)
	{
		return -2;
	}
	ofs << strLine;
	ofs.close();

	return 0;
}
