using AbroadConcepts.IO;

namespace ZipCmd.Models;

public class ZipRemove(ZipArchiver archive, PatternArgument argument, Stream stream) : IZipAction
{
    public string ActionName => "-r";

    public void Execute()
    {
        archive.Remove(argument.Pattern);
        archive.Save(stream);
    }
}
