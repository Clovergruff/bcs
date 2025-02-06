public class UnityActorClass : UnityClass
{
	public UnityActorClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLines(new string[]
		{
			"using UnityEngine;",
			"using Gruffdev.BCS;",
			"",
			$"[AddComponentMenu(\"{actor}/{actor}\")]",
			$"public partial class {actor} : MonoBehaviour",
			"	, IActor",
		});

		if (hasUpdate)
			AddLine("	, IActorUpdate");
		if (hasFixedUpdate)
			AddLine("	, IActorFixedUpdate");
		if (hasLateUpdate)
			AddLine("	, IActorLateUpdate");

		AddLines(new string[]
		{
			"{",
			$"	public {actor}Config config;",
			$"	public IActorSystem[] allSystems;",
			$"	public IUpdate[] updateSystems;",
			$"	public ILateUpdate[] lateUpdateSystems;",
			$"	public IFixedUpdate[] fixedUpdateSystems;",
			"",
			$"	public void FindSystems()",
			"	{",
			$"		allSystems = gameObject.GetComponents<IActorSystem>();",
			"		updateSystems = gameObject.GetComponents<IUpdate>();",
			"		lateUpdateSystems = gameObject.GetComponents<ILateUpdate>();",
			"		fixedUpdateSystems = gameObject.GetComponents<IFixedUpdate>();",
			"	}",
			"",
			$"	protected virtual void Awake() => {actor}ActorManager.I.Add(this);",
			$"	protected virtual void OnEnable() => {actor}ActorManager.I.Enable(this);",
			$"	protected virtual void OnDisable() => {actor}ActorManager.I.Disable(this);",
			$"	protected virtual void OnDestroy() => {actor}ActorManager.I.Remove(this);",
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