namespace GenericMapping
{
    public abstract class Source
    {
    }

    public sealed class Source<T> : Source
    {
        public Source(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}