using System.Collections.Generic;
using UnityEngine;

public class QuadShape : PredefinedShape
{
	private float _length;
	private Vector3 _offset;
	private List<Vector3> _verts = new List<Vector3>();
	private float _width;

	public override PredefinedShape Build()
	{
		vertices.Add(new Vector3(0f, 0f, 0f) + _offset);
		uv.Add(new Vector2(0f, 0f));
		normals.Add(Vector3.up);

		vertices.Add(new Vector3(0f, 0f, _length) + _offset);
		uv.Add(new Vector2(0f, 1f));
		normals.Add(Vector3.up);

		vertices.Add(new Vector3(_width, 0f, _length) + _offset);
		uv.Add(new Vector2(1f, 1f));
		normals.Add(Vector3.up);

		vertices.Add(new Vector3(_width, 0f, 0f) + _offset);
		uv.Add(new Vector2(1f, 0f));
		normals.Add(Vector3.up);

		int baseIndex = vertices.Count + _verts.Count - 4;

		triangle[0] = (baseIndex, baseIndex + 1, baseIndex + 2);
		triangle[1] = (baseIndex, baseIndex + 2, baseIndex + 3);
		return this;
	}

	protected override void OnConfigure()
	{
		triangle.Resize(2);
	}

	public QuadShape SetParameters(ref List<Vector3> vert, Vector3 offset, int length, int width)
	{
		_verts = vert;
		_offset = offset;
		_length = length;
		_width = width;
		return this;
	}
}

public class PredefinedShape
{
	public readonly List<Vector3> normals;
	public readonly List<Vector2> uv;
	public readonly List<Vector3> vertices;

	public Triangle triangle;

	protected PredefinedShape()
	{
		triangle = new Triangle(1);
		vertices = new List<Vector3>();
		normals = new List<Vector3>();
		uv = new List<Vector2>();
	}

	public virtual PredefinedShape Build()
	{
		return this;
	}

	protected virtual void OnConfigure()
	{
	}

	public void Combine(ref Triangle Triangle, ref List<Vector3> Vertices, ref List<Vector3> Normals,
		ref List<Vector2> UV)
	{
		Triangle += triangle;
		Vertices.AddRange(vertices);
		Normals.AddRange(normals);
		UV.AddRange(uv);
	}

	public void Combine(ref Triangle Triangle, ref List<Vector3> Vertices)
	{
		Triangle += triangle;
		Vertices.AddRange(vertices);
	}

	public static T Create<T>() where T : PredefinedShape, new()
	{
		var shape = new T();
		shape.OnConfigure();
		return shape;
	}
}