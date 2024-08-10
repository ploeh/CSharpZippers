namespace Ploeh.Samples.Zippers;

public sealed class BinaryTree<T>
{
    private readonly IBinaryTree root;

    private BinaryTree(IBinaryTree root)
    {
        this.root = root;
    }

    public BinaryTree() : this(Empty.Instance)
    {
    }

    public BinaryTree(T value, BinaryTree<T> left, BinaryTree<T> right)
        : this(new Node(value, left.root, right.root))
    {
    }

    public TResult Aggregate<TResult>(
        Func<TResult> whenEmpty,
        Func<T, TResult, TResult, TResult> whenNode)
    {
        return root.Aggregate(whenEmpty, whenNode);
    }

    /// <summary>
    /// Church-encoded pattern matching of a <see cref="BinaryTree{T}"/>
    /// instance.
    /// </summary>
    /// <typeparam name="TResult">The type of the return value.</typeparam>
    /// <param name="whenEmpty">
    /// A function to run when the tree is empty.
    /// </param>
    /// <param name="whenNode">
    /// A function to run when the tree is a node.
    /// </param>
    /// <returns>
    /// A value produced by either <paramref name="whenEmpty"/> or
    /// <paramref name="whenNode"/>.
    /// </returns>
    public TResult Match<TResult>(
        Func<TResult> whenEmpty,
        Func<T, BinaryTree<T>, BinaryTree<T>, TResult> whenNode)
    {
        // This may strike code readers as a roundabout way to Church-encode the
        // data structure, but it serves to demonstrate the conjecture that the
        // catamorphism (the Aggregate method) is the 'universal API' for a type
        // of this kind.
        return root
            .Aggregate(
                () => (tree: new BinaryTree<T>(), result: whenEmpty()),
                (x, l, r) => (
                    new BinaryTree<T>(x, l.tree, r.tree),
                    whenNode(x, l.tree, r.tree)))
            .result;
    }

    private interface IBinaryTree
    {
        TResult Aggregate<TResult>(
            Func<TResult> whenEmpty,
            Func<T, TResult, TResult, TResult> whenNode);
    }

    private sealed class Empty : IBinaryTree
    {
        public readonly static Empty Instance = new();

        private Empty()
        {
        }

        public TResult Aggregate<TResult>(
            Func<TResult> whenEmpty,
            Func<T, TResult, TResult, TResult> whenNode)
        {
            return whenEmpty();
        }
    }

    private sealed record Node(T Value, IBinaryTree Left, IBinaryTree Right)
        : IBinaryTree
    {
        public TResult Aggregate<TResult>(
            Func<TResult> whenEmpty,
            Func<T, TResult, TResult, TResult> whenNode)
        {
            return whenNode(
                Value,
                Left.Aggregate(whenEmpty, whenNode),
                Right.Aggregate(whenEmpty, whenNode));
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is BinaryTree<T> tree &&
               EqualityComparer<IBinaryTree>.Default.Equals(root, tree.root);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(root);
    }
}
