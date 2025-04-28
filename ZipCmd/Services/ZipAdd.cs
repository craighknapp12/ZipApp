using AbroadConcepts.CommandLine;
using AbroadConcepts.IO;
using ZipCmd.Models;

namespace ZipCmd.Services;

public class ZipAdd(ZipCore core) : IZipAction
{
    public string CommandOption => "-a";

    public Type ArgumentType => typeof(AddArgument);

    public void Execute()
    {
        var argument = core.CommandArguments.GetNextArgument<AddArgument>();
        core.Archiver.Add(argument.Filename, argument.EntryLevel, argument.Override, argument.Compression);

        core.Archiver.Save(core.Stream);
    }
}
