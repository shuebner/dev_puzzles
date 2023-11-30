using System.Runtime.Serialization;

namespace GenericDeserialization
{
    sealed class PipelineDeserializer : ICallback3_3<Pipeline, ISerializable>
    {
        public Pipeline Deserialize(string sourceType, string tempType, string targetType)
        {
            var sourceTypeArgument = GetTypeArgument(sourceType);
            var tempTypeArgument = GetTypeArgument(tempType);
            var targetTypeArgument = GetSerializableTypeArgument(targetType);

            var typeArguments = sourceTypeArgument
                .Add(tempTypeArgument)
                .Add(targetTypeArgument);

            Pipeline pipeline = typeArguments.Invoke(this);
            return pipeline;
        }

        static TypeArguments1 GetTypeArgument(string typeStr) =>
            typeStr switch
            {
                "string" => TypeArguments1.From<string>(),
                "int" => TypeArguments1.From<int>(),
                "SomeType" => TypeArguments1.From<SomeComplexType>(),
                _ => throw new ArgumentException("unknown type", nameof(typeStr))
            };

        static TypeArguments1<ISerializable> GetSerializableTypeArgument(string typeStr) =>
            typeStr switch
            {
                "SomeType" => TypeArguments1<ISerializable>.From<SomeComplexType>(),
                _ => throw new ArgumentException("unknown type", nameof(typeStr))
            };

        public Pipeline Invoke<T1, T2, T3>() where T3 : ISerializable =>
            new DefaultPipeline<T1, T2, T3>(
                Sources.Create<T1>(),
                Mappers.Create<T1, T2>(),
                Manipulators.Create<T2>(),
                Mappers.Create<T2, T3>(),
                Sinks.Create<T3>());
    }
}