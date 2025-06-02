using AbroadConcepts.CommandLine;

namespace ZipCmd.Models;

public class MainArgument : IArgument
{
    public string ZipFile { get; set; } = string.Empty;
}

