# ZipApp
Provides zip file operation from a command line.   


# Notes

This zip tool Add feature allows for wild card patterns in file.   Unlike most wild card pattern matching, this tool allows for multiple wild card patterns such as '*' or '?' can appear multiple times.  
For example "ZipCmd .\test -a C:\Some*\Sub*" would be consider valid and would match files and directories that start with "Sub" that appear in directory that start with "Some" under the C drive. 

The File matching pattern routines return fully qualified names. In order to inject the file information in the zip file, an entry level mechanism is implemented.  This means that new files can be added
to the zip file and customize the directory structure as it will show in the zip files.  A directory in the zip file container will end with "/" character.   For example, you may have a test.zip that when you run
"ZipCmd test -l" that it will show:

test.txt 
Folder/test2.txt
Folder/

Note: "Folder/" is a directory called Folder and there is a file in it by the name of test2.txt.

If the fully qualified name is "C:\SomeDir\SubDir\Folder\test2.txt" and the following commands are ran:

ZipCmd test -a C:\SomeDir\SubDir\Folder\test2.txt true SmallestSize 1 -a C:\SomeDir\SubDir\Folder\test2.txt true SmallestSize 2
ZipCmd test -l

You will have zip file called test.zip and contains

Folder/test2.txt
SubDir/Folder/test2.txt

Note: The "1" in the entry level say go up 1 level so Folder is included in name while "2" goes up two levels so includes SubDir in entry name.

