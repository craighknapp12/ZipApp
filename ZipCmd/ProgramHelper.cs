
using System.Diagnostics.CodeAnalysis;
using AbroadConcepts.CommandLine;
using AbroadConcepts.IO;
using Microsoft.Extensions.DependencyInjection;
using ZipCmd.Services;

namespace ZipCmd;
public static class ProgramHelper
{
    public static IZipCommand? OpenZipFile(ZipCore zipCore, ServiceProvider serviceProvider, Stream stream)
    {
        var zipArchiver = new ZipArchiver(stream);
        zipCore.Archiver = zipArchiver;
        zipCore.Stream = stream;
        return serviceProvider.GetService<IZipCommand>();
    }

    public static bool RunCommand(CommandArguments commandArguments, IZipCommand? zipCommand)
    {
        var showHelp = true;
        foreach (var option in commandArguments.Options)
        {
            zipCommand?.Execute(option);
            showHelp = false;
        }

        return showHelp;
    }

    public static void ShowMultipleFilesError(List<string> filenames)
    {
        Console.WriteLine("ZipCmd - ZipFileName pattern does not reference a single filename.\n");
        Console.WriteLine("Possible Files:");
        foreach (var filename in filenames)
        {
            Console.WriteLine($"\t{filename}");
        }
        Console.WriteLine();
    }

    public static IServiceCollection SetupInitialServices(ZipCore core)
    {
        return new ServiceCollection()
            .AddTransient<IZipCommand, ZipCommand>()
            .AddTransient<IZipAction, ZipAdd>()
            .AddTransient<IZipAction, ZipExtract>()
            .AddTransient<IZipAction, ZipList>()
            .AddTransient<IZipAction, ZipRemove>()
            .AddTransient<IZipAction, ZipHelp>()
            .AddSingleton(core);
    }
}
