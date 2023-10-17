namespace Lib;

/// <summary>
/// Simple string methods.
/// </summary>
public abstract class StringMethod
{
    /// <summary>
    /// The method checks the occurrence of any character in the specified string.
    /// </summary>
    /// <param name="content">String to check.</param>
    /// <param name="includeLetters">Letters to check.</param>
    /// <returns>Is string contains any letter.</returns>
    public static bool ContainsAny(string content, IEnumerable<char> includeLetters)
    {
        return includeLetters.Any(content.Contains);
    }
}