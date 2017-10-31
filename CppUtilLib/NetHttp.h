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

	// 获取 strUrl 指向的内容
	char* GetFileContent(char* strUrl);

	// 获取 strUrl 中的域名
	char* GetHostA(char* strUrl);

	// 获取 strUrl 中域名后的部分
	char* GetParamA(char* strUrl);

	// 获取 hostent 结构指针
	hostent* GetHostentA(char* strUrl);

	// 获取 http 请求头
	// method: 请求方式
	// param: 域名后的部分，以 / 开通
	// host: 域名
	// other: 加到标准请求头的其他内容
	char* GetHttpHeadA(char* method, char* param, char* host, char* other);
	wchar_t* GetHttpHeadW(wchar_t* method, wchar_t* param, wchar_t* host, wchar_t* other);

	// 获取 strUrl 页面的 html
	char* GetHtmlA(char* strUrl);

private:
	HINTERNET m_hi;
	HINTERNET m_hUrl;

	BOOL CheckNetConnect();
	void ClearInternet();
};

