
public static class Debug
{
	public static void LogError(string text) => Log(text, ConsoleColor.Red);
	public static void LogSuccess(string text) => Log(text, ConsoleColor.Green);
	public static void LogWarning(string text) => Log(text, ConsoleColor.Yellow);
	public static void Log(string text) => Log(text, ConsoleColor.White);

	public static void LogFileExistsError(string fileName) => Debug.LogError($"{fileName} already exists. Skipping.");
	public static void LogFileGenerated(string fileName) => Debug.LogSuccess($"{fileName} generated successfully.");
	public static void LogFileOverwrite(string fileName) => Debug.LogWarning($"{fileName} overwritten successfully.");
	
	public static void Log(string text, ConsoleColor color)
	{
		Console.ForegroundColor = color;
		Console.WriteLine(text);
		Console.ForegroundColor = ConsoleColor.White;
	}
}