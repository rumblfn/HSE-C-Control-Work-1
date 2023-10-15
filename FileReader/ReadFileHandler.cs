using Lib;

namespace FileReader;

internal static class ReadFileHandler
{
    /// <summary>
    /// Checks the correctness of the data format specified by the task conditions.
    /// </summary>
    /// <param name="content">file content</param>
    /// <returns>data correctness</returns>
    private static double[][] ParseFileContent(string content)
    {
        // Remove empty lines at the end
        string[] lines = content.TrimEnd().Split(Lib.Constants.LinesSeparator);
        if (lines.Length == 0)
        {
            throw new Exception(Constants.FileEmptyLinesErrorMessage);
        }

        string[] sizes = lines[0].Trim().Split(" ");
        if (sizes.Length != 2)
        {
            throw new Exception(Constants.FileFirstLineErrorMessage);
        }

        bool rowCountParsed = int.TryParse(sizes[0], out int rowCount);
        bool columnCountParsed = int.TryParse(sizes[1], out int columnCount);
        if (!rowCountParsed || !columnCountParsed)
        {
            throw new FormatException(Constants.SizesParseErrorMessage);
        }

        if (lines.Length - 1 != rowCount)
        {
            throw new Exception(Constants.RowSizeErrorMessage);
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
                throw new Exception(Constants.LineEndErrorMessage);
            }

            // Remove line ending.
            row = row.Remove(row.Length - Lib.Constants.LineEnd.Length);
            string[] rowElements = row.Split(Lib.Constants.ElementsSeparator);
            
            if (rowElements.Length != columnCount)
            {
                throw new Exception(Constants.ColumnSizeErrorMessage);
            }

            foreach (var element in rowElements.Select((value, idx) => (value, idx)))
            {
                if (!double.TryParse(element.value, out double parsedValue))
                {
                    throw new Exception(Constants.ElementParseErrorMessage);
                }
                else
                {
                    array[rowIdx][element.idx] = Math.Round(parsedValue, Lib.Constants.RoundNumber);   
                }
            }
        }
        
        return array;
    }

    /// <summary>
    /// Produces a matrix shift.
    /// </summary>
    /// <param name="arr">two-dimensional array</param>
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
    /// <param name="path">File path (relative or absolute)</param>
    private static void HandleOpenFile(string path)
    {
        try
        {
            string readText = File.ReadAllText(path);
            try
            {
                double[][] parsedMatrix = ParseFileContent(readText);
                ConsoleMethod.NicePrint(Constants.InitialDataMessage);
                ConsoleMethod.PrintArray(parsedMatrix);
                
                ShiftArray(ref parsedMatrix);
                ConsoleMethod.NicePrint(Constants.ProcessedDataMessage);
                ConsoleMethod.PrintArray(parsedMatrix);
            }
            catch (Exception ex)
            {
                ConsoleMethod.NicePrint(Constants.FileContentFormatErrorMessage, CustomColor.ErrorColor);
            }
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