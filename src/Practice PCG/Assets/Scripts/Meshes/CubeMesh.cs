using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ShowShape("Cube")]
public class CubeMesh : Shape
{
	public CubeMesh(MeshFilter _filter, MeshRenderer _renderer, Material _material) : base(_filter, _renderer,
		_material)
	{
	}

	public override void CreateMesh()
	{
		filter.mesh = mesh = new Mesh
		{
			name = "Cube Mesh Shape"
		};


		var verts    = new List<Vector3>();
		var triangle = new Triangle(6);

		for (var i = 0; i < 4; i++)
			PredefinedShape
				.Create<QuadShape>()
				.AddOption(x => x.Rotate(Quaternion.Euler(i * 90f, 0f, 0f)))
				.AddParameter(x => x.SetParameters(verts,
					new Vector3(-0.5f, 0.5f, -0.5f),
					1, 1))
				.Build()
				.Combine(ref triangle, ref verts);

		PredefinedShape
			.Create<QuadShape>()
			.AddOption(x => x.Rotate(Quaternion.Euler(0f, 0f, 90f)))
			.AddParameter(x => x.SetParameters(verts,
				new Vector3(-0.5f, 0.5f, -0.5f),
				1, 1))
			.Build()
			.Combine(ref triangle, ref verts);

		PredefinedShape
			.Create<QuadShape>()
			.AddOption(x => x.Rotate(Quaternion.Euler(90f, 90f, 0f)))
			.AddParameter(x => x.SetParameters(verts,
				new Vector3(-0.5f, 0.5f, -0.5f),
				1, 1))
			.Build()
			.Combine(ref triangle, ref verts);


		mesh.vertices     = verts.ToArray();
		mesh.triangles    = triangle;
		renderer.material = material;

		mesh.RecalculateBounds();
	}
}