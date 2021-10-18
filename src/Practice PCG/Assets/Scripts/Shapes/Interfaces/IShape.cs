using UnityEngine;

public interface IShape
{
	public MeshFilter filter { get; }
	public MeshRenderer renderer { get; }

	public Material material { get; }

	public void CreateMesh();
}