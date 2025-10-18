StreamReader sr = new StreamReader("input.txt");
var rules = new Dictionary<int, List<int>>();
var updates = new List<List<int>>();
while (!sr.EndOfStream)
{
    string line = sr.ReadLine() ?? string.Empty;
    if (string.IsNullOrEmpty(line)) continue;
    if (line.Contains("|"))
    {
        var vals = line.Split("|").Select(r => int.Parse(r)).ToList();
        if (rules.ContainsKey(vals[0]))
        {
            rules[vals[0]].Add(vals[1]);
        }
        else
        {
            rules.Add(vals[0], new List<int> { vals[1] });
        }
    }
    if (line.Contains(","))
    {
        updates.Add(line.Split(",").Select(u => int.Parse(u)).ToList());
    }
}

// Add middle value of update lists that adhere to the rules (where 43|10 = 43 should come before 10 in any list)
int GetResultTask1()
{
    int result = 0;
    bool valid = true;

    foreach (var update in updates)
    {
        foreach (var number in update)
        {
            if (rules.ContainsKey(number))
            {
                foreach (int after in rules[number])
                {
                    if (update.IndexOf(after) != -1 && update.IndexOf(after) < update.IndexOf(number))
                    {
                        valid = false;
                        break;
                    }
                }
            }
        }
        if (valid)
        {
            result += update.ElementAt(update.Count / 2);
        }
        valid = true;
    }

    return result;
}

// Fix incorrect lists following the rules and add the lists middle value together 
int GetResultTask2()
{
    int result = 0;

    foreach (var original in updates)
    {
        var update = new List<int>(original);
        if (IsValidUpdate(update)) continue;

        int middleValue = ErrorCorrectUpdate(update);
        result += middleValue;
    }

    return result;
}

int ErrorCorrectUpdate(List<int> update)
{
    for (int i = 0; i < update.Count; i++)
    {
        int value = update[i];
        if (!rules.ContainsKey(value)) continue;

        int insertAt = int.MaxValue;
        foreach (int after in rules[value])
        {
            int indexAfter = update.IndexOf(after);
            if (indexAfter != -1 && indexAfter < i)
            {
                if (indexAfter < insertAt) insertAt = indexAfter;
            }
        }

        if (insertAt != int.MaxValue)
        {
            update.RemoveAt(i);
            update.Insert(i < insertAt ? insertAt - 1 : insertAt, value);
            return ErrorCorrectUpdate(update);
        }
    }

    return update[update.Count / 2];
}

bool IsValidUpdate(List<int> update)
{
    for (int i = 0; i < update.Count; i++)
    {
        int value = update[i];
        if (!rules.ContainsKey(value)) continue;

        foreach (int after in rules[value])
        {
            int indexAfter = update.IndexOf(after);
            if (indexAfter != -1 && indexAfter < i)
            {
                return false;
            }
        }
    }
    return true;
}

int resultTask1 = GetResultTask1();
int resultTask2 = GetResultTask2();

Console.WriteLine($"Result Task 1: {resultTask1}");
Console.WriteLine($"Result Task 2: {resultTask2}");