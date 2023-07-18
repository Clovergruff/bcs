public class UnityEntityConfigClass : UnityClass
{
	public UnityEntityConfigClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLines(new string[]
		{
			"using UnityEngine;",
			"using Gruffdev.BCS;",
			"",
			$"[CreateAssetMenu(fileName = \"{entity}\", menuName = \"Data/{entity}/{entity} entity\")]",
			$"public class {entity}Config : EntityConfigAsset<{entity}ComponentConfig>",
			"{",
			"}",
		});
	}
}