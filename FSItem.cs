namespace Ploeh.Samples.Zippers;

public sealed class FSItem
{
    private readonly IFSItem imp;

    private FSItem(IFSItem imp)
    {
        this.imp = imp;
    }

    public static FSItem CreateFile(string name, string data)
    {
        return new(new File(name, data));
    }

    public static FSItem CreateFolder(
        string name,
        IReadOnlyCollection<FSItem> items)
    {
        return new(new Folder(name, items));
    }

    public TResult Aggregate<TResult>(
        Func<string, string, TResult> whenFile,
        Func<string, IReadOnlyCollection<TResult>, TResult> whenFolder)
    {
        return imp.Aggregate(whenFile, whenFolder);
    }

    public TResult Match<TResult>(
        Func<string, string, TResult> whenFile,
        Func<string, IReadOnlyCollection<FSItem>, TResult> whenFolder)
    {
        var foo = imp.Aggregate(
            (name, data) => (FSItem.CreateFile(name, data), whenFile(name, data)),
            (name, items) => (
                FSItem.CreateFolder(name, items.Select(i => i.Item1).ToList()),
                whenFolder(name, items.Select(i => i.Item1).ToList())));
        return foo.Item2;
    }

    private interface IFSItem
    {
        TResult Aggregate<TResult>(
            Func<string, string, TResult> whenFile,
            Func<string, IReadOnlyCollection<TResult>, TResult> whenFolder);
    }

    private sealed record File(string Name, string Data) : IFSItem
    {
        public TResult Aggregate<TResult>(
            Func<string, string, TResult> whenFile,
            Func<string, IReadOnlyCollection<TResult>, TResult> whenFolder)
        {
            return whenFile(Name, Data);
        }
    }

    private sealed class Folder : IFSItem
    {
        private readonly string name;
        private readonly IReadOnlyCollection<FSItem> items;

        public Folder(string Name, IReadOnlyCollection<FSItem> Items)
        {
            name = Name;
            items = Items;
        }

        public TResult Aggregate<TResult>(
            Func<string, string, TResult> whenFile,
            Func<string, IReadOnlyCollection<TResult>, TResult> whenFolder)
        {
            return whenFolder(
                name,
                items.Select(i => i.Aggregate(whenFile, whenFolder)).ToList());
        }

        public override bool Equals(object? obj)
        {
            return obj is Folder folder &&
                   name == folder.name &&
                   items.SequenceEqual(folder.items);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, items);
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is FSItem item &&
               EqualityComparer<IFSItem>.Default.Equals(imp, item.imp);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(imp);
    }
}
