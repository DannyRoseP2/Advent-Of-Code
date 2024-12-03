partial class Program
{
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
            var ogSafe = IsReportSafe(row);
            if (ogSafe)
            {
                //Console.WriteLine($"Result: SAFE\n");
                results.Add(ogSafe);
            }
            else
            {
                var firstBadIndex = IndexOfFirstUnsafeLevel(row);
                var safeAfterFirstDamperAttempt = IsDampenedReportSafe(row, firstBadIndex);
                if (safeAfterFirstDamperAttempt)
                {
                    //Console.WriteLine($"Result: SAFE after first damp removing {row[firstBadIndex]}\n");
                    results.Add(safeAfterFirstDamperAttempt);
                }
                else if (firstBadIndex == row.Count - 2)
                {
                    var safeAfterSecondDampterAttempt = IsDampenedReportSafe(row, firstBadIndex + 1);
                    if (safeAfterSecondDampterAttempt)
                    {
                        //Console.WriteLine($"Result: SAFE after second removing {row[firstBadIndex + 1]}\n");
                    }
                    else
                    {
                        Console.WriteLine(string.Join(" ", row) + " <- UNSAFE\n");
                    }
                    results.Add(safeAfterSecondDampterAttempt);
                } else
                {
                    Console.WriteLine(string.Join(" ", row) + " <- UNSAFE\n");
                    results.Add(false);
                }
            }

        }
        var totalReports = results.Count;
        var safeReports = (results.Where(x => x)).Count();
        Console.WriteLine($"Total Reports: {totalReports}. Safe Reports: {safeReports}");

    }

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

    public static bool IsDampenedReportSafe(List<int> report, int indexToDampen)
    {
        //Console.WriteLine($"Attempting to make safe by removing {report[indexToDampen]}");
        var dapenedReport = new List<int>(report);
        dapenedReport.RemoveAt(indexToDampen);
        return IsReportSafe(dapenedReport);
    }

    public static int IndexOfFirstUnsafeLevel(List<int> report)
    {
        var increasing = (report.Last() - report.First()) > 0;
        for (int i = 0; i < report.Count - 1; i++)
        {
            if (!IsStepSafe(report[i], report[i + 1], increasing))
                return i;
        }
        return -1;
    }

    public static bool IsStepSafe(int a, int b, bool increasing)
    {

        var diff = a - b;
        var absDiff = Math.Abs(diff);
        var cond1 = (increasing && diff < 0) || (!increasing && diff > 0);
        var cond2 = 1 <= absDiff && absDiff <= 3;
        var result =  cond1 && cond2;
        //if (!result)
        //{
        //    Console.WriteLine($"{a} -> {b} failed. <->: {cond1} +/-: {cond2}");
        //}
        return result;
    }
}

