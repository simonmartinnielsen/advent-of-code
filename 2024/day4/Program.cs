StreamReader sr = new StreamReader("input.txt");
var lines = new List<string>();

while (!sr.EndOfStream)
{
    string? line = sr.ReadLine();
    if (line is null) continue;
    lines.Add(line);
}

int matches1 = GetMatchesTask1(lines);
int matches2 = GetMatchesTask2(lines);

Console.WriteLine($"Task 1: {matches1}\nTask 2: {matches2}");

// Find XMAS matches in all directions
int GetMatchesTask1(List<string> lines)
{
    var directions = new (int x, int y)[]
    {
        ( 0,  1),
        ( 0, -1),
        ( 1,  0),
        (-1,  0),
        ( 1,  1),
        ( 1, -1),
        (-1,  1),
        (-1, -1)
    };

    int matches = 0;

    for (int i = 0; i < lines.Count; i++)
    {
        for (int j = 0; j < lines[i].Length; j++)
        {
            if (lines[i][j] != 'X') continue;

            foreach (var (x, y) in directions)
            {
                int endX = i + x * 3;
                int endY = j + y * 3;

                if (endX < 0 || endX >= lines.Count || endY < 0 || endY >= lines[i].Length) continue;

                char first = lines[i + x][j + y];
                char second = lines[i + x * 2][j + y * 2];
                char third = lines[i + x * 3][j + y * 3];
                bool foundMatch = first == 'M' && second == 'A' && third == 'S';
                if (foundMatch) matches++;
            }
        }
    }

    return matches;
}

// Find X-MAS (In a vector where X in center with M,S or S,M diagonally)
int GetMatchesTask2(List<string> lines)
{
    var directions = new (int x, int y)[]
    {
        (-1, -1),
        (-1, 1)
    };

    int matches = 0;
    for (int i = 1; i < lines.Count - 1; i++)
    {
        for (int j = 1; j < lines[i].Length - 1; j++)
        {
            if (lines[i][j] != 'A') continue;

            bool foundMatch = true;

            foreach (var (x, y) in directions)
            {
                char a = lines[i + x][j + y];
                char b = lines[i - x][j - y];
                bool valid = (a == 'M' && b == 'S') || (a == 'S' && b == 'M');

                if (!valid)
                {
                    foundMatch = false;
                    break;
                }
            }

            if (foundMatch) matches++;
        }
    }

    return matches;
}