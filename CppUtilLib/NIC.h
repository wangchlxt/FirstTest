#pragma once
#include <vector>
using namespace std;

// ��������
struct _NICDataA
{
	// ����
	CAtlStringA name;

	// ����
	CAtlStringA desc;

	// mac ��ַ
	vector<WORD> vtMAC;

	// ip ��ַ
	CAtlStringA ip;

	// ��������
	CAtlStringA ipMask;

	// ����
	CAtlStringA gateway;

	/* ��ȡ mac ��ַ�ַ���
	 * fmt��mac ��ַ��ʽ���ַ��������磺%02X-%02X-%02X-%02X-%02X-%02X
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

	/* ��ȡ mac ��ַ�ַ���
	* fmt��mac ��ַ��ʽ���ַ��������磺%02X-%02X-%02X-%02X-%02X-%02X
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

// ���� Network Interface Card
class CNIC
{
public:
	CNIC();
	~CNIC();

	// ��ʼ����������
	int Init();

	vector<_NICDataA> m_vtNICA;
};

