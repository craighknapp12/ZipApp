using Microsoft.Extensions.DependencyInjection;
using AbroadConcepts.IO;
using ZipCmd;
using ZipCmd.Models;

namespace TestZipCmd;

public class ProgramUnitTest
{
    [Fact]
    public void TestCreationOfServices()
    {
        var serviceCollection = Program.CreateServiceCollection();
        using var memoryStream = new MemoryStream();
        using var zipArchiver = new ZipArchiver(memoryStream);
        serviceCollection.AddSingleton<Stream>(memoryStream);
        serviceCollection.AddSingleton<ZipArchiver>(zipArchiver);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        Assert.Equal(12, serviceCollection.Count);
        Assert.NotNull(serviceProvider);
        Assert.NotNull(serviceProvider.GetService<AddArgument>()!);
        Assert.NotNull(serviceProvider.GetService<PatternArgument>()!);
        Assert.NotNull(serviceProvider.GetService<ExtractArgument>()!);
        Assert.NotNull(serviceProvider.GetService<PatternArgument>()!);
        Assert.NotNull(serviceProvider.GetService<MainArgument>()!);
        Assert.NotNull(serviceProvider.GetService<IZipCommand>()!);

    }

    [Fact]
    public void TestCanCreateZipAdd()
    {
        var serviceCollection = Program.CreateServiceCollection();
        using var memoryStream = new MemoryStream();
        using var zipArchiver = new ZipArchiver(memoryStream);
        serviceCollection.AddSingleton<Stream>(memoryStream);
        serviceCollection.AddSingleton<ZipArchiver>(zipArchiver);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var zipCommand = serviceProvider.GetService<IZipCommand>()!;
        zipCommand.Execute("-a");
        Assert.True(true);
    }

    [Fact]
    public void TestCanCreateZipExtract()
    {
        var serviceCollection = Program.CreateServiceCollection();
        using var memoryStream = new MemoryStream();
        using var zipArchiver = new ZipArchiver(memoryStream);
        serviceCollection.AddSingleton<Stream>(memoryStream);
        serviceCollection.AddSingleton<ZipArchiver>(zipArchiver);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var zipCommand = serviceProvider.GetService<IZipCommand>()!;
        zipCommand.Execute("-e");
        Assert.True(true);
    }
    [Fact]
    public void TestCanCreateZipRemove()
    {
        var serviceCollection = Program.CreateServiceCollection();
        using var memoryStream = new MemoryStream();
        using var zipArchiver = new ZipArchiver(memoryStream);
        serviceCollection.AddSingleton<Stream>(memoryStream);
        serviceCollection.AddSingleton<ZipArchiver>(zipArchiver);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var zipCommand = serviceProvider.GetService<IZipCommand>()!;
        zipCommand.Execute("-r");
        Assert.True(true);
    }
    [Fact]
    public void TestCanCreateZipList()
    {
        var serviceCollection = Program.CreateServiceCollection();
        using var memoryStream = new MemoryStream();
        using var zipArchive = new ZipArchiver(memoryStream);
        serviceCollection.AddSingleton<Stream>(memoryStream);
        serviceCollection.AddSingleton<ZipArchiver>(zipArchive);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var zipCommand = serviceProvider.GetService<IZipCommand>()!;
        zipCommand.Execute("-l");
        Assert.True(true);
    }
}
