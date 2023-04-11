# Solution

Remove the `readonly` modifier on the `_wrapper` field.

# Explanation

The `readonly` modifier in the original code forces the compiler to make a defensive copy of the struct every time code calls a method on it, accesses a property getter etc..
This can both cause unexpected behavior and be a performance bottleneck.

Removing the `readonly` modifier relaxes the guarantees that the compiler
must give you.
No defensive copy is necessary now.

The proper solution however is to never use mutable structs in the first place.
That is why C# 7.2 gave us [`readonly struct`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/struct#readonly-struct).

More information on defensive copying and other pitfalls is available [here](https://devblogs.microsoft.com/premier-developer/the-in-modifier-and-the-readonly-structs-in-c/) from one of .NET's performance experts Sergey Tepliakov.
