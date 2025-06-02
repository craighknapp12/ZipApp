using AbroadConcepts.CommandLine;

namespace Zipper.Models;
public class MainArgument : IArgument
{
    public string ZipFile { get; set; } = string.Empty;
}
