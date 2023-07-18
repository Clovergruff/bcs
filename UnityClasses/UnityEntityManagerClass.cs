public class UnityEntityManagerClass : UnityClass
{
	public UnityEntityManagerClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLines(new string[]
		{	
			"using Gruffdev.BCS;",
			"",
			$"public class {entity}EntityManager : EntityManager<{entity}>",
			"{",
			"}",
		});
	}
}