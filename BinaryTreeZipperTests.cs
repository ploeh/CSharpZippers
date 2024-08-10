namespace Ploeh.Samples.Zippers;

public sealed class BinaryTreeZipperTests
{
    private static readonly BinaryTree<char> freeTree =
        new('P',
            new('O',
                new('L',
                    new('N', new(), new()),
                    new('T', new(), new())),
                new('Y',
                    new('S', new(), new()),
                    new('A', new(), new()))),
            new('L',
                new('W',
                    new('C', new(), new()),
                    new('R', new(), new())),
                new('A',
                    new('A', new(), new()),
                    new('C', new(), new()))));

    [Fact]
    public void GoLeftOnFreeTree()
    {
        var sut = new BinaryTreeZipper<char>(freeTree, []);

        var actual = sut.GoLeft();

        Assert.NotNull(actual);
        Assert.Equal(new BinaryTreeZipper<char>(
            new('O',
                new('L',
                    new('N', new(), new()),
                    new('T', new(), new())),
                new('Y',
                    new('S', new(), new()),
                    new('A', new(), new()))),
            [Crumb.Left('P', new('L',
                new('W',
                    new('C', new(), new()),
                    new('R', new(), new())),
                new('A',
                    new('A', new(), new()),
                    new('C', new(), new()))))]), actual);
    }
}
