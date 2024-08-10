namespace Ploeh.Samples.Zippers;

public sealed class BinaryTreeTests
{
    [Fact]
    public void InOrderTraversalExample()
    {
        BinaryTree<int> sut = new(
            42,
            new(1337, new(), new()),
            new(2112, new(), new()));
        var actual = sut.Aggregate(
            whenEmpty: Array.Empty<int>,
            whenNode: (value, left, right) => [.. left, value, .. right]);
        Assert.Equal([1337, 42, 2112], actual);
    }

    // Just a sanity check that Match works.
    [Fact]
    public void UseMatchToDetectEmptyLeft()
    {
        BinaryTree<int> sut = new(
            42,
            new(),
            new(2, new(), new()));
        var actual = sut.Match(
            whenEmpty: () => false,
            whenNode: (_, l, _) => l.Match(() => true, (_, _, _) => false));
        Assert.True(actual);
    }
}
