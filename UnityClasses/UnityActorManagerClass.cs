public class UnityActorManagerClass : UnityClass
{
	public UnityActorManagerClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLines(new string[]
		{	
			"using Gruffdev.BCS;",
			"",
			$"public class {actor}ActorManager : ActorManager<{actor}>",
			"{",
			"}",
		});
	}
}