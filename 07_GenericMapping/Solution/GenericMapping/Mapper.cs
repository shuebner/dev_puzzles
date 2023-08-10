namespace GenericMapping
{
    sealed class Mapper : Source.IVisitor<Target>
    {
        public Target Map(Source source) => source.Accept(this);

        public Target Visit<T>(Source<T> source) =>
            new Target<T>(source.Value);
    }
}
