
Console.WriteLine("Hello, Day 5!");
var lines = File.ReadLines(@"..\..\..\input.txt");
var seeds = lines.First().Split(":")[1].Trim().Split().Select(long.Parse);
List<List<Path>> map = new List<List<Path>>();

for (long i = 0; i < 7; i++)
    map.Add(new List<Path>());
var mapIndex = 0;
foreach (var line in lines.Skip(3))
{
    if (line == "")
    {
        continue;
    }

    if (line.Contains(":"))
    {
        mapIndex++;
        continue;
    }

    var ranges = line.Trim().Split();
    var destinationRangeStart = long.Parse(ranges[0]);
    var sourceRangeStart = long.Parse(ranges[1]);
    var rangeLength = long.Parse(ranges[2]);
    map[mapIndex].Add(new Path(destinationRangeStart, sourceRangeStart, rangeLength));
}

var seedLocations = new List<long>();
foreach (var seed in seeds)
{
    long next = seed;
    for (int i = 0; i < map.Count; i++)
    {
        var path = map[i].LastOrDefault(p => p.sourceRangeStart <= next && p.sourceRangeStart + p.rangeLength > next);

        if (path == null)
        {
            continue;
        }

        next = path.destinationRangeStart + next - path.sourceRangeStart;
    }

    seedLocations.Add(next);
}

var seedsArray = seeds.ToArray();
// Start at location, move up the layers and check what seed value is and see if it matches a seed in the ranges
for (long s = 0;; s ++)
{
    long next = s;
    for (int i = map.Count - 1; i >= 0; i--)
    {
        var path = map[i].LastOrDefault(p => p.destinationRangeStart <= next && p.destinationRangeStart + p.rangeLength > next);

        if (path == null)
        {
            continue;
        }

        next = path.sourceRangeStart + next - path.destinationRangeStart;
    }

    if (isValidSeed(next))
    {
        Console.WriteLine("Lowest: " + s);
        return;
    }

}


bool isValidSeed(long val) {
    for (var i = 0; i < seedsArray.Count(); i += 2) {
        if (val >= seedsArray[i] && val < seedsArray[i] + seedsArray[i + 1]) {
            return true;
        }
    }
    return false;
}


record Path(long destinationRangeStart, long sourceRangeStart, long rangeLength);

