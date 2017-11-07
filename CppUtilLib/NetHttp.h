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
	CAtlStringA GetFileContent(CAtlStringA strUrl);

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
	CAtlString GetHttpHeadW(CAtlString method, CAtlString param, CAtlString host, CAtlString other);

	// 获取 strUrl 页面的 html
	CAtlStringA GetHtmlA(CAtlStringA strUrl);

	// 获取 url 指向的 html (utf-8 没处理，会乱码)
	CAtlStringA GetHtmlByDownFileA(CAtlStringA url);

	// 获取本机外网 ip
	CAtlStringA GetHostNetIp();

	// 获取本机外网 ip，从百度获取
	CAtlStringA GetHostNetIpFormBaidu();

	// 获取本机外网 ip，从 ip138 获取
	CAtlStringA GetHostNetIpFormIP138();

	// 获取本机外网 ip 和城市，从 ip138 获取
	CAtlStringA GetHostNetIpAndCityFormIP138();

private:
	HINTERNET m_hi;
	HINTERNET m_hUrl;

	BOOL CheckNetConnect();
	void ClearInternet();
};

