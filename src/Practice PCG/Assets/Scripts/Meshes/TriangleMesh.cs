using System.Collections.Generic;
using UnityEngine;

[ShowShape("Triangle")]
public class TriangleMesh : Shape
{
	public TriangleMesh(MeshFilter _filter, MeshRenderer _renderer, Material _material) : base(_filter, _renderer,
		_material)
	{
	}

	public override void CreateMesh()
	{
		filter.mesh = mesh = new Mesh
		{
			name = "Triangle Mesh Shape"
		};

		var verts = new List<Vector3>
		{
			new Vector3(-0.5f, 1, 0),
			new Vector3(0.5f, 1, 0),
			new Vector3(0f, 0, 0)
		};

		var v = new[]
		{
			new Vector3(-0.5f, 1, 0),
			new Vector3(0.5f, 1, 0),
			new Vector3(0f, 0, 0)
		};

		var reverse = Quaternion.Euler(new Vector3(0f, 180f, 0f));

		for (var i = 0; i < v.Length; i++) v[i] = reverse * v[i];

		verts.AddRange(v);

		var tris = new Triangle(verts.Count)
		{
			[0] = (0, 1, 2),
			[1] = (2, 3, 4)
		};

		mesh.vertices     = verts.ToArray();
		mesh.triangles    = tris;
		renderer.material = material;

		mesh.RecalculateNormals();
	}
}