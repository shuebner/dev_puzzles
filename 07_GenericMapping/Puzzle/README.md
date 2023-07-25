# Setting

You need to map from a generic source object to a generic target object with the same type argument. 
Both the source and the target instance are only known by their respective abstract non-generic base type at compile-time.

# Objective

Make the tests pass by implementing `Mapper`.

Bonus Question:
Which language feature would C# need to make the implementation simpler with less boilerplate? Hint: TypeScript has this feature.

# Rules

Everything MUST be type-safe.
You MUST NOT use reflection.

`Source` MUST NOT reference `Target`.

`Target` MUST NOT reference `Source`.

Bonus rule: the `Source.cs` and `Target.cs` _files_ should be changed as little as possible.