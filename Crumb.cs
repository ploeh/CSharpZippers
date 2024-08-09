namespace Ploeh.Samples.Zippers;

public sealed class Crumb<T>
{
    private readonly ICrumb imp;

    private Crumb(ICrumb imp)
    {
        this.imp = imp;
    }

    internal static Crumb<T> Left(T value, BinaryTree<T> right)
    {
        return new(new LeftCrumb(value, right));
    }

    internal static Crumb<T> Right(T value, BinaryTree<T> left)
    {
        return new(new RightCrumb(value, left));
    }

    public TResult Match<TResult>(
        Func<T, BinaryTree<T>, TResult> whenLeft,
        Func<T, BinaryTree<T>, TResult> whenRight)
    {
        return imp.Match(whenLeft, whenRight);
    }

    private interface ICrumb
    {
        TResult Match<TResult>(
            Func<T, BinaryTree<T>, TResult> whenLeft,
            Func<T, BinaryTree<T>, TResult> whenRight);
    }

    private sealed record LeftCrumb(T Value, BinaryTree<T> Right) : ICrumb
    {
        public TResult Match<TResult>(
            Func<T, BinaryTree<T>, TResult> whenLeft,
            Func<T, BinaryTree<T>, TResult> whenRight)
        {
            return whenLeft(Value, Right);
        }
    }

    private sealed record RightCrumb(T Value, BinaryTree<T> Left) : ICrumb
    {
        public TResult Match<TResult>(
            Func<T, BinaryTree<T>, TResult> whenLeft,
            Func<T, BinaryTree<T>, TResult> whenRight)
        {
            return whenRight(Value, Left);
        }
    }
}

public static class Crumb
{
    public static Crumb<T> Left<T>(T value, BinaryTree<T> right)
    {
        return Crumb<T>.Left(value, right);
    }

    public static Crumb<T> Right<T>(T value, BinaryTree<T> left)
    {
        return Crumb<T>.Right(value, left);
    }
}