using System.Collections;

namespace Ploeh.Samples.Zippers;

public sealed class ListZipper<T> : IEnumerable<T>
{
    private readonly IEnumerable<T> values;
    public IEnumerable<T> Breadcrumbs { get; }

    private ListZipper(IEnumerable<T> values, IEnumerable<T> breadcrumbs)
    {
        this.values = values;
        Breadcrumbs = breadcrumbs;
    }

    public ListZipper(IEnumerable<T> values) : this(values, [])
    {
    }

    public ListZipper(params T[] values) : this(values.AsEnumerable())
    {
    }

    public ListZipper<T>? GoForward()
    {
        var head = values.Take(1);
        if (!head.Any())
            return null;

        var tail = values.Skip(1);
        return new ListZipper<T>(tail, head.Concat(Breadcrumbs));
    }

    public ListZipper<T>? GoBack()
    {
        var head = Breadcrumbs.Take(1);
        if (!head.Any())
            return null;

        var tail = Breadcrumbs.Skip(1);
        return new ListZipper<T>(head.Concat(values), tail);
    }

    public ListZipper<T> Insert(T value)
    {
        return new ListZipper<T>(values.Prepend(value), Breadcrumbs);
    }

    public ListZipper<T>? Remove()
    {
        if (!values.Any())
            return null;

        return new ListZipper<T>(values.Skip(1), Breadcrumbs);
    }

    public ListZipper<T>? Replace(T newValue)
    {
        return Remove()?.Insert(newValue);
    }

    public override bool Equals(object? obj)
    {
        if (obj is ListZipper<T> other)
            return values.SequenceEqual(other.values)
                && Breadcrumbs.SequenceEqual(other.Breadcrumbs);

        return base.Equals(obj);
    }

    public override string ToString()
    {
        return $"LZ ([{string.Join(", ", values)}], [{string.Join(", ", Breadcrumbs)})]";
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(values, Breadcrumbs);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}