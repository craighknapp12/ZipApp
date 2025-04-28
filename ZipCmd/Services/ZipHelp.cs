using AbroadConcepts.CommandLine;
using ZipCmd.Models;

namespace ZipCmd.Services;

public class ZipHelp(ZipCore core) : IZipAction
{
    public string CommandOption => "-h";

    public Type ArgumentType => null!;

    public void Execute()
    {
        Console.WriteLine("ZipCmd - Command line to operate on a zip file.");
        Console.WriteLine();
        Console.WriteLine(core.CommandArguments.ShowArguments());
        Console.WriteLine();
        Console.WriteLine("Command: ZipCmd.exe <ZipFilename> [-h] [-a <filePattern> <overwrite> <compression> <entryLevel>]  [-d <filePattern> ]  [-e <filePattern> ]  [-l <filePattern> ]");
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Options:");
        Console.WriteLine("\t-a <filePattern> <overwrite> <compression> <entryLevel> - Adds files to the zip file");
        Console.WriteLine("\t-r <filePattern> - Deletes files from the zip file");
        Console.WriteLine("\t-e <filePattern> <directory> <overwrite> - Extract files from the zip file");
        Console.WriteLine("\t-h - Show Help");
        Console.WriteLine("\t-l <filePattern> - List files in the zip file");
    }
}
