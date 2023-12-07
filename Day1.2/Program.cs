
Console.WriteLine("Hello, World!");
var lines = File.ReadLines(@"..\..\..\input.txt");

string[] letterNums = { "one", "1", "two", "2", "three", "3", "four", "4", "five", "5", "six", "6", "seven", "7", "eight", "8","nine", "9" };
Console.WriteLine(lines.Select(Num).Sum());
int Num(string s)
{
    int maxIndex = Int32.MinValue;
    int minIndex = Int32.MaxValue;
    string firstString = "";
    string lastString = "";
    foreach (string subString in letterNums)
    {
        int index = s.IndexOf(subString);
        if (index != -1 && index < minIndex)
        {
            minIndex = index;
            firstString = subString;
        }
        index = s.LastIndexOf(subString);
        if (index != -1 && index > maxIndex)
        {
            maxIndex = index;
            lastString = subString;
        }
    }

    var numbers = "";
    if (int.TryParse(firstString, out var fs))
    {
        numbers += firstString;
    }
    else
    {
        numbers += letterNums[letterNums.ToList().IndexOf(firstString)+1];
    }
    if (int.TryParse(lastString, out var ls))
    {
        numbers += lastString;
    }
    else
    {
        numbers += letterNums[letterNums.ToList().IndexOf(lastString)+1];
    }
    return int.Parse(numbers);
}