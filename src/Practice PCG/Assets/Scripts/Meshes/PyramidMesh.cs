using System.Collections.Generic;
using UnityEngine;

[ShowShape("Pyramid")]
public class PyramidMesh : Shape
{
	public PyramidMesh(MeshFilter _filter, MeshRenderer _renderer, Material _material) : base(_filter, _renderer,
		_material)
	{
	}

	public override void CreateMesh()
	{
		filter.mesh = mesh = new Mesh
		{
			name = "Pyramid Mesh Shape"
		};


		var verts    = new List<Vector3>();
		var triangle = new Triangle(4);

		for (var i = 0; i < 4; i++)
		{
			var index = i;
			PredefinedShape
				.Create<TriangleShape>()
				.AddParameter(x => x.SetParameters(verts,
					new Vector3(0f, 0f, 0f), 1, 1f / 1.269f))
				.AddOption(x => x.Rotate(
					Quaternion.Euler(new Vector3(52f, 90f * index, 180f))))
				.Build()
				.Combine(ref triangle, ref verts);
		}

		PredefinedShape.Create<QuadShape>()
			.AddParameter(x => x.SetParameters(verts, new Vector3(-0.5f, 0.866f, -0.5f),
				1, 1))
			.AddOption(x => x.Rotate(Quaternion.Euler(180f, 0f, 0f)))
			.Build()
			.Combine(ref triangle, ref verts);

		mesh.vertices  = verts.ToArray();
		mesh.triangles = triangle;

		mesh.RecalculateBounds();
	}
}