using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text;

partial class Program
{
    const string XMAS = "XMAS";
    public static void RunDay4()
    {

        string[] input = File.ReadAllLines("inputs\\day4test.txt");


        int rows = input.Length;
        int cols = input[0].Length;
        char[,] matrix = GetMatrix(input, rows, cols);
        int row = 3;
        int col = 0;



        //var total = GetWordCountForNode(matrix, row, col);
        var total = SearchMatrix(matrix);
        Console.WriteLine($"total:{total}");
    }

    static int  SearchMatrix(char[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        var total = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                total += GetWordCountForNode(matrix, i, j);
            }
        }
        return total;
    }
    

    static int GetWordCountForNode(char[,] matrix, int row, int col)
    {
        int count = 0;

        //if (SearchEast(matrix, row, col))
        //    count++;

        //if (SearchWest(matrix, row, col))
        //    count++;

        if (SearchNorth(matrix, row, col))
            count++;

        if (SearchSouth(matrix, row, col))
            count++;

        return count;
    }

    //Searches
    static bool SearchEast(char[,] matrix, int row, int col)
    {
        bool result;
        List<char> east = new List<char>(); 
        var charsRemaining = matrix.GetLength(0) - col;
        
        if (charsRemaining < XMAS.Length)
        {
            result = false;
        }
        else
        {
            for (int i = col; i < col + XMAS.Length; i++)
            {
                east.Add(matrix[row, i]);
            }
            var eastStr = new string(east.ToArray());
            result = eastStr.Equals(XMAS);
        }
        return result;          
    }

    static bool SearchWest(char[,] matrix, int row, int col)
    {
        bool result;
        List<char> west = new List<char>();

        if (col < XMAS.Length - 1)
        {
            result = false;
        }
        else
        {
            for (int i = col; i >= (col  - (XMAS.Length - 1)); i--)
            {
                west.Add(matrix[row, i]);
            }
            var weastStr = new string(west.ToArray());
            result = weastStr.Equals(XMAS);
        }
        return result;
    }

    static bool SearchNorth(char[,] matrix, int row, int col)
    {
        bool result;
        List<char> north = new List<char>();

        if (row < XMAS.Length - 1)
        {
            result = false;
        }
        else
        {
            for (int i = row; i >= (row - (XMAS.Length - 1)); i--)
            {
                north.Add(matrix[i, col]);
            }
            var northStr = new string(north.ToArray());
            result = northStr.Equals(XMAS);
        }

        if (result)
        {
            Console.WriteLine($"Found North at: ({row},{col})");
            PrintMatrixWithNodeHighlight(matrix, row, col);
            Console.WriteLine();
        }

        return result;
    }

    static bool SearchSouth(char[,] matrix, int row, int col)
    {
        bool result;
        List<char> south = new List<char>();
        var totalRows = matrix.GetLength(1);
        if ((totalRows - row) < (XMAS.Length - 1))
        {
            result = false;
        }
        else
        {
            for (int i = row; i <= row + XMAS.Length; i++)
            {
                south.Add(matrix[i, col]);
            }
            var southStr = new string(south.ToArray());
            Console.WriteLine($"Southstr:{southStr}");
            result = southStr.Equals(XMAS);
        }

        if (result)
        {
            Console.WriteLine($"Found North at: ({row},{col})");
            PrintMatrixWithNodeHighlight(matrix, row, col);
            Console.WriteLine();
        }

        return result;
    }


    //Utility

    static void PrintMatrix(char[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static void PrintMatrixWithNodeHighlight(char[,] matrix, int hlRow, int hlCol)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        const int cellWidth = 3; 
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (i == hlRow && j == hlCol)
                {
                    Console.Write($"({matrix[i, j]})".PadRight(cellWidth + 1));
                }
                else if (i == hlRow && j == (hlCol - 1))
                {
                    Console.Write($"{matrix[i, j]}".PadRight(cellWidth - 1));
                }
                else
                {
                    Console.Write($"{matrix[i, j]}".PadRight(cellWidth));
                }
            }
            Console.WriteLine(); 
        }
    }

    static char[,] GetMatrix(string[] input, int rows, int cols)
    {
        char[,] matrix = new char[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = input[i][j];
            }
        }
        return matrix;
    }
}