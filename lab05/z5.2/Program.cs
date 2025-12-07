//  https://github.com/kvlaskarolina/csharp-dotnet
using System;
using System.Reflection;

public class Program
{
    public static void Main()
    {
        Customer customer = new Customer("Karo Karo");

        Type customerType = typeof(Customer);

        PropertyInfo[] properties = customerType.GetProperties(
            BindingFlags.Public |
            BindingFlags.Instance
        );


        foreach (var property in properties)
        {
            Console.WriteLine($"wlasciwosc: {property.Name} (typ: {property.PropertyType.Name})");

            if (property.CanWrite)
            {

                if (property.PropertyType == typeof(string))
                {

                    property.SetValue(customer, "adres adres adres");
                    Console.WriteLine($"   ✓ adres adres adres");

                }
                else if (property.PropertyType == typeof(int))
                {
                    property.SetValue(customer, 807);
                    Console.WriteLine($"   ✓  807");

                }
                else if (property.PropertyType == typeof(bool))
                {
                    property.SetValue(customer, true);
                    Console.WriteLine($"   ✓ Ustawiono wartość: true");
                }

            }
            else
            {
                Console.WriteLine($"   ⓘ read only");
            }

            Console.WriteLine();
        }

    }

}

public class Customer
{
    private string _name;
    protected int _age;
    public bool isPreferred;

    public Customer(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("Customer name!");
        _name = name;
    }

    public string Name
    {
        get { return _name; }
    }

    public string Address { get; set; }

    public int SomeValue { get; set; }

    public int ImportantCalculation()
    {
        return 1000;
    }

    public void ImportantVoidMethod()
    {
    }

    public enum SomeEnumeration
    {
        ValueOne = 1,
        ValueTwo = 2
    }

    public class SomeNestedClass
    {
        private string _someString;
    }
}
