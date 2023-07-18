public class UnityComponentConfigClass : UnityClass
{
	public UnityComponentConfigClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLines(new string[]
		{
			"using Gruffdev.BCS;",
			"",
			$"public abstract class {entity}ComponentConfig : ConfigScriptableObject",
			"{",
			$"	public abstract void ConstructSystemComponent({entity} {entity.FirstCharToLower()});",
			"}",
		});
	}
}