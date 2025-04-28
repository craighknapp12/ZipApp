using System.IO.Compression;
using AbroadConcepts.CommandLine;

namespace ZipCmd.Models;

public partial class AddArgument : IArgument
{
    public string Filename { get; set; } = string.Empty;
    public bool Override { get; set; } = false;
    public CompressionLevel Compression { get; set; } = CompressionLevel.NoCompression;
    public int EntryLevel { get; set; } = 0;
}
