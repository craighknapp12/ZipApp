using System.IO.Compression;

namespace ZipCmd.Models;

public partial class AddArgument
{
    public string Filename { get; set; } = string.Empty;
    public bool Override { get; set; } = false;
    public CompressionLevel Compression { get; set; } = CompressionLevel.NoCompression;
}