namespace Lib;
/// <summary>
/// Class for common console methods.
/// </summary>
public abstract class ConsoleMethod
{
    /// <summary>
    /// Prints a message with a specified color.
    /// </summary>
    /// <param name="message">Message content.</param>
    /// <param name="color">Message color.</param>
    public static void NicePrint(string message, ConsoleColor color = CustomColor.DefaultColor)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    /// <summary>
    /// Outputs an array to the console with the specified delimiters.
    /// </summary>
    /// <param name="arr">Common array.</param>
    /// <param name="elementsSeparator">Sep for elements.</param>
    /// <param name="linesSeparator">End for line.</param>
    public static void PrintArray(in double [][] arr, string? linesSeparator = null, string? elementsSeparator = " ")
    {
        string[] arrayRows = new string[arr.Length];
        for (int i = 0; i < arr.Length; i++)
        {
            arrayRows[i] = string.Join(elementsSeparator, arr[i]);
        }

        NicePrint(string.Join(linesSeparator ?? Environment.NewLine, arrayRows), CustomColor.ProgressColor);
    }
}
