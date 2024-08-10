﻿namespace Ploeh.Samples.Zippers;

public sealed class BinaryTreeTests
{
    [Fact]
    public void InOrderTraversalExample()
    {
       var sut = new BinaryTree<int>(
            42,
            new BinaryTree<int>(1337, new BinaryTree<int>(), new BinaryTree<int>()),
            new BinaryTree<int>(2112, new BinaryTree<int>(), new BinaryTree<int>()));
        var actual = sut.Aggregate(
            whenEmpty: Array.Empty<int>,
            whenNode: (value, left, right) => [.. left, value, .. right]);
        Assert.Equal([1337, 42, 2112], actual);
    }

    // Just a sanity check that Match works.
    [Fact]
    public void UseMatchToDetectEmptyLeft()
    {
        var sut = new BinaryTree<int>(
            42,
            new BinaryTree<int>(),
            new BinaryTree<int>(2, new BinaryTree<int>(), new BinaryTree<int>()));
        var actual = sut.Match(
            whenEmpty: () => false,
            whenNode: (_, l, _) => l.Match(() => true, (_, _, _) => false));
        Assert.True(actual);
    }
}
