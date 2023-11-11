namespace NullabilityAttributes
{
    public class SomeData
    {
        private object? _value;

        public SomeData(string id)
        {
            if (id is null)
            {
                ThrowHelper.ArgumentNull(id);
            }

            Id = id;
        }

        public string Id { get; }

        public object? Value
        {
            get => _value;
            set => _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public DateTime TimeStamp { get; set; }
    }
}