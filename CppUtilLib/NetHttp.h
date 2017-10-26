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

	// ��ȡ strUrl �е�����
	CAtlStringA GetHostA(CAtlStringA strUrl);

	// ��ȡ strUrl ��������Ĳ���
	CAtlStringA GetParamA(CAtlStringA strUrl);

	// ��ȡ hostent �ṹָ��
	hostent* GetHostentA(CAtlStringA strUrl);

	// ��ȡ http ����ͷ
	// method: ����ʽ
	// param: ������Ĳ��֣��� / ��ͨ
	// host: ����
	// other: �ӵ���׼����ͷ����������
	CAtlStringA GetHttpHeadA(CAtlStringA method, CAtlStringA param, CAtlStringA host, CAtlStringA other);
	CAtlStringW GetHttpHeadW(CAtlStringW method, CAtlStringW param, CAtlStringW host, CAtlStringW other);

	// ��ȡ strUrl ҳ��� html
	CAtlStringA GetHtmlA(CAtlStringA strUrl);

	HINTERNET m_hi;
	HINTERNET m_hUrl;
};

