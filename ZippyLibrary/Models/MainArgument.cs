using AbroadConcepts.CommandLine;

namespace ZippyLibrary.Models;

public class MainArgument : IArgument
{
    public string ZipFile { get; set; } = string.Empty;
}

