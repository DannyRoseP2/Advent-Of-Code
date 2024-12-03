using System.Text;

partial class Program
{
    
    public static void RunDay3()
    {
        string filePath = "inputs\\day3input.txt";
        string input = File.ReadAllText(filePath);
        List<string> instructons = new List<string>();
        int total = 0;

        const string startToken = "mul(";
        int nextStartTokenIndex;

        while (input.Length >= 8 ) 
        {
            nextStartTokenIndex = input.IndexOf(startToken);
            input = input.Substring(nextStartTokenIndex);
            input = TryParseInstruction(input, instructons, ref total);
        }
        Console.WriteLine($"Instructions Found:{instructons.Count}");
        foreach (var instruction in instructons) 
        { 
            Console.WriteLine("   " + instruction);
        }
        Console.WriteLine("");
        Console.WriteLine("input:" + input+"");
        Console.WriteLine($"total: {total}");
    }

    public static string TryParseInstruction(string input, List<string> instructons, ref int total)
    {
        //Skip the initial "mul("
        input = input.Substring(4);
        var digitBuffer = new StringBuilder();
        var cursorIndex = 0;
        int factor1, factor2;

        for(int i = 0; char.IsDigit(input[i]); i++)
        {
            digitBuffer.Append(input[i]);
            cursorIndex++;
        }

        factor1 = int.Parse(digitBuffer.ToString());
        digitBuffer.Clear();

        if (input[cursorIndex] != ',')
            return input.Substring(cursorIndex);

        cursorIndex++;

        if (!char.IsDigit(input[cursorIndex]))
            return input.Substring(cursorIndex);

        for (int i = cursorIndex; char.IsDigit(input[i]); i++)
        {
            digitBuffer.Append(input[i]);
            cursorIndex++;
        }

        factor2 = int.Parse(digitBuffer.ToString());
        digitBuffer.Clear();

        if (input[cursorIndex] != ')')
            return input.Substring(cursorIndex);

        cursorIndex++;

        instructons.Add($"mul({factor1},{factor2})");
        total += factor1 * factor2;
        return input.Substring(cursorIndex);
    }

}