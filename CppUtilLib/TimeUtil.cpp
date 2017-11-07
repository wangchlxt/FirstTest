#include "stdafx.h"
#include "TimeUtil.h"


CTimeUtil::CTimeUtil()
{
}


CTimeUtil::~CTimeUtil()
{
}

CAtlStringA CTimeUtil::GetTimeStringA(CAtlStringA fmt)
{
	SYSTEMTIME m_time;
	GetSystemTime(&m_time);

	CAtlStringA strTime;
	strTime.Format(fmt, m_time.wYear, m_time.wMonth, m_time.wDay, m_time.wHour + 8, m_time.wMinute, m_time.wSecond, m_time.wMilliseconds);

	return strTime;

}

CAtlString CTimeUtil::GetTimeString(CAtlString fmt)
{
	SYSTEMTIME m_time;
	GetSystemTime(&m_time);

	CAtlString strTime;
	strTime.Format(fmt, m_time.wYear, m_time.wMonth, m_time.wDay, m_time.wHour + 8, m_time.wMinute, m_time.wSecond, m_time.wMilliseconds);

	return strTime;

}
