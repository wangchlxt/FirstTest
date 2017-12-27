#include "stdafx.h"
#include "RegexTest.h"
#include "FileUtil.h"

#include <iostream>
using namespace std;

#include "boost\regex.hpp"
using namespace boost;

CRegexTest::CRegexTest()
{
}

CRegexTest::~CRegexTest()
{
}

void CRegexTest::Test()
{
	/*Test_regex_match();
	Test_regex_search();
	Test_regex_replace();*/
	Test_regex_split();
}

// regex_match() �������������ַ�����ȫƥ��������ʽʱ������ true�����򷵻� false
void CRegexTest::Test_regex_match()
{
	// test 1
	char* pszTmp = "chi6naen 8Glis7h";
	regex reg1(".*");
	cmatch m1;
	if (regex_match(pszTmp, m1, reg1))
	{
		for (cmatch::iterator it = m1.begin();it != m1.end();it++)
		{
			cout << *it << endl;
		}
	}

	// test 2
	char* pszUrl = "http://www.zineworm.com/71/71063/http://www.cnblogs.com/hjdx/archive/2013/03/07/2948130.html";
	regex reg("http://.*?/");
	cmatch m;

	if (regex_match(pszUrl, m, reg))
	{
		for (cmatch::iterator it = m.begin();it != m.end();it++)
		{
			cout << it->first << ' ' << it->second << ' ' << *it << endl;
		}
	}
	else
	{
		cout << "ƥ��ʧ��" << endl;
	}
}

// regex_search() ���������ַ������в���ƥ��������ʽʱ������ true��ƥ�䲻��ʱ������ false
void CRegexTest::Test_regex_search()
{
	char* pszUrl = "http://www.zineworm.com/71/71063/http://www.cnblogs.com/hjdx/archive/2013/03/07/2948130.html";
	regex reg("http://.*?/");
	cmatch m;

	while (regex_search(pszUrl, m, reg))
	{
		for (cmatch::iterator it = m.begin();it != m.end();it++)
		{
			cout << it->str() << endl;
			pszUrl = (char*)it->second;
		}
	}
}

void CRegexTest::Test_regex_replace()
{
	string strTxt = "\
		&nbsp;&nbsp;&nbsp;&nbsp;��001�� �ҽ����ǲ���<br />\
		<br />\
		&nbsp;&nbsp;&nbsp;&nbsp;�ҽ�½����������û�뵽�Ҿ�Ȼ��Խ�ˡ����ӡ�־�ӳ�����һ�����ôһ���ӵ������硣<br />\
		<br />\
		&nbsp;&nbsp;&nbsp;&nbsp;����һ���ĸ�ĸ�����ҳ������ã�����ս���ϱ��˸����ˣ���Ҳ˳���ĳ��˹¶���<br />\
		<br />\
		&nbsp;&nbsp;&nbsp;&nbsp;�����ڵ����������ǲ������������꣬�������ڴ󲮼ң���һ���ø�����ǲ��ߣ�����һ������ͬ���Сƨ���������ǲ�Ȫ�Σ�������������������������С�ģ������ǲ��ɡ����ǲ��ۺ����ǲ�����<br />\
		<br />";
		
	regex reg("<br />|&nbsp;");
	string newTxt = regex_replace(strTxt, reg, "", match_default | format_all);

	cout << newTxt << endl;
}

void CRegexTest::Test_regex_split()
{
	string flx = "QRequestBlock";
	string txt = "QRequestBlock^^1,10,19,22,25,77,90";
	string head = txt.substr(0, flx.length() + 2);
	cout << head << endl;

	txt.replace(0, head.length(), "");
	cout << txt << endl;

	list<string> listIds;
	regex reg(",",boost::regbase::normal | boost::regbase::icase);
	regex_split(back_inserter(listIds), txt, reg);

	for (list<string>::iterator it = listIds.begin();it != listIds.end();it++)
	{
		cout << *it << endl;
	}
}

