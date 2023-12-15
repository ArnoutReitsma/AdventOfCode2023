
Console.WriteLine("Hello, Day7!");

var hands = File.ReadLines(@"..\..\..\input.txt")
    .Select(l => new Hand(l.Split()[0].ToArray(), int.Parse(l.Split()[1]))).ToArray();

foreach (var hand in hands)
{
    if (hand.Matches.Any(c => c.count == 5))
    {
        hand.Rank = 7;
    }
    else if (hand.Matches.Any(c => c.count == 4))
    {
        hand.Rank = 6;
    }
    else if (hand.Matches.Any(c => c.count == 2) && hand.Matches.Any(c => c.count == 3))
    {
        hand.Rank = 5;
    }
    else  if (hand.Matches.Any(c => c.count == 3))
    {
        hand.Rank = 4;
    }
    else if (hand.Matches.Count(c => c.count == 2) == 2)
    {
        hand.Rank = 3;
    }
    else if (hand.Matches.Any(c => c.count == 2))
    {
        hand.Rank = 2;
    }
    else
    {
        hand.Rank = 1;
    }
}

var orderd = hands.OrderBy(h => h.Rank).ThenByDescending(h => h.OrderedCards).ToList();
var sumGame = 0;
foreach (var hand in orderd)
{
    sumGame += (orderd.IndexOf(hand) + 1) * hand.bid;
}
Console.WriteLine("Sum of games: " + sumGame);
record Hand(char[] cards, int bid)
{
    public List<Match> Matches1  { get => cards.ToArray().GroupBy(c => c)
        .Select(group => new Match(group.Key,  group.Count())).ToList();  }

    public List<Match> Matches
    {
        get
        {
             var matches = cards.ToArray().Where(c => c != 'J').GroupBy(c => c)
                .Select(group => new Match(group.Key, group.Count())).ToList();
             matches = matches.OrderByDescending(m => m.count).ToList();
             if(matches.Any())
                 matches.First().count += Jokers;
             else
                 matches.Add(new Match('J', 5));
             return matches;
        }
    }

    public int Jokers { get => cards.Count(c => c == 'J'); }
    public int Rank { get; set; }
    public string OrderedCards
    {
        get
        {
            var numbers = new char[]{'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J'};
            return string.Join("", cards.Select(c => ((char)(numbers.ToList().IndexOf(c) + 65)).ToString()));
        }
    }
};

record Match(char c, int count)
{
    public int count { get; set; } = count;
}