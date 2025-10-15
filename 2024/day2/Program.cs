/* 
Count number of safe reports (line of numbers) by these rules: 
- Must all increase or decrease.
- Must not increase/decrease by more than 3, and at least 1.
*/
int GetSafeReportsCount(string filepath)
{
    StreamReader sr = new StreamReader(filepath);
    int safeReports = 0;

    while (!sr.EndOfStream)
    {
        var report = sr.ReadLine()?.Split(" ").Select(int.Parse).ToList();
        if (report is null) break;

        if (IsSafe(report))
        {
            safeReports++;
            continue;
        }
    }

    return safeReports;
}

//Same problem, but allow one bad entry.
int GetSafeReportsCountWithErrorCorrection(string filepath)
{
    StreamReader sr = new StreamReader(filepath);
    int safeReports = 0;

    while (!sr.EndOfStream)
    {
        var report = sr.ReadLine()?.Split(" ").Select(int.Parse).ToList();
        if (report is null) break;

        if (IsSafe(report))
        {
            safeReports++;
            continue;
        }

        for (int i = 0; i < report.Count; i++)
        {
            var tempReport = new List<int>(report);
            tempReport.RemoveAt(i);

            if (IsSafe(tempReport))
            {
                safeReports++;
                break;
            }
        }
    }

    return safeReports;
}

bool IsSafe(List<int> report)
{
    if (report.Count < 2) return true;

    bool shouldIncrease = report[1] > report[0];
    for (int i = 1; i < report.Count; i++)
    {
        int prev = report[i - 1];
        int current = report[i];
        int diff = current - prev;

        if (shouldIncrease && diff <= 0) return false;
        if (!shouldIncrease && diff >= 0) return false;

        int absDiff = Math.Abs(diff);
        if (absDiff < 1 || absDiff > 3) return false;
    }

    return true;
}

string filepath = "input.txt";
int safeReports = GetSafeReportsCount(filepath);
int safeReportsWithErrorCorrection = GetSafeReportsCountWithErrorCorrection(filepath);

Console.WriteLine($"Safe reports: {safeReports}");
Console.WriteLine($"Safe reports with error correction: {safeReportsWithErrorCorrection}");