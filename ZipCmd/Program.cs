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
            var filename = filenames[0];

            var zipCommand = serviceProvider.GetService<IZipCommand>();
            using var stream = File.Open(filename, FileMode.OpenOrCreate);
            using var zipArchiver = new ZipArchiver(stream);
            zipCore.Update(zipArchiver, stream);

            ProgramHelper.RunCommand(commandArguments, zipCommand);
        }
        else
        {
            ProgramHelper.ShowMultipleFilesError(filenames);
        }
    }
    else
    {
        Console.WriteLine("No zip filename given.");
        ProgramHelper.ShowHelp(serviceProvider);
    }
}
else
{
    Console.WriteLine(commandArguments.Message);
    ProgramHelper.ShowHelp(serviceProvider);
}
