using System.Data;

namespace GenericDeserialization
{
    public interface ICallback1<TReturn>
    {
        TReturn Invoke<T1>();
    }
    public interface ICallback1<TReturn, TConstraint>
    {
        TReturn Invoke<T1>() where T1 : TConstraint;
    }

    public interface ICallback2<TReturn>
    {
        TReturn Invoke<T1, T2>();
    }

    public interface ICallback3<TReturn>
    {
        TReturn Invoke<T1, T2, T3>();
    }

    public interface ICallback3_3<TReturn, T3Constraint>
    {
        TReturn Invoke<T1, T2, T3>() where T3 : T3Constraint;
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

    abstract class TypeArguments1<TConstraint>
    {
        public static TypeArguments1<TConstraint> From<T>() where T : TConstraint => new Of<T>();
        public abstract TReturn Invoke<TReturn>(ICallback1<TReturn, TConstraint> callback);

        public sealed class Of<T1> : TypeArguments1<TConstraint>, ICallback1<TypeArguments2> where T1 : TConstraint
        {
            public override TReturn Invoke<TReturn>(ICallback1<TReturn, TConstraint> callback) => callback.Invoke<T1>();

            public TypeArguments2 Invoke<T2>() => new TypeArguments2.Of<T1, T2>();
        }
    }

    abstract class TypeArguments2
    {
        public abstract TypeArguments3 Add(TypeArguments1 typeArgument);
        public abstract TypeArguments3<TConstraint> Add<TConstraint>(TypeArguments1<TConstraint> typeArgument);
        public abstract TReturn Invoke<TReturn>(ICallback2<TReturn> callback);

        public sealed class Of<T1, T2> : TypeArguments2, ICallback1<TypeArguments3>
        {
            public override TypeArguments3 Add(TypeArguments1 typeArgument) => typeArgument.Invoke(this);
            public override TypeArguments3<TConstraint> Add<TConstraint>(TypeArguments1<TConstraint> typeArgument) => typeArgument.Invoke(
                new Callback<T1, T2, TConstraint>());
            public override TReturn Invoke<TReturn>(ICallback2<TReturn> callback) => callback.Invoke<T1, T2>();

            TypeArguments3 ICallback1<TypeArguments3>.Invoke<T3>() => new TypeArguments3.Of<T1, T2, T3>();
        }
    }
    sealed class Callback<T11, T12, TConstraint> : ICallback1<TypeArguments3<TConstraint>, TConstraint>
    {
        public TypeArguments3<TConstraint> Invoke<T13>() where T13 : TConstraint => new TypeArguments3<TConstraint>.Of<T11, T12, T13>();
    }

    abstract class TypeArguments3
    {
        public abstract TReturn Invoke<TReturn>(ICallback3<TReturn> callback);

        public class Of<T1, T2, T3> : TypeArguments3
        {
            public override TReturn Invoke<TReturn>(ICallback3<TReturn> callback) => callback.Invoke<T1, T2, T3>();
        }
    }

    abstract class TypeArguments3<T3Constraint>
    {
        public abstract TReturn Invoke<TReturn>(ICallback3_3<TReturn, T3Constraint> callback);

        public class Of<T1, T2, T3> : TypeArguments3<T3Constraint> where T3 : T3Constraint
        {
            public override TReturn Invoke<TReturn>(ICallback3_3<TReturn, T3Constraint> callback) => callback.Invoke<T1, T2, T3>();
        }
    }
}
