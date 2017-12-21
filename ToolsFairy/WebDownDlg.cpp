// WebDownDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "ToolsFairy.h"
#include "WebDownDlg.h"
#include "afxdialogex.h"
#include "SysPath.h"
#include "NetHttp.h"
#include "AppSet.h"
#include "TimeUtil.h"

#include <process.h>
#include <ShlObj.h>
#pragma comment(lib,"Shell32.lib")

#include <boost\regex.hpp>
using namespace boost;

// CWebDownDlg 对话框

IMPLEMENT_DYNAMIC(CWebDownDlg, CDialogEx)

CWebDownDlg::CWebDownDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_DIALOG_WEB_DOWN, pParent)
{

}

CWebDownDlg::~CWebDownDlg()
{
}

void CWebDownDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT_DIR_URL, m_cEditDirUrl);
	DDX_Control(pDX, IDC_LIST_DOWN, m_cListDown);
	DDX_Control(pDX, IDC_EDIT_MSG, m_cEditMsg);
}


BEGIN_MESSAGE_MAP(CWebDownDlg, CDialogEx)
	ON_BN_CLICKED(IDC_BUTTON_DOWN, &CWebDownDlg::OnBnClickedButtonDown)
	ON_BN_CLICKED(IDC_BUTTON_GET_DIR, &CWebDownDlg::OnBnClickedButtonGetDir)
	ON_COMMAND(ID_WEB_DOWN_OPEN, &CWebDownDlg::OnWebDownOpen)
END_MESSAGE_MAP()


// CWebDownDlg 消息处理程序

BOOL CWebDownDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	InitListCtrl();
	m_cEditDirUrl.SetWindowTextW(_T("http://www.zineworm.com/71/71063/"));

	return TRUE;  // return TRUE unless you set the focus to a control
				  // 异常: OCX 属性页应返回 FALSE
}

void CWebDownDlg::InitListCtrl()
{
	// 通过 GetWindowLong 来获取 CListCtrl 已有的样式 
	DWORD dwStyle = GetWindowLong(m_cListDown.m_hWnd, GWL_STYLE);

	// 在原有样式的基本上，添加 LVS_REPORT 扩展样式 
	SetWindowLong(m_cListDown.m_hWnd, GWL_STYLE, dwStyle | LVS_REPORT);

	// 获取已有的扩展样式 
	DWORD dwStyles = m_cListDown.GetExStyle();

	// 取消复选框样式 
	dwStyles &= ~LVS_EX_CHECKBOXES;

	// 添加整行选择和表格线扩展样式 
	m_cListDown.SetExtendedStyle(dwStyles | LVS_EX_FULLROWSELECT | LVS_EX_GRIDLINES);

	// 设置列
	m_cListDown.InsertColumn(0, _T("URL"), LVCFMT_LEFT, 400);
	m_cListDown.InsertColumn(1, _T("标题"), LVCFMT_LEFT, 400);
	m_cListDown.InsertColumn(2, _T("状态"), LVCFMT_LEFT, 100);
}



