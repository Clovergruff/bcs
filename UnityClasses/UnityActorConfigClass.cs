public class UnityActorConfigClass : UnityClass
{
	public UnityActorConfigClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLines(new string[]
		{
			"using UnityEngine;",
			"using Gruffdev.BCS;",
			"",
			$"[CreateAssetMenu(fileName = \"{actor}\", menuName = \"Data/{actor}/{actor} actor\")]",
			$"public class {actor}Config : ActorConfigAsset<{actor}ComponentConfig>",
			"{",
			"}",
		});
	}
}