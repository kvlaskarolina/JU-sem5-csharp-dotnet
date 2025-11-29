//  https://github.com/kvlaskarolina/csharp-dotnet
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        var n = int.Parse(Console.ReadLine());
        var m = int.Parse(Console.ReadLine());
        Random random = new Random();

        var matrixNxM = Enumerable.Range(0, n)
            .Select(_ =>
            {
                return Enumerable.Range(0, m)
                    .Select(col => random.Next(1, 101))
                    .ToList();
            })
            .ToList();

        var suma = matrixNxM
            .SelectMany(wiersz => wiersz)
            .Sum(x => (long)x);

        Console.WriteLine(suma);

        string macierzStr = string.Join("\n",
            matrixNxM.Select(wiersz => string.Join(" ", wiersz)));
        Console.WriteLine(macierzStr);
    }
}