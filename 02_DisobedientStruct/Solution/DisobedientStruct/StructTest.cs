namespace DisobedientStruct;

public class StructTest
{
    /*
     * Removing the 'readonly' modifier is one way of making the test pass.
     */
    Wrapper _wrapper = new Wrapper
    {
        Value = 1
    };

    [Fact]
    public void Increment_should_increment()
    {
        _wrapper.IncrementValue();

        Assert.Equal(
            expected: 2,
            _wrapper.Value);
    }
}

struct Wrapper
{
    public int Value { get; set; }

    public void IncrementValue()
    {
        Value++;
    }
}