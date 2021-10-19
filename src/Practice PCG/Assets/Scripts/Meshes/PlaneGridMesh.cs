using System.Collections.Generic;
using UnityEngine;

[ShowShape("Plane Grid")]
public class PlaneGridMesh : Shape
{
	public PlaneGridMesh(MeshFilter _filter, MeshRenderer _renderer, Material _material) : base(_filter, _renderer,
		_material)
	{
	}

	public override void CreateMesh()
	{
		filter.mesh = mesh = new Mesh
		{
			name = "Plane Grid Mesh"
		};
		var verts    = new List<Vector3>();
		var triangle = new Triangle(10 * 10);

		for (var row = 0; row < 10; row++)
		for (var col = 0; col < 10; col++)
			PredefinedShape
				.Create<QuadShape>()
				.AddParameter(
					x => x.SetParameters(verts, new Vector3(row, 0f, col) - new Vector3(5f, 0f, 5f), 1, 1))
				.Build()
				.Combine(ref triangle, ref verts);

		mesh.vertices     = verts.ToArray();
		mesh.triangles    = triangle;
		renderer.material = material;

		mesh.RecalculateNormals();
	}
}