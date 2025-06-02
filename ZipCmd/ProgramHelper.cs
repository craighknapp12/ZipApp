
using System.Diagnostics.CodeAnalysis;
using AbroadConcepts.IO;
using Microsoft.Extensions.DependencyInjection;
using ZipCmd.Services;

namespace ZipCmd;
public static class ProgramHelper
{
    [ExcludeFromCodeCoverage]
    public static bool RunCommand(string filename, IEnumerable<string> options, ZipCore zipCore, IZipCommand zipCommand)
    {
        bool isNew = !File.Exists(filename);
        using var stream = File.Open(filename, FileMode.OpenOrCreate);
        bool result = RunCommand(stream, options, zipCore, zipCommand);
        if (isNew)
        {
            zipCore.Archiver.Save(stream);
        }

        return result;
    }

    public static bool RunCommand(Stream stream, IEnumerable<string> options, ZipCore zipCore, IZipCommand zipCommand)
    {
        using var zipArchiver = new ZipArchiver(stream);
        zipCore.Update(zipArchiver, stream);

        return ProgramHelper.RunCommand(options, zipCommand!);
    }

    private static bool RunCommand(IEnumerable<string> options, IZipCommand zipCommand)
    {
        var result = true;
        var showHelp = false;
        foreach (var option in options)
        {
            if (option == "-h" && showHelp)
            {
                continue;
            }

            result = result && zipCommand.Execute(option);
            if (option == "-h")
            {
                showHelp = true;
            }
        }

        return result;
    }

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

    [ExcludeFromCodeCoverage]
    public static void ShowHelp(ServiceProvider serviceProvider)
    {
        var helper = serviceProvider.GetServices<IZipAction>().FirstOrDefault(s => s.GetType() == typeof(ZipHelp));
        helper?.Execute();
    }
}
