
namespace Ploeh.Samples.Zippers;

internal class ListZipper<T>
{
    private int[] values;
    private int[] breadcrumbs;

    public ListZipper(int[] ints1, int[] ints2)
    {
        this.values = ints1;
        this.breadcrumbs = ints2;
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