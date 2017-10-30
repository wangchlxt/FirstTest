#pragma once
#include <vector>
using namespace std;

// 网卡数据
struct _NICDataA
{
	// 名称
	CAtlStringA name;

	// 描述
	CAtlStringA desc;

	// mac 地址
	vector<WORD> vtMAC;

	// ip 地址
	CAtlStringA ip;

	// 子网掩码
	CAtlStringA ipMask;

	// 网关
	CAtlStringA gateway;

	/* 获取 mac 地址字符串
	 * fmt：mac 地址格式化字符串，例如：%02X-%02X-%02X-%02X-%02X-%02X
	 */
	CAtlStringA GetMACStringA(CAtlStringA fmt)
	{
		CAtlStringA mac;
		int iFmtCharCount = 0;
		for (int i = 0; i < fmt.GetLength(); i++)
		{
			if (fmt.GetAt(i) == '%')
			{
				iFmtCharCount++;
			}
		}

		if (iFmtCharCount == 6 && vtMAC.size() == 6)
		{
			mac.Format(fmt, vtMAC[0], vtMAC[1], vtMAC[2], vtMAC[3], vtMAC[4], vtMAC[5]);
		}

		return mac;
	}

	/* 获取 mac 地址字符串
	* fmt：mac 地址格式化字符串，例如：%02X-%02X-%02X-%02X-%02X-%02X
	*/
	CAtlStringW GetMACStringW(CAtlStringW fmt)
	{
		CAtlStringW mac;
		int iFmtCharCount = 0;
		for (int i = 0; i < fmt.GetLength(); i++)
		{
			if (fmt.GetAt(i) == '%')
			{
				iFmtCharCount++;
			}
		}

		if (iFmtCharCount == 6 && vtMAC.size() == 6)
		{
			mac.Format(fmt, vtMAC[0], vtMAC[1], vtMAC[2], vtMAC[3], vtMAC[4], vtMAC[5]);
		}

		return mac;
	}
};

// 网卡 Network Interface Card
class CNIC
{
public:
	CNIC();
	~CNIC();

	// 初始化网卡数据
	int Init();

	vector<_NICDataA> m_vtNICA;
};

