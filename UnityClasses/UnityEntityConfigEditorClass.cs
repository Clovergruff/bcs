public class UnityActorConfigEditorClass : UnityClass
{
	public UnityActorConfigEditorClass(string filePath) : base(filePath)
	{
	}

	protected override void GenerateLines()
	{
		AddLines(new string[]
		{
			"using UnityEditor;",
			"using Gruffdev.BCSEditor;",
			"",
			$"[CustomEditor(typeof({actor}Config))]",
			$"public class {actor}ConfigEditor : ActorConfigAssetEditorBase<{actor}ComponentConfig, {actor}Config>",
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
			"				EditorUtility.SetDirty(actorConfigAsset);",
			"				serializedObject.ApplyModifiedProperties();",
			"			}",
			"		}",
			"	}",
			"}",
		});
	}
}