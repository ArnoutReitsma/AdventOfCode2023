// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

Console.WriteLine("Hello, Day4!");
var lines = File.ReadLines(@"..\..\..\input.txt");
var input = File.ReadAllText(@"..\..\..\input.txt");

Console.WriteLine(lines.Select(Score).Sum());

int Score(string s)
{
    var score = 0;
    var numbers = s.Replace("  ", " ").Split(":")[1].Trim().Split("|");
    var winningNumbers = numbers[0].Trim().Split();
    var myNumbers = numbers[1].Trim().Split();
    foreach (var winningNumber in winningNumbers)
    {
        if(myNumbers.Contains(winningNumber))
        {
            if (score == 0) score = 1;
            else score *= 2;
        }
    }
    return score;
}

Console.WriteLine(Score2(input));
int Score2(string input)
{
    var cards = input.Split("\n").Select(ParseCard).ToArray();
    var counts = cards.Select(_ => 1).ToArray();

    for (var i = 0; i < cards.Length; i++) {
        var (card, count) = (cards[i], counts[i]);
        for (var j = 0; j < card.matches; j++) {
            counts[i + j + 1] += count;
        }
    }
    return counts.Sum();
}

Card ParseCard(string line) {
    var parts = line.Split(':', '|');
    var l = from m in Regex.Matches(parts[1], @"\d+") select m.Value;
    var r = from m in Regex.Matches(parts[2], @"\d+") select m.Value;
    return new Card(l.Intersect(r).Count());
}
record Card(int matches);