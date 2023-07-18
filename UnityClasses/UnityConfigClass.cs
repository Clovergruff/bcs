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
			$"[CreateAssetMenu(fileName = \"{component}\", menuName = \"Data/{entity}/{component}\")]",
			$"public class {entity}{component}Config : {entity}ComponentConfig",
			"{",
			$"	public override void ConstructSystemComponent({entity} entityObject)",
			"	{",
			$"		entityObject.Add{component}(this);",
			"	}",
			"}",
		});
	}
}