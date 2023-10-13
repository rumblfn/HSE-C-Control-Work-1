namespace Lib;
/// <summary>
/// Class for common console methods.
/// </summary>
public abstract class ConsoleMethod
{
    // Elements separator in human read array format.
    public const string ElementsSeparator = "*";
    
    /// <summary>
    /// Prints a message with a specified color.
    /// </summary>
    /// <param name="message">Message content</param>
    /// <param name="color">Message color</param>
    public static void NicePrint(string message, ConsoleColor color = CustomColor.DefaultColor)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}
