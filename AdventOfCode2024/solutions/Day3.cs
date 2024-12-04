using System.Text;

partial class Program
{
    const string enableToken = "do()";
    const string disableToken = "don't()";
    const string instructionToken = "mul(";

    public static void RunDay3()
    {
        string logFileName = Path.Combine("logs", "day3log.txt");
        string input = File.ReadAllText("inputs\\day3input.txt");
        
        Dictionary<int, string> instructions = new Dictionary<int, string>();
        List<int> enableTokenIndexes = new List<int>();
        List<int> disableTokenIndexes = new List<int>();
        List<int> possibleInstructionTokenIndexes = new List<int>();

        
        enableTokenIndexes = FindTokenIndexes(input, enableToken);
        foreach (var index in enableTokenIndexes)
        {
            instructions.Add(index, enableToken);
        }

        disableTokenIndexes = FindTokenIndexes(input, disableToken);
        foreach (var index in disableTokenIndexes)
        {
            instructions.Add(index, disableToken);
        }

        possibleInstructionTokenIndexes = FindTokenIndexes(input, instructionToken);
        foreach (int i in possibleInstructionTokenIndexes) 
        {
            if (TryParseInstruction(input.Substring(i), out string instruction))
            {
                instructions.Add(i, instruction);
            }
        }

        var enabled = true;
        var total = 0;
        foreach (var instruction in instructions.OrderBy(x => x.Key))
        {
            if (instruction.Value == enableToken)
            {
                enabled = true;
            }
            else if (instruction.Value == disableToken)
            {
                enabled = false;
            }
            else if (instruction.Value.StartsWith(instructionToken) && enabled)
            {
                total += GetInstructionProduct(instruction.Value);
            }
        }

        Console.WriteLine($"Total: {total}");
    }

    static List<int> FindTokenIndexes(string input, string token)
    {
        List<int> indexes = new List<int>();
        int index = 0;

        while ((index = input.IndexOf(token, index, StringComparison.Ordinal)) != -1)
        {
            indexes.Add(index);
            index += token.Length;
        }

        return indexes;
    }

    static int GetInstructionProduct(string instruction)
    {
        var commaIndex = instruction.IndexOf(',');
        var strFactor1 = instruction.Substring(4, commaIndex - 4);
        var strFactor2 = instruction.Substring(commaIndex + 1, instruction.Length - (commaIndex + 2));
        var factor1 = int.Parse(strFactor1);
        var factor2 = int.Parse(strFactor2);
        return factor1 * factor2;
    }

    static bool TryParseInstruction(string input, out string instruction)
    {
        //Skip the initial "mul("
        input = input.Substring(4);
        instruction = "";
        var digitBuffer = new StringBuilder();
        var cursorIndex = 0;
        int factor1, factor2;

        for (int i = 0; char.IsDigit(input[i]); i++)
        {
            digitBuffer.Append(input[i]);
            cursorIndex++;
        }

        factor1 = int.Parse(digitBuffer.ToString());
        digitBuffer.Clear();

        if (input[cursorIndex] != ',')
            return false;

        cursorIndex++;

        if (!char.IsDigit(input[cursorIndex]))
            return false;

        for (int i = cursorIndex; char.IsDigit(input[i]); i++)
        {
            digitBuffer.Append(input[i]);
            cursorIndex++;
        }

        factor2 = int.Parse(digitBuffer.ToString());
        digitBuffer.Clear();

        if (input[cursorIndex] != ')')
            return false;

        instruction = $"mul({factor1},{factor2})";
        return true;
    }
}