using System;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ShapeGenerator : MonoBehaviour
{
	[HideInInspector] public int selected;
	[HideInInspector] public Material material;

	private Shape shape;

	private void Start()
	{
		if (shape == null) Generate<TriangleMesh>();
	}

	private void OnValidate()
	{
		shape?.CreateMesh();
	}

	public void Generate<T>() where T : Shape
	{
		shape = ShapeBuilder<T>.Build(this);
	}

	public void Generate(Type type)
	{
		shape = ShapeBuilder.Build(type, this);
	}
}