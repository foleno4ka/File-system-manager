using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemManager._CommandLine
{
    class Help
    {
        ///
        private Dictionary<string, string> helpItems;

        public Help()
        {
            helpItems = new Dictionary<string, string>();
            helpItems.Add("ATTRIB", "print attributes of file");
            helpItems.Add("CD", "change Directory/move to a specific Folder");
            helpItems.Add("CLS", "clear the screen");
            helpItems.Add("COPY", "copy the folder/file to another location without confirmation");
            helpItems.Add("COPY!", "copy the folder/file to another location with confirmation");
            helpItems.Add("DATE", "display or set the date");
            helpItems.Add("DEL", "delete file");
            helpItems.Add("DIR", "display a list of files and directorys");
            helpItems.Add("EXIT", "quit from command line");
            helpItems.Add("HELP", "display all commands");
            helpItems.Add("History", "display history of commands, inputted by user");
            helpItems.Add("MD", "create new directory");
            helpItems.Add("MF", "create new file");
            helpItems.Add("MOVE", "move file/directory from one directory to another");
            helpItems.Add("OPEN", "open file like document");
            helpItems.Add("RD", "remove directory");
            helpItems.Add("REMANE", "rename a file or files");
            helpItems.Add("SEARCH", "search the file");
            helpItems.Add("TYPE", "print a text from file");
        }

        public void DisplayHelpItems(string[] path)
        {
            foreach (var command in helpItems)
            {
                if (hasKey(path))
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


        public bool hasKey(string[] path)
        {
            return path.Length > 1;

        }

        public static void DisplayComands()
        {
            
        }
    }
}
