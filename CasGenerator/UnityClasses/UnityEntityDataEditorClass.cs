public class UnityEntityDataEditorClass : UnityClass
{
	public UnityEntityDataEditorClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLine("using UnityEditor;");
		AddLine("");
		AddLine($"[CustomEditor(typeof({entity}Data))]");
		AddLine($"public class {entity}DataEditor : EntityDataAssetEditorBase<{entity}ComponentConfig, {entity}Data>");
		AddLine("{");
		AddLine("	private SerializedProperty prefabProperty;");
		AddLine("");
		AddLine("	protected override void OnEnable()");
		AddLine("	{");
		AddLine("		base.OnEnable();");
		AddLine("		prefabProperty = serializedObject.FindProperty(\"prefab\");");
		AddLine("	}");
		AddLine("");
		AddLine("	public override void OnInspectorGUI()");
		AddLine("	{");
		AddLine("		using (var check = new EditorGUI.ChangeCheckScope())");
		AddLine("		{");
		AddLine("			// DrawDefaultInspector();");
		AddLine("			EditorGUILayout.PropertyField(prefabProperty);");
		AddLine("			// EditorGUILayout.Space();");
		AddLine("");
		AddLine("			DrawComponentList();");
		AddLine("");
		AddLine("			if (check.changed)");
		AddLine("			{");
		AddLine("				EditorUtility.SetDirty(entityDataAsset);");
		AddLine("				serializedObject.ApplyModifiedProperties();");
		AddLine("			}");
		AddLine("		}");
		AddLine("	}");
		AddLine("}");
	}
}