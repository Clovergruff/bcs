public class UnityConfigClass : UnityClass
{
	public UnityConfigClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLines(new string[]
		{
			"using UnityEngine;",
			"using Gruffdev.BCS;",
			"",
			$"[CreateAssetMenu(fileName = \"{component}\", menuName = \"Data/{actor}/{component}\")]",
			$"public class {actor}{component}Config : {actor}ComponentConfig",
			"{",
			$"	public override void ConstructSystemComponent({actor} actor)",
			"	{",
			$"		actor.Add{component}(this);",
			"	}",
			"}",
		});
	}
}