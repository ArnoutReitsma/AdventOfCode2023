Console.WriteLine("Hello, Day8!");

var lines = File.ReadLines(@"..\..\..\input.txt");

var instructions = lines.First();

var map = new Dictionary<string, (string, string)>();

foreach (var line in lines.Skip(2))
{
    map.Add(line.Substring(0, 3), (line.Substring(7, 3), line.Substring(12, 3)));
}


var steps = 1;
var found = false;
// var currentEllement = map["AAA"];
// while (!found)
// {
//     foreach (var instruction in instructions)
//     {
//         var nextEllement = instruction == 'L' ? currentEllement.Item1 : currentEllement.Item2;
//
//         if (nextEllement == "ZZZ")
//         {
//             Console.WriteLine("Steps, part 1: " + steps);
//             found = true;
//             break;
//         }
//
//         if (map.TryGetValue(nextEllement, out var nextEllementPair))
//         {
//             currentEllement = nextEllementPair;
//             steps++;
//         }
//     }
// }

{
    var startNodes = map.Where(m => m.Key.EndsWith('A'));

    int ProcessPart2(KeyValuePair<string, (string, string)> node)
    {
        var steps = 0;
        while (true)
        {
            foreach (var instruction in instructions)
            {
                steps++;
                var nextEllement = instruction == 'L' ? node.Value.Item1 : node.Value.Item2;

                node = GetEntry(map, nextEllement);
                if (node.Key.EndsWith('Z'))
                {
                    return steps;
                }

            }
        }
    }


    static int gcd(int n1, int n2)
    {
        if (n2 == 0)
        {
            return n1;
        }
        else
        {
            return gcd(n2, n1 % n2);
        }
    }

    var stepsList = startNodes.Select(sn => ProcessPart2(sn));
    var totalSteps = stepsList.Aggregate((S, val) => S * val / gcd(S, val));
    Console.WriteLine("Steps part 2: " + totalSteps);

    static KeyValuePair<TKey, TValue> GetEntry<TKey, TValue>
    (IDictionary<TKey, TValue> dictionary,
        TKey key)
    {
        return new KeyValuePair<TKey, TValue>(key, dictionary[key]);
    }
}