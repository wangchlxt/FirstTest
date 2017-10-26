#include "stdafx.h"
#include "NIC.h"
#include <iphlpapi.h>
#pragma comment(lib,"Iphlpapi.lib")

CNIC::CNIC()
{
}


CNIC::~CNIC()
{
}

int CNIC::Init()
{
	PIP_ADAPTER_INFO pAdapterInfo;
	PIP_ADAPTER_INFO pAdapter = NULL;
	ULONG ulOutBufLen = sizeof(IP_ADAPTER_INFO);

	pAdapterInfo = (PIP_ADAPTER_INFO)malloc(ulOutBufLen);
	DWORD dwRetVal = GetAdaptersInfo(pAdapterInfo, &ulOutBufLen);

	// 第一次调用GetAdapterInfo获取ulOutBufLen大小
	if (dwRetVal == ERROR_BUFFER_OVERFLOW)
	{
		free(pAdapterInfo);
		pAdapterInfo = (IP_ADAPTER_INFO *)malloc(ulOutBufLen);
		dwRetVal = GetAdaptersInfo(pAdapterInfo, &ulOutBufLen);
	}

	if (dwRetVal == NO_ERROR)
	{
		pAdapter = pAdapterInfo;
		while (pAdapter)
		{
			_NICDataA nic;

			nic.name = pAdapter->AdapterName;
			nic.desc = pAdapter->Description;
			nic.ip = pAdapter->IpAddressList.IpAddress.String;
			nic.ipMask = pAdapter->IpAddressList.IpMask.String;
			nic.gateway = pAdapter->GatewayList.IpAddress.String;

			for (int i = 0; i < 6; i++)
			{
				nic.vtMAC.push_back(pAdapter->Address[i]);
			}

			m_vtNICA.push_back(nic);

			pAdapter = pAdapter->Next;
		}
	}

	free(pAdapterInfo);

	return 0;
}
