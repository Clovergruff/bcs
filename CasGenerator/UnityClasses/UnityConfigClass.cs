public class UnityConfigClass : UnityClass
{
	public UnityConfigClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLine("using UnityEngine;");
		AddLine("using System.Collections;");
		AddLine("");

		AddLine($"[CreateAssetMenu(fileName = \"{component}\", menuName = \"Data/{entity}/{component}\")]");
		AddLine($"public class {entity}{component}Config : {entity}ComponentConfig");
		AddLine("{");
		AddLine($"	public override void ConstructSystemComponent({entity} entityObject)");
		AddLine("	{");
		AddLine($"		entityObject.Add{component}(this);");
		AddLine("	}");
		AddLine("}");
	}
}