using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShapeGenerator))]
public class ShapeGeneratorEditor : Editor
{
	private static readonly Dictionary<string, Type> ShapeTypes = LoadShapes();
	private ShapeGenerator _generator;
	private Material Material;
	private int selected;

	private void Awake()
	{
		_generator = target as ShapeGenerator;

		if (_generator == null || selected >= ShapeTypes.Count) return;
		selected = _generator.selected;
		Material = _generator.material;

		var value = ShapeTypes.ElementAtOrDefault(selected);
		_generator.Generate(value.Value);
	}

	private void OnEnable()
	{
		_generator = target as ShapeGenerator;

		if (_generator == null || selected >= ShapeTypes.Count) return;
		selected = _generator.selected;
		Material = _generator.material;

		var value = ShapeTypes.ElementAtOrDefault(selected);
		_generator.Generate(value.Value);
	}

	private static Dictionary<string, Type> LoadShapes()
	{
		return GetTypes().ToDictionary(x => x.attribute.Name, x => x.type);
	}

	private static IEnumerable<(Type type, ShowShapeAttribute attribute)> GetTypes()
	{
		Assembly assembly = typeof(ShowShapeAttribute).Assembly;
		foreach (Type type in assembly.GetTypes())
			if (type.GetCustomAttributes(typeof(ShowShapeAttribute), true).Length > 0)
			{
				Debug.Log(type.Name);
				yield return (type, type.GetCustomAttribute<ShowShapeAttribute>(true));
			}
	}

	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();

		EditorGUILayout.BeginHorizontal();
		selected = EditorGUILayout.Popup("Shape type", selected, ShapeTypes.Keys.ToArray());
		Material = EditorGUILayout.ObjectField(Material, typeof(Material), true, GUILayout.Width(175)) as Material;
		EditorGUILayout.EndHorizontal();

		if (selected < ShapeTypes.Count && ShapeTypes.ElementAtOrDefault(selected).Value is { } type &&
		    !type.IsSubclassOf(typeof(Shape)))
		{
			EditorGUILayout.HelpBox($"{type.Name} does not inherit type '{nameof(Shape)}'", MessageType.Error);
			return;
		}

		if (!EditorGUI.EndChangeCheck()) return;
		_generator.material = Material;
		if (selected >= ShapeTypes.Count) return;
		_generator.selected = selected;
		var value = ShapeTypes.ElementAtOrDefault(selected);
		_generator.Generate(value.Value);
	}
}