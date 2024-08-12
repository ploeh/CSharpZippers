
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
