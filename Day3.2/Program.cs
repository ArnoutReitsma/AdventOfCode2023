
Console.WriteLine("Hello, Day3!");
var lines = File.ReadLines(@"..\..\..\input.txt").Select(s => s.ToCharArray()).ToArray();
int sum = 0;
int[,] coordinates = { { 0, 1 }, { 1, 0 }, { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 }, { 0, -1 }, { -1, 0 } };
List<GearConnection> gearConnections = new List<GearConnection>();
for (int y = 0; y < lines.Length; y++)
{
    var cx = 0;
    var cy = 0;
    for (int x = 0; x < lines[y].Length; x++)
    {
        var hasGear = false;
        if (Char.IsNumber(lines[y][x]))
        {
            for (int xy = 0; xy < coordinates.GetLength(0); xy++)
            {
                cx = x + coordinates[xy, 0];
                cy = y + coordinates[xy, 1];
                if (cx >= 0 && cx < lines[y].Length && cy >= 0 && cy < lines.Length)
                {
                    if (IsGear(lines[cy][cx]))
                    {
                        hasGear = true;
                        break;
                    }
                }
            }
        }

        if (hasGear)
        {
            var sNumber = "";
            var lx = x - 1;
            while (lx >= 0 && Char.IsNumber(lines[y][lx]))
            {
                sNumber = lines[y][lx] + sNumber;
                lx--;
            }

            while (x < lines[y].Length && Char.IsNumber(lines[y][x]))
            {
                sNumber += lines[y][x];
                x++;
            }

            var number = int.Parse(sNumber);
            if (gearConnections.Any(gc => gc.x == cx && gc.y == cy))
            {
                gearConnections.Find(gc => gc.x == cx && gc.y == cy).numbers.Add(number);
            }
            else
            {
                gearConnections.Add(new GearConnection(cx, cy, new List<int>(){number}));
            }
        }
    }
}

Console.WriteLine(gearConnections.Where(gc => gc.numbers.Count >= 2).Select(gc => gc.numbers.Aggregate((a, x) => a * x)).Sum());

bool IsGear(char c)
{
    return c == '*';
}

record GearConnection(int x, int y, List<int> numbers);
