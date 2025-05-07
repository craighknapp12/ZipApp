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

        var commandLine = new CommandArguments(new string[] { "-a", "*.*" });
        var mainArg = new MainArgument();
        var zipCore = new ZipCore(commandLine);
        var serviceCollection = ProgramHelper.SetupInitialServices(zipCore);
        using var memoryStream = new MemoryStream();
        using var zipArchiver = new ZipArchiver(memoryStream);
        zipCore.Update(zipArchiver, memoryStream);

        serviceCollection.AddSingleton<ZipArchiver>(zipArchiver);

        Assert.False(showHelp);
        Assert.True(parseResult);
        Assert.Equal(7, serviceCollection.Count);
        Assert.NotNull(serviceProvider);
        Assert.NotNull(serviceProvider.GetService<IZipCommand>()!);
    }

    [Fact]
    public void TestCanCreateZipAdd() 
    {
        var commandLine = new CommandArguments(new string[] { "-a", "*.*" });
        var mainArg = new MainArgument();
        var zipCore = new ZipCore(commandLine);
        var serviceCollection = ProgramHelper.SetupInitialServices(zipCore);
        using var memoryStream = new MemoryStream();
        using var zipArchiver = new ZipArchiver(memoryStream);
        zipCore.Update(zipArchiver, memoryStream);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var result = commandLine.Parse<IZipAction>(mainArg, serviceProvider.GetServices<IZipAction>());
        var zipCommand = serviceProvider.GetService<IZipCommand>()!;
        zipCommand.Execute("-a");
        Assert.True(result);
    }

    [Fact]
    public void TestCanCreateZipExtract()
    {
        var commandLine = new CommandArguments(new string[] { "-e", "*" });
        var mainArg = new MainArgument();
        var zipCore = new ZipCore(commandLine);
        var serviceCollection = ProgramHelper.SetupInitialServices(zipCore);
        using var memoryStream = new MemoryStream();
        using var zipArchiver = new ZipArchiver(memoryStream);
        zipCore.Update(zipArchiver, memoryStream);

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var result = commandLine.Parse<IZipAction>(mainArg, serviceProvider.GetServices<IZipAction>());
        var zipCommand = serviceProvider.GetService<IZipCommand>()!;
        zipCommand.Execute("-e");
        Assert.True(result);
    }
  
    [Fact]
    public void TestCanCreateZipRemove()
    {
        var commandLine = new CommandArguments(new string[] {"-r", "*" });
        var mainArg = new MainArgument();
        var zipCore = new ZipCore(commandLine);
        var serviceCollection = ProgramHelper.SetupInitialServices(zipCore);
        using var memoryStream = new MemoryStream();
        using var zipArchiver = new ZipArchiver(memoryStream);
        zipCore.Update(zipArchiver, memoryStream);

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var result = commandLine.Parse<IZipAction>(mainArg, serviceProvider.GetServices<IZipAction>());
        var zipCommand = serviceProvider.GetService<IZipCommand>()!;
        zipCommand.Execute("-r");
        Assert.True(result);
    }

    [Fact]
    public void TestCanCreateZipList()
    {
        var commandLine = new CommandArguments(new string[] {"-l", "*" });
        var mainArg = new MainArgument();
        var zipCore = new ZipCore(commandLine);
        var serviceCollection = ProgramHelper.SetupInitialServices(zipCore);
        using var memoryStream = new MemoryStream();
        using var zipArchiver = new ZipArchiver(memoryStream);
        zipCore.Update(zipArchiver, memoryStream);

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var result = commandLine.Parse<IZipAction>(mainArg, serviceProvider.GetServices<IZipAction>());
        var zipCommand = serviceProvider.GetService<IZipCommand>()!;
        zipCommand.Execute("-l");
        Assert.True(result);
    }

    [Fact]
    public void TestCanCreateZipHelp()
    {
        var commandLine = new CommandArguments(new string[] {"-h"});
        var mainArg = new MainArgument();
        var zipCore = new ZipCore(commandLine);
        var serviceCollection = ProgramHelper.SetupInitialServices(zipCore);

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var result = commandLine.Parse<IZipAction>(mainArg, serviceProvider.GetServices<IZipAction>());
        var zipCommand = serviceProvider.GetService<IZipCommand>()!;
        zipCommand.Execute("-h");
        Assert.True(result);
    }
}
