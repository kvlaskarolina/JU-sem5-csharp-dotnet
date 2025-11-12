//  https://github.com/kvlaskarolina/csharp-dotnet
class Complex<T>
{
    T realPart;
    T imaginaryPart;
    public Complex(T real, T imaginary)
    {
        realPart = real;
        imaginaryPart = imaginary;
    }
    public override string ToString()
    {
        return $"{realPart} + {imaginaryPart}i";
    }
    public T GetRealPart()
    {
        return realPart;
    }
    public T GetImaginaryPart()
    {
        return imaginaryPart;
    }
}
class Program
{
    static void Main(string[] args)
    {
        Complex<int> complexInt = new Complex<int>(9, 8);
        Console.WriteLine("Complex Number (int): " + complexInt);
        Console.WriteLine("Real Part: " + complexInt.GetRealPart());
        Console.WriteLine("Imaginary Part: " + complexInt.GetImaginaryPart());

        Complex<double> complexDouble = new Complex<double>(8.07, 30.10);
        Console.WriteLine("Complex Number (double): " + complexDouble);
        Console.WriteLine("Real Part: " + complexDouble.GetRealPart());
        Console.WriteLine("Imaginary Part: " + complexDouble.GetImaginaryPart());
    }
}