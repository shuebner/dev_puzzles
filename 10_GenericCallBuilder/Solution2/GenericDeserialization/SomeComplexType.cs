using System.Runtime.Serialization;

namespace GenericDeserialization
{
    public sealed class SomeComplexType : ISerializable
    {
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}