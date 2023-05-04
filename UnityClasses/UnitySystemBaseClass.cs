public class UnitySystemBaseClass : UnityClass
{
	public UnitySystemBaseClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLine("using UnityEngine;");
		AddLine("");

		AddLine($"public interface I{entity}System");
		AddLine("{");
		AddLine($"	void LateSetup();");
		if (hasUpdate)
			AddLine("	void OnUpdate();");
		if (hasFixedUpdate)
			AddLine("	void OnFixedUpdate();");
		if (hasLateUpdate)
			AddLine("	void OnLateUpdate();");
		AddLine("}");
		AddLine("");
		AddLine($"public class {entity}System<T> : MonoBehaviour, I{entity}System");
		AddLine($"	where T : {entity}ComponentConfig");
		AddLine("{");/*  */
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
		AddLine("");
		if (hasUpdate)
			AddLine("	public virtual void OnUpdate() {}");
		if (hasFixedUpdate)
			AddLine("	public virtual void OnFixedUpdate() {}");
		if (hasLateUpdate)
			AddLine("	public virtual void OnLateUpdate() {}");
		AddLine("	public virtual void Remove() {}");
		AddLine("}");
	}
}