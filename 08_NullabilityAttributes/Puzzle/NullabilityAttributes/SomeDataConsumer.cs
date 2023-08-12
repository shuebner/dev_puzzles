namespace NullabilityAttributes
{
    public class SomeDataConsumer
    {
        public static string Serialize(SomeData data) =>
            (data.Id ?? "<none>") + data.Value.ToString();

        public static bool TryDeserialize(string dataStr, out SomeData data)
        {
            var segments = dataStr.Split('/');
            if (segments.Length is 1)
            {
                data = new(segments[0]);
                return true;
            }

            if (segments.Length is 2)
            {
                data = new(segments[0]) { Value = segments[1] };
                return true;
            }

            data = null;
            return false;
        }
    }
}
