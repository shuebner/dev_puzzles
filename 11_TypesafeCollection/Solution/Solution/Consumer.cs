using System.Collections;

namespace Puzzle;

sealed class Consumer
{
    public static void WriteSomeValues(IDatabase database)
    {
        var values = new ValueCollection
        {
            1,
            true,
            "foo"
        };

        database.WriteAll(values);
    }

    public static void WriteSomeOtherValues(IDatabase database, long value)
    {
        var values = new ValueCollection
        {
            // oops, somehow a long value ended up in our list.
            // This is a bug and will lead to a runtime exception.
            // Ideally, programming errors like this should be caught at compile-time.
            value
        };

        database.WriteAll(values);
    }
}

// This is a 3rd party interface.
// You may not change it.
interface IDatabase
{
    /// <summary>
    /// Writes all values.
    /// </summary>
    /// <param name="values">May only be int, bool or string</param>
    /// <exception cref="ArgumentException">When any value is not of type int, bool or string</exception>
    void WriteAll(object[] values);
}

sealed class ValueCollection : IEnumerable
{
    readonly List<object> _values = [];

    public void Add(int value)
    {
        _values.Add(value);
    }

    public void Add(string value)
    {
        _values.Add(value);
    }

    public void Add(bool value)
    {
        _values.Add(value);
    }

    public IEnumerator GetEnumerator()
    {
        return ((IEnumerable)_values).GetEnumerator();
    }

    public static implicit operator object[](ValueCollection collection) => collection._values.ToArray();
}