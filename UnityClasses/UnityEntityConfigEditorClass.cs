public class UnityEntityConfigEditorClass : UnityClass
{
	public UnityEntityConfigEditorClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLines(new string[]
		{
			"using UnityEditor;",
			"using Gruffdev.BCSEditor;",
			"",
			$"[CustomEditor(typeof({entity}Config))]",
			$"public class {entity}ConfigEditor : EntityConfigAssetEditorBase<{entity}ComponentConfig, {entity}Config>",
			"{",
			"	protected override void OnEnable()",
			"	{",
			"		base.OnEnable();",
			"	}",
			"",
			"	public override void OnInspectorGUI()",
			"	{",
			"		using (var check = new EditorGUI.ChangeCheckScope())",
			"		{",
			"			DrawComponentList();",
			"",
			"			if (check.changed)",
			"			{",
			"				EditorUtility.SetDirty(entityConfigAsset);",
			"				serializedObject.ApplyModifiedProperties();",
			"			}",
			"		}",
			"	}",
			"}",
		});
	}
}