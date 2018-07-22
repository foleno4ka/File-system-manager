using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication63
{
    class Folder
    {
        //Change of current directory
        public static string Cd(string[] parameters)
        {
            if (parameters[1] == ".." && $"{Directory.GetDirectoryRoot(Directory.GetCurrentDirectory())}" != Directory.GetCurrentDirectory())
            {
                return $"{Directory.GetParent(Directory.GetCurrentDirectory())}";
            }
            else if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), parameters[1])))
                return Path.Combine(Directory.GetCurrentDirectory(), parameters[1]);
            else return Directory.GetCurrentDirectory();
        }

        //Display all directories
        public static void Dir()
        {
            Console.WriteLine(" ");
            List<string> dirs = new List<string>(Directory.EnumerateDirectories(Directory.GetCurrentDirectory()));
            foreach (var dir in dirs)
            {
                Console.WriteLine("{0} \t {1} \t {2}", File.GetCreationTime(dir), "<DIR>",
                    dir.Substring(dir.LastIndexOf(@"\") + 1));
            }
            Files.DisplayFiles();
            Console.WriteLine("{0} directories found.", dirs.Count);
        }

        //Remove
        public static void Rd(string[] parameters)
        {
            if (Directory.Exists(parameters[1]))
            {
                Directory.Delete(parameters[1]);
                Console.WriteLine("The folder deleted successfully!");
            }
            else if (Directory.Exists(Directory.GetCurrentDirectory() + @"\" + parameters[1]))
            {
                Directory.Delete($"{parameters[1]}");
                Console.WriteLine("The folder deleted successfully!");
            }
            else
                Console.WriteLine("The directory doesn't exists");
        }

        //Create directory
        public static void Md(string[] parameters)
        {
            string fileName = parameters.ToString().Substring(3);
            string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            if (Directory.Exists(path))
            {
                Console.WriteLine("That path exists already.");
                return;
            }
            DirectoryInfo di = Directory.CreateDirectory(path);
            Console.WriteLine($"The directory was created successfully at {Directory.GetCreationTime(path)}");
        }

        //Copy of directory
        public static void Copy(string sourcePath, string targetPath)
        {
            string newFoldersPath="";
            if (Directory.Exists(sourcePath))
                newFoldersPath = targetPath + @"\" + sourcePath.Substring(sourcePath.LastIndexOf("\\") + 1);
            else if (Directory.Exists(Directory.GetCurrentDirectory() + @"\" + sourcePath))
                newFoldersPath = targetPath + @"\" + sourcePath;
            if (!Directory.Exists(newFoldersPath))
            {
                DirectoryInfo folder = new DirectoryInfo(targetPath);
                folder.CreateSubdirectory(sourcePath.Substring(sourcePath.LastIndexOf("\\") + 1));
                foreach (string file in Directory.GetFiles(sourcePath))
                {
                    string newFilePath = newFoldersPath + "\\" + Path.GetFileName(file);
                    File.Copy(file, newFilePath);
                }
                foreach (string dir in Directory.GetDirectories(sourcePath))
                {
                    Copy(dir, newFoldersPath + "\\" + Path.GetFileName(dir));
                }
            }
            else
                Console.WriteLine("Thsi directory doesn't exists");
        }

        // override of method CopyFolder(string, string) to CopyFolder(string)
        public static void Copy(string currentDirectory)
        {
            string newFoldersPath = "";
            string sourthName = "";
            string[] parameters = new Parser(currentDirectory).ParseCommands(currentDirectory, '>');
            if (parameters.Length == 0)
                Console.WriteLine("For this action to use the special symbol '>'");
            else
            {
                if (Directory.Exists(parameters[1]))
                {
                    sourthName = parameters[1].Substring(parameters[1].LastIndexOf("\\") + 1);
                    newFoldersPath = (parameters[2] + @"\" + parameters[1].Substring(parameters[1].LastIndexOf("\\") + 1));
                }
                else if (Directory.Exists(Directory.GetCurrentDirectory() + @"\" + parameters[1]))
                {
                    sourthName = ((Directory.GetCurrentDirectory() + @"\" + parameters[1]).Substring(parameters[1].LastIndexOf("\\") + 1));
                    newFoldersPath = parameters[2] + @"\" + parameters[1];
                }
            if (!Directory.Exists(newFoldersPath))
                { 
                    if (!Directory.Exists(newFoldersPath))
                    {
                        DirectoryInfo folder = new DirectoryInfo(parameters[2]);
                        folder.CreateSubdirectory(sourthName);
                        foreach (string file in Directory.GetFiles(parameters[1]))
                        {
                            string newFilePath = newFoldersPath + "\\" + Path.GetFileName(file);
                            File.Copy(file, newFilePath);
                        }
                        foreach (string dir in Directory.GetDirectories(parameters[1]))
                        {
                            Copy(dir, newFoldersPath + "\\" + Path.GetFileName(dir));
                        }

                    }
                }
            }
        }

        //Move directory
        public static void Move(string sourcePath, string targetPath)
        {
            if (Directory.Exists(sourcePath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(sourcePath);

                string destination = Path.Combine(targetPath, directoryInfo.Name);
                Directory.Move(sourcePath, destination);
            }
        }

        // override of method CopyFolder(string, string) to CopyFolder(string)
        public static void Move(string currentDirectory)
        {
            string[] parameters = new Parser(currentDirectory).ParseCommands(currentDirectory, '>');
            if (parameters.Length == 0)
                Console.WriteLine("For this action to use the special symbol '>'");
            else
            {
                if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), parameters[1])))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(parameters[1]);

                    string destination = Path.Combine(parameters[2], directoryInfo.Name);
                    Directory.Move(parameters[1], destination);
                }
            }
        }

        //Get the length of all files and subdirectoies from current path;
        public static List<string> GetDirectoryItems(string dir)
        {
            List<string> dirItems = new List<string>();
            try
            {
                bool isDirRoot = Directory.GetDirectoryRoot(dir) == dir;
                if (!isDirRoot)
                    dirItems.Add("..");
                dirItems.AddRange(Directory.EnumerateDirectories(dir));
                dirItems.AddRange(Directory.EnumerateFiles(dir));
            }catch
            { }
            return dirItems;
        }

        public static long GetDirectorySize(DirectoryInfo dir)
        {
            long size = 0;
            try
            {
                FileInfo[] fis = dir.GetFiles();
                if (fis.Length != 0)
                {
                    foreach (FileInfo fi in fis)
                        size += fi.Length;
                }
            }
            catch { }
            return size;
        }
    }
}
