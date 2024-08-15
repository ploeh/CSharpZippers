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
        var sut = new BinaryTreeZipper<char>(freeTree);

        var actual = sut.GoLeft();

        Assert.NotNull(actual);
        Assert.Equal(
            new('O',
                new('L',
                    new('N', new(), new()),
                    new('T', new(), new())),
                new('Y',
                    new('S', new(), new()),
                    new('A', new(), new()))),
            actual.Tree);
        Assert.Equal(
            [Crumb.Left('P', new('L',
                new('W',
                    new('C', new(), new()),
                    new('R', new(), new())),
                new('A',
                    new('A', new(), new()),
                    new('C', new(), new()))))],
            actual.Breadcrumbs);
    }

    [Fact]
    public void GoRightOnFreeTree()
    {
        var sut = new BinaryTreeZipper<char>(freeTree);

        var actual = sut.GoRight();

        Assert.NotNull(actual);
        Assert.Equal(
            new('L',
                new('W',
                    new('C', new(), new()),
                    new('R', new(), new())),
                new('A',
                    new('A', new(), new()),
                    new('C', new(), new()))),
            actual.Tree);
        Assert.Equal(
            [Crumb.Right('P', new('O',
                new('L',
                    new('N', new(), new()),
                    new('T', new(), new())),
                new('Y',
                    new('S', new(), new()),
                    new('A', new(), new()))))],
            actual.Breadcrumbs);
    }

    [Fact]
    public void ReplaceYWithP()
    {
        var sut = new BinaryTreeZipper<char>(freeTree);

        var actual = sut.GoLeft()?.GoRight()?.Modify(_ => 'P');

        Assert.NotNull(actual);
        Assert.Equal(
            new('P',
                new('S', new(), new()),
                new('A', new(), new())),
            actual.Tree);
        Assert.Equal(
            [
                Crumb.Right(
                    'O',
                    new('L',
                        new('N', new(), new()),
                        new('T', new(), new()))),
                Crumb.Left(
                    'P',
                    new('L',
                        new('W',
                            new('C', new(), new()),
                            new('R', new(), new())),
                        new('A',
                            new('A', new(), new()),
                            new('C', new(), new()))))
            ],
            actual.Breadcrumbs);
    }

    // Reprodces the example from
    // https://learnyouahaskell.com/zippers
    [Fact]
    public void ExtendedArticleExample()
    {
        var sut = new BinaryTreeZipper<char>(freeTree);

        var actual =
            sut.GoLeft()?.GoRight()?.Modify(_ => 'P').GoUp()?.Modify(_ => 'X');

        Assert.NotNull(actual);
        Assert.Equal(
            new('X',
                new('L',
                        new('N', new(), new()),
                        new('T', new(), new())),
                new('P',
                        new('S', new(), new()),
                        new('A', new(), new()))),
            actual.Tree);
        Assert.Equal(
            [
                Crumb.Left(
                    'P',
                    new('L',
                        new('W',
                            new('C', new(), new()),
                            new('R', new(), new())),
                        new('A',
                            new('A', new(), new()),
                            new('C', new(), new()))))
            ],
            actual.Breadcrumbs);
    }

    [Fact]
    public void AttachExample()
    {
        var sut = new BinaryTreeZipper<char>(freeTree);

        var farLeft = sut.GoLeft()?.GoLeft()?.GoLeft()?.GoLeft();
        var actual = farLeft?.Attach(new('Z', new(), new()));

        Assert.NotNull(actual);
        Assert.Equal(new('Z', new(), new()), actual.Tree);
        Assert.Equal(
            [
                Crumb.Left('N', new()),
                Crumb.Left('L', new('T', new(), new())),
                Crumb.Left(
                    'O',
                    new('Y',
                        new('S', new(), new()),
                        new('A', new(), new()))),
                Crumb.Left(
                    'P',
                    new('L',
                        new('W',
                            new('C', new(), new()),
                            new('R', new(), new())),
                        new('A',
                            new('A', new(), new()),
                            new('C', new(), new()))))
            ],
            actual.Breadcrumbs);
    }

    [Fact]
    public void AttachAndGoTopMost()
    {
        var sut = new BinaryTreeZipper<char>(freeTree);

        // Same scenario as AttachExample, but with TopMost at the end.
        var farLeft = sut.GoLeft()?.GoLeft()?.GoLeft()?.GoLeft();
        var actual = farLeft?.Attach(new('Z', new(), new())).TopMost();

        Assert.NotNull(actual);
        Assert.Equal(
            new('P',
                new('O',
                    new('L',
                        new('N',
                            new('Z', new(), new()),
                            new()),
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
                        new('C', new(), new())))),
            actual.Tree);
        Assert.Empty(actual.Breadcrumbs);
    }
}
