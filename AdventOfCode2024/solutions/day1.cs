class Program
{
    static void Main()
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

        var totalDistance = GetTotalDistance(list1, list2);
        var similarityScore = GetSimilarityScore(list1, list2);
        Console.WriteLine($"Part One Total :{totalDistance}");
        Console.WriteLine($"Similarity Score :{similarityScore}");
    }

    static int GetTotalDistance(List<int> list1, List<int> list2)
    {
        var l1 = new List<int>(list1);
        var l2 = new List<int>(list2);
        var total = 0;
        while (l1.Count > 0)
        {
            var min1 = l1.Min();
            var min2 = l2.Min();
            l1.Remove(min1);
            l2.Remove(min2);
            total += min1 > min2 ? (min1 - min2) : (min2 - min1);
        }
        return total;
    }

    static int GetSimilarityScore(List<int> list1, List<int> list2)
    {
        
        var similarityScore = 0;
        for(int i = 0; i < list1.Count; i++)
        {
            var num = list1[i];
            var numCount = list2.FindAll(t => t == num).Count();
            similarityScore += num * numCount;
        }
        return similarityScore;
    }
}