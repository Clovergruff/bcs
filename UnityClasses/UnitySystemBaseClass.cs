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
			$"public class {entity}System<T> : MonoBehaviour, IEntitySystem",
			$"	where T : {entity}ComponentConfig",
			"{",
			$"	public {entity} {lowerCaseEntityName} {"{ get; private set; }"}",
			"",
			"	[SerializeField] protected T config;",
			"",
			"	public T Config { get => config; }",
			"",
			$"	public virtual void Init({entity} {lowerCaseEntityName}, T config)",
			"	{",
			"		this.config = config;",
			$"		this.{lowerCaseEntityName} = {lowerCaseEntityName};",
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