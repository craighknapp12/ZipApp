using AbroadConcepts.IO;

namespace ZipCmd.Models;

public class ZipAdd(ZipArchiver archive, AddArgument argument, Stream stream) : IZipAction
{
    public string ActionName => "-a";

    public void Execute()
    {
        archive.Add(argument.Filename, argument.EntryLevel, argument.Override, argument.Compression);
        archive.Save(stream);
    }
}
