using System.Runtime.CompilerServices;
using AbroadConcepts.CommandLine;
using AbroadConcepts.IO;
using ZipCmd.Models;
namespace ZipCmd.Services;

public class ZipRemove(ZipCore core) : IZipAction
{
    public string CommandOption => "-r";

    public Type ArgumentType => typeof(PatternArgument);

    public void Execute()
    {
        var argument = core.CommandArguments.GetNextArgument<PatternArgument>();
        core.Archiver.Remove(argument.Pattern, (name, status) => {
            Console.WriteLine($"Removed {name} {status}");
        });

        core.Archiver.Save(core.Stream);
    }
}
