public class UnitySystemClass : UnityClass
{
	public UnitySystemClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLines(new string[]
		{
			"using UnityEngine;",
			"using Gruffdev.BCS;",
			"",
			$"[AddComponentMenu(\"{entity}/{component}\")]",
			$"public class {entity}{component}System : {entity}System<{entity}{component}Config>",
			"{",
			$"	public override void Init({entity} {lowerCaseEntityName}, {entity}{component}Config config)",
			"	{",
			$"		base.Init({lowerCaseEntityName}, config);",
			"	}",
			"}",
		});
	}
}