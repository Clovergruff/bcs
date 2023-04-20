public class UnitySystemClass : UnityClass
{
	public UnitySystemClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLine("using UnityEngine;");
		AddLine("using System.Collections;");
		AddLine("");

		if (generateConfig)
		{
			AddLine($"public class {entity}{component}System : {entity}System<{entity}{component}Config>");
			AddLine("{");
			AddLine($"	public override void Init({entity} {lowerCaseEntityName}, {entity}{component}Config config)");
			AddLine("	{");
			AddLine($"		base.Init({lowerCaseEntityName}, config);");
			AddLine("	}");
			AddLine("}");
		}
		else
		{
			AddLine($"public class {entity}{component}System : {entity}System<{entity}ComponentConfig>");
			AddLine("{");
			AddLine($"	public override void Init()");
			AddLine("	{");
			AddLine("	}");
			AddLine("}");
		}
	}
}