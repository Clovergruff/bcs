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
		AddLine($"	public static {entity} Create({entity}Config config) => Create(config, Vector3.zero, Quaternion.identity, null);");
		AddLine($"	public static {entity} Create({entity}Config config, Vector3 position) => Create(config, position, Quaternion.identity, null);");
		AddLine($"	public static {entity} Create({entity}Config config, Vector3 position, Transform parent) => Create(config, position, Quaternion.identity, parent);");
		AddLine($"	public static {entity} Create({entity}Config config, Vector3 position, float direction, Transform parent = null) => Create(config, position, Quaternion.Euler(0, direction, 0), parent);");
		AddLine($"	public static {entity} Create({entity}Config config, Vector3 position, Quaternion rotation, Transform parent = null)");
		AddLine("	{");
		AddLine($"		// Create the {entity} game object");
		AddLine($"		{entity} {lowerCaseEntityName} = config.prefab.enabled");
		AddLine("			? GameObject.Instantiate(config.prefab.value)");
		AddLine($"			: new GameObject(config.name).AddComponent<{entity}>();");
		AddLine("");
		AddLine("		");
		AddLine($"		Transform transform = {lowerCaseEntityName}.transform;");
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
		AddLine($"			componentConfig.ConstructSystemComponent({lowerCaseEntityName});");
		AddLine("		}");
		AddLine("");
		AddLine($"		var systems = {lowerCaseEntityName}.FindSystems();");
		AddLine("");
		AddLine("		foreach (var system in systems)");
		AddLine($"			system.LateSetup();");
		AddLine("");
		AddLine($"		{lowerCaseEntityName}.Init(config);");
		AddLine("");
		AddLine($"		return {lowerCaseEntityName};");
		AddLine("	}");
		AddLine("}");
	}
}