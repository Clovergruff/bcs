public class UnityExtensionClass : UnityClass
{
	public UnityExtensionClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		string hasComponentVariable = $"{prefix}{component}";
		string systemVariable = $"{lowerCaseComponentName}";
		string configVariable = $"{lowerCaseComponentName}Config";

		string privateSetter = "{ private set; get; }";

		AddLine("using UnityEngine;");
		AddLine("using System.Collections;");
		AddLine("");

		AddLine($"public partial class {entity} : MonoBehaviour, IEntity");
		AddLine("{");
		AddLine($"	public bool {hasComponentVariable} {privateSetter}");
		AddLine($"	public {entity}{component}System {systemVariable} {privateSetter}");
		
		if (generateConfig)
			AddLine($"	public {entity}{component}Config {configVariable} {privateSetter}");

		AddLine("	");

		if (generateConfig)
			AddLine($"	public {entity}{component}System Add{component}({entity}{component}Config config)");
		else
			AddLine($"	public {entity}{component}System Add{component}()");

		AddLine("	{");
		AddLine($"		if ({hasComponentVariable})");
		AddLine($"			Destroy({systemVariable});");
		AddLine("		");
		AddLine($"		{systemVariable} = gameObject.AddComponent<{entity}{component}System>();");

		if (generateConfig)
		{
			AddLine($"		{configVariable} = config;");
			AddLine($"		{systemVariable}.Init(this, config);");
		}
		else
		{
			AddLine($"		{systemVariable}.Init();");
		}

		AddLine($"		{hasComponentVariable} = true;");
		AddLine($"		return {systemVariable};");
		AddLine("	}");
		AddLine("	");
		AddLine($"	public void Remove{component}()");
		AddLine("	{");
		AddLine($"		if (!{hasComponentVariable})");
		AddLine("			return;");
		AddLine("	");
		AddLine($"		{systemVariable}.Remove();");
		AddLine($"		Destroy({systemVariable});");
		AddLine("	");
		AddLine($"		{hasComponentVariable} = false;");
		AddLine($"		{systemVariable} = null;");

		if (generateConfig)
			AddLine($"		{configVariable} = null;");

		AddLine("	}");
		AddLine("}");
	}
}