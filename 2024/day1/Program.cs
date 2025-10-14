
(List<int>, List<int>) GetSortedListsFromInput()
{
    using StreamReader sr = new("./input.txt");
    var listA = new List<int>();
    var listB = new List<int>();

    while (!sr.EndOfStream)
    {
        var line = sr.ReadLine();
        if (string.IsNullOrWhiteSpace(line)) continue;

        var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2) continue;

        if (int.TryParse(parts[0], out int x))
        {
            listA.Add(x);
        }
        if (int.TryParse(parts[1], out int y))
        {
            listB.Add(y);
        }
    }

    listA.Sort();
    listB.Sort();
    
    return (listA, listB);

   
}
int SumDifferenceInLists(List<int> listA, List<int> listB)
{
    int sum = 0;
    for (int i = 0; i < listA.Count; i++)
    {
        sum += Math.Abs(listA[i] - listB[i]);
    }

    return sum;
}

int GetSimilarityScoreFromLists(List<int> listA, List<int> listB)
{
    int similaryScore = 0;
    var occurancesDict = new Dictionary<int, int>();

    foreach (int val in listB)
    {
        if (occurancesDict.TryGetValue(val, out int occurances))
        {
            occurancesDict[val] = occurances + 1;
        }
        else
        {
            occurancesDict[val] = 1;
        }
    }

    foreach (int id in listA)
    {
        if (occurancesDict.TryGetValue(id, out int occurances))
        {
            similaryScore += id * occurances;
        }
    }

    return similaryScore;
}

var (listA, listB) = GetSortedListsFromInput();
Console.WriteLine($"Sum of id difference: {SumDifferenceInLists(listA, listB)}");
Console.WriteLine($"Similarity score: {GetSimilarityScoreFromLists(listA, listB)}");