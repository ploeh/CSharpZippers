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
}
