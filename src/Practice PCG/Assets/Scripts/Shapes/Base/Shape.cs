using System;
using UnityEngine;

public abstract class Shape : IShape
{
	protected Mesh mesh;

	protected Shape(MeshFilter _filter, MeshRenderer _renderer, Material _material)
	{
		filter = _filter;
		renderer = _renderer;
		material = _material;
	}

	public MeshFilter filter { get; }
	public MeshRenderer renderer { get; }

	public Material material { get; }

	public virtual void CreateMesh()
	{
	}

	public static T CreateShape<T>(MeshFilter _filter, MeshRenderer _renderer, Material generatorMaterial)
		where T : Shape
	{
		_filter.sharedMesh.Clear();
		return Activator.CreateInstance(typeof(T), _filter, _renderer, generatorMaterial) as T;
	}

	public static Shape CreateShape(Type type, MeshFilter _filter, MeshRenderer _renderer, Material generatorMaterial)
	{
		_filter.sharedMesh.Clear();
		return Activator.CreateInstance(type, _filter, _renderer, generatorMaterial) as Shape;
	}
}