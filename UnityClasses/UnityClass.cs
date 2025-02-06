
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class UnityClass
{
	protected string filePath = "";
	protected string filename = "";
	protected string lowerCaseComponentName = "";
	protected string lowerCaseActorName = "";

	protected string actor = "";
	protected string prefix = "";
	protected string component = "";
	protected bool hasUpdate = true;
	protected bool hasFixedUpdate = true;
	protected bool hasLateUpdate = true;

	protected List<string> lines = new List<string>();

	public UnityClass(string filePath)
	{
		this.filePath = filePath;
		filename = Path.GetFileName(filePath);
	}

	public void Generate(
		string actor = "",
		string prefix = "has",
		string component = "",
		bool hasUpdate = true,
		bool hasFixedUpdate = true,
		bool hasLateUpdate = true)
	{
		this.actor = actor;
		this.prefix = prefix;
		this.component = component;
		this.hasUpdate = hasUpdate;
		this.hasFixedUpdate = hasFixedUpdate;
		this.hasLateUpdate = hasLateUpdate;

		lowerCaseComponentName = component.FirstCharToLower();
		lowerCaseActorName = actor.FirstCharToLower();

		GenerateLines();
		WriteFile();
	}

	protected virtual void GenerateLines()
	{
	}

	private void WriteFile()
	{
		var fileWriteState = GetFileWriteState(filePath);

		if (!fileWriteState.canWrite && fileWriteState.fileExists)
		{
			Debug.LogFileExistsError(filename);
		}
		else
		{
			try
			{
				using (StreamWriter o = new StreamWriter(filePath))
				{
					foreach (var line in lines)
						o.WriteLine(line);
				}

				if (fileWriteState.canWrite && fileWriteState.fileExists)
					Debug.LogFileOverwrite(filename);
				else
					Debug.LogFileGenerated(filename);
			}
			catch
			{
				Debug.LogError($"Could not write {filename}");
			}
		}
	}

	protected void AddLine(string line) => lines.Add(line);
	protected void AddLines(string[] newLines)
	{
		foreach (var line in newLines)
			lines.Add(line);
	}

	private (bool canWrite, bool fileExists) GetFileWriteState(string filePath)
	{
		bool fileExists = File.Exists(filePath);
		if (Program.forceOverwrite)
			return (true, fileExists);
			
		return (!fileExists, fileExists);
	}
}