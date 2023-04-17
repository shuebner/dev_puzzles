# Setting

You implemented a query method that resolves a service, calls an asynchronous method on it and returns its result.
A bug was discovered whose cause is that the method does not dispose the service.

You decide that a test should be written to force the implementation to provably do the right thing.

# Objective

Implement the two scaffolded tests.

The first test should ensure the correct return value.
The second test should ensure the correct lifetime management for the scoped service

# Rules

You MUST NOT change any type definitions.

You MUST NOT change the test setup.
