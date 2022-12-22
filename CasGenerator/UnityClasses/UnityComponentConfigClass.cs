public class UnityComponentConfigClass : UnityClass
{
	public UnityComponentConfigClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLine($"public abstract class {entity}ComponentConfig : DataScriptableObject");
		AddLine("{");
		AddLine($"	public abstract void ConstructSystemComponent({entity} {entity.FirstCharToLower()});");
		AddLine("}");
	}
}