public class UnityActorFactoryClass : UnityClass
{
	public UnityActorFactoryClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		string configNameString = "{config.name}";

		AddLines(new string[]
		{
			"using UnityEngine;",
			"using Gruffdev.BCS;",
			"",
			$"public static class {actor}Factory",
			"{",
			$"	public static {actor} CreatePooled({actor}Config config, Vector3 position, Quaternion rotation, Transform parent = null)",
			"	{",
			$"		if ({actor}ActorManager.Instance.InactiveEntities.Count > 0)",
			"		{",
			$"			{actor} actor = {actor}ActorManager.Instance.InactiveEntities[0];",
			"			actor.gameObject.SetActive(true);",
			"		",
			"			Transform transform = actor.transform;",
			"			transform.position = position;",
			"			transform.rotation = rotation;",
			"			transform.SetParent(parent);",
			"",
			"			foreach (var system in actor.allSystems)",
			"				system.ReusedSetup();",
			"",
			"			return actor;",
			"		}",
			"		else",
			"		{",
			"			return Create(config, position, rotation, parent);",
			"		}",
			"	}",
			"",
			$"	public static {actor} Create({actor}Config config, Vector3 position, Quaternion rotation, Transform parent = null)",
			"	{",
			$"		// Create the {actor} game object",
			$"		{actor} actor = new GameObject(config.name).AddComponent<{actor}>();",
			$"		actor.config = config;",
			"",
			$"		Transform transform = actor.transform;",
			"		transform.position = position;",
			"		transform.rotation = rotation;",
			"		transform.SetParent(parent);",
			"",
			$"		// Construct the {actor} MonoBehaviour components",
			"		foreach (var componentConfig in config.components)",
			"		{",
			"			if (componentConfig == null)",
			"			{",
			$"				Debug.LogError($\"{configNameString} has a NULL item in its component list. Consider removing it.\");",
			"				continue;",
			"			}",
			"",
			$"			componentConfig.ConstructSystemComponent(actor);",
			"		}",
			"",
			$"		actor.FindSystems();",
			"",
			$"		foreach (var system in actor.allSystems)",
			$"			system.LateSetup();",
			"",
			$"		return actor;",
			"	}",
			"}",
		});
	}
}