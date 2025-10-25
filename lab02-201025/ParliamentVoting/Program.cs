using System;

namespace VotingSystem
{
    public delegate void VotingDelegate();

    public class VotingTopic
    {
        public string name { get; set; }
        public string description { get; set; }

        public VotingTopic(string name, string description)
        {
            this.name = name;
            this.description = description;
        }
    }

    public class Parliamentarian
    {
        public string name { get; set; }
        public bool lastVote { get; set; }
        private Random random = new Random();

        public Parliamentarian(string name)
        {
            this.name = name;
            this.lastVote = false;
        }

        public void Voting()
        {
            bool vote = random.Next(2) == 0;
            lastVote = vote;
            Console.WriteLine($"{name} oddał głos: {(vote ? "ZA" : "PRZECIW")}");
        }
    }

    public class Parlament
    {
        private List<Parliamentarian> parliamentarians = new List<Parliamentarian>();

        public event VotingDelegate StartVoting;

        public event EventHandler<List<Parliamentarian>> EndVoting;

        public void AddParliamentarian(Parliamentarian Parliamentarian)
        {
            parliamentarians.Add(Parliamentarian);
            Console.WriteLine($"Dodano parlamentarzystę: {Parliamentarian.name}");
        }

        public void Start(VotingTopic topic)
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine($"ROZPOCZĘCIE GŁOSOWANIA");
            Console.WriteLine($"topic: {topic.name}");
            Console.WriteLine($"description: {topic.description}");
            Console.WriteLine(new string('=', 60));

            OnStartVoting();

            var votingResult = GetVotingResult();

            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"WYNIKI GŁOSOWANIA:");
            Console.WriteLine($"  ZA: {votingResult.votesFor}");
            Console.WriteLine($"  PRZECIW: {votingResult.votesAgainst}");
            Console.WriteLine($"  Rezultat: {(votingResult.votesFor > votingResult.votesAgainst ? "WNIOSEK PRZYJĘTY" : "WNIOSEK ODRZUCONY")}");
            Console.WriteLine(new string('-', 60));

            OnEndVoting();
        }


        protected virtual void OnStartVoting()
        {
            Console.WriteLine("\nParlamentarzyści oddają głosy:\n");
            StartVoting?.Invoke();
        }

        protected virtual void OnEndVoting()
        {
            Console.WriteLine("\nGłosowanie zakończone!");
            // Pass the list of parliamentarians to the EndVoting event
            EndVoting?.Invoke(this, parliamentarians);
        }

        public (int votesFor, int votesAgainst) GetVotingResult()
        {
            int votesFor = 0;
            int votesAgainst = 0;

            foreach (var p in parliamentarians)
            {
                if (p.lastVote)
                    votesFor++;
                else
                    votesAgainst++;
            }

            return (votesFor, votesAgainst);
        }

        public void AddToVoting(Parliamentarian p)
        {
            StartVoting += p.Voting;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Parlament parlament = new Parlament();

            parlament.EndVoting += (sender, e) =>
            {
                Console.WriteLine($"\n[System]: Wyniki głosowania zostały zapisane. Liczba parlamentarzystów: {e.Count}");
            };

            var p1 = new Parliamentarian("Parliamentarian1");
            var p2 = new Parliamentarian("Parliamentarian2");
            var p3 = new Parliamentarian("Parliamentarian3");
            var p4 = new Parliamentarian("Parliamentarian4");
            var p5 = new Parliamentarian("Parliamentarian5");

            parlament.AddParliamentarian(p1);
            parlament.AddParliamentarian(p2);
            parlament.AddParliamentarian(p3);
            parlament.AddParliamentarian(p4);
            parlament.AddParliamentarian(p5);

            parlament.AddToVoting(p1);
            parlament.AddToVoting(p2);
            parlament.AddToVoting(p3);
            parlament.AddToVoting(p4);
            parlament.AddToVoting(p5);

            var topic1 = new VotingTopic(
                "Ustawa o ochronie środowiska",
                "Projekt ustawy dotyczącej zwiększenia ochrony lasów i parków narodowych"
            );
            parlament.Start(topic1);

            var topic2 = new VotingTopic(
                "Budżet na rok 2025",
                "Zatwierdzenie projektu budżetu państwa na rok 2025"
            );
            parlament.Start(topic2);

            Console.WriteLine("\n\nNaciśnij dowolny klawisz, aby zakończyć...");
            Console.ReadKey();
        }
    }
}