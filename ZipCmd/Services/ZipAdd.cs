using ZipCmd.Models;

namespace ZipCmd.Services;

public class ZipAdd(ZipCore core) : IZipAction
{
    public string CommandOption => "-a";

    public Type ArgumentType => typeof(AddArgument);

    public bool Execute()
    {
        var result = true;

        try
        {
            var argument = core.CommandArguments.GetNextArgument<AddArgument>();
            core.Archiver.Add(argument.Filename,  argument.EntryLevel, argument.Override, argument.Compression, argument.Directory, (name, status) =>
            {
                Console.WriteLine($"Added {name} {status}");
                result = result && status == "OK";
            });
            if (result)
            {
                core.Archiver.Save(core.Stream);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            result = false;
        }

        return result;
    }
}
