// ����ָ��
// ���ã�
//		1. ������ڴ�
// ʹ��Լ����
//		1. ���ܽ�ջ����ȡ��ַ��ֵ������ָ��

#pragma once

// ����ָ����
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

	// �������캯��
	CSmartPoint(const CSmartPoint& sp)
	{
		piCount = sp.piCount;
		(*piCount)++;
		p = sp.p;
	}

	// ���ظ�ֵ�����
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

	// ���ظ�ֵ�����
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

	// �����±������
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
