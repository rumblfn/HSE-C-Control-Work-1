using Lib;

namespace FileWriter;

internal abstract class Validator
{
    /// <summary>
    /// Validates and saves the value entered by the user according to the criteria of the task.
    /// </summary>
    /// <param name="input">User input.</param>
    /// <param name="arraySize">The variable in which the checked value should be stored.</param>
    /// <returns>Validation status.</returns>
    public static bool IsValidSizeInput(string? input, out int arraySize)
    {
        string error = "";
        bool valid = false;
        bool sizeParsed = int.TryParse(input, out arraySize);

        if (sizeParsed)
        {
            if (0 < arraySize && arraySize <= 15) valid = true;
            else error = Constants.ArraySizeRangeErrorMessage;
        }
        else error = Constants.DefaultArgumentErrorMessage;

        if (error.Length > 0)
        {
            error += Environment.NewLine + Constants.TryAgainMessage;
            ConsoleMethod.NicePrint(error, CustomColor.ErrorColor);
        }
        
        return valid;
    }
    
    /// <summary>
    /// Validates file name.
    /// </summary>
    /// <param name="fileName">FileName to check.</param>
    /// <returns>Is file name correct.</returns>
    public static bool IsValidFileNameInput(string? fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            return false;
        }
        
        if (StringMethod.ContainsAny(fileName, Constants.FileNameNotRecommendedLetters))
        {
            ConsoleMethod.NicePrint(Constants.NotRecommendedLettersWarningMessage, CustomColor.WarningColor);
        }

        return true;
    }
}