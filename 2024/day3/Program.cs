using System.Text.RegularExpressions;

// Find matches of mul(x,y) and calculate the sum of all multiplications
int GetResult()
{
    StreamReader sr = new StreamReader("input.txt");
    string fileContent = sr.ReadToEnd();

    var regex = new Regex(@"(mul\((\d{1,3}),(\d{1,3})\))");
    var matches = regex.Matches(fileContent);
    int result = 0;

    foreach (Match match in matches)
    {
        int x = int.Parse(match.Groups[2].Value);
        int y = int.Parse(match.Groups[3].Value);
        result += x * y;
    }

    sr.Close();
    return result;

}

// do() and don't() conditions before mul(x,y) determine if following mul's should be counted 
int GetResultWithConditions()
{
    StreamReader sr = new StreamReader("input.txt");
    string fileContent = sr.ReadToEnd();

    var mulRegex = new Regex(@"(mul\((\d{1,3}),(\d{1,3})\))");
    var doRegex = new Regex(@"(do\(\))");
    var dontRegex = new Regex(@"(don't\(\))");

    var mulMatches = mulRegex.Matches(fileContent);
    var doMatches = doRegex.Matches(fileContent);
    var dontMatches = dontRegex.Matches(fileContent);
    var doIndices = doMatches.Select(match => match.Index).ToList();
    var dontIndices = dontMatches.Select(match => match.Index).ToList();
    var mulIndices = mulMatches.Select(match => match.Index).ToList();

    int result = 0;

    foreach (Match mulMatch in mulMatches)
    {
        int mulIndex = mulMatch.Index;
        var lastDoLineIndex = doIndices.FindLast(doIndex => doIndex < mulIndex);
        var lastDontLineIndex = dontIndices.FindLast(dontIndex => dontIndex < mulIndex);

        if (lastDoLineIndex > lastDontLineIndex || lastDoLineIndex == 0 && lastDontLineIndex == 0)
        {
            int x = int.Parse(mulMatch.Groups[2].Value.Trim());
            int y = int.Parse(mulMatch.Groups[3].Value.Trim());
            result += x * y;
        }
    }

    sr.Close();
    return result;
}

int result = GetResult();
Console.WriteLine(result);

int resultWithConditions = GetResultWithConditions();
Console.WriteLine(resultWithConditions);
