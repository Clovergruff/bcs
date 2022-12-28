using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataScriptableObject : ScriptableObject
{
	public virtual bool alwaysEnableFoldout {get;}
	public bool foldedOut {get; set;}

	public static bool IsComponentNull(ScriptableObject stackObject, DataScriptableObject dataScriptableObject)
	{
		if (dataScriptableObject == null)
		{
			Debug.LogWarning($"{stackObject.name} has a null item in it's data list. Please consider a cleanup.");
			return true;
		}

		return false;
	}
}
