using AbroadConcepts.CommandLine;
using AbroadConcepts.IO;
using Microsoft.Extensions.DependencyInjection;
using ZipCmd;
using ZipCmd.Models;
using ZipCmd.Services;

var showHelp = true;
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
            showHelp = false;
            ProgramHelper.ShowMultipleFilesError(filenames);
        }
    }
    else
    {
        Console.WriteLine("No zip filename given.");
    }
}
else
{
    Console.WriteLine(commandArguments.Message);
}

if (showHelp)
{
    var helper = serviceProvider.GetServices<IZipAction>().FirstOrDefault(s => s.GetType() == typeof(ZipHelp));
    helper?.Execute();
}
