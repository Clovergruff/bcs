public class UnityComponentConfigEditorClass : UnityClass
{
	public UnityComponentConfigEditorClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLine("using UnityEditor;");
		AddLine("");
		AddLine($"[CustomEditor(typeof({entity}{component}Config))]");
		AddLine($"public class {entity}{component}ConfigEditor : EntityComponentEditorBase<{entity}{component}Config>");
		AddLine("{");
		AddLine("");
		AddLine("	protected override void OnEnable()");
		AddLine("	{");
		AddLine("		base.OnEnable();");
		AddLine("	}");
		AddLine("");
		AddLine("	public override void OnInspectorGUI()");
		AddLine("	{");
		AddLine("		using (var check = new EditorGUI.ChangeCheckScope())");
		AddLine("		{");
		AddLine("			base.OnInspectorGUI();");
		AddLine("			");
		AddLine("			if (check.changed)");
		AddLine("			{");
		AddLine("				EditorUtility.SetDirty(config);");
		AddLine("				serializedObject.ApplyModifiedProperties();");
		AddLine("			}");
		AddLine("		}");
		AddLine("	}");
		AddLine("}");
	}
}