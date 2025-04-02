
using AbroadConcepts.CommandLine;
using AbroadConcepts.IO;
using ZipCmd.Models;

MainArgument mainArg = new();
AddArgument addArg = new();
PatternArgument removeArg = new();
ExtractArgument extractArg = new();
PatternArgument listArg = new();

var cmdLine = new CommandArguments();
cmdLine.Register("-a", addArg);
cmdLine.Register("-r", removeArg);
cmdLine.Register("-e", extractArg);
cmdLine.Register("-l", listArg);

if (cmdLine.Parse(mainArg, args))
{

    var filenames = mainArg.ZipFile.EnsureExtension(".zip").GetFiles(false, true).ToList();

    if (filenames.Count == 1)
    {
        using var stream = File.Open(filenames[0], FileMode.OpenOrCreate);
        using ZipArchiver zipArchiver = new(stream);
        foreach (var option in cmdLine.Options)
        {
            switch (option)
            {
                case "-a":
                    zipArchiver.Add(addArg.Filename, addArg.EntryLevel, addArg.Override, addArg.Compression);
                    zipArchiver.Save(stream);
                    break;
                case "-r":
                    zipArchiver.Remove(removeArg.Pattern);
                    zipArchiver.Save(stream);
                    break;
                case "-e":
                    zipArchiver.Extract(extractArg.Directory, extractArg.Overwrite, extractArg.Pattern);
                    break;
                case "-l":
                    foreach (var s in zipArchiver.GetEntries(listArg.Pattern))
                    {
                        Console.WriteLine(s);
                    }
                    break;
            }
        }
    }
    else
    {
        Console.WriteLine("ZipCmd - ZipFileName pattern does not reference a single filename.\n");
        foreach (var filename in filenames)
        {
            Console.WriteLine(filename);
        }
    }
}
else
{
    ShowHelp(cmdLine.Message);
}

static void ShowHelp(string message)
{
    Console.WriteLine("ZipCmd - Command line to operate on a zip file.");
    Console.WriteLine();
    Console.WriteLine(message);
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



