public class UnityEntityDataClass : UnityClass
{
	public UnityEntityDataClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLine("using UnityEngine;");
		AddLine("");

		AddLine($"[CreateAssetMenu(fileName = \"{entity}\", menuName = \"Data/{entity}/{entity} entity\")]");
		AddLine($"public class {entity}Data : EntityDataAsset<{entity}ComponentConfig>");
		AddLine("{");
		AddLine($"	public Optional<{entity}> prefab;");
		AddLine("}");
	}
}