//  https://github.com/kvlaskarolina/csharp-dotnet
using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    static void Main()
    {
        var n = int.Parse(Console.ReadLine());
        IEnumerable<int> nums = Enumerable.Range(1, n);
        var squares = nums.Where(x => x != 5 && x != 9 && (x % 2 == 1 || x % 7 == 0)).Select(x => x * x);
        //Console.WriteLine(string.Join(", ", squares)); // to test:)
        Console.WriteLine(squares.Sum());
        Console.WriteLine(squares.Count());
        Console.WriteLine(squares.First());
        Console.WriteLine(squares.Last());
        Console.WriteLine(squares.ElementAt(2));
    }
}