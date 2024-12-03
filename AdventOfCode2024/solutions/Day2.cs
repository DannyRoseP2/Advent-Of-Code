partial class Program
{
    public static bool IsReportSafe(List<int> report)
    {
        var increasing = (report.Last() - report.First()) > 0;
        for (int i = 0; i < report.Count - 1; i++)
        {
            if (!IsStepSafe(report[i], report[i + 1], increasing))
                return false;
        }
        return true;
    }

    public static bool IsStepSafe(int a, int b, bool increasing)
    {

        var diff = a - b;
        var absDiff = Math.Abs(diff);
        var cond1 = (increasing && diff < 0) || (!increasing && diff > 0);
        var cond2 = 1 <= absDiff && absDiff <= 3;
        return cond1 && cond2;
    }

    public static void RunDay2()
    {
        string filePath = "inputs\\day2input.txt";

        List<List<int>> reports = new List<List<int>>();
        List<bool> results = new List<bool>();

        foreach (var line in File.ReadLines(filePath))
        {
            var report = new List<int>();
            foreach (var part in line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                if (int.TryParse(part, out int level))
                {
                    report.Add(level);
                }
            }
            reports.Add(report);
        }

        foreach (var row in reports)
        {
            results.Add(IsReportSafe(row));
        }
        var totalReports = results.Count;
        var safeReports = (results.Where(x => x)).Count();
        Console.WriteLine($"Number of safe report: {safeReports}");
        
    }
}

