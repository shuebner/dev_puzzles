namespace GenericMapping
{
    public abstract partial class Source
    {
    }

    public sealed partial class Source<T> : Source
    {
        public Source(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}