// WebDownDlg.cpp : ʵ���ļ�
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

// CWebDownDlg �Ի���

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


// CWebDownDlg ��Ϣ�������

BOOL CWebDownDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	InitListCtrl();
	m_cEditDirUrl.SetWindowTextW(_T("http://www.zineworm.com/71/71063/"));

	return TRUE;  // return TRUE unless you set the focus to a control
				  // �쳣: OCX ����ҳӦ���� FALSE
}

void CWebDownDlg::InitListCtrl()
{
	// ͨ�� GetWindowLong ����ȡ CListCtrl ���е���ʽ 
	DWORD dwStyle = GetWindowLong(m_cListDown.m_hWnd, GWL_STYLE);

	// ��ԭ����ʽ�Ļ����ϣ���� LVS_REPORT ��չ��ʽ 
	SetWindowLong(m_cListDown.m_hWnd, GWL_STYLE, dwStyle | LVS_REPORT);

	// ��ȡ���е���չ��ʽ 
	DWORD dwStyles = m_cListDown.GetExStyle();

	// ȡ����ѡ����ʽ 
	dwStyles &= ~LVS_EX_CHECKBOXES;

	// �������ѡ��ͱ������չ��ʽ 
	m_cListDown.SetExtendedStyle(dwStyles | LVS_EX_FULLROWSELECT | LVS_EX_GRIDLINES);

	// ������
	m_cListDown.InsertColumn(0, _T("URL"), LVCFMT_LEFT, 400);
	m_cListDown.InsertColumn(1, _T("����"), LVCFMT_LEFT, 400);
	m_cListDown.InsertColumn(2, _T("״̬"), LVCFMT_LEFT, 100);
}



void CWebDownDlg::OnBnClickedButtonGetDir()
{
	CString url;
	m_cEditDirUrl.GetWindowTextW(url);
	CAtlStringA urla = CW2A(url);

	// ��ȡ host
	regex regHost("http://.*?/");
	cmatch mHost;
	regex_search(urla.GetBuffer(), mHost, regHost);
	CAtlStringA host = mHost.str().c_str();
	host.Delete(host.GetLength() - 1, 1);

	// ��ȡĿ¼�б�
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

	// ��ȡ����
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

	// ����Ŀ¼�б�
	CTimeUtil timeUtil;

	m_strSavePath = CAppSet::currentPath + m_strBookName + _T("\\") + timeUtil.GetTimeString() + _T("\\");
	SHCreateDirectoryEx(NULL, m_strSavePath, NULL);

	m_strBookCfgFile = m_strSavePath + _T("list.txt");
	HANDLE hFile = CreateFile(m_strBookCfgFile, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, NULL, OPEN_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);

	if (hFile == INVALID_HANDLE_VALUE)
	{
		MessageBox(_T("book �����ļ�����ʧ��"));
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
		m_cListDown.SetItemText(row, 2, _T("������"));

		CAtlStringA cfgTxt = url + "\r\n" + title + "\r\n������\r\n";
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
		MessageBox(NULL,_T("�����ļ�ʧ�ܣ�") + fileFullName,NULL,MB_OK);
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
			pThis->m_cListDown.SetItemText(i, 2, _T("����ʧ��"));
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
				pThis->m_cListDown.SetItemText(i, 2, _T("���سɹ�"));
			}
			else
			{
				pThis->m_cListDown.SetItemText(i, 2, _T("����ʧ��"));
			}
		}
	}

	CloseHandle(hFile);
	MessageBox(NULL,_T("�������"),NULL,MB_OK);

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

