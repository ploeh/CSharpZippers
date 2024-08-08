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
}
