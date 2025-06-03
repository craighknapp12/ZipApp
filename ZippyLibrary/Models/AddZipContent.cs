using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZippyLibrary.Models;
public class AddZipContent
{
    public string Filename { get; set; } = string.Empty;
    public bool Override { get; set; } = false;
    public CompressionLevel Compression { get; set; } = CompressionLevel.NoCompression;
    public int EntryLevel { get; set; } = 0;
    public string Directory { get; set; } = string.Empty;
}
