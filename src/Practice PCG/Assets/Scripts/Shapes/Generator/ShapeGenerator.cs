using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ShapeGenerator : MonoBehaviour
{
	public static readonly Dictionary<string, Type> ShapeTypes = LoadShapes();

	[HideInInspector] public int selected;
	[HideInInspector] public Material material;

	public Type GetType(int index)
	{
		return ShapeTypes?.ElementAtOrDefault(index).Value;
	}

	private Shape shape;

	private void Start()
	{
		if (shape == null) Generate(GetType(selected));
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

	private static Dictionary<string, Type> LoadShapes()
	{
		return GetTypes().ToDictionary(x => x.attribute.Name, x => x.type);
	}

	private static IEnumerable<(Type type, ShowShapeAttribute attribute)> GetTypes()
	{
		var assembly = typeof(ShowShapeAttribute).Assembly;
		foreach (var type in assembly.GetTypes())
			if (type.GetCustomAttributes(typeof(ShowShapeAttribute), true).Length > 0)
			{
				Debug.Log(type.Name);
				yield return (type, type.GetCustomAttribute<ShowShapeAttribute>(true));
			}
	}
}