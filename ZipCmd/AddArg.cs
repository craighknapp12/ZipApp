using System.IO.Compression;

public partial class AddArg
{
    public string Filename { get; set; } = string.Empty;
    public bool Override { get; set; } = false;
    public CompressionLevel Compression { get; set; } = CompressionLevel.NoCompression;
}


