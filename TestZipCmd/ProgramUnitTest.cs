using Microsoft.Extensions.DependencyInjection;
using ZipCmd;
using ZipCmd.Services;
using AbroadConcepts.CommandLine;
using ZipCmd.Models;

namespace TestZipCmd;

public class ProgramUnitTest
{
    [Fact]
    public void TestCreationOfServices()
    {
        var commandArguments = new CommandArguments(new string[] { "-a", "*.*" });
        var mainArgument = new MainArgument();
        var zipCore = new ZipCore(commandArguments);
        var serviceCollection = ProgramHelper.SetupInitialServices(zipCore);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var parseResult = commandArguments.Parse<IZipAction>(mainArgument, serviceProvider.GetServices<IZipAction>());
        using var memoryStream = new MemoryStream();
        var zipCommand = ProgramHelper.OpenZipFile(zipCore, serviceProvider, memoryStream);
        var showHelp = ProgramHelper.RunCommand(commandArguments, zipCommand);

        Assert.False(showHelp);
        Assert.True(parseResult);
        Assert.Equal(7, serviceCollection.Count);
        Assert.NotNull(serviceProvider);
        Assert.NotNull(serviceProvider.GetService<IZipCommand>()!);
    }

    [Fact]
    public void TestCanCreateZipAdd() 
    {
        var commandArguments = new CommandArguments(new string[] { "-a", "*.*" });
        var mainArgument = new MainArgument();
        var zipCore = new ZipCore(commandArguments);
        var serviceCollection = ProgramHelper.SetupInitialServices(zipCore);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var parseResult = commandArguments.Parse<IZipAction>(mainArgument, serviceProvider.GetServices<IZipAction>());

        using var memoryStream = new MemoryStream();
        var zipCommand = ProgramHelper.OpenZipFile(zipCore, serviceProvider, memoryStream);
        var showHelp = ProgramHelper.RunCommand(commandArguments, zipCommand);

        Assert.True(parseResult);
        Assert.False(showHelp);
    }

    [Fact]
    public void TestCanCreateZipExtract()
    {
        var commandArguments = new CommandArguments(new string[] { "-e", "*" });
        var mainArgument = new MainArgument();
        var zipCore = new ZipCore(commandArguments);
        var serviceCollection = ProgramHelper.SetupInitialServices(zipCore);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var parseResult = commandArguments.Parse<IZipAction>(mainArgument, serviceProvider.GetServices<IZipAction>());

        using var memoryStream = new MemoryStream();
        var zipCommand = ProgramHelper.OpenZipFile(zipCore, serviceProvider, memoryStream);
        var showHelp = ProgramHelper.RunCommand(commandArguments, zipCommand);
        
        Assert.True(parseResult);
        Assert.False(showHelp);
    }
    [Fact]
    public void TestCanCreateZipRemove()
    {
        var commandArguments = new CommandArguments(new string[] {"-r", "*" });
        var mainArgument = new MainArgument();
        var zipCore = new ZipCore(commandArguments);
        var serviceCollection = ProgramHelper.SetupInitialServices(zipCore);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var parseResult = commandArguments.Parse<IZipAction>(mainArgument, serviceProvider.GetServices<IZipAction>());

        using var memoryStream = new MemoryStream();
        var zipCommand = ProgramHelper.OpenZipFile(zipCore, serviceProvider, memoryStream);
        var showHelp = ProgramHelper.RunCommand(commandArguments, zipCommand);

        Assert.True(parseResult);
        Assert.False(showHelp);
    }
    [Fact]
    public void TestCanCreateZipList()
    {
        var commandArguments = new CommandArguments(new string[] {"-l", "*" });
        var mainArgument = new MainArgument();
        var zipCore = new ZipCore(commandArguments);
        var serviceCollection = ProgramHelper.SetupInitialServices(zipCore);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var parseResult = commandArguments.Parse<IZipAction>(mainArgument, serviceProvider.GetServices<IZipAction>());

        using var memoryStream = new MemoryStream();
        var zipCommand = ProgramHelper.OpenZipFile(zipCore, serviceProvider, memoryStream);
        var showHelp = ProgramHelper.RunCommand(commandArguments, zipCommand);

        Assert.True(parseResult);
        Assert.False(showHelp);
    }
    [Fact]
    public void TestCanCreateZipHelp()
    {
        var commandArguments = new CommandArguments(new string[] {"-h"});
        var mainArgument = new MainArgument();
        var zipCore = new ZipCore(commandArguments);
        var serviceCollection = ProgramHelper.SetupInitialServices(zipCore);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var parseResult = commandArguments.Parse<IZipAction>(mainArgument, serviceProvider.GetServices<IZipAction>());

        using var memoryStream = new MemoryStream();
        var zipCommand = ProgramHelper.OpenZipFile(zipCore, serviceProvider, memoryStream);
        var showHelp = ProgramHelper.RunCommand(commandArguments, zipCommand);

        Assert.True(parseResult);
        Assert.True(showHelp);
    }
}
