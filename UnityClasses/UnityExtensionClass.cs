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
			$"	private event Action On{component}Added;",
			$"	private event Action On{component}Removed;",
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
			$"		On{component}Added?.Invoke();",
			"		",
			$"		return {systemVariable};",
			"	}",
			"	",
			$"	public void Remove{component}()",
			"	{",
			$"		if (!{hasComponentVariable})",
			"			return;",
			"	",
			$"		On{component}Removed?.Invoke();",
			$"		{systemVariable}.Remove();",
			$"		Destroy({systemVariable});",
			"	",
			$"		{hasComponentVariable} = false;",
			$"		{systemVariable} = null;",
			$"		{configVariable} = null;",
			"	}",
			"",
			$"	public void Add{component}AddedListener(Action action)",
			"	{",
			$"		if ({hasComponentVariable})",
			"			action.Invoke();",
			$"		On{component}Added += action;",
			"	}",
			$"	public void Add{component}RemovedListener(Action action)",
			"	{",
			$"		if (!{hasComponentVariable})",
			"			action.Invoke();",
			$"		On{component}Removed += action;",
			"	}",
			"",
			$"	public void Remove{component}AddedListener(Action action) => On{component}Added -= action;",
			$"	public void Remove{component}RemovedListener(Action action) => On{component}Removed -= action;",
			"}",
		});
	}
}