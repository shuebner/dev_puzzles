# Solution

Mock the `ICommand`s.

Use `TaskCompletionSource` to control the `Task`s that the mocks return.

Note that both of the new tests can run competely synchronously and do not even need to use `async/await`.