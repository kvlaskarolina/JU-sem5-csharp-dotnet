//  https://github.com/kvlaskarolina/csharp-dotnet

using System;
using System.Text;
using System.Globalization;
using System.Data;

public class Matrix<T> : IComparable<Matrix<T>>, IFormattable where T : struct, IComparable<T>
{
    private T[,] elements;
    private int rows;
    private int cols;

    public Matrix(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        elements = new T[rows, cols];
    }

    public int Rows => rows;
    public int Cols => cols;

    public void SetElement(int row, int col, T value)
    {
        elements[row, col] = value;
    }

    public T GetElement(int row, int col)
    {
        return elements[row, col];
    }

    public Matrix<T> AddMatrix(Matrix<T> m)
    {
        if (m.Cols != this.Cols || m.Rows != this.Rows)
        {
            Matrix<T> defaultResult = new Matrix<T>(1, 1);
            defaultResult.SetElement(0, 0, default(T));
            return defaultResult;
        }

        Matrix<T> result = new Matrix<T>(m.Rows, m.Cols);
        for (int i = 0; i < m.Rows; ++i)
        {
            for (int j = 0; j < m.Cols; ++j)
            {
                T sum = (dynamic)this.GetElement(i, j) + (dynamic)m.GetElement(i, j);
                result.SetElement(i, j, sum);
            }
        }
        return result;
    }

    public Matrix<T> MultiplyMatrix(Matrix<T> m)
    {
        if (this.Cols != m.Rows)
        {
            throw new ArgumentException("err");
        }

        Matrix<T> result = new Matrix<T>(this.Rows, m.Cols);

        for (int i = 0; i < this.Rows; i++)
        {
            for (int j = 0; j < m.Cols; j++)
            {
                dynamic sum = default(T);

                for (int k = 0; k < this.Cols; k++)
                {
                    sum = sum + (dynamic)this.GetElement(i, k) * (dynamic)m.GetElement(k, j);
                }
                result.SetElement(i, j, (T)sum);
            }
        }
        return result;
    }


    public override string ToString()
    {
        return this.ToString("G", CultureInfo.CurrentCulture); 
    }

    public int CompareTo(Matrix<T>? other)
    {
        if (other == null) return 1;
        long thisSize = (long)this.rows * this.cols;
        long otherSize = (long)other.rows * other.cols;
        return thisSize.CompareTo(otherSize);
    }
    public void print()
    {
        for (int i = 0; i < this.Rows; i++)
        {
            for (int j = 0; j < this.Cols; j++)
            {
                Console.Write(GetElement(i, j) + " ");
            }
            Console.WriteLine();
        }
    }
}

public class SquareMatrix<T> : Matrix<T> where T : struct, IComparable<T>
{
    public SquareMatrix(int size) : base(size, size)
    {
    }

    public bool IsDiagonal()
    {
        for (int i = 0; i < this.Rows; i++)
        {
            for (int j = 0; j < this.Cols; j++)
            {
                if (i != j && !this.GetElement(i, j).Equals(default(T)))
                {
                    return false;
                }
            }
        }
        return true;
    }
}
class Program
{
    static void Main(string[] args)
    {
        SquareMatrix<int> m1 = new SquareMatrix<int>(2);
        m1.SetElement(0, 0, 1);
        m1.SetElement(0, 1, 0);
        m1.SetElement(1, 0, 0);
        m1.SetElement(1, 1, 7);
        Console.WriteLine("m1 is diagonal? " + m1.IsDiagonal());
        Matrix<int> m2 = new Matrix<int>(2, 1);
        m2.SetElement(0, 0, 3);
        m2.SetElement(1, 0, 3);
        Matrix<int> m3 = m1.MultiplyMatrix(m2);
        Console.WriteLine("m1:");
        m1.print();
        Console.WriteLine("m2:");
        m2.print();
        Console.WriteLine("m3 = m1 * m2:");
        m3.print();

        SquareMatrix<int> m4 = new SquareMatrix<int>(2);
        m4.SetElement(0, 0, 25);
        m4.SetElement(0, 1, -4);
        m4.SetElement(1, 0, 6);
        m4.SetElement(1, 1, 7);

        Matrix<int> m5 = m1.AddMatrix(m4);
        Console.WriteLine("m4:");
        m4.print();
        Console.WriteLine("m5 = m1 + m4:");
        m5.print();

    }
}