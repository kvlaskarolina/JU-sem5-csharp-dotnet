//  https://github.com/kvlaskarolina/csharp-dotnet
using System;
using System.Reflection;
using System.Linq;

public class Program
{
    public static void Main()
    {
        Type customerType = typeof(Customer);


        Console.WriteLine("Fields:");

        FieldInfo[] allFields = customerType.GetFields(
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance |
            BindingFlags.Static
        );


        Console.WriteLine("-- Public:");
        foreach (var field in allFields.Where(f => f.IsPublic).ToList())
        {
            Console.WriteLine($"   Type: \"{field.FieldType.Name}\"; name: \"{field.Name}\"");
        }

        Console.WriteLine("-- Non Public:");
        foreach (var field in allFields.Where(f => !f.IsPublic).ToList())
        {
            string accessModifier = field.IsPrivate ? "private" :
                                   field.IsFamily ? "protected" :
                                   field.IsAssembly ? "internal" : "other";
            Console.WriteLine($"   Type: \"{field.FieldType.Name}\"; name: \"{field.Name}\" ({accessModifier})");
        }

        Console.WriteLine();

        Console.WriteLine("Methods:");

        MethodInfo[] methods = customerType.GetMethods(
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance |
            BindingFlags.Static |
            BindingFlags.DeclaredOnly
        );

        foreach (var method in methods)
        {
            if (method.IsSpecialName)
                continue;

            string accessModifier = method.IsPublic ? "public" :
                                   method.IsPrivate ? "private" :
                                   method.IsFamily ? "protected" :
                                   method.IsAssembly ? "internal" : "other";

            ParameterInfo[] parameters = method.GetParameters();
            string parametersString = string.Join(", ",
                parameters.Select(p => $"{p.ParameterType.Name} {p.Name}"));

            Console.WriteLine($"   {accessModifier} {method.ReturnType.Name} {method.Name}({parametersString})");
        }

        Console.WriteLine();

        Console.WriteLine("Nested types:");

        Type[] nestedTypes = customerType.GetNestedTypes(
            BindingFlags.Public |
            BindingFlags.NonPublic
        );

        foreach (var nestedType in nestedTypes)
        {
            string typeKind = nestedType.IsEnum ? "enum" :
                             nestedType.IsClass ? "class" :
                             nestedType.IsInterface ? "interface" :
                             nestedType.IsValueType ? "struct" : "type";

            string accessModifier = nestedType.IsNestedPublic ? "public" :
                                   nestedType.IsNestedPrivate ? "private" :
                                   nestedType.IsNestedFamily ? "protected" :
                                   nestedType.IsNestedAssembly ? "internal" : "other";

            Console.WriteLine($"   {accessModifier} {typeKind} {nestedType.Name}");

            if (nestedType.IsEnum)
            {
                var enumValues = Enum.GetValues(nestedType);
                foreach (var value in enumValues)
                {
                    Console.WriteLine($"      {value} = {(int)value}");
                }
            }

            if (nestedType.IsClass && !nestedType.IsEnum)
            {
                var nestedFields = nestedType.GetFields(
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance |
                    BindingFlags.Static
                );

                if (nestedFields.Length > 0)
                {
                    Console.WriteLine("      Fields:");
                    foreach (var field in nestedFields)
                    {
                        string fieldAccess = field.IsPublic ? "public" :
                                            field.IsPrivate ? "private" : "other";
                        Console.WriteLine($"         {fieldAccess} {field.FieldType.Name} {field.Name}");
                    }
                }
            }
        }

        Console.WriteLine();

        Console.WriteLine("Properties:");

        PropertyInfo[] properties = customerType.GetProperties(
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance |
            BindingFlags.Static
        );

        foreach (var property in properties)
        {
            string accessors = "";

            if (property.CanRead)
            {
                var getMethod = property.GetGetMethod(true);
                string getAccess = getMethod.IsPublic ? "public" :
                                  getMethod.IsPrivate ? "private" :
                                  getMethod.IsFamily ? "protected" : "internal";
                accessors += $"get({getAccess})";
            }

            if (property.CanWrite)
            {
                var setMethod = property.GetSetMethod(true);
                string setAccess = setMethod.IsPublic ? "public" :
                                  setMethod.IsPrivate ? "private" :
                                  setMethod.IsFamily ? "protected" : "internal";
                if (accessors.Length > 0)
                    accessors += "; ";
                accessors += $"set({setAccess})";
            }

            Console.WriteLine($"   Type: \"{property.PropertyType.Name}\"; name: \"{property.Name}\" [{accessors}]");
        }

        Console.WriteLine();

        Console.WriteLine("Members:");

        MemberInfo[] members = customerType.GetMembers(
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance |
            BindingFlags.Static |
            BindingFlags.DeclaredOnly
        );

        var groupedMembers = members.GroupBy(m => m.MemberType)
                                   .OrderBy(g => g.Key.ToString());

        foreach (var group in groupedMembers)
        {
            Console.WriteLine($"-- {group.Key}:");
            foreach (var member in group)
            {
                Console.WriteLine($"   {member.Name}");
            }
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