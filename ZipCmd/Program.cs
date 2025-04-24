
using Microsoft.Extensions.DependencyInjection;
using AbroadConcepts.CommandLine;
using AbroadConcepts.IO;
using ZipCmd.Models;

namespace ZipCmd;

public static class Program
{
    private static void Main(string[] args)
    {
        var serviceCollection = CreateServiceCollection();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var cmdLine = serviceProvider.GetService<CommandArguments>()!;
        cmdLine.Register("-a", serviceProvider.GetService<AddArgument>()!);
        cmdLine.Register("-r", serviceProvider.GetService<PatternArgument>()!);
        cmdLine.Register("-e", serviceProvider.GetService<ExtractArgument>()!);
        cmdLine.Register("-l", serviceProvider.GetService<PatternArgument>()!);

        var mainArg = serviceProvider.GetService<MainArgument>()!;

        if (cmdLine.Parse(mainArg, args))
        {
            var filenames = mainArg.ZipFile.EnsureExtension(".zip").GetFiles(false, true).ToList();

            if (filenames.Count == 1)
            {
                using var stream = File.Open(filenames[0], FileMode.OpenOrCreate);
                using var zipArchiver = new ZipArchiver(stream);
                serviceCollection.AddSingleton<Stream>(stream);
                serviceCollection.AddSingleton<ZipArchiver>(zipArchiver);

                serviceProvider = serviceCollection.BuildServiceProvider();
                var zipCommand = serviceProvider.GetService<IZipCommand>();
                foreach (var option in cmdLine.Options)
                {
                    zipCommand?.Execute(option);
                }
            }
            else
            {
                Console.WriteLine("ZipCmd - ZipFileName pattern does not reference a single filename.\n");
                foreach (var filename in filenames)
                {
                    Console.WriteLine(filename);
                }
            }
        }
        else
        {
            ShowHelp(cmdLine.Message);
        }
    }

    static void ShowHelp(string message)
        {
            Console.WriteLine("ZipCmd - Command line to operate on a zip file.");
            Console.WriteLine();
            Console.WriteLine(message);
            Console.WriteLine();
            Console.WriteLine("Command: ZipCmd.exe <ZipFilename> [-h] [-a <filePattern> <overwrite> <compression> <entryLevel>]  [-d <filePattern> ]  [-e <filePattern> ]  [-l <filePattern> ]");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("\t-a <filePattern> <overwrite> <compression> <entryLevel> - Adds files to the zip file");
            Console.WriteLine("\t-r <filePattern> - Deletes files from the zip file");
            Console.WriteLine("\t-e <filePattern> <directory> <overwrite> - Extract files from the zip file");
            Console.WriteLine("\t-h - Show Help");
            Console.WriteLine("\t-l <filePattern> - List files in the zip file");
        }

    public static IServiceCollection CreateServiceCollection()
    {
        return new ServiceCollection()
            .AddTransient<IZipCommand, ZipCommand>()
            .AddTransient<IZipAction, ZipAdd>()
            .AddTransient<IZipAction, ZipRemove>()
            .AddTransient<IZipAction, ZipExtract>()
            .AddTransient<IZipAction, ZipList>()
            // need to construct actual instance for singleton to have it carry across dll boundry
            .AddSingleton(new CommandArguments())
            .AddSingleton(new MainArgument())
            .AddSingleton(new AddArgument())
            .AddSingleton(new PatternArgument())
            .AddSingleton(new ExtractArgument());
    }
}