
Console.WriteLine("Hello, Day3!");
var lines = File.ReadLines(@"..\..\..\input.txt").Select(s => s.ToCharArray()).ToArray();
int sum = 0;
int[,] coordinates = { { 0, 1 }, { 1, 0 }, { 1, 1 }, { 1, -1 },{ -1, 1 }, { -1, -1 }, { 0, -1 }, { -1, 0 } };
for(int y = 0; y < lines.Length; y++){
    for(int x = 0; x < lines[y].Length; x++)
    {
        var hasSymbol = false;
        if (Char.IsNumber(lines[y][x]))
        {
            for (int xy = 0; xy < coordinates.GetLength(0); xy++)
            {
                var cx = x + coordinates[xy,0];
                var cy = y + coordinates[xy,1];
                if (cx >= 0 && cx < lines[y].Length && cy >= 0 && cy < lines.Length)
                {
                    if (IsSymbol(lines[cy][cx]))
                    {
                        hasSymbol = true;
                        break;
                    }
                }
            }
        }

        if (hasSymbol)
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

            sum += int.Parse(sNumber);
        }

    }
}

Console.WriteLine(sum);
bool IsSymbol(char c)
{
    return !Char.IsNumber(c) && c != '.';
}