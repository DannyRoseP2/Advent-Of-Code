using System.Diagnostics;
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

        var wordCount = GetWordCountForNode(matrix, 0, 5);

        Console.WriteLine($"WordCount:{wordCount}");
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

    static int GetWordCountForNode(char[,] matrix, int row, int col)
    {
        int count = 0;
        if (SearchEast(matrix, row, col))
            count++;

        return count;
    }

    static bool SearchEast(char[,] matrix, int row, int col)
    {
        for (int i = col; i < XMAS.Length; i++)
        {
            if (!(matrix[row, i] == XMAS[i]))
                return false;
        }
        return true;           
    }

    static void PrintMatrix(char[,] matrix, int rows, int cols)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}