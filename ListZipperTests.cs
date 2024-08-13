namespace Ploeh.Samples.Zippers;

public sealed class ListZipperTests
{
    [Fact]
    public void GoForward1()
    {
        var sut = new ListZipper<int>(1, 2, 3, 4);

        var actual = sut.GoForward();

        Assert.Equal([2, 3, 4], actual);
        Assert.Equal([1], actual?.Breadcrumbs);
    }

    [Fact]
    public void GoForward2()
    {
        var sut = new ListZipper<int>(1, 2, 3, 4);

        var actual = sut.GoForward()?.GoForward();

        Assert.Equal([3, 4], actual);
        Assert.Equal([2, 1], actual?.Breadcrumbs);
    }

    [Fact]
    public void GoForward3()
    {
        var sut = new ListZipper<int>(1, 2, 3, 4);

        var actual = sut.GoForward()?.GoForward()?.GoForward();

        Assert.Equal([4], actual);
        Assert.Equal([3, 2, 1], actual?.Breadcrumbs);
    }

    [Fact]
    public void GoForwardEmpty()
    {
        var sut = new ListZipper<string>("foo", "bar", "baz");
        var actual = sut.GoForward()?.GoForward()?.GoForward()?.GoForward();
        Assert.Null(actual);
    }

    [Fact]
    public void GoBack1()
    {
        var sut = new ListZipper<int>(1, 2, 3, 4);

        var actual = sut.GoForward()?.GoForward()?.GoForward()?.GoBack();

        Assert.Equal([3, 4], actual);
        Assert.Equal([2, 1], actual?.Breadcrumbs);
    }

    [Fact]
    public void GoBack2()
    {
        var sut = new ListZipper<int>(1, 2, 3, 4);

        var actual = sut.GoForward()?.GoForward()?.GoBack();

        Assert.Equal([2, 3, 4], actual);
        Assert.Equal([1], actual?.Breadcrumbs);
    }

    [Fact]
    public void GoBackEmptyOnBreadcrumbs()
    {
        var sut = new ListZipper<string>("foo", "bar", "baz");
        var actual = sut.GoBack();
        Assert.Null(actual);
    }

    [Fact]
    public void GoForwardOnInfiniteSequence()
    {
        var sut = new ListZipper<string>(Generate.Cycle(["foo", "bar"]));
        var actual = sut.GoForward();
        Assert.Equal(["bar", "foo", "bar"], actual?.Take(3));
    }

    [Fact]
    public void GoForwardAndBackOnInfiniteSequence()
    {
        var sut = new ListZipper<string>(Generate.Cycle(["foo", "bar"]));
        var actual = sut.GoForward()?.GoBack();
        Assert.Equal(["foo", "bar", "foo"], actual?.Take(3));
    }

    [Fact]
    public void InsertAtFocus()
    {
        var sut = new ListZipper<string>("foo", "bar");

        var actual = sut.GoForward()?.Insert("ploeh").GoBack();

        Assert.NotNull(actual);
        Assert.Equal(["foo", "ploeh", "bar"], actual);
        Assert.Empty(actual.Breadcrumbs);
    }

    [Fact]
    public void RemoveAtFocus()
    {
        var sut = new ListZipper<string>("foo", "bar", "baz");

        var actual = sut.GoForward()?.Remove()?.GoBack();

        Assert.NotNull(actual);
        Assert.Equal(["foo", "baz"], actual);
        Assert.Empty(actual.Breadcrumbs);
    }

    [Fact]
    public void RemoveWhenEmpty()
    {
        var sut = new ListZipper<string>();
        var actual = sut.Remove();
        Assert.Null(actual);
    }

    [Fact]
    public void RemoveAtEnd()
    {
        var sut = new ListZipper<string>("foo", "bar").GoForward()?.GoForward();

        var actual = sut?.Remove();

        Assert.Null(actual);
        Assert.NotNull(sut);
        Assert.Empty(sut);
        Assert.Equal(["bar", "foo"], sut.Breadcrumbs);
    }

    [Fact]
    public void ReplaceAtFocus()
    {
        var sut = new ListZipper<string>("foo", "bar", "baz");

        var actual = sut.GoForward()?.Replace("qux")?.GoBack();

        Assert.NotNull(actual);
        Assert.Equal(["foo", "qux", "baz"], actual);
        Assert.Empty(actual.Breadcrumbs);
    }

    [Fact]
    public void ReplaceWhenEmpty()
    {
        var sut = new ListZipper<string>();
        var actual = sut.Replace("qux");
        Assert.Null(actual);
    }
}
