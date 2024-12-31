
using AbroadConcepts.CommandLine;
using AbroadConcepts.IO;

MainArg mainArg = new();
AddArg addArg = new();
PatternArg removeArg = new();
ExtractArg extractArg = new();
PatternArg listArg = new();

var cmdLine = new CommandArguements();
cmdLine.Register("-a", addArg);
cmdLine.Register("-r", removeArg);
cmdLine.Register("-e", extractArg);
cmdLine.Register("-l", listArg);

if (cmdLine.Parse(mainArg, args))
{

    mainArg.ZipFile = mainArg.ZipFile.EnsureExtension(".zip").GetFiles(false, true).First().ToString();
    using var stream = File.Open(mainArg.ZipFile, FileMode.OpenOrCreate);
    using ZipArchiver zipArchiver = new ZipArchiver(stream);
    foreach (var option in cmdLine.Options)
    {
        switch (option)
        {
            case "-a":
                zipArchiver.Add(addArg.Filename, addArg.Override, addArg.Compression);
                break;
            case "-r":
                zipArchiver.Remove(removeArg.Pattern);
                break;
            case "-e":
                zipArchiver.Extract(extractArg.Directory, extractArg.Overwrite,  extractArg.Pattern);
                break;
            case "-l":
                zipArchiver.GetEntries(listArg.Pattern);
                break;
        }

    }

    zipArchiver.Save(stream);
}
else
{
    ShowHelp(cmdLine);
}

static void ShowHelp(CommandArguements cmdLine)
{
    Console.WriteLine("ZipCmd - Command line to operate on a zip file.");
    Console.WriteLine();
    Console.WriteLine(cmdLine.Message);
    Console.WriteLine();
    Console.WriteLine("Command: ZipCmd.exe <ZipFilename> [-h|/h] [-a|/a <filePattern> <overwrite> <compression> ]  [-d|/d <filePattern> ]  [-e|/e <filePattern> ]  [-l|/l <filePattern> ]");
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("Options:");
    Console.WriteLine("\t-a or /a <filePattern> <overwrite> <compression> - Adds files to the zip file");
    Console.WriteLine("\t-r or /r <filePattern> - Deletes files from the zip file");
    Console.WriteLine("\t-e or /e <filePattern> <directory> <overwrite> - Extract files from the zip file");
    Console.WriteLine("\t-h or /h - Show Help");
    Console.WriteLine("\t-l or /l <filePattern> - List files in the zip file");
}



