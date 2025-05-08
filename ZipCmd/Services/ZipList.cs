using ZipCmd.Models;

namespace ZipCmd.Services;

public class ZipList(ZipCore core) : IZipAction
{
    public string CommandOption => "-l";

    public Type ArgumentType => typeof(PatternArgument);

    public bool Execute()
    {
        try
        {
            var argument = core.CommandArguments.GetNextArgument<PatternArgument>();
            foreach (var s in core.Archiver.GetEntries(argument.Pattern))
            {
                Console.WriteLine(s);
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}