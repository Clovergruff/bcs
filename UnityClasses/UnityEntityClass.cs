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
		AddLine($"	public {entity}Config config;");
		AddLine($"	public IEntitySystem[] allSystems;");
		AddLine($"	public ISystemUpdate[] updateSystems;");
		AddLine($"	public ISystemLateUpdate[] lateUpdateSystems;");
		AddLine($"	public ISystemFixedUpdate[] fixedUpdateSystems;");
		AddLine("");
		AddLine($"	public void Init({entity}Config config)");
		AddLine("	{");
		AddLine("		this.config = config;");
		AddLine("	}");
		AddLine("");
		AddLine($"	public void FindSystems()");
		AddLine("	{");
		AddLine($"		allSystems = gameObject.GetComponents<IEntitySystem>();");
		AddLine("		updateSystems = gameObject.GetComponents<ISystemUpdate>();");
		AddLine("		lateUpdateSystems = gameObject.GetComponents<ISystemLateUpdate>();");
		AddLine("		fixedUpdateSystems = gameObject.GetComponents<ISystemFixedUpdate>();");
		AddLine("	}");
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
			AddLine("		for (int i = 0; i < updateSystems.Length; i++)");
			AddLine("			updateSystems[i].OnUpdate();");
			AddLine("	}");
		}

		if (hasLateUpdate)
		{
			AddLine("");
			AddLine("	public void OnLateUpdate()");
			AddLine("	{");
			AddLine("		for (int i = 0; i < lateUpdateSystems.Length; i++)");
			AddLine("			lateUpdateSystems[i].OnLateUpdate();");
			AddLine("	}");
		}

		if (hasFixedUpdate)
		{
			AddLine("");
			AddLine("	public void OnFixedUpdate()");
			AddLine("	{");
			AddLine("		for (int i = 0; i < fixedUpdateSystems.Length; i++)");
			AddLine("			fixedUpdateSystems[i].OnFixedUpdate();");
			AddLine("	}");
		}
		
		AddLine("}");
	}
}