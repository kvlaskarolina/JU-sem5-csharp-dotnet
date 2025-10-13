class Game
{
    public void start()
    {
        Console.WriteLine("Witaj w grze <gra>!");
        Console.WriteLine("[1] Zacznij nową grę");
        Console.WriteLine("[X] Zamknij program");
        string? choice = Console.ReadLine();
        if (choice == "1")
        {
            NewGame();
        }
        else if (choice?.ToUpper() == "X")
        {
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("Nieprawidłowy wybór, spróbuj ponownie.");
            start();
        }
    }
    public void NewGame()
    {
        Console.Clear();
        Console.WriteLine("Nowa gra");
        Console.WriteLine("Podaj nazwę gracza:");
        string? playerName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(playerName))
        {
            Console.WriteLine("Nazwa gracza nie może być pusta.");
            return;
        }
        Player player = new Player();
        player.validateName(playerName);
        Console.WriteLine($"Witaj, {player.name}!");
        player.chooseType();
        Console.Clear();
        Console.WriteLine($"{player.type} {player.name} wyrusza na przygode:DD");
    }
}

class Player
{
    public string name { get; set; } = string.Empty;
    public CharacterType type { get; set; }

    public enum CharacterType { Barbarzyńca, Paladyn, Amazonka };

    public void setName(string playerName)
    {
        name = playerName;
    }
    public void validateName(string playerName)
    {
        setName(playerName);
    }
    public void chooseType()
    {
        Console.WriteLine("Wybierz klasę postaci:");
        Console.WriteLine("[1] Barbarzyńca");
        Console.WriteLine("[2] Paladyn");
        Console.WriteLine("[3] Amazonka");
        string? classChoice = Console.ReadLine();
        switch (classChoice)
        {
            case "1":
                type = CharacterType.Barbarzyńca;
                Console.WriteLine("Wybrałeś Barbarzyńcę.");
                break;
            case "2":
                type = CharacterType.Paladyn;
                Console.WriteLine("Wybrałeś Paladyna.");
                break;
            case "3":
                type = CharacterType.Amazonka;
                Console.WriteLine("Wybrałeś Amazonkę.");
                break;
            default:
                Console.WriteLine("Nieprawidłowy wybór, spróbuj ponownie.");
                chooseType();
                break;
        }
    }
}
class Program
{
    static void Main()
    {
        Game game = new Game();
        game.start();
    }
}