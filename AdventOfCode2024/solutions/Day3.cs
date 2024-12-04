using System.Text;

partial class Program
{
    //99675963
    public static void RunDay3()
    {
        string filePath = "inputs\\day3input.txt";
        string input = File.ReadAllText(filePath);
        List<string> instructons = new List<string>();
        int total = 0;

        const string instructionToken = "mul(";
        const string enableToken = "do()";
        const string disableToken = "don't()";
        int nextinstructionTokenIndex;
        int nextEnableTokenIndex;
        int nextDisableTokenIndex;
        var enabled = true;
        var loops = 1;

        while (input.Length >= 8) 
        {
            nextinstructionTokenIndex = input.IndexOf(instructionToken);
            nextEnableTokenIndex = input.IndexOf(enableToken);
            nextDisableTokenIndex = input.IndexOf(disableToken);
            if (enabled)
            {

                if ((nextDisableTokenIndex > 0) && (nextDisableTokenIndex < nextinstructionTokenIndex))
                {
                    input = input.Substring(nextDisableTokenIndex + disableToken.Length);
                    enabled = false;
                }
                else
                {
                    input = input.Substring(nextinstructionTokenIndex);
                    input = TryParseInstruction(input, instructons, ref total);
                }
            }
            else
            {
                input = input.Substring(nextEnableTokenIndex + enableToken.Length);
                enabled = true;
            }
            loops++;
        }


        Console.WriteLine($"Instructions Found:{instructons.Count}");
        Console.WriteLine("input.Length:" + input.Length);
        Console.WriteLine($"total: {total}");
        Console.WriteLine($"loops: {loops}");
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

        var instruction = $"mul({factor1},{factor2})";
        instructons.Add(instruction);
        total += factor1 * factor2;
        return input.Substring(cursorIndex);
    }

}