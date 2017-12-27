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

// regex_match() 函数，当整个字符串完全匹配正则表达式时，返回 true，否则返回 false
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
		cout << "匹配失败" << endl;
	}
}

// regex_search() 函数，当字符串中有部分匹配正则表达式时，返回 true，匹配不到时，返回 false
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
		&nbsp;&nbsp;&nbsp;&nbsp;第001章 我叫宇智波玄<br />\
		<br />\
		&nbsp;&nbsp;&nbsp;&nbsp;我叫陆玄，我万万没想到我竟然穿越了。×杂∮志∮虫×而且还是这么一个坑爹的世界。<br />\
		<br />\
		&nbsp;&nbsp;&nbsp;&nbsp;我这一世的父母，在我出生不久，就在战场上被人给砍了，我也顺利的成了孤儿。<br />\
		<br />\
		&nbsp;&nbsp;&nbsp;&nbsp;我现在的名字是宇智波玄，今年三岁，被收养在大伯家，有一个堂哥叫宇智波斑，还有一个和我同岁的小屁孩，叫宇智波泉奈，除了这两个，还有三个比我小的，叫宇智波飞、宇智波痕和宇智波良。<br />\
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

