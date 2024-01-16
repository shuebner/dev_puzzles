# Setting

You are using a database library that accepts exactly three different types when writing values: `int`, `bool` and `string`.
The relevant library method just accepts `object[]` as values and throws exceptions at runtime.

You think this is too error-prone.
You know you can do better to avoid programming errors by using the C# compiler.

Thus, you would like to replace the non-typesafe `object[]` with a typesafe alternative.

At several points in your application, you programmatically create a collection of arbitrary values for consumption by that library.

You would like the change to be unintrusive, changing as little code as possible.

# Objective

Create a collection type to serve as drop-in replacement for `object[]`.

The new collection type MUST be type-safe with regards to the allowed database types, i. e. adding any value of type other than `int`, `bool` or `string` MUST result in a compile-time error.

Change as little code as possible.

You do not have to worry about boxing, since the database library will cause boxing anyway.