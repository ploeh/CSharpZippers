namespace Ploeh.Samples.Zippers;

public sealed class NodeTests
{
    [Fact]
    public void Example()
    {
        var node1 = new Node<string>("foo");
        var node2 = new Node<string>("bar") { Previous = node1 };
        node1.Next = node2;

        // Not much of an assertion, but I only add this test as a sanity check
        // that I've implemented the Node class correctly.
        Assert.Equal("bar", node1.Next.Value);
        Assert.Equal("foo", node1.Next.Previous.Value);
    }
}
