namespace GenericDeserialization
{
    public interface ICallback1<TReturn>
    {
        TReturn Invoke<T1>();
    }

    public interface ICallback2<TReturn>
    {
        TReturn Invoke<T1, T2>();
    }

    public interface ICallback3<TReturn>
    {
        TReturn Invoke<T1, T2, T3>();
    }

    abstract class TypeArguments1
    {
        public static TypeArguments1 From<T>() => new Of<T>();
        public abstract TypeArguments2 Add(TypeArguments1 typeArgument);
        public abstract TReturn Invoke<TReturn>(ICallback1<TReturn> callback);

        public sealed class Of<T1> : TypeArguments1, ICallback1<TypeArguments2>
        {
            public override TypeArguments2 Add(TypeArguments1 typeArgument) => typeArgument.Invoke(this);
            public override TReturn Invoke<TReturn>(ICallback1<TReturn> callback) => callback.Invoke<T1>();

            public TypeArguments2 Invoke<T2>() => new TypeArguments2.Of<T1, T2>();
        }
    }

    abstract class TypeArguments2
    {
        public abstract TypeArguments3 Add(TypeArguments1 typeArgument);
        public abstract TReturn Invoke<TReturn>(ICallback2<TReturn> callback);

        public sealed class Of<T1, T2> : TypeArguments2, ICallback1<TypeArguments3>
        {
            public override TypeArguments3 Add(TypeArguments1 typeArgument) => typeArgument.Invoke(this);
            public override TReturn Invoke<TReturn>(ICallback2<TReturn> callback) => callback.Invoke<T1, T2>();

            TypeArguments3 ICallback1<TypeArguments3>.Invoke<T3>() => new TypeArguments3.Of<T1, T2, T3>();
        }
    }

    abstract class TypeArguments3
    {
        public abstract TReturn Invoke<TReturn>(ICallback3<TReturn> callback);

        public sealed class Of<T1, T2, T3> : TypeArguments3
        {
            public override TReturn Invoke<TReturn>(ICallback3<TReturn> callback) => callback.Invoke<T1, T2, T3>();
        }
    }
}
