namespace Lib;
public abstract class StringMethod
{
    public static bool ContainsAny(string content, IEnumerable<char> includeLetters)
    {
        return includeLetters.Any(content.Contains);
    }
}