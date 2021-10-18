using System;
using UnityEngine;

public class ShapeBuilder<T> where T : Shape
{
	private readonly Shape shape;

	public ShapeBuilder(Shape _shape)
	{
		shape = _shape;
	}

	private Shape Build()
	{
		shape.CreateMesh();
		return shape;
	}

	public static Shape Build(ShapeGenerator generator)
	{
		if (!generator.TryGetComponent(out MeshFilter _filter) ||
		    !generator.TryGetComponent(out MeshRenderer _renderer))
			return null;

		return new ShapeBuilder<T>(Shape.CreateShape<T>(_filter, _renderer, generator.material))
			.Build();
	}
}

public static class ShapeBuilder
{
	public static Shape Build(Type type, ShapeGenerator generator)
	{
		if (!type.IsSubclassOf(typeof(Shape))) return null;

		if (!generator.TryGetComponent(out MeshFilter _filter) ||
		    !generator.TryGetComponent(out MeshRenderer _renderer))
			return null;

		object builder = Activator.CreateInstance(typeof(ShapeBuilder<>).MakeGenericType(type),
			Shape.CreateShape(type, _filter, _renderer, generator.material));

		return builder.GetType().GetMethod("Build")?.Invoke(builder, new object[] { generator }) as Shape;
	}
}