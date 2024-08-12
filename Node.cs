namespace Ploeh.Samples.Zippers;

// This class only exists to demonstrate how imperative code relies on state
// mutation to implement a doubly-linked list.
public sealed class Node<T>
{
    public Node(T value)
    {
        Value = value;
    }

    public T Value { get; }
    public Node<T>? Previous { get; set; }
    public Node<T>? Next { get; set; }
}
