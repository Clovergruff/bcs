public class UnityComponentConfigEditorClass : UnityClass
{
	public UnityComponentConfigEditorClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLines(new string[]
		{
			"using UnityEditor;",
			"using Gruffdev.BCSEditor;",
			"",
			$"[CustomEditor(typeof({actor}{component}Config))]",
			$"public class {actor}{component}ConfigEditor : ActorComponentEditorBase<{actor}{component}Config>",
			"{",
			"",
			"	protected override void OnEnable()",
			"	{",
			"		base.OnEnable();",
			"	}",
			"",
			"	public override void OnInspectorGUI()",
			"	{",
			"		using (var check = new EditorGUI.ChangeCheckScope())",
			"		{",
			"			base.OnInspectorGUI();",
			"			",
			"			if (check.changed)",
			"			{",
			"				EditorUtility.SetDirty(config);",
			"				serializedObject.ApplyModifiedProperties();",
			"			}",
			"		}",
			"	}",
			"}",
		});
	}
}