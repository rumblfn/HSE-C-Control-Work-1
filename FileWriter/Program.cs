using Lib;

namespace FileWriter;

/// <summary>
/// Main class of the file writer program.
/// Starts handlers.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Checks for exit from the program.
    /// </summary>
    /// <returns>Key is not escape.</returns>
    private static bool HandleAgain()
    {
        ConsoleMethod.NicePrint(Constants.AgainMessage, CustomColor.SystemColor);
        return Console.ReadKey(true).Key != Constants.ExitKeyboardKey;
    }
    
    /// <summary>
    /// Main entry point of the program.
    /// </summary>
    private static void Main()
    {
        ConsoleMethod.NicePrint(Constants.ProgramStartedMessage, CustomColor.SystemColor);
        
        // Loop handler.
        do
        {
            WriteArrayHandler.Run();
        } while (HandleAgain());
        
        ConsoleMethod.NicePrint(Constants.ProgramFinishedMessage, CustomColor.ProgressColor);
    }
}
