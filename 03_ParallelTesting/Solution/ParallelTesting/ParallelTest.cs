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
    // note that everything here runs synchronously
    // we do not need to play with timeouts
    // we do not even need async/await at all
    public void ExecuteAll_Should_complete_when_all_commands_are_complete()
    {
        var command1Mock = new Mock<ICommand>();
        var command2Mock = new Mock<ICommand>();
        var command3Mock = new Mock<ICommand>();
        TaskCompletionSource tcs1 = new();
        TaskCompletionSource tcs2 = new();
        TaskCompletionSource tcs3 = new();
        command1Mock.Setup(c => c.ExecuteAsync()).Returns(tcs1.Task);
        command2Mock.Setup(c => c.ExecuteAsync()).Returns(tcs2.Task);
        command3Mock.Setup(c => c.ExecuteAsync()).Returns(tcs3.Task);

        var executorTask = _executor.ExecuteAllAsync(command1Mock.Object, command2Mock.Object, command3Mock.Object);

        Assert.False(executorTask.IsCompleted);

        // only first command execution done
        tcs1.SetResult();
        Assert.False(executorTask.IsCompleted);

        // first and third, almost there
        tcs3.SetResult();
        Assert.False(executorTask.IsCompleted);

        // now all command executions are done and the executor should be done too
        tcs2.SetResult();
        Assert.True(executorTask.IsCompleted);
    }

    [Fact]
    // note that everything here runs synchronously
    // we do not need to play with timeouts
    // we do not even need async/await at all
    public void ExecuteAll_Should_execute_all_commands_in_parallel()
    {
        var command1Mock = new Mock<ICommand>();
        var command2Mock = new Mock<ICommand>();
        var command3Mock = new Mock<ICommand>();
        TaskCompletionSource tcs1 = new();
        TaskCompletionSource tcs2 = new();
        TaskCompletionSource tcs3 = new();
        bool command1WasExecuted = false;
        bool command2WasExecuted = false;
        bool command3WasExecuted = false;
        command1Mock.Setup(c => c.ExecuteAsync())
            .Callback(() => command1WasExecuted = true)
            .Returns(tcs1.Task);
        command2Mock.Setup(c => c.ExecuteAsync())
            .Callback(() => command2WasExecuted = true)
            .Returns(tcs2.Task);
        command3Mock.Setup(c => c.ExecuteAsync())
            .Callback(() => command3WasExecuted = true)
            .Returns(tcs3.Task);

        var executorTask = _executor.ExecuteAllAsync(command1Mock.Object, command2Mock.Object, command3Mock.Object);

        Assert.True(command1WasExecuted);
        Assert.True(command2WasExecuted);
        Assert.True(command3WasExecuted);
        // not strictly necessary, but IMHO nice to have for context and readability
        Assert.False(executorTask.IsCompleted);

        // finish the command executions
        tcs1.SetResult();
        tcs2.SetResult();
        tcs3.SetResult();

        Assert.True(executorTask.IsCompleted);
    }
}

public interface ICommand
{
    Task ExecuteAsync();
}

sealed class Executor
{
    public Task ExecuteAllAsyncOriginal(params ICommand[] commands)
    {
        foreach (var command in commands)
        {
            _ = command.ExecuteAsync();
        }
        
        return Task.CompletedTask;
    }

    public async Task ExecuteAllAsyncUntilComplete(params ICommand[] commands)
    {
        foreach (var command in commands)
        {
            await command.ExecuteAsync();
        }
    }

    public Task ExecuteAllAsync(params ICommand[] commands)
    {
        List<Task> tasks = new(commands.Length);
        foreach (var command in commands)
        {
            tasks.Add(command.ExecuteAsync());
        }

        return Task.WhenAll(tasks);
    }
}