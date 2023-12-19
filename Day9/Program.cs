// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, Day9!");
var lines = File.ReadLines(@"..\..\..\input.txt").Select(l => l.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).ToList();

Console.WriteLine("OASIS Report sum extrapolated vals: " + lines.Select(GetNext)
    .Sum());

int GetNext(int[] history)
{
    int[][] layers = new int[100][];
    layers[0] = history;
    var layerIndex = 1;
    var isZero = false;
    while (!isZero)
    {
        layers[layerIndex] = new int[history.Length - layerIndex];
        for (int i = 1; i <= history.Length - layerIndex; i++)
        {
            layers[layerIndex][i - 1] = layers[layerIndex - 1][i] - layers[layerIndex - 1][i - 1];
        }

        if (layers[layerIndex].All(i => i == 0))
        {
            isZero = true;
        }

        layerIndex++;
    }

    var next = 0;
    var index = 0;
    while (layers[index] != null)
    {
        next += layers[index].Last();
        index++;
    }

    return next;
}


Console.WriteLine("part2: OASIS Report sum extrapolated vals: " + lines.Select(GetNext2)
    .Sum());

int GetNext2(int[] history)
{
    var newHistory = history.ToList();
    var diffs = new List<List<int>> { newHistory };

    while (newHistory.Any(n => n != 0))
    {
        newHistory = new();
        for (var i = 0; i < diffs.Last().Count - 1; i++)
        {
            var diff = diffs.Last()[i + 1] - diffs.Last()[i];
            newHistory.Add(diff);
        }

        diffs.Add(newHistory);
    }

    for (var i = diffs.Count - 1; i > 0; i--)
    {
        diffs[i - 1].Insert(0, diffs[i - 1].First() - diffs[i].First());
    }

    return diffs[0].First();
}