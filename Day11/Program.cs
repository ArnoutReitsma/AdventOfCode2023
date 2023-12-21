internal class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, Day11!");

        var universe = File.ReadAllLines(@"..\..\..\input.txt").Select(l => l.ToCharArray().ToList()).ToList();
        ExpandHorizontalPivot(universe);
        universe = Pivot(universe);
        ExpandHorizontalPivot(universe);
        universe = Pivot(universe);
        universe = Pivot(universe);
        universe = Pivot(universe);
        foreach (var chars in universe)
        {
            Console.WriteLine(new string(chars.ToArray()));
        }

        Console.WriteLine($"Expanded univers; {universe.Count}:{universe[0].Count} ");
        var galaxies = GetGalaxies(universe);
        Console.WriteLine($"Galaxies found; {galaxies.Count} ");
        var sum = SumDistances(galaxies);
        Console.WriteLine($"Galaxies distance sum; {sum} ");
        var sum2 = SumDistances2(galaxies,universe);
        Console.WriteLine($"Galaxies distance sum2; {sum2} ");
        Console.WriteLine($"asd; {nCr(428, 2)} ");
    }

    private static int SumDistances(List<Galaxy> galaxies)
    {
        var sum = 0;
        var index = 0;
        var galaxypairs = galaxies.SelectMany(x => galaxies, (x, y) => (x, y))
            .Where(tuple => tuple.x != tuple.y);
        foreach (var pair in galaxypairs)
        {
            sum += Math.Abs(pair.x.Col - pair.y.Col) + Math.Abs(pair.x.Row - pair.y.Row);
            index++;
        }

        Console.WriteLine($"summed times; {index} ");

        return sum / 2;
    }

    private static int SumDistances2(List<Galaxy> galaxies, List<List<char>> universe)
    {
        var sum = 0;
        var index = 0;
        var galaxypairs = galaxies.SelectMany(x => galaxies, (a, b) => (a: a, b: b))
            .Where(tuple => tuple.a != tuple.b);
        foreach (var pair in galaxypairs)
        {
            var colA = new string(universe[pair.a.Row].ToArray()).Substring(0, pair.a.Col).Count(s => s == 'X') * 1000000 + pair.a.Col;
            var colB = new string(universe[pair.b.Row].ToArray()).Substring(0, pair.b.Col).Count(s => s == 'X') * 1000000 + pair.b.Col;
            var rowA = universe.Select(l => l[pair.a.Row]).ToString().Count(s => s == 'X') * 1000000 + pair.a.Col;
            var rowB = universe.Select(l => l[pair.b.Row]).ToString().Count(s => s == 'X') * 1000000 + pair.b.Col;
            sum += Math.Abs(colA - colB) + Math.Abs(rowA - rowB);
            index++;
        }

        Console.WriteLine($"summed times; {index} ");

        return sum / 2;
    }


    private static List<Galaxy> GetGalaxies(List<List<char>> universe)
    {
        var galaxies = new List<Galaxy>();
        var array = universe.Select(m => m.ToArray()).ToArray();
        for (int row = 0; row < universe.Count; row++)
        {
            for (int col = 0; col < universe[row].Count; col++)
            {
                if (array[row][col] == '#')
                {
                    galaxies.Add(new Galaxy(row, col));
                }
            }
        }

        return galaxies;
    }

    record Galaxy(int Row, int Col);

    private static void ExpandHorizontalPivot(List<List<char>> output)
    {
        var index = 0;
        while (index < output.Count)
        {
            if (output[index].TrueForAll(c => c == '.'))
            {
                output[index] = Enumerable.Repeat('X', output[index].Count).ToList();
            }

            index++;
        }
    }

    private static List<List<char>> Pivot(List<List<char>> myList)
    {
        var data = myList
            .SelectMany((innerList, outerIndex) =>
                innerList.Select((c, innerIndex) =>
                    new { Row = outerIndex, Col = innerIndex, Char = c }));

        return data
            .GroupBy(x => x.Col)
            .Select(g => new { Col = g.Key, Chars = g.OrderBy(x => x.Row).Select(x => x.Char).ToArray() }).Select(x => x.Chars.ToList()).ToList();
    }

    private static long nCr(int n, int r)
    {
        return nPr(n, r) / Factorial(r);
    }

    private static long nPr(int n, int r)
    {
        // naive: return Factorial(n) / Factorial(n - r);
        return FactorialDivision(n, n - r);
    }

    private static long FactorialDivision(int topFactorial, int divisorFactorial)
    {
        long result = 1;
        for (int i = topFactorial; i > divisorFactorial; i--)
            result *= i;
        return result;
    }

    private static long Factorial(int i)
    {
        if (i <= 1)
            return 1;
        return i * Factorial(i - 1);
    }
}