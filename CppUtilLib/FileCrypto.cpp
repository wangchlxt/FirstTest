#include "StdAfx.h"
#include "FileCrypto.h"
#include <fstream>
#include <iostream>
#define BLOCK_SIZE 4096 //设置读写缓存块大小为 4K

CFileCrypto::CFileCrypto()
{
}


CFileCrypto::~CFileCrypto()
{
}

//解密文件
bool CFileCrypto::DesFile(string sourceFile,string targetFile)
{
	MakePsw();
	ifstream fin(sourceFile.c_str(),ios::binary);
	if(!fin){
		cout<<"File open error!\n";
		return false;
	}
	ofstream fout(targetFile.c_str(),ios::binary);
	char c[BLOCK_SIZE];
	char d[BLOCK_SIZE];
	while(!fin.eof())
	{
		fin.read(c,BLOCK_SIZE);
		for(int j = 0; j < BLOCK_SIZE;j++ )
		{
			d[j] = FixByte(c[j],(j + 1)%16);
		}
		fout.write(d,fin.gcount());
	}
	fin.close();
	fout.close();
	return true;
}

char CFileCrypto::FixByte(char SourceByte,int pswN)
{
	if(pswN >= 16)
	{
		return '0';
	}
	return SourceByte^m_strPassword[pswN];
}

//格式化密码
bool CFileCrypto::MakePsw()
{
	string iWord = m_strPassword;
	char p[17] = {'\0'};
	int sizePsd = iWord.size();
	if(sizePsd == 0)
	{
		for(int i = 0; i < 16; i ++)
		{
			p[i] = 125;
		}
	}
	else if(sizePsd >16)
	{
		for(int i = 0; i < 16; i++)
		{
			p[i] = iWord[i];
		}
	}
	else
	{
		for(int i = 0; i < sizePsd ; i++)
		{
			p[i] = iWord[i];
		}
		for(int j = sizePsd ; j <16 ; j++)
		{
			p[j] = 125;
		}
	}
	m_strPassword = p;
	return true;
}