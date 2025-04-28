using AbroadConcepts.CommandLine;
using AbroadConcepts.IO;
using ZipCmd.Models;

namespace ZipCmd.Services;

public class ZipList(ZipCore core) : IZipAction
{
    public string CommandOption => "-l";

    public Type ArgumentType => typeof(PatternArgument);

    public void Execute()
    {
        var argument = core.CommandArguments.GetNextArgument<PatternArgument>();
        foreach (var s in core.Archiver.GetEntries(argument.Pattern))
        {
            Console.WriteLine(s);
        }
    }
}