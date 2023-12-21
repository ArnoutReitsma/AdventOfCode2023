
var map = File.ReadAllLines(@"..\..\..\input.txt").ToList();

Coord? startCoord = null;
for (var i = 0; i < map.Count; i++)
{
    var indexOfS = map[i].IndexOf('S');
    if (indexOfS != -1)
    {
        startCoord = new Coord(i, indexOfS);
        break;
    }
}

Console.WriteLine(startCoord);

Coord? prev = null;
var current = startCoord;
var steps = 0;
while (true)
{
    var adjacent = FindAdjacentCoords(current, map).FirstOrDefault(c => c != prev);
    steps++;
    if (adjacent == null)
    {
        break;
    }

    prev = current;
    current = adjacent;
    if (current == startCoord)
    {
        break;
    }
}

var furthestDistance = (int)Math.Ceiling((double)steps / 2);
Console.WriteLine($"Steps: {steps}, distance: {furthestDistance}");

List<Coord> FindAdjacentCoords(Coord coord, List<string> map)
{
    var adj = new List<Coord>(2);
    if (coord.Row > 0 && "S|JL".Contains(map[coord.Row][coord.Col]))
    {
        if ("|7F".Contains(map[coord.Row - 1][coord.Col]))
        {
            adj.Add(new Coord(coord.Row - 1, coord.Col));
        }
    }

    if (coord.Row < map.Count - 1 && "S|7F".Contains(map[coord.Row][coord.Col]))
    {
        if ("|JL".Contains(map[coord.Row + 1][coord.Col]))
        {
            adj.Add(new Coord(coord.Row + 1, coord.Col));
        }
    }

    if (coord.Col > 0 && "S-7J".Contains(map[coord.Row][coord.Col]))
    {
        if ("-FL".Contains(map[coord.Row][coord.Col - 1]))
        {
            adj.Add(new Coord(coord.Row, coord.Col - 1));
        }
    }

    if (coord.Col < map[0].Length - 1 && "S-LF".Contains(map[coord.Row][coord.Col]))
    {
        if ("-J7".Contains(map[coord.Row][coord.Col + 1]))
        {
            adj.Add(new Coord(coord.Row, coord.Col + 1));
        }
    }

    return adj;
}

record Coord(int Row, int Col);