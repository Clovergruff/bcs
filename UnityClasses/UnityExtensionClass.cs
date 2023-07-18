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

		AddLines(new string[]
		{
			"using UnityEngine;",
			"using Gruffdev.BCS;",
			"",
			$"public partial class {entity} : MonoBehaviour, IEntity",
			"{",
			$"	public bool {hasComponentVariable} {privateSetter}",
			$"	public {entity}{component}System {systemVariable} {privateSetter}",
			$"	public {entity}{component}Config {configVariable} {privateSetter}",
			"	",
			$"	public {entity}{component}System Add{component}({entity}{component}Config config)",
			"	{",
			$"		if ({hasComponentVariable})",
			$"			Destroy({systemVariable});",
			"		",
			$"		{systemVariable} = gameObject.AddComponent<{entity}{component}System>();",
			$"		{configVariable} = config;",
			$"		{systemVariable}.Init(this, config);",
			$"		{hasComponentVariable} = true;",
			$"		return {systemVariable};",
			"	}",
			"	",
			$"	public void Remove{component}()",
			"	{",
			$"		if (!{hasComponentVariable})",
			"			return;",
			"	",
			$"		{systemVariable}.Remove();",
			$"		Destroy({systemVariable});",
			"	",
			$"		{hasComponentVariable} = false;",
			$"		{systemVariable} = null;",
			$"		{configVariable} = null;",
			"	}",
			"}",
		});
	}
}