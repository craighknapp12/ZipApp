using AbroadConcepts.CommandLine;

namespace ZipCmd.Services;
public interface IZipAction : ICommandArgument
{

    public bool Execute();
}
