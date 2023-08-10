namespace GenericMapping
{
    partial class Source
    {
        public abstract TReturn Accept<TReturn>(IVisitor<TReturn> visitor);

        public interface IVisitor<TReturn>
        {
            TReturn Visit<T>(Source<T> source);
        }
    }

    partial class Source<T>
    {
        public override TReturn Accept<TReturn>(IVisitor<TReturn> visitor) =>
            visitor.Visit(this);
    }
}
