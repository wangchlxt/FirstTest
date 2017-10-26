// 智能指针
// 作用：
//		1. 管理堆内存
// 使用约束：
//		1. 不能将栈对象取地址后赋值给智能指针

#pragma once

// 智能指针类
template<class T>
class CSmartPoint
{
	unsigned int* piCount;
	T* p;
public:
	CSmartPoint()
	{
		piCount = new unsigned int(0);
		p = NULL;
	}

	CSmartPoint(T* pt)
	{
		piCount = new unsigned int(1);
		p = pt;
	}

	~CSmartPoint()
	{
		if (piCount == NULL || (*piCount) == 0)
		{
			if (piCount != NULL)
			{
				delete piCount;
				piCount = NULL;
			}
			
			return;
		}

		(*piCount)--;

		if (p && (*piCount) == 0)
		{
			delete piCount;
			piCount = NULL;
			delete p;
			p = NULL;
		}
	}

	void Destroy()
	{
		this->~CSmartPoint();
	}

	// 拷贝构造函数
	CSmartPoint(const CSmartPoint& sp)
	{
		piCount = sp.piCount;
		(*piCount)++;
		p = sp.p;
	}

	// 重载赋值运算符
	CSmartPoint& operator=(T* pt)
	{
		if (pt == NULL || p == pt)
		{
			return *this;
		}

		if (p)
		{
			this->~CSmartPoint();
		}

		piCount = new unsigned int(1);
		p = pt;

		return *this;
	}

	// 重载赋值运算符
	CSmartPoint& operator=(CSmartPoint& sp)
	{
		if (sp.p == NULL || p == sp.p)
		{
			return *this;
		}

		if (p)
		{
			this->~CSmartPoint();
		}

		piCount = sp.piCount;
		(*piCount)++;
		p = sp.p;

		return *this;
	}

	// 重载下标运算符
	T* operator->()
	{
		return p;
	}

	T* get()
	{
		return p;
	}

	unsigned int count()
	{
		return *piCount;
	}
};
