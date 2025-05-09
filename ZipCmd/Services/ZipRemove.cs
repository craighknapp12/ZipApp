using ZipCmd.Models;
namespace ZipCmd.Services;

public class ZipRemove(ZipCore core) : IZipAction
{
    public string CommandOption => "-r";

    public Type ArgumentType => typeof(PatternArgument);

    public bool Execute()
    {
        var result = true;
        try
        {
            var argument = core.CommandArguments.GetNextArgument<PatternArgument>();
            core.Archiver.Remove(argument.Pattern, (name, status) =>
            {
                Console.WriteLine($"Removed {name} {status}");
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
