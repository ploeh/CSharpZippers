namespace Ploeh.Samples.Zippers;

public sealed class FSCrumb
{
    public FSCrumb(
        string name,
        IReadOnlyCollection<FSItem> left,
        IReadOnlyCollection<FSItem> right)
    {
        Name = name;
        Left = left;
        Right = right;
    }

    public string Name { get; }
    public IReadOnlyCollection<FSItem> Left { get; }
    public IReadOnlyCollection<FSItem> Right { get; }

    public override bool Equals(object? obj)
    {
        return obj is FSCrumb crumb &&
               Name == crumb.Name &&
               Left.SequenceEqual(crumb.Left) &&
               Right.SequenceEqual(crumb.Right);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Left, Right);
    }
}
