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

	BOOL CheckNetConnect();
	void ClearInternet();
	CAtlString GetFileContent(CAtlString url);

	// 获取 strUrl 中的域名
	CAtlStringA GetHostA(CAtlStringA strUrl);

	// 获取 strUrl 中域名后的部分
	CAtlStringA GetParamA(CAtlStringA strUrl);

	// 获取 hostent 结构指针
	hostent* GetHostentA(CAtlStringA strUrl);

	// 获取 http 请求头
	// method: 请求方式
	// param: 域名后的部分，以 / 开通
	// host: 域名
	// other: 加到标准请求头的其他内容
	CAtlStringA GetHttpHeadA(CAtlStringA method, CAtlStringA param, CAtlStringA host, CAtlStringA other);
	CAtlStringW GetHttpHeadW(CAtlStringW method, CAtlStringW param, CAtlStringW host, CAtlStringW other);

	// 获取 strUrl 页面的 html
	CAtlStringA GetHtmlA(CAtlStringA strUrl);

	HINTERNET m_hi;
	HINTERNET m_hUrl;
};

