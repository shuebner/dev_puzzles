# Setting

You inherited a code base that does not yet take advantage of nullable reference types (NRTs) and the compile-time nullability checks that they provide.

Since you want to let the compiler work for you as much as possible, you decide it is time to take full advantage of that feature.

# Objective

Your task is to annotate the code with the NRT-related tools that the language gives you.
You should not change any code at first, but just annotate the status quo.

Apart from the NRT marker `?` you will take advantage of some of the [Attributes for null-state static analysis](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/attributes/nullable-analysis).

But beware, there may be a nullability-related bug in the code.
Let the compiler lead you to it.
Then fix it.

# Rules

You MUST NOT fundamentally change any signatures, e. g. change/add return types, change/add parameters; this includes constructors.

You MUST NOT change any implementation except for fixing the bug (see Objective).

# Suggested Steps

Enable compile-time nullability analysis for the project.

You should now see several compiler warnings about possible null dereferences, nullability type mismatches and other nullability-related observations.

Then, look at the code and how it handles null, i. e. where there are null-checks, where there are none and where the code may provide certain guarantees regarding nullability.

Annotate the code according to what you see without changing any behavior at first.

Follow the compiler warnings to find inconsistencies in null-handling.
Fix those inconsistencies by changing the annotations and in case of the bug, the implementation.