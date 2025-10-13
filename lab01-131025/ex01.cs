class Game
{
    public Location location { get; set; }
    public enum Location { Las, Wioska, Zamek, Jaskinia };

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
        Player player = new Player();
        player.chooseName();
        player.chooseType();
        Console.Clear();
        Console.WriteLine($"{player.type} {player.name} wyrusza na przygode:DD");
        chooseLocation();
    }
    public void chooseLocation()
    {
        Console.WriteLine("Wybierz lokalizację:");
        Console.WriteLine("[1] Las");
        Console.WriteLine("[2] Wioska");
        Console.WriteLine("[3] Zamek");
        Console.WriteLine("[4] Jaskinia");
        string? locationChoice = Console.ReadLine();
        switch (locationChoice)
        {
            case "1":
                location = Location.Las;
                Console.WriteLine("Wybrałeś Las.");
                break;
            case "2":
                location = Location.Wioska;
                Console.WriteLine("Wybrałeś Wioskę.");
                break;
            case "3":
                location = Location.Zamek;
                Console.WriteLine("Wybrałeś Zamek.");
                break;
            case "4":
                location = Location.Jaskinia;
                Console.WriteLine("Wybrałeś Jaskinię.");
                break;
            default:
                Console.WriteLine("Nieprawidłowy wybór, spróbuj ponownie.");
                chooseLocation();
                break;
        }
    }
}
class Player
{
    public string name { get; set; } = string.Empty;
    public CharacterType type { get; set; }

    public enum CharacterType { Barbarzyńca, Paladyn, Amazonka };
    public void chooseName()
    {
        Console.WriteLine("Podaj nazwę gracza:");
        string? playerName = Console.ReadLine();
        if (playerName != null)
        {
            if (validateName(ref playerName))
            {
                setName(playerName);
            }
            else
            {
                chooseName();
            }
        }
        else
        {
            Console.WriteLine("Nazwa gracza nie może być pusta.");
            chooseName();
        }
    }
    public void setName(string playerName)
    {
        name = playerName;
    }
    public bool validateName(ref string playerName)
    {
        playerName = playerName.Trim();
        playerName = playerName.Replace(" ", "");
        playerName = playerName.ToLower();
        if (playerName.Length < 2)
        {
            Console.WriteLine("Nazwa gracza musi mieć co najmniej 2 znaki.");
            return false;
        }
        return true;
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