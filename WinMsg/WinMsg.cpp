// WinMsg.cpp : 定义应用程序的入口点。
//

#include "stdafx.h"
#include "WinMsg.h"
#include <CommCtrl.h>

#include <atlconv.h>

#define MAX_LOADSTRING 100

// 全局变量: 
HINSTANCE hInst;                                // 当前实例
WCHAR szTitle[MAX_LOADSTRING];                  // 标题栏文本
WCHAR szWindowClass[MAX_LOADSTRING];            // 主窗口类名
HWND hWnd;
HWND hStatus;

HANDLE hFile;

// 此代码模块中包含的函数的前向声明: 
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);

HWND MakeStatusbar(HWND hWnd);
int SetStatus(HWND hWnd, HWND hStatus);
int SetStatusText(HWND hStatus, int i, CString txt);

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    // TODO: 在此放置代码。

    // 初始化全局字符串
    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
    LoadStringW(hInstance, IDC_WINMSG, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // 执行应用程序初始化: 
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_WINMSG));

	// 打开日志文件
	wchar_t path[512] = { 0 };
	GetModuleFileName(NULL, path, 512);
	CString fileName = path;
	fileName += _T(".log");
	hFile = CreateFile(fileName, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, 0, OPEN_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);
	
	if (hFile == INVALID_HANDLE_VALUE)
	{
		MessageBox(NULL, _T("日志文件打开失败"), _T("Error"), MB_OK);
		return -1;
	}

	SetFilePointer(hFile, 0, 0, FILE_END);

    MSG msg;

    // 主消息循环: 
    while (GetMessage(&msg, nullptr, 0, 0))
    {
        if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
        {
            TranslateMessage(&msg);
            DispatchMessage(&msg);
        }
    }

	// 关闭日志文件
	CloseHandle(hFile);
	hFile = NULL;

    return (int) msg.wParam;
}



//
//  函数: MyRegisterClass()
//
//  目的: 注册窗口类。
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
    WNDCLASSEXW wcex;

    wcex.cbSize = sizeof(WNDCLASSEX);

    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = WndProc;
    wcex.cbClsExtra     = 0;
    wcex.cbWndExtra     = 0;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_WINMSG));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_WINMSG);
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

    return RegisterClassExW(&wcex);
}



union KeyDownLParam
{
	LPARAM lParam;
	struct MyStruct
	{
		byte n1[16];
		byte n2[8];
		byte n3;
		byte n4[4];
		byte n5;
		byte n6;
		byte n7;

		CStringA ToString()
		{
			CStringA str;
			str.Format("%d %d %d %d %d %d %d", n1, n2, n3, n4, n5, n6, n7);
			return str;
		}
	} sp;
};

//
//  函数: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  目的:    处理主窗口的消息。
//
//  WM_COMMAND  - 处理应用程序菜单
//  WM_PAINT    - 绘制主窗口
//  WM_DESTROY  - 发送退出消息并返回
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    switch (message)
    {
	case WM_KEYDOWN:
		{
			CStringA str;

			str.Format("\r\nWM_KEYDOWN： %X - %d - %d [%c] - %d",hWnd,message,wParam,wParam,lParam);
			DWORD wlen = 0;
			if (!WriteFile(hFile, str.GetBuffer(), str.GetLength(), &wlen, NULL))
			{
				DWORD errCode = GetLastError();
				CString err;
				err.Format(_T("日志文件写入失败：%d", errCode));
				MessageBox(NULL, err, _T("Error"), MB_OK);
			}

			CString strw = CA2W(str);
			SetStatusText(hStatus, 2, strw.GetBuffer());
		}
    case WM_COMMAND:
        {
            int wmId = LOWORD(wParam);
            // 分析菜单选择: 
            switch (wmId)
            {
            case IDM_ABOUT:
                DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
                break;
            case IDM_EXIT:
                DestroyWindow(hWnd);
                break;
            default:
                return DefWindowProc(hWnd, message, wParam, lParam);
            }
        }
        break;
    case WM_PAINT:
        {
            PAINTSTRUCT ps;
            HDC hdc = BeginPaint(hWnd, &ps);
            
            EndPaint(hWnd, &ps);
        }
        break;
	case WM_SIZE:
	{
		hStatus = MakeStatusbar(hWnd);
		SetStatus(hWnd, hStatus);
	}
	break;
    case WM_DESTROY:
        PostQuitMessage(0);
        break;
    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
    }
    return 0;
}

