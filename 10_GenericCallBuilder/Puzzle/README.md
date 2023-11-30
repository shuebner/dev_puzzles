# Hard Bonus Puzzle Warning

I consider this a very hard puzzle.
Even getting part of the way can be a learning experience.

# Setting

You love DDD and the compiler.
Thus you have designed your models to use the type system and compile-time checks wherever possible.
This lead to some models that enforce consistency with generic types and type constraints.

One of those models is a concrete implementation of an abstract Pipeline class.
The implementation defines extension points for data source, mapping, manipulation and sink.
All extension points are defined generically over their supported types.
The pipeline implementation uses the generic type parameters to naturally ensure that all parts of the pipeline are compatible with each other.

You now need to import such a pipeline from a text-based serialization format.
The text format contains information about which types to use as type arguments for the pipeline.
You already have the strings that are determining the types.
But you still need to build a generic call with multiple type arguments from those strings to be able to construct the pipeline.

Reflection is unacceptable because you did not build strongly typed models just to lose their type-safety on deserialization.

# Objective 1

Implement a deserialization method that accepts three string parameters and returns an instance of a generic type with arity 3.

You MUST NOT use reflection.
You MUST NOT use any other mechanism that loses type-safety (like building expression trees).
The implementation MUST lead to compiler errors when type constraints of the model are not guaranteed.

# Check Objective 1

Run the tests to see if the deserializer produces the correct concrete type.

Make sure that your solution is indeed type-safe.
To that end, introduce a type constraint on the generic type parameter of the data sink, e. g. `ISerializable`.
If you get a compiler error about that constraint, you have successfully solved Objective 1.

# Objective 2

Adapt your implementation so that everything compiles again.