
namespace Ploeh.Samples.Zippers;

internal class ListZipper<T>
{
    private int[] ints1;
    private int[] ints2;

    public ListZipper(int[] ints1, int[] ints2)
    {
        this.ints1 = ints1;
        this.ints2 = ints2;
    }

    internal ListZipper<int> GoForward()
    {
        return new ListZipper<int>(new int[] { 2, 3, 4 }, new int[] { 1 });
    }

    public override bool Equals(object? obj)
    {
        if (obj is ListZipper<T> other)
            return ints1.SequenceEqual(other.ints1)
                && ints2.SequenceEqual(other.ints2);

        return base.Equals(obj);
    }

    public override string ToString()
    {
        return $"LZ ([{string.Join(", ", ints1)}], [{string.Join(", ", ints2)})]";
    }
}