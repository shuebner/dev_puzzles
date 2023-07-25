namespace TestProject1
{
    public abstract class Target
    {
    }

    public sealed class Target<T> : Target
    {
        public Target(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}