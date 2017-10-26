// WinMsg.cpp : ����Ӧ�ó������ڵ㡣
//

#include "stdafx.h"
#include "WinMsg.h"
#include <CommCtrl.h>

#include <atlconv.h>

#define MAX_LOADSTRING 100

// ȫ�ֱ���: 
HINSTANCE hInst;                                // ��ǰʵ��
WCHAR szTitle[MAX_LOADSTRING];                  // �������ı�
WCHAR szWindowClass[MAX_LOADSTRING];            // ����������
HWND hWnd;
HWND hStatus;

HANDLE hFile;

// �˴���ģ���а����ĺ�����ǰ������: 
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

    // TODO: �ڴ˷��ô��롣

    // ��ʼ��ȫ���ַ���
    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
    LoadStringW(hInstance, IDC_WINMSG, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // ִ��Ӧ�ó����ʼ��: 
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_WINMSG));

	// ����־�ļ�
	wchar_t path[512] = { 0 };
	GetModuleFileName(NULL, path, 512);
	CString fileName = path;
	fileName += _T(".log");
	hFile = CreateFile(fileName, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, 0, OPEN_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);
	
	if (hFile == INVALID_HANDLE_VALUE)
	{
		MessageBox(NULL, _T("��־�ļ���ʧ��"), _T("Error"), MB_OK);
		return -1;
	}

	SetFilePointer(hFile, 0, 0, FILE_END);

    MSG msg;

    // ����Ϣѭ��: 
    while (GetMessage(&msg, nullptr, 0, 0))
    {
        if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
        {
            TranslateMessage(&msg);
            DispatchMessage(&msg);
        }
    }

	// �ر���־�ļ�
	CloseHandle(hFile);
	hFile = NULL;

    return (int) msg.wParam;
}



//
//  ����: MyRegisterClass()
//
//  Ŀ��: ע�ᴰ���ࡣ
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
//  ����: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  Ŀ��:    ���������ڵ���Ϣ��
//
//  WM_COMMAND  - ����Ӧ�ó���˵�
//  WM_PAINT    - ����������
//  WM_DESTROY  - �����˳���Ϣ������
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    switch (message)
    {
	case WM_KEYDOWN:
		{
			CStringA str;

			str.Format("\r\nWM_KEYDOWN�� %X - %d - %d [%c] - %d",hWnd,message,wParam,wParam,lParam);
			DWORD wlen = 0;
			if (!WriteFile(hFile, str.GetBuffer(), str.GetLength(), &wlen, NULL))
			{
				DWORD errCode = GetLastError();
				CString err;
				err.Format(_T("��־�ļ�д��ʧ�ܣ�%d", errCode));
				MessageBox(NULL, err, _T("Error"), MB_OK);
			}

			CString strw = CA2W(str);
			SetStatusText(hStatus, 2, strw.GetBuffer());
		}
    case WM_COMMAND:
        {
            int wmId = LOWORD(wParam);
            // �����˵�ѡ��: 
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

// �����ڡ������Ϣ�������
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




/*���ƣ�MakeToolBar

���ܣ�����BMP������

������HWND,HINSTANCE

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
//	ti.hwnd = hWnd;  //�������ľ��   
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
//	ti.lpszText = "����";
//
//	SendMessage(hTooltips, TTM_ADDTOOL, 0, (LPARAM)&ti);
//
//	ti.uId = IDM_STOP;
//
//	ti.lpszText = "ֹͣ";
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

/*���ƣ�MakeStatusbar
���ܣ�����״̬��
������HWND
*/
HWND MakeStatusbar(HWND hWnd)
{
	return CreateWindow(STATUSCLASSNAME, _T(""), WS_CHILD | WS_VISIBLE, 0, 0, 0, 0, hWnd, NULL, hInst, NULL);
}

/*���ƣ�SetStatus
���ܣ���״̬�����ֳɶ��
������hWnd�����ھ����hStatus״̬�����
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

/*���ƣ�SetStatusText
���ܣ�����״̬��ĳһ��ı���
������hStatus״̬�������i��i��txt����
*/
int SetStatusText(HWND hStatus, int i, CString txt)
{
	SendMessage(hStatus, SB_SETTEXT, (WPARAM)i, (LPARAM)txt.GetBuffer());
	return 1;
}

//
//   ����: InitInstance(HINSTANCE, int)
//
//   Ŀ��: ����ʵ�����������������
//
//   ע��: 
//
//        �ڴ˺����У�������ȫ�ֱ����б���ʵ�������
//        ��������ʾ�����򴰿ڡ�
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
	hInst = hInstance; // ��ʵ������洢��ȫ�ֱ�����

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
