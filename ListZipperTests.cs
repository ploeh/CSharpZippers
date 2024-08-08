namespace Ploeh.Samples.Zippers;

public sealed class ListZipperTests
{
    [Fact]
    public void GoForward1()
    {
        var sut = new ListZipper<int>([1, 2, 3, 4], []);
        var actual = sut.GoForward();
        Assert.Equal(new ListZipper<int>([2, 3, 4], [1]), actual);
    }

    [Fact]
    public void GoForward2()
    {
        var sut = new ListZipper<int>([2, 3, 4], [1]);
        var actual = sut.GoForward();
        Assert.Equal(new ListZipper<int>([3, 4], [2, 1]), actual);
    }

    [Fact]
    public void GoForward3()
    {
        var sut = new ListZipper<int>([3, 4], [2, 1]);
        var actual = sut.GoForward();
        Assert.Equal(new ListZipper<int>([4], [3, 2, 1]), actual);
    }

    [Fact]
    public void GoForwardEmpty()
    {
        var sut = new ListZipper<string>([], ["foo", "bar", "baz"]);
        var actual = sut.GoForward();
        Assert.Null(actual);
    }

    [Fact]
    public void GoBack1()
    {
        var sut = new ListZipper<int>([4], [3, 2, 1]);
        var actual = sut.GoBack();
        Assert.Equal(new ListZipper<int>([3, 4], [2, 1]), actual);
    }

    [Fact]
    public void GoBack2()
    {
        var sut = new ListZipper<int>([3, 4], [2, 1]);
        var actual = sut.GoBack();
        Assert.Equal(new ListZipper<int>([2, 3, 4], [1]), actual);
    }

    [Fact]
    public void GoBackEmptyOnBreadcrumbs()
    {
        var sut = new ListZipper<string>(["foo", "bar", "baz"], []);
        var actual = sut.GoBack();
        Assert.Null(actual);
    }

    [Fact]
    public void GoForwardOnInfiniteSequence()
    {
        var sut = new ListZipper<string>(Generate.Cycle(["foo", "bar"]), []);
        var actual = sut.GoForward();
        Assert.Equal(["bar", "foo", "bar"], actual?.Take(3));
    }

    [Fact]
    public void GoForwardAndBackOnInfiniteSequence()
    {
        var sut = new ListZipper<string>(Generate.Cycle(["foo", "bar"]), []);
        var actual = sut.GoForward()?.GoBack();
        Assert.Equal(["foo", "bar", "foo"], actual?.Take(3));
    }

    [Fact]
    public void InsertAtFocus()
    {
        var sut = new ListZipper<string>(["foo", "bar"], []);
        var actual = sut.GoForward()?.Insert("ploeh").GoBack();
        Assert.Equal(
            new ListZipper<string>(["foo", "ploeh", "bar"], []),
            actual);
    }
}
