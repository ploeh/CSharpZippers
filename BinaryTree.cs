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

    TResult Aggregate<TResult>(
        Func<TResult> whenEmpty,
        Func<T, TResult, TResult, TResult> whenNode)
    {
        return root.Aggregate(whenEmpty, whenNode);
    }

    private interface IBinaryTree
    {
        TResult Aggregate<TResult>(
            Func<TResult> whenEmpty,
            Func<T, TResult, TResult, TResult> whenNode);
    }

    private sealed class Empty : IBinaryTree
    {
        public readonly static Empty Instance = new Empty();

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
}
