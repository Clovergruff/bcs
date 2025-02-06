public class UnityExtensionClass : UnityClass
{
	public UnityExtensionClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		string hasComponentVariable = $"{prefix.FirstCharToUpper()}{component}";
		string systemVariable = $"{component}";
		string configVariable = $"{component}Config";

		string privateSetter = "{ private set; get; }";

		AddLines(new string[]
		{
			"using UnityEngine;",
			"using Gruffdev.BCS;",
			"using System;",
			"",
			$"public partial class {actor} : MonoBehaviour, IActor",
			"{",
			$"	public bool {hasComponentVariable} {privateSetter}",
			$"	public {actor}{component}System {systemVariable} {privateSetter}",
			$"	public {actor}{component}Config {configVariable} {privateSetter}",
			"	",
			$"	public event Action<{actor}{component}System> On{component}Added;",
			$"	public event Action<{actor}{component}System> On{component}Removed;",
			"	",
			$"	public {actor}{component}System Add{component}({actor}{component}Config config)",
			"	{",
			$"		if ({hasComponentVariable})",
			$"			Destroy({systemVariable});",
			"		",
			$"		{systemVariable} = gameObject.AddComponent<{actor}{component}System>();",
			$"		{configVariable} = config;",
			$"		{systemVariable}.Init(this, config);",
			$"		{hasComponentVariable} = true;",
			$"		On{component}Added?.Invoke({systemVariable});",
			"		",
			$"		return {systemVariable};",
			"	}",
			"	",
			$"	public void Remove{component}()",
			"	{",
			$"		if (!{hasComponentVariable})",
			"			return;",
			"	",
			$"		On{component}Removed?.Invoke({systemVariable});",
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