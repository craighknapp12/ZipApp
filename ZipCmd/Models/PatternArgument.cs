using AbroadConcepts.CommandLine;

namespace ZipCmd.Models;
 
public class PatternArgument : IArgument
{
    public string Pattern { get; set; } = string.Empty;
}

