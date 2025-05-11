namespace ZipCmd.Services;

public class ZipHelp(ZipCore core) : IZipAction
{
    public string CommandOption => "-h";

    public Type ArgumentType => null!;

    public bool Execute()
    {
        Console.WriteLine("ZipCmd - Command line to operate on a zip file.");
        Console.WriteLine();
        Console.WriteLine("Currant passed arguments:");
        Console.WriteLine($"\t{core.CommandArguments.ShowArguments()}");
        Console.WriteLine();
        Console.WriteLine("Command: ZipCmd.exe <ZipFilename> [-h] [-a <filePattern> <override> <compression> <entryLevel> <directory> ] [-e <filePattern> <directory> <overwrite]  [-l <filePattern> ] [-r <filePattern> ]");
        Console.WriteLine();
        Console.WriteLine("Options:");
        Console.WriteLine("\t-a <filePattern> <override> <compression> <entryLevel> <directory> - Adds files to the zip file");
        Console.WriteLine("\t-e <filePattern> <directory> <overwrite> - Extract files from the zip file");
        Console.WriteLine("\t-h - Show Help");
        Console.WriteLine("\t-l <filePattern> - List files in the zip file");
        Console.WriteLine("\t-r <filePattern> - Deletes files from the zip file");
        Console.WriteLine();
        Console.WriteLine("\t** options can be passed multiple times and are processed left to right");
        Console.WriteLine();
        Console.WriteLine("Compression:");
        Console.WriteLine("\tOptimal");
        Console.WriteLine("\tFastest");
        Console.WriteLine("\tNoCompression");
        Console.WriteLine("\tSmallestSize");
        return true;
    }
}
