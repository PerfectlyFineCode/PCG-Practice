using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShapeGenerator))]
public class ShapeGeneratorEditor : Editor
{
	private ShapeGenerator _generator;
	private Material Material;
	private int selected;

	private void Awake()
	{
		_generator = target as ShapeGenerator;

		if (_generator == null || selected >= ShapeGenerator.ShapeTypes.Count) return;
		selected = _generator.selected;
		Material = _generator.material;

		var value = ShapeGenerator.ShapeTypes.ElementAtOrDefault(selected);
		_generator.Generate(value.Value);
	}

	private void OnEnable()
	{
		_generator = target as ShapeGenerator;

		if (_generator == null || selected >= ShapeGenerator.ShapeTypes.Count) return;
		selected = _generator.selected;
		Material = _generator.material;

		var value = ShapeGenerator.ShapeTypes.ElementAtOrDefault(selected);
		_generator.Generate(value.Value);
	}

	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();

		EditorGUILayout.BeginHorizontal();
		selected = EditorGUILayout.Popup("Shape type", selected, ShapeGenerator.ShapeTypes.Keys.ToArray());
		Material = EditorGUILayout.ObjectField(Material, typeof(Material), true, GUILayout.Width(175)) as Material;
		EditorGUILayout.EndHorizontal();

		if (selected < ShapeGenerator.ShapeTypes.Count &&
		    ShapeGenerator.ShapeTypes.ElementAtOrDefault(selected).Value is { } type &&
		    !type.IsSubclassOf(typeof(Shape)))
		{
			EditorGUILayout.HelpBox($"{type.Name} does not inherit type '{nameof(Shape)}'", MessageType.Error);
			return;
		}

		if (!EditorGUI.EndChangeCheck()) return;
		_generator.material = Material;
		if (selected >= ShapeGenerator.ShapeTypes.Count) return;
		_generator.selected = selected;
		var value = ShapeGenerator.ShapeTypes.ElementAtOrDefault(selected);
		_generator.Generate(value.Value);
	}
}