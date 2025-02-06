public class UnitySystemBaseClass : UnityClass
{
	public UnitySystemBaseClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLines(new string[]
		{
			"using UnityEngine;",
			"using Gruffdev.BCS;",
			"",
			$"public class {actor}System<T> : MonoBehaviour, IActorSystem",
			$"	where T : {actor}ComponentConfig",
			"{",
			$"	public {actor} Actor {"{ get; private set; }"}",
			"",
			"	[SerializeField] protected T config;",
			"",
			"	public T Config { get => config; }",
			"",
			$"	public virtual void Init({actor} actor, T config)",
			"	{",
			"		this.config = config;",
			$"		Actor = actor;",
			"	}",
			"",
			"	public virtual void Init() { }",
			"	public virtual void LateSetup() { }",
			"	public virtual void Remove() { }",
			"	public virtual void ReusedSetup() { }",
			"}",
		});
	}
}