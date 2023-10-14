using Lib;

namespace FileReader;

public static class ReadFileHandler
{
    /// <summary>
    /// File handler entry point.
    /// </summary>
    public static void Run()
    {
        ConsoleMethod.NicePrint(Constants.FileNameInputMessage);
        
        // User input filename or path.
        string? fileNameInput = Console.ReadLine();

        if (File.Exists(fileNameInput))
        {
            ConsoleMethod.NicePrint(Constants.FileExistMessage, CustomColor.ProgressColor);
            // TODO: handle open file
        }
        else
        {
            ConsoleMethod.NicePrint(Constants.FileNotExistMessage, CustomColor.ErrorColor);
        }
    }
}