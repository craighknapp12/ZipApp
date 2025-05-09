# ZipApp
Provides zip file operation from a command line.   

```
Command: ZipCmd.exe <ZipFilename> 
	[-a <filePattern> <override> <compression> <entryLevel>] 
	[-e <filePattern> <directory> <overwrite] 
	[-l <filePattern> ]
	[-h] 
	[-r <filePattern> ]  
```
Reference Guide:
```
-a : Adds files or directories into the ZipFilename
-e : Extracts files or directory entries and places them at the provided directory location.
-h : Show Help content (Also displays when there are errors)
-l : List all or matching pattern file entries
-r : Removes a file or directory entry from the zip file.
```

# Notes

This zip tool Add feature allows for wild card patterns in file.   Unlike most wild card pattern matching, this tool allows for multiple wild card patterns such as '\*' or '?' can appear multiple times.  For example "ZipCmd .\test -a C:\Some\*\Sub\*" would be consider valid command and would match files and directories that start with "Sub" that appear in directory that start with "Some" under the C drive. 

The File matching pattern routines return fully qualified names. In order to inject the file information in the zip file, an entry level mechanism is implemented.  This means that new files can be added to the zip file and customize the directory structure as it will show in the zip files.  The entry level is zero base and represents how many directory levels to capture in the name.  A directory in the zip file container will end with "/" character.   For example, you may have a test.zip that when you run "ZipCmd test -l" that it will show:

```
test.txt 
Folder/test2.txt
Folder/
```

Note: "Folder/" is a directory called Folder and there is a file in it by the name of test2.txt.

If the fully qualified name is "C:\SomeDir\SubDir\Folder\test2.txt" and the following commands are ran:

```
ZipCmd test2 -a C:\SomeDir\SubDir\Folder\test2.txt true SmallestSize 1 -a C:\SomeDir\SubDir\Folder\test2.txt true SmallestSize 2
ZipCmd test2 -l
```


In this hypothetical example, you will have zip file called test2.zip and contains:

```
Folder/test2.txt
SubDir/Folder/test2.txt
```

Note: The "1" in the entry level say go up 1 level so Folder is included in name while "2" goes up two levels so includes SubDir in entry name.

