using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public abstract class EntityDataAssetEditorBase<T1, T2> : Editor
	where T1 : DataScriptableObject
	where T2 : EntityDataAsset<T1>
{
	private const int FOLDOUT_WIDTH = 13;

	protected T2 entityDataAsset;
	protected GUIStyle iconButtonStyle = new GUIStyle();
	protected GUIStyle componentListStyle = new GUIStyle();

	public EntityDataEditorInstance editorInstance;

	protected virtual void OnEnable()
	{
		entityDataAsset = (T2)target;

		iconButtonStyle.normal.background = null;
		iconButtonStyle.active.background = null;
		iconButtonStyle.hover.background = null;

		RegenerateEditors();
	}

	protected void DrawComponentList()
	{
		float oneLineHeight = EditorGUIUtility.singleLineHeight;
		int editorCount = editorInstance.editors.Length;

		entityDataAsset.foldedOut = EditorExt.FoldoutHeader("Components", entityDataAsset.foldedOut);

		if (entityDataAsset.foldedOut)
		{
			EditorExt.BeginBoxGroup();
				for (int i = 0; i < editorCount; i++)
				{
					Editor editor = editorInstance.editors[i];

					using (var check = new EditorGUI.ChangeCheckScope())
					{
						// Header
						GUILayout.BeginHorizontal();

							int oldIndentLevel = EditorGUI.indentLevel;
							bool canBeFoldedOut = false;
							EditorGUI.indentLevel = 0;

							if (entityDataAsset.components[i] != null)
							{
								var iterator = editor.serializedObject.GetIterator();
								if (iterator.CountRemaining() > 1)
								{
									canBeFoldedOut = true;

									if (!entityDataAsset.components[i].foldedOut)
									{
										EditorGUI.BeginDisabledGroup(false);
											entityDataAsset.components[i].foldedOut = EditorGUILayout.Toggle(entityDataAsset.components[i].foldedOut, EditorStyles.foldout, GUILayout.Width(FOLDOUT_WIDTH));
										EditorGUI.EndDisabledGroup();
									}
									else
									{
										entityDataAsset.components[i].foldedOut = EditorGUILayout.Toggle(entityDataAsset.components[i].foldedOut, EditorStyles.foldout, GUILayout.Width(FOLDOUT_WIDTH));
									}
								}
							}

							if (!canBeFoldedOut)
								GUILayout.Space(FOLDOUT_WIDTH + 3);						

							entityDataAsset.components[i] = (T1)EditorGUILayout.ObjectField(entityDataAsset.components[i], typeof(T1), false);

							if (GUILayout.Button("-", GUILayout.Width(oneLineHeight), GUILayout.Height(oneLineHeight)))
							{
								entityDataAsset.components.RemoveAt(i);
								RegenerateEditors();
								return;
							}
							EditorGUI.indentLevel = oldIndentLevel;

						GUILayout.EndHorizontal();
						
						GUILayout.Space(2);

						if (check.changed)
						{
							RegenerateEditors();
							return;
						}

						// Component Editor
						if (entityDataAsset.components[i] != null && entityDataAsset.components[i].foldedOut)
						{
							EditorExt.BeginBoxGroup();
							EditorGUI.indentLevel++;
								editor.OnInspectorGUI();
							EditorGUI.indentLevel--;
							EditorExt.EndBoxGroup();
						}

						if (i != editorCount -1)
							GUILayout.Space(5);

						if (check.changed)
							ApplyChanges();
					}
				}

				GUILayout.Space(3);

				// Add Component area
				GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
						if (GUILayout.Button("Add Component", GUILayout.Width(200), GUILayout.Height(oneLineHeight + 6)))
						{
							entityDataAsset.components.Add(null);
							ApplyChanges();
							RegenerateEditors();
						}
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();

				GUILayout.Space(3);

			EditorExt.EndBoxGroup();
		}

		// Drag items from the assets window
		if (Event.current.type == EventType.DragUpdated)
		{
			DragAndDrop.visualMode = DragAndDropVisualMode.Link;
			Event.current.Use();
		}
		else if (Event.current.type == EventType.DragPerform)
		{
			bool componentsUpdated = false;
			DragAndDrop.AcceptDrag();

			if (DragAndDrop.paths.Length == DragAndDrop.objectReferences.Length)
			{
				for (int i = 0; i < DragAndDrop.objectReferences.Length; i++)
				{
					object obj = DragAndDrop.objectReferences[i];
					string path = DragAndDrop.paths[i];

					if (obj is T1 dataAsset)
					{
						entityDataAsset.components.Add(dataAsset);
						ApplyChanges();
						componentsUpdated = true;
					}
				}
			}

			if (componentsUpdated)
				RegenerateEditors();
		}
	}

	private void ApplyChanges()
	{
		EditorUtility.SetDirty(entityDataAsset);
		serializedObject.ApplyModifiedProperties();
	}


	private void RegenerateEditors()
	{
		editorInstance = (EntityDataEditorInstance)ScriptableObject.CreateInstance(typeof(EntityDataEditorInstance));

		int editorCount = entityDataAsset.components.Count;
		editorInstance.editors = new Editor[editorCount];

		for (int i = 0; i < editorCount; i++)
		{
			if (entityDataAsset.components[i] == null)
				continue;

			editorInstance.editors[i] = Editor.CreateEditor(entityDataAsset.components[i]);
		}
	}
}
