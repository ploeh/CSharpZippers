namespace Ploeh.Samples.Zippers;

public sealed class FSZipper
{
    public FSZipper(FSItem fSItem, IReadOnlyCollection<FSCrumb> breadcrumbs)
    {
        FSItem = fSItem;
        Breadcrumbs = breadcrumbs;
    }

    public FSItem FSItem { get; }
    public IReadOnlyCollection<FSCrumb> Breadcrumbs { get; }

    public FSZipper? GoUp()
    {
        if (Breadcrumbs.Count == 0)
            return null;

        var head = Breadcrumbs.First();
        var tail = Breadcrumbs.Skip(1);

        return new FSZipper(
            FSItem.CreateFolder(head.Name, [.. head.Left, FSItem, .. head.Right]),
            tail.ToList());
    }

    public override bool Equals(object? obj)
    {
        return obj is FSZipper zipper &&
               FSItem.Equals(zipper.FSItem) &&
               Breadcrumbs.SequenceEqual(zipper.Breadcrumbs);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FSItem, Breadcrumbs);
    }
}
