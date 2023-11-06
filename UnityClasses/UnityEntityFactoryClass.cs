public class UnityEntityFactoryClass : UnityClass
{
	public UnityEntityFactoryClass(string filePath) : base(filePath)
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
			$"public static class {entity}Factory",
			"{",
			$"	public static {entity} CreatePooled({entity}Config config, Vector3 position, Quaternion rotation, Transform parent = null)",
			"	{",
			$"		if ({entity}EntityManager.I.inactiveEntities.Count > 0)",
			"		{",
			$"			{entity} entity = {entity}EntityManager.I.inactiveEntities[0];",
			"			entity.gameObject.SetActive(true);",
			"		",
			"			Transform transform = entity.transform;",
			"			transform.position = position;",
			"			transform.rotation = rotation;",
			"			transform.SetParent(parent);",
			"",
			"			foreach (var system in entity.allSystems)",
			"				system.ReusedSetup();",
			"",
			"			return entity;",
			"		}",
			"		else",
			"		{",
			"			return Create(config, position, rotation, parent);",
			"		}",
			"	}",
			"",
			$"	public static {entity} Create({entity}Config config, Vector3 position, Quaternion rotation, Transform parent = null)",
			"	{",
			$"		// Create the {entity} game object",
			$"		{entity} entity = new GameObject(config.name).AddComponent<{entity}>();",
			$"		entity.config = config",
			"",
			$"		Transform transform = entity.transform;",
			"		transform.position = position;",
			"		transform.rotation = rotation;",
			"		transform.SetParent(parent);",
			"",
			$"		// Construct the {entity} MonoBehaviour components",
			"		foreach (var componentConfig in config.components)",
			"		{",
			"			if (componentConfig == null)",
			"			{",
			$"				Debug.LogError($\"{configNameString} has a NULL item in its component list. Consider removing it.\");",
			"				continue;",
			"			}",
			"",
			$"			componentConfig.ConstructSystemComponent(entity);",
			"		}",
			"",
			$"		entity.FindSystems();",
			"",
			$"		foreach (var system in entity.allSystems)",
			$"			system.LateSetup();",
			"",
			$"		return entity;",
			"	}",
			"}",
		});
	}
}