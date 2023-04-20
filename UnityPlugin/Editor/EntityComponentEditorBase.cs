using UnityEditor;

public class EntityComponentEditorBase<T> : Editor
	where T : DataScriptableObject
{
	protected T data;

	protected virtual void OnEnable()
	{
		if (target != null)
			data = (T)target;
	}

	public override void OnInspectorGUI()
	{
		using (var check = new EditorGUI.ChangeCheckScope())
		{
			this.DrawDefaultInspectorWithoutScriptField();

			if (check.changed)
			{
				EditorUtility.SetDirty(data);
				serializedObject.ApplyModifiedProperties();
			}
		}
	}
}