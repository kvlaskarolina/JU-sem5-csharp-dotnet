abstract class Figure
{
    public abstract int GetArea();
}

class Square : Figure
{
    public virtual int A { get; set; }
    public Square(int AA)
    {
        this.A = AA;
    }
    public override int GetArea()
    {
        return A * A;
    }
}

class Rectangle : Square
{
    public int B { get; set; }

    public Rectangle(int AA, int BB) : base(AA)
    {
        this.B = BB;
    }

    public override int GetArea()
    {
        return A * B;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var r = new Rectangle(1, 2);
        Console.WriteLine(r.GetArea());

        var s = new Square(3);
        Console.WriteLine(s.GetArea());

        Figure fig = r;
        Console.WriteLine(fig.GetArea());

        fig = s;
        Console.WriteLine(fig.GetArea());
    }
}