using AbroadConcepts.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using ZipCmd;
using ZipCmd.Models;
using ZipCmd.Services;

namespace TestZipCmd;

public class ProgramUnitTest
{
    [Fact]
    public void TestCreationOfServices()
    {
        var zipCore = new ZipCore(new CommandArguments(new[] { "" }));
        var serviceCollection = ProgramHelper.SetupInitialServices(zipCore);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        Assert.Equal(7, serviceCollection.Count);
        Assert.NotNull(serviceProvider);
        Assert.NotNull(serviceProvider.GetService<IZipCommand>()!);
    }

    [Fact]
    public void TestCanCreateZipAdd()
    {
        var commandLine = new CommandArguments(new string[] { "-a", "*.*" });
        var result = false;
        var runResult = false;

        Setup(commandLine, (mainArg, services, stream, options, zipCore, zipCommand) =>
        {
            result = commandLine.Parse<IZipAction>(mainArg, services);
            runResult = ProgramHelper.RunCommand(stream, options, zipCore, zipCommand);
        });

        Assert.True(runResult);
        Assert.True(result);
    }

    [Fact]
    public void TestCanCreateZipExtract()
    {
        var commandLine = new CommandArguments(new string[] { "-e", "*" });
        var result = false;
        var runResult = false;

        Setup(commandLine, (mainArg, services, stream, options, zipCore, zipCommand) =>
        {
            result = commandLine.Parse<IZipAction>(mainArg, services);
            runResult = ProgramHelper.RunCommand(stream, options, zipCore, zipCommand);
        });

        Assert.True(runResult);
        Assert.True(result);
    }

    [Fact]
    public void TestCanCreateZipRemove()
    {
        var commandLine = new CommandArguments(new string[] { "-r", "*" });
        var result = false;
        var runResult = false;

        Setup(commandLine, (mainArg, services, stream, options, zipCore, zipCommand) =>
        {
            result = commandLine.Parse<IZipAction>(mainArg, services);
            runResult = ProgramHelper.RunCommand(stream, options, zipCore, zipCommand);
        });

        Assert.True(runResult);
        Assert.True(result);
    }

    [Fact]
    public void TestCanCreateZipList()
    {
        var commandLine = new CommandArguments(new string[] { "-l", "*" });
        var result = false;
        var runResult = false;

        Setup(commandLine, (mainArg, services, stream, options, zipCore, zipCommand) =>
        {
            result = commandLine.Parse<IZipAction>(mainArg, services);
            runResult = ProgramHelper.RunCommand(stream, options, zipCore, zipCommand);
        });

        Assert.True(runResult);
        Assert.True(result);
    }

    [Fact]
    public void TestCanCreateZipHelp()
    {
        var commandLine = new CommandArguments(new string[] { "-h", "-h" });
        var result = false;
        var runResult = false;

        Setup(commandLine, (mainArg, services, stream, options, zipCore, zipCommand) =>
        {
            result = commandLine.Parse<IZipAction>(mainArg, services);
            runResult = ProgramHelper.RunCommand(stream, options, zipCore, zipCommand);
        });

        Assert.True(runResult);
        Assert.True(result);
    }

    private static void Setup(CommandArguments commandLine, Action<MainArgument, IEnumerable<IZipAction>, Stream, IEnumerable<string>, ZipCore, IZipCommand> run)
    {
        var mainArg = new MainArgument();
        var zipCore = new ZipCore(commandLine);
        var serviceCollection = ProgramHelper.SetupInitialServices(zipCore);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        using var stream = new MemoryStream();

        run.Invoke(mainArg, serviceProvider.GetServices<IZipAction>(), stream, commandLine.Options, zipCore, serviceProvider.GetService<IZipCommand>()!);
    }
}
