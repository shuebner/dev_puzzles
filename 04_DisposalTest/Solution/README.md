# Solution

The first test is for warming up and can be written using Moq's `ReturnsAsync` setup.

In the second test you can employ the technique from [Puzzle 03](../../03_ParallelTesting\Solution\README.md) for controlling completion of the returned Task.
In addition you have to use Moq's `As<T>()` mock setup function to add the `IDisposable` interface to your mock.

The implementation is now forced to both wait for the service method to complete and dispose the scope.