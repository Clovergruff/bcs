public class UnityEntityManagerClass : UnityClass
{
	public UnityEntityManagerClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLine($"public class {entity}EntityManager : EntityManager<{entity}>");
		AddLine("{");
		AddLine("}");
	}
}