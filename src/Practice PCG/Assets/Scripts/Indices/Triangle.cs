using System;

public struct Triangle
{
	private int[] _value;

	public Triangle(int length)
	{
		_value = new int[3 * length];
	}

	public Triangle(int[] array)
	{
		_value = array;
	}

	public void Add(int index, int a, int b, int c)
	{
		this[index] = (a, b, c);
	}

	public void Resize(int size)
	{
		Array.Resize(ref _value, size * 3);
	}

	public (int a, int b, int c) this[int index]
	{
		set
		{
			int i = index * 3;
			if (i + 2 >= _value.Length)
				throw new ArgumentOutOfRangeException(nameof(index), "Index was out of range");

			_value[i] = value.a;
			_value[i + 1] = value.b;
			_value[i + 2] = value.c;
		}
	}

	public int Length => _value?.Length ?? 0;

	public static implicit operator int[](Triangle triangle)
	{
		return triangle._value;
	}

	public static Triangle operator +(Triangle a, Triangle b)
	{
		var values = new int[a.Length + b.Length];
		a._value.CopyTo(values, 0);
		b._value.CopyTo(values, a.Length);
		return new Triangle(values);
	}
}