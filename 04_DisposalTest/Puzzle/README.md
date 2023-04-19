# Setting

You implemented a query method that resolves a service, calls an asynchronous method on it and returns its result.
A bug was discovered whose cause is that the method does not dispose the service.

You decide that a test should be written to force the implementation to provably do the right thing.

# Objective

Implement the two scaffolded tests.

The first test should ensure the correct return value.

The second test should ensure the correct lifetime management for the scoped service.
This means, it must fail when:
* the service is not disposed
* the service is disposed before it is done

# Rules

You MUST NOT change any type definitions.

You MUST NOT change the test setup.

You MUST NOT do any type checks on the `INumberService` instance in your implementation.

You MAY change the test method signatures, e. g. make them `async`, return `Task` etc..

# Suggested Steps

1. Implement the first test. That should be straightforward. The test should immediately pass.
2. Implement the second test and watch it fail for the initial implementation.
3. Change the implementation to make the second test pass.
