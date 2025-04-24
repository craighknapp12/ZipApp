using AbroadConcepts.IO;

namespace ZipCmd.Models;

public class ZipExtract(ZipArchiver archive, ExtractArgument argument) : IZipAction
{
    public string ActionName => "-e";

    public void Execute()
    {
        archive.Extract(argument.Directory, argument.Overwrite, argument.Pattern);
    }
}
