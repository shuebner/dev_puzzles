namespace NullabilityAttributes
{
    internal static class SomeService
    {
        public static SomeData GetWithResetValueOrDefault(string dataStr)
        {
            if (SomeDataConsumer.TryDeserialize(dataStr, out SomeData data))
            {
                data.Value = "how are you?";
            }

            return data;
        }
    }
}
