#include "stdafx.h"
#include "LogTest.h"

#include "LogUtil.h"

#include "stdafx.h"
#include <stdio.h>
#include <iostream>

#include <boost/log/core.hpp>
#include <boost/log/trivial.hpp>
#include <boost/log/expressions.hpp>
#include <boost/log/sinks/text_file_backend.hpp>
#include <boost/log/utility/setup/file.hpp>
#include <boost/log/utility/setup/common_attributes.hpp>
#include <boost/log/sources/severity_logger.hpp>
#include <boost/log/sources/record_ostream.hpp>

namespace logging = boost::log;
namespace src = boost::log::sources;
namespace sinks = boost::log::sinks;
namespace keywords = boost::log::keywords;

namespace logging = boost::log;
namespace src = boost::log::sources;
namespace sinks = boost::log::sinks;
namespace keywords = boost::log::keywords;

using namespace std;

CLogTest::CLogTest()
{
}


CLogTest::~CLogTest()
{
}

void CLogTest::Test()
{
	//TestPrintToConsole();
	//TestPrintToFile();
	TestCLogUtil();
}

void CLogTest::TestPrintToConsole()
{
	cout << "hello, world" << endl;
	BOOST_LOG_TRIVIAL(trace) << "A trace severity message";
	BOOST_LOG_TRIVIAL(debug) << "A debug severity message";
	BOOST_LOG_TRIVIAL(info) << "An informational severity message";
	BOOST_LOG_TRIVIAL(warning) << "A warning severity message";
	BOOST_LOG_TRIVIAL(error) << "An error severity message";
	BOOST_LOG_TRIVIAL(fatal) << "A fatal severity message";
}

void CLogTest::TestPrintToFileInit()
{
	logging::add_file_log
	(
		keywords::file_name = "sample_%N.log",                                        /*< file name pattern >*/
		keywords::rotation_size = 10 * 1024 * 1024,                                   /*< rotate files every 10 MiB... >*/
		keywords::time_based_rotation = sinks::file::rotation_at_time_point(0, 0, 0), /*< ...or at midnight >*/
		keywords::format = "[%TimeStamp%]: %Message%"                                 /*< log record format >*/
	);

	logging::core::get()->set_filter(logging::trivial::severity >= logging::trivial::info);
}

void CLogTest::TestPrintToFile()
{
	TestPrintToFileInit();
	logging::add_common_attributes();

	using namespace logging::trivial;
	src::severity_logger<severity_level> lg;

	BOOST_LOG_SEV(lg, trace) << "A trace severity message";
	BOOST_LOG_SEV(lg, debug) << "A debug severity message";
	BOOST_LOG_SEV(lg, info) << "An informational severity message";
	BOOST_LOG_SEV(lg, warning) << "A warning severity message";
	BOOST_LOG_SEV(lg, error) << "An error severity message";
	BOOST_LOG_SEV(lg, fatal) << "A fatal severity message";
}

void CLogTest::TestCLogUtil()
{
	CLogUtil::Init("D:\\");

	CLogUtil::Log("A trace severity message");

	for (int i = 0;i < 100;i++)
	{
		CAtlStringA txt;
		txt.Format("%d", i + 1);

		CLogUtil::Log(txt.GetBuffer());
	}

	CLogUtil::Log("Endl");
}
