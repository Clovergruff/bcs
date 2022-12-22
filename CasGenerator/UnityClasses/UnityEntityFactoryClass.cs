public class UnityEntityFactoryClass : UnityClass
{
	public UnityEntityFactoryClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		string dataNameString = "{data.name}";

		AddLine("using UnityEngine;");
		AddLine("");

		AddLine($"public static class {entity}Factory");
		AddLine("{");
		AddLine($"	public static {entity} Create({entity}Data data) => Create(data, Vector3.zero, Quaternion.identity, null);");
		AddLine($"	public static {entity} Create({entity}Data data, Vector3 position) => Create(data, position, Quaternion.identity, null);");
		AddLine($"	public static {entity} Create({entity}Data data, Vector3 position, Transform parent) => Create(data, position, Quaternion.identity, parent);");
		AddLine($"	public static {entity} Create({entity}Data data, Vector3 position, float direction, Transform parent = null) => Create(data, position, Quaternion.Euler(0, direction, 0), parent);");
		AddLine($"	public static {entity} Create({entity}Data data, Vector3 position, Quaternion rotation, Transform parent = null)");
		AddLine("	{");
		AddLine($"		// Create the {entity} game object");
		AddLine($"		{entity} {lowerCaseEntityName} = data.prefab.Enabled");
		AddLine("			? GameObject.Instantiate(data.prefab.Value)");
		AddLine($"			: new GameObject(data.name).AddComponent<{entity}>();");
		AddLine("");
		AddLine("		");
		AddLine($"		Transform transform = {lowerCaseEntityName}.transform;");
		AddLine("		transform.position = position;");
		AddLine("		transform.rotation = rotation;");
		AddLine("		transform.SetParent(parent);");
		AddLine("");
		AddLine($"		// Construct the {entity} MonoBehaviour components");
		AddLine("		foreach (var componentData in data.components)");
		AddLine("		{");
		AddLine("			if (componentData == null)");
		AddLine("			{");
		AddLine($"				Debug.LogError($\"{dataNameString} has a NULL item in its component list. Consider removing it.\");");
		AddLine("				continue;");
		AddLine("			}");
		AddLine("");
		AddLine($"			componentData.ConstructSystemComponent({lowerCaseEntityName});");
		AddLine("		}");
		AddLine("");
		AddLine($"		var systems = {lowerCaseEntityName}.FindSystems();");
		AddLine("");
		AddLine("		foreach (var system in systems)");
		AddLine($"			system.LateSetup({lowerCaseEntityName});");
		AddLine("");
		AddLine($"		{lowerCaseEntityName}.Init(data);");
		AddLine("");
		AddLine($"		return {lowerCaseEntityName};");
		AddLine("	}");
		AddLine("}");
	}
}