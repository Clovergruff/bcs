public class UnitySystemBaseClass : UnityClass
{
	public UnitySystemBaseClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLine("using UnityEngine;");
		AddLine("");

		AddLine($"public class {entity}System<T> : MonoBehaviour, IEntitySystem");
		AddLine($"	where T : {entity}ComponentConfig");
		AddLine("{");
		AddLine($"	public {entity} {lowerCaseEntityName} {"{ get; private set; }"}");
		AddLine("");
		AddLine("	[SerializeField] protected T config;");
		AddLine("");
		AddLine("	public T Config { get => config; }");
		AddLine("");
		AddLine($"	public virtual void Init({entity} {lowerCaseEntityName}, T config)");
		AddLine("	{");
		AddLine("		this.config = config;");
		AddLine($"		this.{lowerCaseEntityName} = {lowerCaseEntityName};");
		AddLine("	}");
		AddLine("");
		AddLine("	public virtual void Init() { }");
		AddLine("	public virtual void LateSetup() { }");
		AddLine("	public virtual void Remove() {}");
		AddLine("}");
	}
}