// “关于”框的消息处理程序。
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
    UNREFERENCED_PARAMETER(lParam);
    switch (message)
    {
    case WM_INITDIALOG:
        return (INT_PTR)TRUE;

    case WM_COMMAND:
        if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
        {
            EndDialog(hDlg, LOWORD(wParam));
            return (INT_PTR)TRUE;
        }
        break;
    }
    return (INT_PTR)FALSE;
}




/*名称：MakeToolBar

功能：创建BMP工具栏

参数：HWND,HINSTANCE

*/

//HWND MakeToolbar(HWND hWnd, HINSTANCE hInst)
//{
//	HWND    hTOOL;
//
//	TBBUTTON tbb[3];
//
//	HIMAGELIST hImageList;
//
//	HBITMAP      hBitmap;
//
//
//
//	hTOOL = CreateWindowEx(0, TOOLBARCLASSNAME, (LPCTSTR)NULL, WS_CHILD | WS_BORDER | WS_VISIBLE | TBSTYLE_TOOLTIPS, 0, 0, 0, 0, hWnd, (HMENU)ID_TOOLBAR, hInst, NULL);
//
//	SendMessage(hTOOL, TB_SETSTYLE, 0, (LPARAM)TBSTYLE_FLAT | CCS_TOP);
//
//	SendMessage(hTOOL, TB_BUTTONSTRUCTSIZE, (WPARAM) sizeof(TBBUTTON), 0);
//
//
//
//	hImageList = ImageList_Create(32, 30, ILC_COLOR24, 2, 1);
//
//	hBitmap = (HBITMAP)LoadImage(hInst, "toolbar.bmp", IMAGE_BITMAP, 64, 30, LR_LOADFROMFILE);
//
//	ImageList_Add(hImageList, hBitmap, NULL);
//
//	DeleteObject(hBitmap);
//
//	SendMessage(hTOOL, TB_SETIMAGELIST, 0, (LPARAM)hImageList);
//
//
//
//	ZeroMemory(tbb, sizeof(tbb));
//
//	tbb[0].iBitmap = MAKELONG(0, 0);//MAKELONG(index,0);
//
//	tbb[0].idCommand = IDM_SAVE;
//
//	tbb[0].fsState = TBSTATE_ENABLED;
//
//	tbb[0].fsStyle = TBSTYLE_FLAT;
//
//	tbb[0].iString = (INT_PTR)TEXT("");
//
//	tbb[1].iBitmap = MAKELONG(1, 0);//MAKELONG(index,0);
//
//	tbb[1].idCommand = IDM_SAVE;
//
//	tbb[1].fsState = TBSTATE_ENABLED;
//
//	tbb[1].fsStyle = TBSTYLE_FLAT;
//
//	tbb[1].iString = (INT_PTR)TEXT("");
//
//	tbb[2].iBitmap = 0
//
//		tbb[2].idCommand = 0;
//
//	tbb[2].fsState = TBSTATE_ENABLED;
//
//	tbb[2].fsStyle = TBSTYLE_SEP;
//
//	tbb[2].iString = (INT_PTR)TEXT("");
//
//	SendMessage(hTOOL, TB_ADDBUTTONS, 3, (LPARAM)&tbb);
//
//	ShowWindow(hTOOL, SW_SHOW);
//
//
//
//	MakeTooltips(hTOOL, hInst);
//
//	return hTOOL;
//
//}

