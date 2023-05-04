public class UnityEntityClass : UnityClass
{
	public UnityEntityClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLine("using UnityEngine;");
		AddLine("");

		AddLine($"[AddComponentMenu(\"{entity}/{entity}\")]");
		AddLine($"public partial class {entity} : MonoBehaviour");
		AddLine("	, IEntity");
		if (hasUpdate)
			AddLine("	, IEntityUpdate");
		if (hasFixedUpdate)
			AddLine("	, IEntityFixedUpdate");
		if (hasLateUpdate)
			AddLine("	, IEntityLateUpdate");
		AddLine("{");
		AddLine($"	[SerializeField] private {entity}Config config;");
		AddLine($"	private I{entity}System[] systems;");
		AddLine("");
		AddLine($"	public {entity}Config Config {"{ get => config; }"}");
		AddLine($"	public I{entity}System[] Systems {"{ get => systems; }"}");
		AddLine("");
		AddLine($"	public void Init({entity}Config config)");
		AddLine("	{");
		AddLine("		this.config = config;");
		AddLine("	}");
		AddLine("");
		AddLine($"	public I{entity}System[] FindSystems() => systems = gameObject.GetComponents<I{entity}System>();");
		AddLine("");
		AddLine($"	protected virtual void Awake() => {entity}EntityManager.I.AddEntity(this);");
		AddLine($"	protected virtual void OnEnable() => {entity}EntityManager.I.EnableEntity(this);");
		AddLine($"	protected virtual void OnDisable() => {entity}EntityManager.I.DisableEntity(this);");
		AddLine($"	protected virtual void OnDestroy() => {entity}EntityManager.I.RemoveEntity(this);");
		if (hasUpdate)
		{
			AddLine("");
			AddLine("	public void OnUpdate()");
			AddLine("	{");
			AddLine("		for (int i = 0; i < systems.Length; i++)");
			AddLine("			systems[i].OnUpdate();");
			AddLine("	}");
		}

		if (hasLateUpdate)
		{
			AddLine("");
			AddLine("	public void OnLateUpdate()");
			AddLine("	{");
			AddLine("		for (int i = 0; i < systems.Length; i++)");
			AddLine("			systems[i].OnLateUpdate();");
			AddLine("	}");
		}

		if (hasFixedUpdate)
		{
			AddLine("");
			AddLine("	public void OnFixedUpdate()");
			AddLine("	{");
			AddLine("		for (int i = 0; i < systems.Length; i++)");
			AddLine("			systems[i].OnFixedUpdate();");
			AddLine("	}");
		}
		
		AddLine("}");
	}
}