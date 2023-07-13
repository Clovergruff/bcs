public class UnityEntityFactoryClass : UnityClass
{
	public UnityEntityFactoryClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		string configNameString = "{config.name}";

		AddLine("using UnityEngine;");
		AddLine("");

		AddLine($"public static class {entity}Factory");
		AddLine("{");
		AddLine($"public static {entity} CreatePooled({entity}Config config, Vector3 position, Quaternion rotation, Transform parent = null)");
		AddLine("{");
		AddLine($"	if ({entity}EntityManager.I.inactiveEntities.Count > 0)");
		AddLine("	{");
		AddLine($"		{entity} entity = {entity}EntityManager.I.inactiveEntities[0];");
		AddLine("		entity.gameObject.SetActive(true);");
		AddLine("	");
		AddLine("		Transform transform = entity.transform;");
		AddLine("		transform.position = position;");
		AddLine("		transform.rotation = rotation;");
		AddLine("		transform.SetParent(parent);");
		AddLine("");
		AddLine("		foreach (var system in entity.allSystems)");
		AddLine("			system.ReusedSetup();");
		AddLine("");
		AddLine("		return entity;");
		AddLine("	}");
		AddLine("	else");
		AddLine("	{");
		AddLine("		return Create(config, position, rotation, parent);");
		AddLine("	}");
		AddLine("}");
		AddLine("");
		AddLine($"	public static {entity} Create({entity}Config config, Vector3 position, Quaternion rotation, Transform parent = null)");
		AddLine("	{");
		AddLine($"		// Create the {entity} game object");
		AddLine($"		{entity} entity = new GameObject(config.name).AddComponent<{entity}>();");
		AddLine("");
		AddLine("		");
		AddLine($"		Transform transform = entity.transform;");
		AddLine("		transform.position = position;");
		AddLine("		transform.rotation = rotation;");
		AddLine("		transform.SetParent(parent);");
		AddLine("");
		AddLine($"		// Construct the {entity} MonoBehaviour components");
		AddLine("		foreach (var componentConfig in config.components)");
		AddLine("		{");
		AddLine("			if (componentConfig == null)");
		AddLine("			{");
		AddLine($"				Debug.LogError($\"{configNameString} has a NULL item in its component list. Consider removing it.\");");
		AddLine("				continue;");
		AddLine("			}");
		AddLine("");
		AddLine($"			componentConfig.ConstructSystemComponent(entity);");
		AddLine("		}");
		AddLine("");
		AddLine($"		entity.FindSystems();");
		AddLine("");
		AddLine($"		foreach (var system in entity.allSystems)");
		AddLine($"			system.LateSetup();");
		AddLine("");
		AddLine($"		entity.Init(config);");
		AddLine("");
		AddLine($"		return entity;");
		AddLine("	}");
		AddLine("}");
	}
}