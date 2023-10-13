using System.Text;
using Lib;

namespace FileWriter;

/// <summary>
/// It's used to calculate and save two-dimensional arrays in file.
/// </summary>
public abstract class WriteArrayHandler
{
    private static readonly string BasePath = AppDomain.CurrentDomain.BaseDirectory;

    /// <summary>
    /// Calculates the value of the function f(n) = n * cos(n) / (n^2 + 1).
    /// </summary>
    /// <param name="n">The argument of the calculated function</param>
    /// <returns>function result</returns>
    private static double CalcFunc(double n)
    {
        double numerator = n * Math.Cos(n);
        double denominator = Math.Pow(n, 2) + 1;
        
        return Math.Round(numerator / denominator, 2);
    }

    /// <summary>
    /// Fills an array with numbers.
    /// </summary>
    /// <param name="array">array to save</param>
    /// <param name="size">two-dimensional array size</param>
    private static void FillTwoDimensionalArray(out double[,] array, int size)
    {
        // Creating a new tow-dimensional array with rank size x size.
        double[,] arr = new double[size, size];
        for (int rowIndex = 0; rowIndex < size; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < size; columnIndex++)
            {
                // Human read rows and columns.
                int numberOfCalculation = (rowIndex * 3) + (columnIndex + 1);
                arr[rowIndex, columnIndex] = CalcFunc(numberOfCalculation);
            }
        }

        // Saving to the passed array.
        array = arr;
    }

    /// <summary>
    /// Converts two-dimensional array to human read format (string).
    /// </summary>
    /// <param name="array">Two-dimensional array</param>
    /// <returns>String with breaks</returns>
    private static string ConvertTwoDimensionalArrayToString(in double[,] array)
    {
        // Get array sizes.
        int rowSize = array.GetLength(0);
        int columnSize = array.GetLength(1);
        
        var resultString = new StringBuilder($"{rowSize} {columnSize}{Environment.NewLine}");
        
        for (int rowIndex = 0; rowIndex < rowSize; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < columnSize; columnIndex++)
            {
                resultString.Append(array[rowIndex, columnIndex] + ConsoleMethod.ElementsSeparator);
            }
            resultString.Append(ConsoleMethod.ElementsSeparator + Environment.NewLine);
        }

        // Convert StringBuilder to string.
        return resultString.ToString();
    }
    
    /// <summary>
    /// Method saves the text to a file.
    /// </summary>
    /// <param name="filePath">path where save</param>
    /// <param name="content">text to save</param>
    private static bool SaveStringAsFile(string filePath, in string content)
    {
        try
        {
            ConsoleMethod.NicePrint(
                File.Exists(filePath)
                    ? Constants.SaveFileExistMessage
                    : Constants.SaveFileMessage
                , CustomColor.ProgressColor);

            File.WriteAllText(filePath, content);
            ConsoleMethod.NicePrint(Constants.FileSavedPathMessage(filePath));
            return true;
        }
        catch (Exception ex)
        {
            ConsoleMethod.NicePrint(ex.Message, CustomColor.ErrorColor);
            ConsoleMethod.NicePrint(Constants.FileNameErrorMessage);
        }

        return false;
    }

    /// <summary>
    /// Use this method to save the content to a file with the specified name.
    /// The method is necessary to call again and handle errors related to the file name.
    /// </summary>
    /// <param name="content">content to write to a file</param>
    /// <returns>save success status</returns>
    private static bool HandleSaveFile(in string content)
    {
        string fileNameInput;
        do
        {
            ConsoleMethod.NicePrint(Constants.FileNameInputMessage);
            fileNameInput = Console.ReadLine() ?? Constants.EmptyInput;
        } while (!Validator.IsValidFileNameInput(fileNameInput));
        
        // Can also use a relative path (without AppDomain).
        string filePath = BasePath + fileNameInput;
        return SaveStringAsFile(filePath, content);
    }
    
    /// <summary>
    /// Starts the handler.
    /// </summary>
    public static void Run()
    {
        int arraySize;
        string? userInput;
        
        do
        {
            ConsoleMethod.NicePrint(Constants.ArraySizeInputMessage);
            userInput = Console.ReadLine();
        } while (!Validator.IsValidSizeInput(userInput, out arraySize));
        
        FillTwoDimensionalArray(out double[,] array, arraySize);
        ConsoleMethod.NicePrint(Constants.ArrayCalculated, CustomColor.ProgressColor);

        string convertedArrayString = ConvertTwoDimensionalArrayToString(array);
        
        // Recursive call is possible, but the depth is undefined.
        bool saveFileStatus = false;
        while (!saveFileStatus)
        {
            saveFileStatus = HandleSaveFile(in convertedArrayString);
        }
    }
}