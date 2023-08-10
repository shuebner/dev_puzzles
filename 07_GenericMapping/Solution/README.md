# Solution

To avoid reflection, we must at some point call `new Target<T>(...)` with a generic type argument.
According to the objective that type argument needs to be the one of the specific `Source<T>` instance.
The `Source<T>` instance, however, is "hiding" behind the abstract `Source` at compile time.
This means that we have no way of getting at the type argument from outside.
The information is only known to the `Source<T>` instance.

Thus, a generic call with type argument `T` must be added to `Source<T>`.

To have this available on `Source`, too, we have to add an abstract method to `Source` which is then implemented by `Source<T>`.

If it wasn't for the rule that `Source` must not reference `Target`, this would already be the solution, since we could just do
```csharp
// in Source
abstract Target Map();

// in Source<T>
override Target Map() => new Target<T>(this.Value);
```

Not being able to reference `Target` means that we need to add one level of indirection.
The information about `T` must be exported out of `Source`, so we can use it in some other place that is allowed to reference `Target`.
Because only `Source<T>` has the `Value` we need, we could export a reference to `Source<T>` to get both the property and the information about `T`.
We can do this by defining an interface with a generic method on it:
```csharp
interface ICall
{
    void Call<T>(Source<T> source);
}

// in Source
abstract void Map(ICall call);

// in Source<T>
override void Map(ICall call) => call.Call(this);
```
And then call that in our Mapper:
```csharp
class Mapper : ICall
{
    Target _target;

    Target Map(Source source)
    {
        source.Map(this);
        return _target;
    }

    void Call<T>(Source<T> source)
    {
        _target = new Target<T>(source.Value);
    }
}
```
This already works and would be a solution.

But we can improve on it by simplifying and restructuring:

1. Since this is essentially a Visitor pattern, use the terminology of `Visit` and `Accept` for readability
1. To avoid state on the mapper (`_target`), add a generic return value to the visitor interface
1. Since the visitor interface is specific to `Source`, define it in the `Source` class. That way it won't collide with similar interfaces on other types.
1. To avoid cluttering our types with technical pattern boilerplate, declare `Source` and `Source<T>` as `partial` and extract their visitor code into a separate file.

Which leads us to the final proposed solution.

## Bonus Question

Defining a whole interface to make some generic callback may seem natural in C#.
However, in principle a `delegate` would suffice.
Unfortunately, C# does not have open generic delegate types, which is why we do in fact need the interface.

In contrast, TypeScript does have open generic function types, so we can just define our generic delegate type and then use it like this:
```typescript
type Visit<TReturn> = <T>(source: Source<T>) => TReturn;

abstract class SourceBase {
  public abstract Accept<TReturn>(visit: Visit<TReturn>): TReturn
}

class Mapper {
  public Map(source: SourceBase): TargetBase {
    return source.Accept(Mapper.ToTarget);
  }

  private static ToTarget<T>(source: Source<T>): Target<T> {
    return new Target<T>(source.value);
  }
}
```

## Further Generalization

Assume we have multiple classes like `Source<T>`.
With the current solution, we need to define a visitor interface (or, in TypeScript, a generic delegate) for each and every one of them.

We would of course like to further generalize that interface.
But for that we would need higher kinded types like [Haskell's type classes](http://learnyouahaskell.com/types-and-typeclasses#typeclasses-101).
Neither language supports those.
If they did, it could look something like this (pseudo code):
```csharp
// not possible in C#
interface IVisitor<TBase<T>, TReturn>
```
or
```typescript
// not possible in TypeScript
type Visit<TBase, TReturn> = <T>(source: TBase<T>) => TReturn;
```
As it is, it must remain a pattern to be repeated over and over again either by hand or by source generation.