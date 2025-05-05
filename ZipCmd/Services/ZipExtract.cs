using AbroadConcepts.CommandLine;
using AbroadConcepts.IO;
using ZipCmd.Models;

namespace ZipCmd.Services;

public class ZipExtract(ZipCore core) : IZipAction
{
    public string CommandOption => "-e";

    public Type ArgumentType => typeof(ExtractArgument);

    public void Execute()
    {
        var argument = core.CommandArguments.GetNextArgument<ExtractArgument>();
        core.Archiver.Extract(argument.Directory, argument.Overwrite, argument.Pattern, (name, status) => {
            Console.WriteLine($"Extracting {name} {status}");
        });
    }
}
