partial class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please specify the day to run (e.g., `dotnet run -- 1`).");
            return;
        }

        var day = args[0];
        switch (day)
        {
            case "1":
                RunDay1();
                break;
            case "2":
                RunDay2();
                break;
            // Add cases for more days
            default:
                Console.WriteLine($"Day {day} is not implemented.");
                break;
        }
    }
}