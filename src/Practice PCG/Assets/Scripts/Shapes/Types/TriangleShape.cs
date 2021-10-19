using System.Collections.Generic;
using UnityEngine;

public class TriangleShape : PredefinedShape
{
	private float _length;
	private Vector3 _offset;
	private List<Vector3> _verts = new List<Vector3>();

	protected internal override void CreateShape()
	{
		vertices.Add(new Vector3(-0.5f, _length, 0f) + _offset);
		uv.Add(new Vector2(-0.5f, 1f));
		normals.Add(Vector3.up);

		vertices.Add(new Vector3(0.5f, _length, 0f) + _offset);
		uv.Add(new Vector2(0.5f, 1f));
		normals.Add(Vector3.up);

		vertices.Add(new Vector3(0f, 0f, 0f) + _offset);
		uv.Add(new Vector2(0f, 0f));
		normals.Add(Vector3.up);

		var baseIndex = vertices.Count + _verts.Count - 3;

		triangle[0] = (baseIndex, baseIndex + 1, baseIndex + 2);
	}

	protected override void OnConfigure()
	{
		triangle.Resize(1);
	}

	public TriangleShape SetParameters(List<Vector3> vert, Vector3 offset, int length)
	{
		_verts  = vert;
		_offset = offset;
		_length = length;
		return this;
	}
}