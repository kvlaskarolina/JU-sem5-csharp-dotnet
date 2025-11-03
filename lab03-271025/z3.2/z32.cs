//  https://github.com/kvlaskarolina/csharp-dotnet

public class Queue_D : ArrayList
{
    public void Enqueue(object value)
    {
        base.Add(value);
    }

    public object Dequeue()
    {
        if (base.Count == 0)
        {
            throw new InvalidOperationException("err");
        }
        object value = base[0];
        base.RemoveAt(0);
        return value;
    }
}
public class Queue_K
{
    private readonly List<object> _items = new List<object>();

    public void Enqueue(object value)
    {
        _items.Add(value);
    }

    public object Dequeue()
    {
        if (_items.Count == 0)
        {
            throw new InvalidOperationException("err");
        }

        object value = _items[0];

        _items.RemoveAt(0);

        return value;
    }

    public int Count
    {
        get { return _items.Count; }
    }
}