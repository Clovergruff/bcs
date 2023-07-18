public class UnityEntityClass : UnityClass
{
	public UnityEntityClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLines(new string[]
		{
			"using UnityEngine;",
			"using Gruffdev.BCS;",
			"",
			$"[AddComponentMenu(\"{entity}/{entity}\")]",
			$"public partial class {entity} : MonoBehaviour",
			"	, IEntity",
		});

		if (hasUpdate)
			AddLine("	, IEntityUpdate");
		if (hasFixedUpdate)
			AddLine("	, IEntityFixedUpdate");
		if (hasLateUpdate)
			AddLine("	, IEntityLateUpdate");

		AddLines(new string[]
		{
			"{",
			$"	public {entity}Config config;",
			$"	public IEntitySystem[] allSystems;",
			$"	public IUpdate[] updateSystems;",
			$"	public ILateUpdate[] lateUpdateSystems;",
			$"	public IFixedUpdate[] fixedUpdateSystems;",
			"",
			$"	public void Init({entity}Config config)",
			"	{",
			"		this.config = config;",
			"	}",
			"",
			$"	public void FindSystems()",
			"	{",
			$"		allSystems = gameObject.GetComponents<IEntitySystem>();",
			"		updateSystems = gameObject.GetComponents<IUpdate>();",
			"		lateUpdateSystems = gameObject.GetComponents<ILateUpdate>();",
			"		fixedUpdateSystems = gameObject.GetComponents<IFixedUpdate>();",
			"	}",
			"",
			$"	protected virtual void Awake() => {entity}EntityManager.I.AddEntity(this);",
			$"	protected virtual void OnEnable() => {entity}EntityManager.I.EnableEntity(this);",
			$"	protected virtual void OnDisable() => {entity}EntityManager.I.DisableEntity(this);",
			$"	protected virtual void OnDestroy() => {entity}EntityManager.I.RemoveEntity(this);",
		});

		if (hasUpdate)
		{
			AddLines(new string[]
			{
				"",
				"	public void OnUpdate()",
				"	{",
				"		for (int i = 0; i < updateSystems.Length; i++)",
				"			updateSystems[i].OnUpdate();",
				"	}",
			});
		}

		if (hasLateUpdate)
		{
			AddLines(new string[]
			{
				"",
				"	public void OnLateUpdate()",
				"	{",
				"		for (int i = 0; i < lateUpdateSystems.Length; i++)",
				"			lateUpdateSystems[i].OnLateUpdate();",
				"	}",
			});
		}

		if (hasFixedUpdate)
		{
			AddLines(new string[]
			{
				"",
				"	public void OnFixedUpdate()",
				"	{",
				"		for (int i = 0; i < fixedUpdateSystems.Length; i++)",
				"			fixedUpdateSystems[i].OnFixedUpdate();",
				"	}",
			});
		}
		
		AddLine("}");
	}
}