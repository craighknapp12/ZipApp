
using Microsoft.Extensions.DependencyInjection;
using AbroadConcepts.IO;
using ZipCmd.Models;
using ZipCmd.Services;
using AbroadConcepts.CommandLine;

namespace ZipCmd;
public static class Program
{
    private static void Main(string[] args)
    {
        var showHelp = true;
        var commandArguments = new CommandArguments(args);
        var mainArgument = new MainArgument();

        var zipCore = new ZipCore(commandArguments);
        var serviceCollection = SetupInitialServices(zipCore);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        if (args.Length > 0 && commandArguments.Parse<IZipAction>(mainArgument,  serviceProvider.GetServices<IZipAction>()))
        {
            if (!string.IsNullOrEmpty(mainArgument.ZipFile))
            {
                var filenames = mainArgument.ZipFile.EnsureExtension(".zip").GetFiles(false, true).ToList();

                if (filenames.Count == 1)
                {
                    using var stream = File.Open(filenames[0], FileMode.OpenOrCreate);
                    using var zipArchiver = new ZipArchiver(stream);
                    zipCore.Archiver = zipArchiver;
                    zipCore.Stream = stream;
                    var zipCommand = serviceProvider.GetService<IZipCommand>();
                    foreach (var option in commandArguments.Options)
                    {
                        showHelp = false;
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
                    Console.WriteLine("No zip filename given.");
            }
        }
        
        if (showHelp) 
        {
            var helper = serviceProvider.GetServices<IZipAction>().FirstOrDefault(s => s.GetType() == typeof(ZipHelp));
            helper?.Execute();
        }
    }

    public static IServiceCollection SetupInitialServices(ZipCore  core)
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