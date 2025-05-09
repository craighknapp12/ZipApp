using AbroadConcepts.CommandLine;
using AbroadConcepts.IO;
using Microsoft.Extensions.DependencyInjection;
using ZipCmd;
using ZipCmd.Models;
using ZipCmd.Services;

var commandArguments = new CommandArguments(args);
var mainArgument = new MainArgument();

var zipCore = new ZipCore(commandArguments);
var serviceCollection = ProgramHelper.SetupInitialServices(zipCore);
var serviceProvider = serviceCollection.BuildServiceProvider();
if (args.Length > 0 && commandArguments.Parse<IZipAction>(mainArgument, serviceProvider.GetServices<IZipAction>()))
{
    if (!string.IsNullOrEmpty(mainArgument.ZipFile))
    {
        var filenames = mainArgument.ZipFile.EnsureExtension(".zip").GetFiles(false, true).ToList();

        if (filenames.Count == 1)
        {
            return ProgramHelper.RunCommand(filenames[0], commandArguments.Options, zipCore, serviceProvider.GetService<IZipCommand>()!) ? 0 : -1;
        }
        else
        {
            ProgramHelper.ShowMultipleFilesError(filenames);
            return -1;
        }
    }
    else
    {
        Console.WriteLine("No zip filename given.");
        ProgramHelper.ShowHelp(serviceProvider);
        return -1;
    }
}
else
{
    Console.WriteLine(commandArguments.Message);
    ProgramHelper.ShowHelp(serviceProvider);
    return -1;
}
