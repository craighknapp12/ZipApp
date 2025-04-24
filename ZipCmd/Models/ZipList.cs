using AbroadConcepts.IO;

namespace ZipCmd.Models;

public class ZipList(ZipArchiver archive, PatternArgument argument) : IZipAction
{
    public string ActionName => "-l";

    public void Execute()
    {
        foreach (var s in archive.GetEntries(argument.Pattern))
        {
            Console.WriteLine(s);
        }
    }
}