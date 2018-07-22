using System;
using System.Collections.Generic;

namespace FileSystemManager.CommandLine
{
    class Help
    {
        private Dictionary<string, string> helpItems;

        public Help()
        {
            helpItems = new Dictionary<string, string>
            {
                { "ATTRIB", "print attributes of file" },
                { "CD", "change Directory/move to a specific Folder" },
                { "CLS", "clear the screen" },
                { "COPY", "copy the folder/file to another location without confirmation" },
                { "COPY!", "copy the folder/file to another location with confirmation" },
                { "DATE", "display or set the date" },
                { "DEL", "delete file" },
                { "DIR", "display a list of files and directorys" },
                { "EXIT", "quit from command line" },
                { "HELP", "display all commands" },
                { "History", "display history of commands, inputted by user" },
                { "MD", "create new directory" },
                { "MF", "create new file" },
                { "MOVE", "move file/directory from one directory to another" },
                { "OPEN", "open file like document" },
                { "RD", "remove directory" },
                { "REMANE", "rename a file or files" },
                { "SEARCH", "search the file" },
                { "TYPE", "print a text from file" }
            };
        }

        public void DisplayHelpItems(string[] path)
        {
            foreach (var command in helpItems)
            {
                if (HasKey(path))
                {
                    if (command.Key.ToLowerInvariant() == path[1].ToLowerInvariant())
                    {
                        Console.WriteLine(command.Key + "\t-\t" + command.Value);
                        break;
                    }
                }
                else
                    Console.WriteLine(command.Key + "\t-\t" + command.Value);
            }
        }


        public bool HasKey(string[] path)
        {
            return path.Length > 1;
        }

    }
}
