# Solution

In addition to adding some `?` here and there you need the following attributes:

* `NotNullWhen`
* `MemberNotNull`
* `DoesNotReturn`
* `DisallowNull`

There are two places to which the compiler will not point you and that you therefore have to spot on your own.

1. `DisallowNull` on `SomeData.Value`, because the setter throws on `null``.
1. `?` on `IFooService.GetFooOrDefault`'s return type, because the `***OrDefault` naming together with a reference type return means that null could be returned