//int MakeTooltips(HWND hWnd, HINSTANCE hInst)
//
//{
//
//	HWND           hTooltips;
//
//	INITCOMMONCONTROLSEX icex;
//
//	TOOLINFO    ti;
//
//
//
//	// Load the ToolTip class from the DLL.
//
//	icex.dwSize = sizeof(icex);
//
//	icex.dwICC = ICC_BAR_CLASSES;
//
//	ti.cbSize = sizeof(TOOLINFO);
//
//	ti.uFlags = TTF_SUBCLASS;
//
//	ti.hwnd = hWnd;  //工具栏的句柄   
//
//	ti.hinst = hInst;
//
//	ti.rect.left = 0;
//
//	ti.rect.top = 0;
//
//	ti.rect.bottom = 0;
//
//	ti.rect.right = 0;
//
//
//
//
//
//	hTooltips = CreateWindowEx(WS_EX_TOPMOST, TOOLTIPS_CLASS, NULL,
//
//		WS_POPUP | TTS_NOPREFIX | TTS_ALWAYSTIP,
//
//		CW_USEDEFAULT, CW_USEDEFAULT,
//
//		CW_USEDEFAULT, CW_USEDEFAULT,
//
//		hWnd, NULL, hInst,
//
//		NULL);
//
//	SetWindowPos(hTooltips, HWND_TOPMOST, 0, 0, 0, 0,
//
//		SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
//
//
//
//	ti.uId = IDM_START;
//
//	ti.lpszText = "启动";
//
//	SendMessage(hTooltips, TTM_ADDTOOL, 0, (LPARAM)&ti);
//
//	ti.uId = IDM_STOP;
//
//	ti.lpszText = "停止";
//
//	SendMessage(hTooltips, TTM_ADDTOOL, 0, (LPARAM)&ti);
//
//
//
//	SendMessage(hWnd, TB_SETTOOLTIPS, (WPARAM)hTooltips, 0);
//
//	return 1;
//
//}

/*名称：MakeStatusbar
功能：创建状态栏
参数：HWND
*/
HWND MakeStatusbar(HWND hWnd)
{
	return CreateWindow(STATUSCLASSNAME, _T(""), WS_CHILD | WS_VISIBLE, 0, 0, 0, 0, hWnd, NULL, hInst, NULL);
}

/*名称：SetStatus
功能：将状态栏划分成多格
参数：hWnd主窗口句柄，hStatus状态栏句柄
*/
int SetStatus(HWND hWnd, HWND hStatus)
{
	int Rightend[3];
	RECT WinRect;
	GetClientRect(hWnd, &WinRect);

	Rightend[0] = 100;
	Rightend[1] = 200;
	Rightend[2] = WinRect.right;

	SendMessage(hStatus, SB_SETPARTS, (WPARAM)3, (LPARAM)Rightend);

	return 1;
}

/*名称：SetStatusText
功能：设置状态栏某一格的标题
参数：hStatus状态栏句柄，i第i格，txt标题
*/
int SetStatusText(HWND hStatus, int i, CString txt)
{
	SendMessage(hStatus, SB_SETTEXT, (WPARAM)i, (LPARAM)txt.GetBuffer());
	return 1;
}

//
//   函数: InitInstance(HINSTANCE, int)
//
//   目的: 保存实例句柄并创建主窗口
//
//   注释: 
//
//        在此函数中，我们在全局变量中保存实例句柄并
//        创建和显示主程序窗口。
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
	hInst = hInstance; // 将实例句柄存储在全局变量中

	hWnd = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, nullptr, nullptr, hInstance, nullptr);

	if (!hWnd)
	{
		return FALSE;
	}

	hStatus = MakeStatusbar(hWnd);
	SetStatus(hWnd, hStatus);

	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);

	return TRUE;
}
