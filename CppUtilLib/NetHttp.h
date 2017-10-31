#pragma once
#include "NetTcp.h"
#include <SensAPI.h>
#include <ShlObj.h>
#include <WinInet.h>

#pragma comment(lib,"Wininet.lib")
#pragma comment(lib,"Sensapi.lib")

class CNetHttp : public CNetTcp
{
public:
	CNetHttp();
	~CNetHttp();

	// ��ȡ strUrl ָ�������
	char* GetFileContent(char* strUrl);

	// ��ȡ strUrl �е�����
	char* GetHostA(char* strUrl);

	// ��ȡ strUrl ��������Ĳ���
	char* GetParamA(char* strUrl);

	// ��ȡ hostent �ṹָ��
	hostent* GetHostentA(char* strUrl);

	// ��ȡ http ����ͷ
	// method: ����ʽ
	// param: ������Ĳ��֣��� / ��ͨ
	// host: ����
	// other: �ӵ���׼����ͷ����������
	char* GetHttpHeadA(char* method, char* param, char* host, char* other);
	wchar_t* GetHttpHeadW(wchar_t* method, wchar_t* param, wchar_t* host, wchar_t* other);

	// ��ȡ strUrl ҳ��� html
	char* GetHtmlA(char* strUrl);

private:
	HINTERNET m_hi;
	HINTERNET m_hUrl;

	BOOL CheckNetConnect();
	void ClearInternet();
};

