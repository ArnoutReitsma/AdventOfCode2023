
Console.WriteLine("Hello, Day2!");
var lines = File.ReadLines(@"..\..\..\input.txt").Select(l => l.Replace(",", ""));
int sum = 0, redGame = 12, blueGame = 14, greenGame = 13;
foreach (var line in lines)
{
    ProcessLine(line);
}
Console.WriteLine(sum);

void ProcessLine(string gameLine)
{
    var gameNumb = int.Parse(gameLine.Split()[1].Replace(":",""));
    var gameText = gameLine.Split(":")[1];
    var red = 0;
    var green = 0;
    var blue = 0;
    foreach(var games in gameText.Split(";"))
    {
        var game = games.Split().Skip(1).ToArray();

        for (int i = 0; i < game.Count(); i += 2)
        {
            var numb = int.Parse(game[i]);
            if (game[i + 1] == "red" && numb > red)
            {
                red = numb;
            }

            if (game[i + 1] == "blue" && numb > blue)
            {
                blue = numb;
            }

            if (game[i + 1] == "green"&& numb > green)
            {
                green = numb;
            }
        }
    }

    sum += red * blue * green;
}