namespace FileWriter;

// More correct to implement a config file (text format) and read values from it.
// For example, using a Dictionary.
/// <summary>
/// Application constants, analogous to the config.
/// </summary>
internal struct Constants
{
    public const ConsoleKey ExitKeyboardKey = ConsoleKey.Q;

    public const string TryAgainMessage = "Try again.";
    public const string ArrayCalculated = "The result is calculated.";
    public const string DefaultArgumentErrorMessage = "Wrong argument.";
    
    public const string ProgramStartedMessage = "FileWriter program started.";
    public const string ProgramFinishedMessage = "FileWriter program finished.";
    
    public const string ArraySizeRangeErrorMessage = "The value must be greater than 0 and less than or equal to 15.";
    
    public static readonly char[] FileNameExcludeLetters = Path.GetInvalidPathChars();
    public static readonly char[] FileNameNotRecommendedLetters = 
        {'%', '#', '&', '{', '}', '$', '!', '\'', '@', '+', '=', ' '};
    
    public static readonly string ExcludeLettersErrorMessage =
        $"Do not use the following characters in the file name: {string.Join(", ", FileNameExcludeLetters)}";

    public static readonly string NotRecommendedLettersWarningMessage =
        $"Characters not recommended for use in the file name: {string.Join(", ", FileNameNotRecommendedLetters)}";

    public const string SecurityErrorMessage = "Security error detected.";
    public const string FileNameSameAsProcessNameMessage = 
        "The file name is the same as the program name, try another name.";
    
    public const string ArraySizeInputMessage = "Enter the size of a two-dimensional array:";
    public const string FileNameInputMessage = "Enter the file name:";
    
    public static readonly Func<string, string> FileSavedPathMessage = 
        (string filePath) => $"The file is saved on the path: {filePath}";

    public const string EmptyInput = "";

    public const string SaveFileExistMessage = "The file already exists, trying to overwrite it.";
    public const string SaveFileMessage = "Saving the file.";

    public const string FileNameErrorMessage = "Try again, with another filename.";
    public const string PathTooLongErrorMessage =
        "The specified path, file name, or both exceed the system-defined maximum length.";
    public static readonly string AgainMessage = $"Press any key to restart or {ExitKeyboardKey} to exit.";
    public const string SaveFileUnexpectedErrorMessage = "An unexpected error occurred while saving the file.";
}