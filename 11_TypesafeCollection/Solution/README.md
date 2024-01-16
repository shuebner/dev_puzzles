# Solution

The solution leverages:
1. Collection initializer syntax for clean creation
1. Implicit conversion to `object[]` for seemless integration

Collection initializer syntax is available whenever a type implements `IEnumerable` (yes, the non-generic one) and provides at least one method called `Add`.
There can be multiple `Add` methods, each with its own signature.
The methods can even be extension methods and generic, which makes the technique very versatile.
For more info, see https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers#collection-initializers.

The only required code change is then replacing `new object[]` with `new ValueCollection`.
Everything else can stay the same.
