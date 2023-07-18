
using System.IO;

class Program
{
	public static string entityName = "";
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

			entityName = args[0];
			entityName = entityName.FirstCharToUpper();

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

			Debug.Log($"Generating {entityName} entity...");
			GenerateEntityClasses();

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

		string folderPath = $"{entitiesFolder}{sl}{entityName}{sl}Components{sl}{componentName}";
		string editorFolderPath = $"{folderPath}{sl}Editor";

		string configClassPath = $"{folderPath}{sl}{entityName}{componentName}Config.cs";
		string systemClassPath = $"{folderPath}{sl}{entityName}{componentName}System.cs";
		string extensionClassPath = $"{folderPath}{sl}{entityName}{componentName}Ext.cs";

		string editorClassPath = $"{editorFolderPath}{sl}{entityName}{componentName}ConfigEditor.cs";

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		if (!Directory.Exists(editorFolderPath))
			Directory.CreateDirectory(editorFolderPath);

		Generator.CreateConfigClass(configClassPath, entityName, componentName);
		Generator.CreateComponentConfigEditorClass(editorClassPath, entityName, componentName);
		
		if (generateSystem)
			Generator.CreateSystemClass(systemClassPath, entityName, componentName);

		Generator.CreateExtensionClass(extensionClassPath, entityName, componentPrefix, componentName);
		Debug.Log("Done!\n");
	}

	public static void GenerateEntityClasses()
	{

		string entityFolderPath = $"{entitiesFolder}{sl}{entityName}";
		string componentsFolderPath = $"{entityFolderPath}{sl}Components";
		string editorFolderPath = $"{entityFolderPath}{sl}Editor";

		string entityBase = $"{entityFolderPath}{sl}{entityName}";

		string mainClassPath = 				$"{entityBase}.cs";
		string baseComponentConfigPath = 	$"{entityBase}ComponentConfig.cs";
		string entityConfigClassPath = 		$"{entityBase}Config.cs";
		string managerClassPath = 			$"{entityBase}EntityManager.cs";
		string factoryClassPath = 			$"{entityBase}Factory.cs";
		string baseSystemClassPath = 		$"{entityBase}System.cs";

		string entityConfigEditorClassPath = 	$"{editorFolderPath}{sl}{entityName}ConfigEditor.cs";

		if (!Directory.Exists(componentsFolderPath))
			Directory.CreateDirectory(componentsFolderPath);

		if (!Directory.Exists(editorFolderPath))
			Directory.CreateDirectory(editorFolderPath);

		Generator.CreateMainEntityClass(mainClassPath, entityName, generateUpdateMethods, generateFixedUpdateMethods, generateLateUpdateMethods);
		Generator.CreateComponentConfigClass(baseComponentConfigPath, entityName);
		Generator.CreateEntityConfigClass(entityConfigClassPath, entityName);
		Generator.CreateEntityManagerClass(managerClassPath, entityName);
		Generator.CreateEntityFactoryClass(factoryClassPath, entityName);
		Generator.CreateSystemBaseClass(baseSystemClassPath, entityName, generateUpdateMethods, generateFixedUpdateMethods, generateLateUpdateMethods);
		Generator.CreateEntityConfigEditorClass(entityConfigEditorClassPath, entityName);

		Debug.Log("Done!\n");
	}
}