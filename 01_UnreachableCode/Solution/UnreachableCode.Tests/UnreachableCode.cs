namespace UnreachableCode.Tests;

public class UnreachableCode
{
    [Fact]
    public void GetNumbers_ShouldNotThrow()
    {
        Exception exception = Record.Exception(() => GetNumbers());

        Assert.Null(exception);
    }

    static IEnumerable<int> GetNumbers()
    {
        throw new NotImplementedException();
        yield break;
    }

    [Fact]
    public void GetNumbers_and_iteration_ShouldThrow()
    {
        IEnumerable<int> numbers = GetNumbers();
        IEnumerator<int> enumerator = numbers.GetEnumerator();

        Exception exception = Record.Exception(() => enumerator.MoveNext());

        Assert.IsType<NotImplementedException>(exception);
    }
}