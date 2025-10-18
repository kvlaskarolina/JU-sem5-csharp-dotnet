using System.Security.Cryptography.X509Certificates;
// link do repo na github - pewnie bedzie lepiej patrzeć na to tam niż tutaj :D -
//  https://github.com/kvlaskarolina/csharp-dotnet
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
        while (true)
        {
            location = chooseLocation();
            NPC npc1 = new NPC();
            NPC npc2 = new NPC();
            Random random = new Random();
            NPC.NPCType randomNPCType = (NPC.NPCType)random.Next(Enum.GetValues(typeof(NPC.NPCType)).Length);
            npc1.setNPCType(randomNPCType);
            randomNPCType = (NPC.NPCType)random.Next(Enum.GetValues(typeof(NPC.NPCType)).Length);
            while (randomNPCType == npc1.npcType)
            {
                randomNPCType = (NPC.NPCType)random.Next(Enum.GetValues(typeof(NPC.NPCType)).Length);
            }
            npc2.setNPCType(randomNPCType);
            player.meetNPC(npc1, npc2, location);
            Console.ReadKey();
            Console.Clear();
        }
    }
    public Location chooseLocation()
    {
        Console.WriteLine("\nWybierz lokalizację:");
        Console.WriteLine("[1] Las");
        Console.WriteLine("[2] Wioska");
        Console.WriteLine("[3] Zamek");
        Console.WriteLine("[4] Jaskinia");
        Console.WriteLine("[X] Wyjdź z gry");
        string? locationChoice = Console.ReadLine();


        switch (locationChoice?.ToUpper())
        {
            case "1":
                location = Location.Las;
                Console.WriteLine("Wybrałeś Las.");
                return location;
            case "2":
                location = Location.Wioska;
                Console.WriteLine("Wybrałeś Wioskę.");
                return location;
            case "3":
                location = Location.Zamek;
                Console.WriteLine("Wybrałeś Zamek.");
                return location;
            case "4":
                location = Location.Jaskinia;
                Console.WriteLine("Wybrałeś Jaskinię.");
                return location;
            case "X":
                Environment.Exit(0);
                return location;
            default:
                Console.WriteLine("Nieprawidłowy wybór, spróbuj ponownie.");
                return chooseLocation();
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
    public void meetNPC(NPC npc1, NPC npc2, Game.Location location)
    {
        Console.WriteLine($"Spotkałeś {npc1.npcType} oraz {npc2.npcType}. Wybierz z kim chcesz porozmawiać:");
        Console.WriteLine($"[1] {npc1.npcType}");
        Console.WriteLine($"[2] {npc2.npcType}");
        string? npcChoice = Console.ReadLine();
        switch (npcChoice)
        {
            case "1":
                Console.WriteLine($"Rozmawiasz z {npc1.npcType}.");
                npc1.NPCInteraction(location);
                break;
            case "2":
                Console.WriteLine($"Rozmawiasz z {npc2.npcType}.");
                npc2.NPCInteraction(location);
                break;
            default:
                Console.WriteLine("Nieprawidłowy wybór, spróbuj ponownie.");
                meetNPC(npc1, npc2, location);
                break;
        }

    }
}
class NPC : Player
{
    public enum NPCType { Kupiec, Żołnierz, Wieśniak, Szaman, Łowca };
    public NPCType npcType { get; set; }
    public void setNPCType(NPCType type)
    {
        npcType = type;
    }
    public void NPCInteraction(Game.Location location)
    {
        Console.WriteLine("------------------------------------------");
        Console.WriteLine($"Rozmowa z {npcType} w {location}:");
        bool dialogActive = true;
        string choice;

        while (dialogActive)
        {
            Console.Clear();
            Console.WriteLine($"--- ROZMAWIASZ Z {npcType} W {location} ---");
            Console.WriteLine("------------------------------------------");

            switch (npcType)
            {
                case NPCType.Kupiec:
                    Console.WriteLine("Kupiec: 'Witaj, podróżniku! Zawsze mam coś ciekawego w ofercie. Czego szukasz?'");
                    Console.WriteLine("[1] Czegoś do sprzedania.");
                    Console.WriteLine("[2] Czegoś do kupienia.");
                    Console.WriteLine("[3] Nie, żegnaj.");
                    choice = Console.ReadLine()?.ToUpper() ?? string.Empty;

                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("Hero: 'Czegoś do sprzedania.'");
                            Console.WriteLine("Kupiec: 'Świetnie! Pokaż, co masz. Za rzadkie przedmioty płacę więcej, niż w tej Wiosce/Lesie/Zamku/Jaskini!'");
                            dialogActive = false;
                            break;
                        case "2":
                            Console.WriteLine("Hero: 'Czegoś do kupienia.'");
                            Console.WriteLine("Kupiec: 'Mam miecze, zbroje i mikstury leczące! Ale musisz mieć złoto.'");
                            dialogActive = false;
                            break;
                        case "3":
                            Console.WriteLine("Hero: 'Nie, żegnaj.'");
                            Console.WriteLine("Kupiec: 'Szkoda, może następnym razem. Bezpiecznej drogi.'");
                            dialogActive = false;
                            break;
                        default:
                            Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                            Console.ReadKey();
                            break;
                    }
                    break;

                case NPCType.Żołnierz:
                    Console.WriteLine("Żołnierz: 'Stój! Jesteś nową twarzą w okolicy. Masz jakieś wieści czy jesteś tu w interesach?'");
                    Console.WriteLine("[1] Mam pilną wiadomość dla kapitana.");
                    Console.WriteLine("[2] Jestem tu przejazdem.");
                    Console.WriteLine("[3] To nie twój interes. (Agresywnie)");
                    choice = Console.ReadLine()?.ToUpper() ?? string.Empty;

                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("Hero: 'Mam pilną wiadomość dla kapitana.'");
                            Console.WriteLine("Żołnierz: 'Rozumiem. Pójdź za mną, natychmiast cię do niego zaprowadzę!'");
                            dialogActive = false;
                            break;
                        case "2":
                            Console.WriteLine("Hero: 'Jestem tu przejazdem.'");
                            Console.WriteLine("Żołnierz: 'Dobrze, ale trzymaj się z dala od problemów. Miasto/Zamek jest pod specjalnym nadzorem.'");
                            dialogActive = false;
                            break;
                        case "3":
                            Console.WriteLine("Hero: 'To nie twój interes.'");
                            Console.WriteLine("Żołnierz: 'Ostrożnie z tonem, cywilu. Nie stwarzaj kłopotów, albo cię zamknę!'");
                            dialogActive = false;
                            break;
                        default:
                            Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                            Console.ReadKey();
                            break;
                    }
                    break;

                case NPCType.Wieśniak:
                    int goldReward = 100;
                    Console.WriteLine("Wieśniak: 'Och, widzę bohatera! Czy możesz mi pomóc dostać się do innego miasta? Czuję, że grozi mi niebezpieczeństwo.'");
                    Console.WriteLine("[1] Tak, chętnie pomogę. (Za nagrodę 100 złota)");
                    Console.WriteLine("[2] 100 sztuk złota to za mało!");
                    Console.WriteLine("[3] Nie, nie pomogę, żegnaj.");
                    choice = Console.ReadLine()?.ToUpper() ?? string.Empty;

                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("Hero: 'Tak, chętnie pomogę.'");
                            Console.WriteLine($"Wieśniak: 'Dziękuję! Otrzymasz ode mnie {goldReward} sztuk złota. Do zobaczenia!'");
                            dialogActive = false;
                            break;

                        case "2":
                            Console.WriteLine("Hero: '100 sztuk złota to za mało!'");
                            Console.WriteLine("Wieśniak: 'Niestety nie mam więcej. Jestem bardzo biedny.'");

                            Console.WriteLine("\n[1] OK, może być 100 sztuk złota.");
                            Console.WriteLine("[2] W takim razie radź sobie sam.");
                            Console.Write("Twój wybór: ");
                            string? subChoice = Console.ReadLine();

                            if (subChoice == "1")
                            {
                                Console.WriteLine("Hero: 'OK, może być 100 sztuk złota.'");
                                Console.WriteLine($"Wieśniak: 'Dziękuję! Będę czekał.'");
                                dialogActive = false;
                            }
                            else if (subChoice == "2")
                            {
                                Console.WriteLine("Hero: 'W takim razie radź sobie sam.'");
                                Console.WriteLine("Wieśniak: 'Rozumiem, będę musiał prosić kogoś innego.'");
                                dialogActive = false;
                            }
                            else
                            {
                                Console.WriteLine("Nieprawidłowy wybór. Wracasz do głównych opcji dialogu.");
                                Console.ReadKey();
                            }
                            break;

                        case "3":
                            Console.WriteLine("Hero: 'Nie, nie pomogę, żegnaj.'");
                            Console.WriteLine("Wieśniak: 'Żegnaj. Szkoda, że nie chciałeś pomóc.'");
                            dialogActive = false;
                            break;

                        default:
                            Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                            Console.ReadKey();
                            break;
                    }
                    break;

                case NPCType.Szaman:
                    Console.WriteLine("Szaman: 'Duchy mi szepczą, że masz kłopot, wędrowcze. Czego pragniesz? Porady, klątwy, czy może mikstury?'");
                    Console.WriteLine("[1] Potrzebuję potężnej mikstury leczącej.");
                    Console.WriteLine("[2] Szukam wiedzy o tej krainie.");
                    Console.WriteLine("[3] Nie wierzę w twoje czary. (Wzgardliwie)");
                    choice = Console.ReadLine()?.ToUpper() ?? string.Empty;

                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("Hero: 'Potrzebuję potężnej mikstury leczącej.'");
                            Console.WriteLine("Szaman: 'Potrzebujesz rzadkich ziół ze szczytu Złej Góry. Przynieś mi je, a dam ci w zamian eliksir życia.'");
                            dialogActive = false;
                            break;
                        case "2":
                            Console.WriteLine("Hero: 'Szukam wiedzy o tej krainie.'");
                            Console.WriteLine("Szaman: 'Wiedz, że w Lesie kryje się starożytna siła, a w Jaskini czeka wielka potworność. Idź ostrożnie.'");
                            dialogActive = false;
                            break;
                        case "3":
                            Console.WriteLine("Hero: 'Nie wierzę w twoje czary.'");
                            Console.WriteLine("Szaman: 'Twoja arogancja cię zgubi. Lepiej odejdź, zanim sprowadzę na ciebie pecha.'");
                            dialogActive = false;
                            break;
                        default:
                            Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                            Console.ReadKey();
                            break;
                    }
                    break;

                case NPCType.Łowca:
                    Console.WriteLine("Łowca: 'Cześć. Nie mam czasu na pogaduszki, poluję na Bestię. Coś cię sprowadza w te niebezpieczne tereny?'");
                    Console.WriteLine("[1] Pomogę ci upolować Bestię.");
                    Console.WriteLine("[2] Szukam informacji o kryjówce bandytów.");
                    Console.WriteLine("[3] Tylko podziwiam twoje futra.");
                    choice = Console.ReadLine()?.ToUpper() ?? string.Empty;

                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("Hero: 'Pomogę ci upolować Bestię.'");
                            Console.WriteLine("Łowca: 'Naprawdę? To świetnie! Będziemy działać razem, ale ja biorę trofeum. Zgoda?'");
                            dialogActive = false;
                            break;
                        case "2":
                            Console.WriteLine("Hero: 'Szukam informacji o kryjówce bandytów.'");
                            Console.WriteLine("Łowca: 'Bandytów? Ostatnio widziałem ich ślady w okolicach starej Jaskini. To wszystko, co wiem.'");
                            dialogActive = false;
                            break;
                        case "3":
                            Console.WriteLine("Hero: 'Tylko podziwiam twoje futra.'");
                            Console.WriteLine("Łowca: 'Futro nie jest do podziwiania, jest do zarabiania. Odejść, jeśli nie masz nic ważnego do powiedzenia.'");
                            dialogActive = false;
                            break;
                        default:
                            Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                            Console.ReadKey();
                            break;
                    }
                    break;
            }
        }
        Console.WriteLine("\nRozmowa zakończona. Naciśnij dowolny klawisz, aby kontynuować...");
        Console.ReadKey();
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