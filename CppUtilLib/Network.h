#pragma once
#include <atlstr.h>

// 此类是从 Network.dll 导出的
class CNetwork {
public:
	CNetwork(void);
	
	// 初始化网络环境
	static int Init();

	// 清理网络环境
	static int UnInit();

	// 获取 url 内容
	CAtlString GetFileContent(CAtlString url);

	// 启动 ftp 服务
	void RunFtpServer();
};

