namespace Ploeh.Samples.Zippers;

public sealed class BinaryTreeZipper<T>
{
    public BinaryTree<T> Tree { get; }
    public IEnumerable<Crumb<T>> Breadcrumbs { get; }

    public BinaryTreeZipper(
        BinaryTree<T> tree,
        IEnumerable<Crumb<T>> breadcrumbs)
    {
        Tree = tree;
        Breadcrumbs = breadcrumbs;
    }

    public BinaryTreeZipper<T>? GoLeft()
    {
        return Tree.Match<BinaryTreeZipper<T>?>(
            whenEmpty: () => null,
            whenNode: (x, l, r) => new BinaryTreeZipper<T>(
                l,
                Breadcrumbs.Prepend(Crumb.Left(x, r))));
    }

    public BinaryTreeZipper<T>? GoRight()
    {
        return Tree.Match<BinaryTreeZipper<T>?>(
            whenEmpty: () => null,
            whenNode: (x, l, r) => new BinaryTreeZipper<T>(
                r,
                Breadcrumbs.Prepend(Crumb.Right(x, l))));
    }

    public override bool Equals(object? obj)
    {
        if (obj is BinaryTreeZipper<T> other)
            return Tree.Equals(other.Tree)
                && Breadcrumbs.SequenceEqual(other.Breadcrumbs);

        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Tree, Breadcrumbs);
    }
}
