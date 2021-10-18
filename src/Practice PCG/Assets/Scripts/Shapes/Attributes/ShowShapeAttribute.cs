using System;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ShowShapeAttribute : Attribute
{
	public ShowShapeAttribute(string name)
	{
		Name = name;
	}

	public string Name { get; }
}