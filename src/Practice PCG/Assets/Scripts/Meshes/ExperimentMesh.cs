using UnityEngine;

[ShowShape("Experiment")]
public class ExperimentMesh : Shape
{
	public ExperimentMesh(MeshFilter _filter, MeshRenderer _renderer, Material _material) : base(_filter, _renderer,
		_material)
	{
	}

	public override void CreateMesh()
	{
		filter.mesh = mesh = new Mesh
		{
			name = "Experiment Mesh Shape"
		};

		var length = 2f;
		var wide = 2f;

		var tris = new Vector3[4];
		var normals = new Vector3[4];
		var uv = new Vector2[4];

		tris[0] = new Vector3(0, 0, 0);
		uv[0] = new Vector2(0f, 0f);
		normals[0] = Vector3.up;

		tris[1] = new Vector3(0, 0, length);
		uv[1] = new Vector2(0f, 1f);
		normals[1] = Vector3.up;

		tris[2] = new Vector3(wide, 0, length);
		uv[2] = new Vector2(1f, 1f);
		normals[2] = Vector3.up;

		tris[3] = new Vector3(length, 0, 0);
		uv[3] = new Vector2(1f, 0f);
		normals[3] = Vector3.up;


		var indices = new Triangle(tris.Length)
		{
			[0] = (0, 1, 2),
			[1] = (0, 2, 3)
		};

		mesh.vertices = tris;
		mesh.normals = normals;
		mesh.uv = uv;
		mesh.triangles = indices;
		renderer.material = material;

		mesh.RecalculateBounds();
	}
}