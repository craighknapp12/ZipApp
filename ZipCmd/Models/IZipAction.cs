using System.IO.Compression;
using System.IO;
using AbroadConcepts.IO;

namespace ZipCmd.Models;
public interface IZipAction
{
    public string ActionName { get; }

    public void Execute();
}
