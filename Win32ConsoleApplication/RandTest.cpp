#include "stdafx.h"
#include "RandTest.h"

#include <stdlib.h>
#include <stdio.h>
#include <time.h>

#include <iostream>
using namespace std;

CRandTest::CRandTest()
{
}


CRandTest::~CRandTest()
{
}

void CRandTest::SimpleRandTest(int n)
{
	// Print n random numbers.
	int i;
	for (i = 0; i < n; i++)
		cout << rand() << endl;
}

void CRandTest::RangedRandTest(int range_min, int range_max, int n)
{
	// Generate random numbers in the half-closed interval
	// [range_min, range_max). In other words,
	// range_min <= random number < range_max
	int i;
	for (i = 0; i < n; i++)
	{
		int u = (double)rand() / (RAND_MAX + 1) * (range_max - range_min) + range_min;
		cout << u << endl;
	}
}


void CRandTest::Test()
{
	// Seed the random-number generator with the current time so that
	// the numbers will be different every time we run.
	srand((unsigned)time(NULL));

	SimpleRandTest(10);
	printf("\n");

	RangedRandTest(-100, 100, 10);
	cout << endl;

	RangedRandTest(100, 999, 10);
}
