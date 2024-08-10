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

    public BinaryTreeZipper<T>? GoUp()
    {
        if (!Breadcrumbs.Any())
            return null;
        var head = Breadcrumbs.First();

        var tail = Breadcrumbs.Skip(1);
        return head.Match(
            whenLeft: (x, r) => new BinaryTreeZipper<T>(
                new BinaryTree<T>(x, Tree, r),
                tail),
            whenRight: (x, l) => new BinaryTreeZipper<T>(
                new BinaryTree<T>(x, l, Tree),
                tail));
    }

    public BinaryTreeZipper<T> Modify(Func<T, T> f)
    {
        return new BinaryTreeZipper<T>(
            Tree.Match(
                whenEmpty: () => new BinaryTree<T>(),
                whenNode: (x, l, r) => new BinaryTree<T>(f(x), l, r)),
            Breadcrumbs);
    }

    public BinaryTreeZipper<T> Attach(BinaryTree<T> tree)
    {
        return new BinaryTreeZipper<T>(tree, Breadcrumbs);
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
