

Console.WriteLine("Hello, Day6!");
var lines = File.ReadLines(@"..\..\..\input.txt")
    .Select(x => x.Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).ToArray();
var lines2 = File.ReadLines(@"..\..\..\input.txt")
    .Select(x => long.Parse(x.Split(":")[1].Replace(" ", ""))).ToArray();
var bigGame = new Game(lines2[0], lines2[1]);
var games = new List<Game>();
games.Add(bigGame);
for (int i = 0; i < lines[0].Length; i++)
{
    // games.Add(new Game(lines[0][i], lines[1][i]));
}

var multiply = 1;

foreach (var game in games)
{
    var win = 0;
    for (int i = 0; i < game.Time; i++)
    {
        var myDistance = (game.Time - i) * i;
        if(myDistance > game.Distance)
            win++;
    }

    if (win != 0)
        multiply *= win;
}
Console.WriteLine("multiply: " + multiply);
record Game(long Time, long Distance);
