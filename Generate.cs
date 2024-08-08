namespace Ploeh.Samples.Zippers;

public static class Generate
{
    public static IEnumerable<T> Cycle<T>(this IEnumerable<T> source)
    {
        while (true)
        {
            foreach (var item in source)
                yield return item;
        }
    }
}
