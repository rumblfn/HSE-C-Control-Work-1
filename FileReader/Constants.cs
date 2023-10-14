namespace FileReader;

public struct Constants
{
    public const string ProgramStartedMessage = "FileReader program started.";
    public const string ProgramFinishedMessage = "FileReader program finished.";
    public const string FileNameInputMessage = "Enter the file name:";

    public const string FileExistMessage = "Something was found. Trying to open it.";
    public const string FileNotExistMessage = "Nothing was found for the specified input.";
    
    public const ConsoleKey ExitKeyboardKey = ConsoleKey.Q;
    public static readonly string AgainMessage = $"Press any key to restart or {ExitKeyboardKey} to exit.";
}