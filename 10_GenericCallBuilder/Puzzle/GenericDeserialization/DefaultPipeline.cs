namespace GenericDeserialization
{
    abstract class Pipeline
    {
        public abstract void Run();
    }

    sealed class DefaultPipeline<TSource, T, TDest> : Pipeline
    {
        private readonly ISource<TSource> source;
        private readonly IMapper<TSource, T> importer;
        private readonly IManipulator<T> manipulator;
        private readonly IMapper<T, TDest> exporter;
        private readonly ISink<TDest> sink;

        public DefaultPipeline(
            ISource<TSource> source,
            IMapper<TSource, T> importer,
            IManipulator<T> manipulator,
            IMapper<T, TDest> exporter,
            ISink<TDest> sink)
        {
            this.source = source;
            this.importer = importer;
            this.manipulator = manipulator;
            this.exporter = exporter;
            this.sink = sink;
        }

        public override void Run()
        {
            foreach (var sourceItem in source.Get())
            {
                var imported = importer.Map(sourceItem);
                var manipulated = manipulator.Invoke(imported);
                var export = exporter.Map(manipulated);
                sink.Push(export);
            }
        }
    }

    // The following are just stubs of generic calls to have an API surface that the serializer can call.
    // We are only interested in the compile-time safety and the type that is constructed by the serializer at runtime.
    // Thus, implementations do not matter and are omitted.

    static class Sources
    {
        public static ISource<T> Create<T>() => null!;
    }
    
    static class Sinks
    {
        public static ISink<T> Create<T>() => null!;
    }

    static class Mappers
    {
        public static IMapper<TSource, TDest> Create<TSource, TDest>() => null!;
    }

    static class Manipulators
    {
        public static IManipulator<T> Create<T>() => null!;
    }

    interface ISource<T>
    {
        IEnumerable<T> Get();
    }

    interface IMapper<TSource, TDest>
    {
        public TDest Map(TSource source);
    }

    interface IManipulator<T>
    {
        public T Invoke(T subject);
    }

    interface ISink<T>
    {
        void Push(T value);
    }
}
