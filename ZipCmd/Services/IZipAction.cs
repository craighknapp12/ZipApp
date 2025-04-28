using System.IO.Compression;
using System.IO;
using AbroadConcepts.IO;
using AbroadConcepts.CommandLine;

namespace ZipCmd.Services;
public interface IZipAction : ICommandArgument
{

    public void Execute();
}
