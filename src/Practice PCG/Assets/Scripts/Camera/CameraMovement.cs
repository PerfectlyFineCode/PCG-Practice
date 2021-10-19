using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField] private float speed = 5f;

	[Range(0f, 360f)] [SerializeField] private float _rotation;

	private float Rotation
	{
		get => _rotation;
		set => _rotation = Mathf.Repeat(value, 360f);
	}

	// Update is called once per frame
	private void Update()
	{
		Rotation += Time.deltaTime * speed;

		transform.rotation =
			Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, Rotation, 0f), Time.deltaTime * 2f);
	}
}