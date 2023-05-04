public class UnityEntityConfigClass : UnityClass
{
	public UnityEntityConfigClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLine("using UnityEngine;");
		AddLine("");

		AddLine($"[CreateAssetMenu(fileName = \"{entity}\", menuName = \"Data/{entity}/{entity} entity\")]");
		AddLine($"public class {entity}Config : EntityConfigAsset<{entity}ComponentConfig>");
		AddLine("{");
		AddLine($"	public Optional<{entity}> prefab;");
		AddLine("}");
	}
}