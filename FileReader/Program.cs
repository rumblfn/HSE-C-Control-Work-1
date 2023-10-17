using Lib;

namespace FileReader;

/// <summary>
/// Main class of the file reader program.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Checks for exit from the program.
    /// </summary>
    /// <returns>Key is not <see cref="Constants.ExitKeyboardKey"/>.</returns>
    private static bool HandleAgain()
    {
        ConsoleMethod.NicePrint(Constants.AgainMessage, CustomColor.SystemColor);
        return Console.ReadKey(true).Key != Constants.ExitKeyboardKey;
    }
    
    /// <summary>
    /// Entry point of the program.
    /// </summary>
    private static void Main()
    {
        ConsoleMethod.NicePrint(Constants.ProgramStartedMessage, CustomColor.SystemColor);
        
        // Loop handler.
        do
        {
            ReadFileHandler.Run();
        } while (HandleAgain());
        
        ConsoleMethod.NicePrint(Constants.ProgramFinishedMessage, CustomColor.ProgressColor);
    }
}
