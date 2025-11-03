//  https://github.com/kvlaskarolina/csharp-dotnet
class Person
{
    public string name { get; set; }
    public string surname { get; set; }
    public Person(string n, string s)
    {
        name = n;
        surname = s;
    }
    public void Display()
    {

        Console.WriteLine($"name: {name} surname: {surname}");
    }
    public void Rename(string newName, string newSurname)
    {
        name = newName;
        surname = newSurname;
    }


}

class Program
{
    public static void RenameNew(string newName, string newSurname, ref Person person)
    {
        var newPerson = new Person(newName, newSurname);
        person = newPerson;
    }
    public static void SetNull(ref Person person)
    {
        person = null;
    }
    static void Main(string[] args)
    {
        var jan = new Person("Jan", "Nowak");
        jan.Display();
        jan.Rename("Jan", "renamed");
        // bez uzycia ref, zmiany sa widoczne, poniewaz modyfikujemy wlasciwosci istniejacego obiektu
        jan.Display();
        RenameNew("Jan", "renamedNew", ref jan);
        // musielismy uzyc ref, aby przypisanie nowego obiektu, do juz istinejacego bylo mozliwe
        // i widoczne zewnatrz metody
        jan.Display();
        SetNull(ref jan);
        if (jan == null)
        {
            Console.WriteLine("person is null");
        }
        else
            jan.Display();
    }
}
