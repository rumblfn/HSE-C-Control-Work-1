namespace FileReader;

internal struct Constants
{
    public const ConsoleKey ExitKeyboardKey = ConsoleKey.Q;
    
    public const string FileNameInputMessage = "Enter the file name:";
    public const string ProgramStartedMessage = "FileReader program started.";
    public const string ProgramFinishedMessage = "FileReader program finished.";
    public const string FileContentFormatErrorMessage = "The data format does not match the condition.";
    
    public const string FileFirstLineErrorMessage = "Sizes error.";
    public const string FileEmptyLinesErrorMessage = "Empty lines.";
    public const string RowSizeErrorMessage = "RowSizeErrorMessage.";
    public const string LineEndErrorMessage = "LineEndErrorMessage.";
    public const string ColumnSizeErrorMessage = "ColumnSizeErrorMessage.";
    public const string ElementParseErrorMessage = "ElementParseErrorMessage.";
    public const string SizesParseErrorMessage = "Cannot parse sizes of matrix.";

    public const string InitialDataMessage = "Initial data:";
    public const string ProcessedDataMessage = "Data after processing:";

    public const string FileExistMessage = "Something was found. Trying to open it.";
    public const string FileNotExistMessage = "Nothing was found for the specified input.";
    
    public static readonly string AgainMessage = $"Press any key to restart or {ExitKeyboardKey} to exit.";
}