namespace ZipCmd.Models;

public class ExtractArgument
{
    public string Pattern { get; set; } = string.Empty;
    public string Directory { get; set; } = string.Empty;   
    public bool Overwrite { get; set; } = false;
}