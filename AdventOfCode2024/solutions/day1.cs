string filePath = "inputs\\day1input.txt";

List<int> list1 = new List<int>();
List<int> list2 = new List<int>();
var total = 0;

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

while (list1.Count > 0)
{
    var min1 = list1.Min();
    var min2 = list2.Min();
    list1.Remove(min1);
    list2.Remove(min2);
    total += min1 > min2 ? (min1 - min2) : (min2 - min1);
}

if (list1.Count == 0 && list2.Count == 0)
    Console.WriteLine("Both lists empty");

Console.WriteLine($"Total:{total}");
