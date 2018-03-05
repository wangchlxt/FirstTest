#pragma once
class CRandTest
{
public:
	CRandTest();
	~CRandTest();

	void Test();

	void SimpleRandTest(int n);
	void RangedRandTest(int range_min, int range_max, int n);
};

