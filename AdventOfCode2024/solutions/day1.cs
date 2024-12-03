partial class Program
{
    static int GetTotalDistanceRecursive(int total, List<int> l1, List<int> l2)
    {
        if (l1.Count == 0)
            return total;

        var min1 = l1.Min();
        var min2 = l2.Min();
        l1.Remove(min1);
        l2.Remove(min2);
        total += min1 > min2 ? (min1 - min2) : (min2 - min1);
        return GetTotalDistanceRecursive(total, l1, l2);
    }

    static int GetSimilarityScore(List<int> list1, List<int> list2)
    {
        var similarityScore = 0;
        foreach (var num in list1)
        {
            var numCount = list2.FindAll(x => x == num).Count();
            similarityScore += num * numCount;
        }
        return similarityScore;
    }

    public static void RunDay1()
    {
        string filePath = "inputs\\day1input.txt";
        List<int> list1 = new List<int>();
        List<int> list2 = new List<int>();

        foreach (var line in File.ReadLines(filePath))
        {
            string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2 &&
                int.TryParse(parts[0], out int num1) &&
                int.TryParse(parts[1], out int num2))
            {
                list1.Add(num1);
                list2.Add(num2);
            }
        }

        var similarityScore = GetSimilarityScore(list1, list2);
        var totalDistance = GetTotalDistanceRecursive(0, list1, list2);

        Console.WriteLine($"Total List Distance : {totalDistance}");
        Console.WriteLine($"Similarity Score : {similarityScore}");
    }
}
