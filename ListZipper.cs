
namespace Ploeh.Samples.Zippers;

internal class ListZipper<T>
{
    private readonly IEnumerable<T> values;
    private IEnumerable<T> breadcrumbs;

    public ListZipper(T[] values, T[] breadcrumbs)
    {
        this.values = values;
        this.breadcrumbs = breadcrumbs;
    }

    internal ListZipper<int> GoForward()
    {
        return new ListZipper<int>(new int[] { 2, 3, 4 }, new int[] { 1 });
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