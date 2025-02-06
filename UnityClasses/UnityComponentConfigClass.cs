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
			$"public abstract class {actor}ComponentConfig : ConfigScriptableObject",
			"{",
			$"	public abstract void ConstructSystemComponent({actor} {actor.FirstCharToLower()});",
			"}",
		});
	}
}