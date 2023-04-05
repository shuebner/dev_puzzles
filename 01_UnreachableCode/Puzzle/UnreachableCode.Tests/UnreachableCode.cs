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
        // insert code below here to make the test pass
    }
}