void CWebDownDlg::OnBnClickedButtonGetDir()
{
	CString url;
	m_cEditDirUrl.GetWindowTextW(url);
	CAtlStringA urla = CW2A(url);

	// 获取 host
	regex regHost("http://.*?/");
	cmatch mHost;
	regex_search(urla.GetBuffer(), mHost, regHost);
	CAtlStringA host = mHost.str().c_str();
	host.Delete(host.GetLength() - 1, 1);

	// 获取目录列表
	CNetHttp http;
	CAtlStringA txt = http.GetHtmlByDownFileA(urla);

	regex reg("<li><a.*?>.*?</a></li>");
	cmatch m;

	vector<CAtlStringA> vtList;

	char* pszTmp = txt.GetBuffer();
	while (regex_search(pszTmp, m, reg))
	{
		for (cmatch::iterator it = m.begin();it < m.end();it++)
		{
			vtList.push_back(it->str().c_str());
			pszTmp = (char*)it->second;
		}
	}

	// 获取标题
	CAtlStringA title;
	pszTmp = txt.GetBuffer();
	CAtlStringA titleRegex = "<meta property=\"og:novel:book_name\" content=\".*?\"/>";
	regex regTitle(titleRegex);
	cmatch mTitle;
	if (regex_search(pszTmp, mTitle, regTitle))
	{
		title = mTitle.str().c_str();

		CAtlStringA startTitleTag = "content=\"";
		CAtlStringA endTitleTag = "\"/>";
		title = title.Right(title.GetLength() - title.Find(startTitleTag) - startTitleTag.GetLength());
		title = title.Left(title.Find(endTitleTag));
	}
	m_strBookName = CA2W(title);

	// 处理目录列表
	CTimeUtil timeUtil;

	m_strSavePath = CAppSet::currentPath + m_strBookName + _T("\\") + timeUtil.GetTimeString() + _T("\\");
	SHCreateDirectoryEx(NULL, m_strSavePath, NULL);

	m_strBookCfgFile = m_strSavePath + _T("list.txt");
	HANDLE hFile = CreateFile(m_strBookCfgFile, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, NULL, OPEN_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);

	if (hFile == INVALID_HANDLE_VALUE)
	{
		MessageBox(_T("book 配置文件创建失败"));
		return;
	}

	for (int i = 0;i < vtList.size();i++)
	{
		CAtlStringA html = vtList.at(i);

		int idx = html.Find("\">");
		CAtlStringA url = html.Left(idx);
		url = url.Right(url.GetLength() - url.ReverseFind('\"') - 1);
		url = host + url;

		CAtlStringA chapterTitle = html.Right(html.GetLength() - idx - 2);
		chapterTitle = chapterTitle.Left(chapterTitle.Find("<"));

		int row = m_cListDown.GetItemCount();
		m_cListDown.InsertItem(row, CA2W(url));
		m_cListDown.SetItemText(row, 1, CA2W(chapterTitle));
		m_cListDown.SetItemText(row, 2, _T("待下载"));

		CAtlStringA cfgTxt = url + "\r\n" + title + "\r\n待下载\r\n";
		DWORD wlen = 0;
		WriteFile(hFile, cfgTxt.GetBuffer(), cfgTxt.GetLength(), &wlen, NULL);
	}

	CloseHandle(hFile);
}

UINT __stdcall WorkThread(LPVOID lpParam)
{
	CWebDownDlg* pThis = (CWebDownDlg*)lpParam;

	CTimeUtil timeUtil;
	CAtlString fileName = pThis->m_strBookName + _T("_") + timeUtil.GetTimeString() + _T(".txt");
	CAtlString fileFullName = CAppSet::currentPath + fileName;
	HANDLE hFile = CreateFile(fileFullName, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, NULL, OPEN_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);

	if (hFile == INVALID_HANDLE_VALUE)
	{
		MessageBox(NULL,_T("创建文件失败：") + fileFullName,NULL,MB_OK);
		return -1;
	}

	int retry = 0;

	for (int i = 0;i < pThis->m_cListDown.GetItemCount();i++)
	{
		CAtlString url = pThis->m_cListDown.GetItemText(i, 0);
		CAtlStringA urla = CW2A(url);
		CAtlString title = pThis->m_cListDown.GetItemText(i, 1);

		CNetHttp netHttp;
		CAtlStringA html = netHttp.GetHtmlByDownFileA(urla);

		if (html.IsEmpty())
		{
			if (retry < 3)
			{
				retry++;
				i--;
				continue;
			}

			retry = 0;
			pThis->m_cListDown.SetItemText(i, 2, _T("下载失败"));
			pThis->PushErrorDown(i);
		}
		else
		{
			CAtlStringA startTag = "<div class=\"inner mt\" id=\"inner\">";
			CAtlStringA endTag = "</div>";

			int idx = html.Find(startTag);
			CAtlStringA txt = html.Right(html.GetLength() - idx - startTag.GetLength());
			txt = txt.Left(txt.Find(endTag));
			string strTxt = txt;

			regex reg("<br />|&nbsp;");
			string newTxt = regex_replace(strTxt, reg, "", match_default | format_all);

			DWORD wlen = 0;
			if (WriteFile(hFile, newTxt.c_str(), newTxt.length(), &wlen, NULL))
			{
				pThis->m_cListDown.SetItemText(i, 2, _T("下载成功"));
			}
			else
			{
				pThis->m_cListDown.SetItemText(i, 2, _T("保存失败"));
			}
		}
	}

	CloseHandle(hFile);
	MessageBox(NULL,_T("下载完成"),NULL,MB_OK);

	return 0;
}


void CWebDownDlg::OnBnClickedButtonDown()
{
	UINT tid = 0;
	_beginthreadex(NULL, 0, WorkThread, this, 0, &tid);
}


void CWebDownDlg::OnWebDownOpen()
{
	
}

void CWebDownDlg::PushErrorDown(int row)
{
	CString txt;
	m_cEditMsg.GetWindowTextW(txt);
	
	CString tmp;
	tmp.Format(_T("%d,"), row);

	txt += tmp;
	m_cEditMsg.SetWindowTextW(txt);
}

