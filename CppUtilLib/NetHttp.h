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
	CAtlStringA GetFileContent(CAtlStringA strUrl);

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
	CAtlString GetHttpHeadW(CAtlString method, CAtlString param, CAtlString host, CAtlString other);

	// ��ȡ strUrl ҳ��� html
	CAtlStringA GetHtmlA(CAtlStringA strUrl);

	// ��ȡ url ָ��� html (utf-8 û����������)
	CAtlStringA GetHtmlByDownFileA(CAtlStringA url);

	// ��ȡ�������� ip
	CAtlStringA GetHostNetIp();

	// ��ȡ�������� ip���ӰٶȻ�ȡ
	CAtlStringA GetHostNetIpFormBaidu();

	// ��ȡ�������� ip���� ip138 ��ȡ
	CAtlStringA GetHostNetIpFormIP138();

	// ��ȡ�������� ip �ͳ��У��� ip138 ��ȡ
	CAtlStringA GetHostNetIpAndCityFormIP138();

private:
	HINTERNET m_hi;
	HINTERNET m_hUrl;

	BOOL CheckNetConnect();
	void ClearInternet();
};

