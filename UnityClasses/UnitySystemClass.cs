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
			$"[AddComponentMenu(\"{actor}/{component}\")]",
			$"public class {actor}{component}System : {actor}System<{actor}{component}Config>",
			"{",
			$"	public override void Init({actor} actor, {actor}{component}Config config)",
			"	{",
			$"		base.Init(actor, config);",
			"	}",
			"}",
		});
	}
}