
namespace Ploeh.Samples.Zippers;

public sealed class ListZipper<T>
{
    private readonly IEnumerable<T> values;
    private readonly IEnumerable<T> breadcrumbs;

    public ListZipper(IEnumerable<T> values, IEnumerable<T> breadcrumbs)
    {
        this.values = values;
        this.breadcrumbs = breadcrumbs;
    }

    public ListZipper<T>? GoForward()
    {
        var head = values.Take(1);
        if (!head.Any())
            return null;

        var tail = values.Skip(1);
        return new ListZipper<T>(tail, head.Concat(breadcrumbs));
    }

    public ListZipper<T>? GoBack()
    {
        var head = breadcrumbs.Take(1);
        if (!head.Any())
            return null;

        var tail = breadcrumbs.Skip(1);
        return new ListZipper<T>(head.Concat(values), tail);
    }

    public override bool Equals(object? obj)
    {
        if (obj is ListZipper<T> other)
            return values.SequenceEqual(other.values)
                && breadcrumbs.SequenceEqual(other.breadcrumbs);

        return base.Equals(obj);
    }

    public override string ToString()
    {
        return $"LZ ([{string.Join(", ", values)}], [{string.Join(", ", breadcrumbs)})]";
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(values, breadcrumbs);
    }
}