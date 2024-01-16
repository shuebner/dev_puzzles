using System.Collections;

namespace Puzzle;

sealed class Consumer
{
    public static void WriteSomeValues(IDatabase database)
    {
        var values = new object[]
        {
            1,
            true,
            "foo"
        };

        database.WriteAll(values);
    }

    public static void WriteSomeOtherValues(IDatabase database, long value)
    {
        var values = new object[]
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