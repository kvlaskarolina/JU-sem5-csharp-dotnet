//  https://github.com/kvlaskarolina/csharp-dotnet
class Program
{
    static void Main()
    {
        List<string> miasta = new List<string>();
        string wejscie;
        char litera;
        do
        {
            wejscie = Console.ReadLine();

            if (wejscie != null && wejscie.Trim().Equals("X", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            if (!string.IsNullOrWhiteSpace(wejscie))
            {
                miasta.Add(wejscie.Trim());
            }

        } while (true);
        do
        {
            litera = Console.ReadLine()[0];

            if (litera == 'X' || litera == 'x')
            {
                break;
            }

            var miastaNaLitere = miasta
            .GroupBy(miasto => miasto[0])
            .OrderBy(grupa => grupa.Key)
            .Where(grupa => char.ToUpper(grupa.Key) == char.ToUpper(litera))
            .SelectMany(grupa => grupa)
            .OrderBy(miasto => miasto)
            .ToList();
            if (miastaNaLitere.Any())
            {
                Console.WriteLine(string.Join(", ", miastaNaLitere));
            }
            else
            {
                Console.WriteLine("PUSTE");
            }


        } while (true);
    }
}
