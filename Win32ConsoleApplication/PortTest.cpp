#include "stdafx.h"
#include "PortTest.h"

#include <iostream>
using namespace std;

#pragma warning(disable:4996)

CPortTest::CPortTest()
{
}


CPortTest::~CPortTest()
{
}


// 根据端口查询进程名，如果有多个进程，只返回第一个
bool CPortTest::GetProcNameByPort(int nPort, string &strResult)
{
	bool bSuc = false;
	char pszPort[16] = { 0 };
	itoa(nPort, pszPort, 10);
	char pResult[80] = { 0 };
	const char* pPortFilePath = "c:\\~vtmp";
	const char* pProcessFilePath = "c:\\~vvtmp";
	sprintf(pResult, "cmd /c netstat -ano|findstr \":%d \" > %s", nPort, pPortFilePath);

	//WinExec 执行cmd命令  
	WinExec(pResult, SW_HIDE);
	Sleep(450);

	//查找端口号  
	FILE *pPortFile = fopen(pPortFilePath, "r");
	if (pPortFile)
	{
		while (!feof(pPortFile))
		{
			memset(pResult, 0, sizeof(pResult));
			fread(pResult, sizeof(pResult), 1, pPortFile);
			pResult[sizeof(pResult) - 1] = 0x00;

			string strPortTmp = pResult;
			int offset = (int)strPortTmp.find_last_of(0x0A);
			if (offset > -1)
			{
				pResult[offset] = 0x00;
				strPortTmp = strPortTmp.substr(0, offset);
				if (!feof(pPortFile))
				{
					fseek(pPortFile, (long)(strPortTmp.length() + 1 - sizeof(pResult)), SEEK_CUR);
				}

				offset = (int)strPortTmp.find_first_of(':');
				if (offset > -1)
				{
					strPortTmp = strPortTmp.substr(offset + 1, 6);
					offset = (int)strPortTmp.find_last_not_of(' ');
					if (offset > -1)
					{
						strPortTmp = strPortTmp.substr(0, offset + 1);
						if (strPortTmp == pszPort)
						{
							strPortTmp = pResult;
							offset = (int)strPortTmp.find_last_of(' ');
							if (offset > -1)
							{
								strPortTmp = strPortTmp.substr(offset + 1);
								sprintf(pResult, "cmd /c tasklist /fi \"pid eq %s\" /nh> %s", strPortTmp.c_str(), pProcessFilePath);
								//根据端口号查找进程ID  
								WinExec(pResult, SW_HIDE);
								Sleep(450);

								FILE *pProcessFile = fopen(pProcessFilePath, "r");
								if (pProcessFile)
								{
									while (!feof(pProcessFile))
									{
										memset(pResult, 0, sizeof(pResult));
										fread(pResult, sizeof(pResult), 1, pProcessFile);
										pResult[sizeof(pResult) - 1] = 0x00;

										string strProcessTmp = pResult;
										int offset = (int)strProcessTmp.find_last_of(0x0A);
										if (offset > -1)
										{
											pResult[offset] = 0x00;
											strProcessTmp = strProcessTmp.substr(0, offset);
											if (!feof(pProcessFile))
											{
												fseek(pProcessFile, (long)(strProcessTmp.length() + 1 - sizeof(pResult)), SEEK_CUR);
											}

											if (0x0A == pResult[0])       //首行只有一个字符 0x0A  
											{
												strProcessTmp = pResult + 1;
											}
											else
											{
												strProcessTmp = pResult;
											}
											offset = (int)strProcessTmp.find_first_of(' ');
											if (offset > -1)
											{
												{
													{
														{
															{
																strProcessTmp = strProcessTmp.substr(0, offset);
																if ("" != strProcessTmp)
																{
																	//查找成功，结束  
																	strResult += "[" + strProcessTmp + "]";
																	bSuc = true;
																}
																continue;
															}
														}
													}
												}
											}
										}
									}
									fclose(pProcessFile);
								}
								sprintf(pResult, "cmd /c del %s", pProcessFilePath);
								WinExec(pResult, SW_HIDE);
								if (bSuc) { continue; }
							}
						}
					}
				}
			}
		}
		fclose(pPortFile);
	}

	if (!bSuc) { strResult = ""; };
	sprintf(pResult, "cmd /c del %s", pPortFilePath);
	WinExec(pResult, SW_HIDE);

	return bSuc;
}

int CPortTest::Test()
{
	string str = "";

	for (int port = 79;port < 65536;port++)
	{
		cout << "处理端口：" << port << endl;

		str = "";
		GetProcNameByPort(port, str);

		if (str != "")
			printf("_%s_\n", str.c_str());
	}

	printf("____End____");

	getchar();
	return 0;
}


