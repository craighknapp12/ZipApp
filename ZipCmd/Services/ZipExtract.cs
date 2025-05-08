using ZipCmd.Models;

namespace ZipCmd.Services;

public class ZipExtract(ZipCore core) : IZipAction
{
    public string CommandOption => "-e";

    public Type ArgumentType => typeof(ExtractArgument);

    public bool Execute()
    {
        var result = true;
        try
        {
            var argument = core.CommandArguments.GetNextArgument<ExtractArgument>();
            core.Archiver.Extract(argument.Directory, argument.Overwrite, argument.Pattern, (name, status) =>
            {
                Console.WriteLine($"Extracted {name} {status}");
                result = result && status == "OK";
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            result = false;
        }

        return result;
    }
}
