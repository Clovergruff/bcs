
using System.IO;

class Program
{
	public static string actorName = "";
	public static List<string> componentNames = new List<string>();
	public static string componentPrefix = "has";

	public static bool generateSystem = true;

	public static bool generateUpdateMethods = true;
	public static bool generateFixedUpdateMethods = true;
	public static bool generateLateUpdateMethods = true;

	public static bool forceOverwrite = false;

	public static bool componentMode = true;

	#if OS_WINDOWS
		public const string sl = "\\";
	#else
		public const string sl = "/";
	#endif

	public const string ASSETS_FOLDER = "Assets";
	public const string UP_ASSETS_FOLDER = $"..{sl}Assets";
	public static string assetsPath = "";
	public static string entitiesFolder = "";

	static void Main(string[] args)
	{
		if (args.Length == 0)
		{
			Debug.LogError("CAS requires parameters");
			return;
		}
		else
		{
			if (Directory.Exists(ASSETS_FOLDER))
				assetsPath = ASSETS_FOLDER;
			else if (Directory.Exists(UP_ASSETS_FOLDER))
				assetsPath = UP_ASSETS_FOLDER;
			else
			{
				Debug.LogError("Could not find the Assets folder");
				return;
			}

			entitiesFolder = $"{assetsPath}{sl}Scripts{sl}Entities";

			actorName = args[0];
			actorName = actorName.FirstCharToUpper();

			// Components			
			if (args.Length > 1)
			{
				for (int i = 1; i < args.Length; i++)
				{
					string arg = args[i];
					
					// Flag
					if (arg[0] == '-')
					{
						componentMode = false;

						switch (arg)
						{
							case "-p":
							case "--prefix":
								if (i < args.Length - 1)
								{
									var potentialPrefix = args[i+1];
									if (potentialPrefix[0] != '-')
										componentPrefix = potentialPrefix;
								}
								break;
							case "-f":
							case "--force":
								forceOverwrite = true;
								break;

							case "--no-system":
								generateSystem = false;
								break;
							case "--no-update":
								generateUpdateMethods = false;
								break;
							case "--no-lateupdate":
								generateLateUpdateMethods = false;
								break;
							case "--no-fixedupdate":
								generateFixedUpdateMethods = false;
								break;
						}
					}
					else if (componentMode)
					{
						componentNames.Add(arg.FirstCharToUpper());
					}
				}
			}

			Debug.Log($"Generating {actorName} actor...");
			GenerateActorClasses();

			if (componentNames.Count > 0)
			{
				if (componentNames.Count > 1)
					Debug.Log("Generating components...");

				foreach (var componentName in componentNames)
					GenerateComponentClasses(componentName);
			}
			Debug.Log("");
		}

	}

	public static void GenerateComponentClasses(string componentName)
	{
		Debug.Log($"Generating {componentName}...");

		string folderPath = $"{entitiesFolder}{sl}{actorName}{sl}Components{sl}{componentName}";
		string editorFolderPath = $"{folderPath}{sl}Editor";

		string configClassPath = $"{folderPath}{sl}{actorName}{componentName}Config.cs";
		string systemClassPath = $"{folderPath}{sl}{actorName}{componentName}System.cs";
		string extensionClassPath = $"{folderPath}{sl}{actorName}{componentName}Ext.cs";

		string editorClassPath = $"{editorFolderPath}{sl}{actorName}{componentName}ConfigEditor.cs";

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		if (!Directory.Exists(editorFolderPath))
			Directory.CreateDirectory(editorFolderPath);

		Generator.CreateConfigClass(configClassPath, actorName, componentName);
		Generator.CreateComponentConfigEditorClass(editorClassPath, actorName, componentName);
		
		if (generateSystem)
			Generator.CreateSystemClass(systemClassPath, actorName, componentName);

		Generator.CreateExtensionClass(extensionClassPath, actorName, componentPrefix, componentName);
		Debug.Log("Done!\n");
	}

	public static void GenerateActorClasses()
	{

		string actorFolderPath = $"{entitiesFolder}{sl}{actorName}";
		string componentsFolderPath = $"{actorFolderPath}{sl}Components";
		string editorFolderPath = $"{actorFolderPath}{sl}Editor";

		string actorBase = $"{actorFolderPath}{sl}{actorName}";

		string mainClassPath = 				$"{actorBase}.cs";
		string baseComponentConfigPath = 	$"{actorBase}ComponentConfig.cs";
		string actorConfigClassPath = 		$"{actorBase}Config.cs";
		string managerClassPath = 			$"{actorBase}ActorManager.cs";
		string factoryClassPath = 			$"{actorBase}Factory.cs";
		string baseSystemClassPath = 		$"{actorBase}System.cs";

		string actorConfigEditorClassPath = 	$"{editorFolderPath}{sl}{actorName}ConfigEditor.cs";

		if (!Directory.Exists(componentsFolderPath))
			Directory.CreateDirectory(componentsFolderPath);

		if (!Directory.Exists(editorFolderPath))
			Directory.CreateDirectory(editorFolderPath);

		Generator.CreateMainActorClass(mainClassPath, actorName, generateUpdateMethods, generateFixedUpdateMethods, generateLateUpdateMethods);
		Generator.CreateComponentConfigClass(baseComponentConfigPath, actorName);
		Generator.CreateActorConfigClass(actorConfigClassPath, actorName);
		Generator.CreateActorManagerClass(managerClassPath, actorName);
		Generator.CreateActorFactoryClass(factoryClassPath, actorName);
		Generator.CreateSystemBaseClass(baseSystemClassPath, actorName, generateUpdateMethods, generateFixedUpdateMethods, generateLateUpdateMethods);
		Generator.CreateActorConfigEditorClass(actorConfigEditorClassPath, actorName);

		Debug.Log("Done!\n");
	}
}