// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, Day1!");
var lines = File.ReadLines(@"..\..\..\input.txt");

Console.WriteLine(lines.Select(Num).Sum());

int Num(string s)
{
   var numbs = new string(s.Where(Char.IsDigit).ToArray());
   if (numbs.Length == 1)
   {
      return int.Parse(numbs + numbs);
   }

   char[] sc = { numbs[0], numbs[^1] };
   return int.Parse(sc);
}
