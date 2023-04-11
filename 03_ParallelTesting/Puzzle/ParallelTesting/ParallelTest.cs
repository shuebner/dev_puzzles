using Moq;

namespace ParallelTesting;

public class ParallelTest
{
    readonly Executor _executor;

    public ParallelTest()
    {
        _executor = new Executor();
    }

    [Fact]
    public async Task ExecuteAll_Should_execute_all_commands()
    {
        var command1Mock = new Mock<ICommand>();
        var command2Mock = new Mock<ICommand>();
        var command3Mock = new Mock<ICommand>();

        await _executor.ExecuteAllAsync(command1Mock.Object, command2Mock.Object, command3Mock.Object);

        command1Mock.Verify(c => c.ExecuteAsync(), Times.Once);
        command2Mock.Verify(c => c.ExecuteAsync(), Times.Once);
        command3Mock.Verify(c => c.ExecuteAsync(), Times.Once);
    }

    [Fact]
    public void ExecuteAll_Should_complete_when_all_commands_are_complete()
    {
        // write a test that forces the implementation to
        // return a Task that completes only when all command executions have completed
    }

    [Fact]
    public void ExecuteAll_Should_execute_all_commands_in_parallel()
    {
        // write a test that forces the implementation to
        // execute the commands asynchronously
    }
}

public interface ICommand
{
    Task ExecuteAsync();
}

sealed class Executor
{
    // this implementation was good enough to pass the first test
    // it must be modified to pass the other tests
    public Task ExecuteAllAsync(params ICommand[] commands)
    {
        foreach (var command in commands)
        {
            _ = command.ExecuteAsync();
        }

        return Task.CompletedTask;
    }
}