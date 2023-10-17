using System.Security;
using Lib;

namespace FileReader;

/// <summary>
/// The handler of the file by the name entered by the user.
/// </summary>
internal static class ReadFileHandler
{
    /// <summary>
    /// Processes a string of data.
    /// </summary>
    /// <param name="content">File content.</param>
    /// <returns>Two-dimensional array of processed string.</returns>
    /// <exception cref="IndexOutOfRangeException">Mismatch of the number of columns or rows.</exception>
    /// <exception cref="FormatException">Incorrect delimiters or empty elements.</exception>
    private static double[][] ParseFileContent(string content)
    {
        // Removes empty lines at the end
        string[] lines = content.TrimEnd().Split(Lib.Constants.LinesSeparator);
        if (lines.Length == 0)
        {
            throw new IndexOutOfRangeException(Constants.FileEmptyLinesErrorMessage);
        }

        string[] sizes = lines[0].Trim().Split(Lib.Constants.ArraySizesSeparator);
        if (sizes.Length != 2)
        {
            throw new IndexOutOfRangeException(Constants.FileFirstLineErrorMessage);
        }

        bool rowCountParsed = int.TryParse(sizes[0], out int rowCount);
        bool columnCountParsed = int.TryParse(sizes[1], out int columnCount);
        if (!rowCountParsed || !columnCountParsed)
        {
            throw new IndexOutOfRangeException(Constants.SizesParseErrorMessage);
        }

        if (lines.Length - 1 != rowCount)
        {
            throw new IndexOutOfRangeException(Constants.RowSizeErrorMessage);
        }

        // Initialize array to fill.
        double[][] array = new double[rowCount][];
        
        for (int rowIdx = 0; rowIdx < rowCount; rowIdx++)
        {
            // Starts from second element.
            string row = lines[rowIdx + 1];
            array[rowIdx] = new double[columnCount];
            
            if (!row.EndsWith(Lib.Constants.LineEnd))
            {
                throw new FormatException(Constants.LineEndErrorMessage);
            }

            row = row.Remove(row.Length - Lib.Constants.LineEnd.Length);
            string[] rowElements = row.Split(Lib.Constants.ElementsSeparator);
            
            if (rowElements.Length != columnCount)
            {
                throw new IndexOutOfRangeException(Constants.ColumnSizeErrorMessage);
            }

            foreach (var element in rowElements.Select((value, idx) => (value, idx)))
            {
                if (!double.TryParse(element.value, out double parsedValue))
                {
                    throw new FormatException(Constants.ElementParseErrorMessage);
                }
                array[rowIdx][element.idx] = Math.Round(parsedValue, Lib.Constants.RoundNumber);
            }
        }
        
        return array;
    }

    /// <summary>
    /// Produces a matrix shift.
    /// </summary>
    /// <param name="arr">Two-dimensional array.</param>
    private static void ShiftArray(ref double[][] arr)
    {
        int rowSize = arr.Length;
        double[][] arrayBox = new double[arr.Length][];
        
        for (int i = 0; i < rowSize; i++)
        {
            int columnLength = (int)Math.Floor(arr[i].Length / 2.0);
            arrayBox[i] = new double[columnLength];
            
            for (int j = 0; j < columnLength; j++)
            {
                arrayBox[i][j] = arr[i][j * 2 + 1];
            }
        }

        arr = arrayBox;
    }
    
    /// <summary>
    /// Use for handling file opening and handling Exceptions.
    /// </summary>
    /// <param name="path">File path (relative or absolute).</param>
    private static void HandleOpenFile(string path)
    {
        try
        {
            string readText = File.ReadAllText(path);

            double[][] parsedMatrix = ParseFileContent(readText);
            ConsoleMethod.NicePrint(Constants.InitialDataMessage);
            ConsoleMethod.PrintArray(parsedMatrix);

            ShiftArray(ref parsedMatrix);
            ConsoleMethod.NicePrint(Constants.ProcessedDataMessage);
            ConsoleMethod.PrintArray(parsedMatrix);
        }
        catch (IndexOutOfRangeException)
        {
            ConsoleMethod.NicePrint(Constants.FileContentFormatErrorMessage, CustomColor.ErrorColor);
        }
        catch (FormatException)
        {
            ConsoleMethod.NicePrint(Constants.FileContentFormatErrorMessage, CustomColor.ErrorColor);
        }
        catch (SecurityException)
        {
            ConsoleMethod.NicePrint(Constants.SecurityErrorMessage, CustomColor.ErrorColor);
        }
        catch (PathTooLongException)
        {
            ConsoleMethod.NicePrint(Constants.PathTooLongErrorMessage, CustomColor.ErrorColor);
        }
        catch (Exception ex)
        {
            ConsoleMethod.NicePrint(ex.Message, CustomColor.ErrorColor);
        }
    }
    
    /// <summary>
    /// File handler entry point.
    /// </summary>
    public static void Run()
    {
        ConsoleMethod.NicePrint(Constants.FileNameInputMessage);
        
        // User input filename or path.
        string? fileNameInput = Console.ReadLine();

        if (File.Exists(fileNameInput))
        {
            ConsoleMethod.NicePrint(Constants.FileExistMessage, CustomColor.ProgressColor);
            HandleOpenFile(fileNameInput);
        }
        else
        {
            ConsoleMethod.NicePrint(Constants.FileNotExistMessage, CustomColor.ErrorColor);
        }
    }
}