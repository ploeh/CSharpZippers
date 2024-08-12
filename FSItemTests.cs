namespace Ploeh.Samples.Zippers;

public sealed class FSItemTests
{
    [Fact]
    public void CountFiles()
    {
        var sut = FSItem.CreateFolder("root",
        [
            FSItem.CreateFile("foo.txt", "Hello, world!"),
            FSItem.CreateFolder("bar",
            [
                FSItem.CreateFile("baz.txt", "Goodbye, world!"),
                FSItem.CreateFile("qux.txt", "Farewell, world!")
            ])
        ]);
        var actual = sut.Aggregate(
            whenFile: (_, _) => 1,
            whenFolder: (_, counts) => counts.Sum());
        Assert.Equal(3, actual);
    }
}
