# Setting

You want to implement a method that takes a list of asynchronously executing commands.
The method should start execution for all of the commands.
It should return a Task that completes when all of the command executions have completed.

Being the hardcore 10x Hero Ninja TDD developer™ that you are, you will not change any behavior before seeing some test fail that necessitates that change.
You already wrote a first reasonable test which asserts that all commands are executed.
You deliberately implemented the method in the most naive way you could think of just to make the test pass.

Looking at the implementation, you notice that this cannot possibly be what you want in the end.
More tests must be written to force the implementation to do the right thing.

# Objective

Write two more tests.

The first new test should assert that the method returns a Task that completes only when all command executions have completed.
The test should fail for the naive implementation.

The second new test should assert that the method starts command execution for all commands immediately, without waiting for any command to complete first. The test should fail for a method implementation that executes the commands one after the other.

# Rules

You SHOULD NOT directly or indirectly start any new threads in your tests, i. e. no `Task.Run`, `TaskFactory.StartNew` etc..
It can be a first step (hence "SHOULD"), but the puzzle can be solved without it.

You MAY use `Task.Delay`, but the puzzle can be solved without it.