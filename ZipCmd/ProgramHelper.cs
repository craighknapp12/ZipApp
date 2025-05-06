
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using ZipCmd.Services;

namespace ZipCmd;
public static class ProgramHelper
{
    [ExcludeFromCodeCoverage]
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
