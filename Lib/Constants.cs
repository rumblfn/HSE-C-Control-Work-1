namespace Lib;

/// <summary>
/// The constant values used by both projects can be changed to any values.
/// </summary>
public struct Constants
{
    // The separator between the array sizes in the matrix record format.
    public const string ArraySizesSeparator = " ";
    
    // Sep for human read array format.
    public const string LineEnd = "**";
    public const string ElementsSeparator = "*";
    public static readonly string LinesSeparator = Environment.NewLine;

    // Number of digits after the decimal separator.
    public const int RoundNumber = 2;
    
    // Messages.
    public const string EmptyArrayMessage = "Empty array.";
}