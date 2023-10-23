using System.Security;
using System.Text;
using Lib;

namespace FileWriter;

/// <summary>
/// It's used to calculate and save two-dimensional arrays in file.
/// </summary>
internal abstract class WriteArrayHandler
{
    private static readonly string BasePath = AppDomain.CurrentDomain.BaseDirectory;

    /// <summary>
    /// Calculates the value of the function f(n) = n * cos(n) / (n^2 + 1).
    /// </summary>
    /// <param name="n">The argument of the calculated function.</param>
    /// <returns>function result.</returns>
    private static double CalcFunc(double n)
    {
        double numerator = n * Math.Cos(n);
        double denominator = Math.Pow(n, 2) + 1;
        
        return numerator / denominator;
    }

    /// <summary>
    /// Fills an array with numbers.
    /// </summary>
    /// <param name="array">Array to save.</param>
    /// <param name="size">Two-dimensional array size.</param>
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
    /// <param name="array">Two-dimensional array.</param>
    /// <returns>String with breaks.</returns>
    private static string ConvertTwoDimensionalArrayToString(in double[,] array)
    {
        // Get array sizes.
        int rowSize = array.GetLength(0);
        int columnSize = array.GetLength(1);

        int elementsSeparatorLength = Lib.Constants.ElementsSeparator.Length;
        var resultString = new StringBuilder($"{rowSize}{Lib.Constants.ArraySizesSeparator}{columnSize}{Environment.NewLine}");
        
        for (int rowIndex = 0; rowIndex < rowSize; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < columnSize; columnIndex++)
            {
                resultString.Append($"{array[rowIndex, columnIndex]:F2}" + Lib.Constants.ElementsSeparator);
            }

            // Removes last element separator.
            if (columnSize > 0)
            {
                resultString.Remove(resultString.Length - elementsSeparatorLength, elementsSeparatorLength);
            }
            
            resultString.Append(Lib.Constants.LineEnd);

            // Add LinesSeparator to all lines except the last one.
            if (rowIndex < rowSize - 1)
            {
                resultString.Append(Lib.Constants.LinesSeparator);
            }
        }

        // Convert StringBuilder to string.
        return resultString.ToString();
    }
    
    /// <summary>
    /// Method saves the text to a file.
    /// </summary>
    /// <param name="filePath">Path where save.</param>
    /// <param name="content">Text to save</param>
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
        catch (ArgumentException ex)
        {
            ConsoleMethod.NicePrint(ex.Message);
        }
        catch (SecurityException)
        {
            ConsoleMethod.NicePrint(Constants.SecurityErrorMessage, CustomColor.ErrorColor);
        }
        catch (PathTooLongException)
        {
            ConsoleMethod.NicePrint(Constants.PathTooLongErrorMessage, CustomColor.ErrorColor);
        }
        catch (IOException ex)
        {
            ConsoleMethod.NicePrint(ex.Message, CustomColor.ErrorColor);
        }
        catch (Exception ex)
        {
            ConsoleMethod.NicePrint(ex.Message, CustomColor.ErrorColor);
        }
        
        ConsoleMethod.NicePrint(Constants.FileNameErrorMessage);
        return false;
    }

    /// <summary>
    /// Use this method to save the content to a file with the specified name.
    /// The method is necessary to call again and handle errors related to the file name.
    /// </summary>
    /// <param name="content">Content to write to a file.</param>
    /// <returns>Save success status.</returns>
    private static bool HandleSaveFile(in string content)
    {
        string? fileNameInput;
        do
        {
            ConsoleMethod.NicePrint(Constants.FileNameInputMessage);
            fileNameInput = Console.ReadLine();
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