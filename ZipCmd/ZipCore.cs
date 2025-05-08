using AbroadConcepts.CommandLine;
using AbroadConcepts.IO;

namespace ZipCmd;

public class ZipCore(CommandArguments commandArguments)
{
    public ZipArchiver Archiver { get; set; } = null!;
    public CommandArguments CommandArguments { get; } = commandArguments;
    public Stream Stream { get; set; } = null!;

    public void Update(ZipArchiver zipArchiver, Stream stream)
    {
        Archiver = zipArchiver;
        Stream = stream;
    }
}
