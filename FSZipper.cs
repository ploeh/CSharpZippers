namespace Ploeh.Samples.Zippers;

public sealed class FSZipper
{
    private FSZipper(FSItem fSItem, IReadOnlyCollection<FSCrumb> breadcrumbs)
    {
        FSItem = fSItem;
        Breadcrumbs = breadcrumbs;
    }

    public FSZipper(FSItem fSItem) : this(fSItem, [])
    {
    }

    public FSItem FSItem { get; }
    public IReadOnlyCollection<FSCrumb> Breadcrumbs { get; }

    public FSZipper? GoTo(string name)
    {
        return FSItem.Match(
            (_, _) => null,
            (folderName, items) =>
            {
                FSItem? item = null;
                var ls = new List<FSItem>();
                var rs = new List<FSItem>();
                foreach (var i in items)
                {
                    if (item is null && i.Match((n, _) => n == name, (n, _) => n == name))
                        item = i;
                    else if (item is null)
                        ls.Add(i);
                    else
                        rs.Add(i);
                }

                if (item is null)
                    return null;

                return new FSZipper(item, Breadcrumbs.Prepend(new FSCrumb(folderName, ls, rs)).ToList());
            });
    }

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
