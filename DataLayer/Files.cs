using FileSystemManager._CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemManager.Far_Manager.DataLayer
{
    class Files
    {
        // group renaming of files of current Directory
        public static void GroupRename()
        {
            List<string> files = new List<string>();
            files.AddRange(Directory.EnumerateFiles(Directory.GetCurrentDirectory()));
            if (files.Count == 0)
                Console.WriteLine("This directory doesn't have any file");
            foreach (string file in files)
            {
                Console.WriteLine("To stop input: cancel");
                Console.WriteLine("Current file: " + Path.GetFileName(file));
                Console.Write("Input new name for file with extention:");

                string newFileName = Console.ReadLine();
                if (newFileName != "cancel")
                {
                    Directory.Move(file, Path.Combine(Directory.GetCurrentDirectory(), newFileName));
                }
                else
                    break;
            }
        }

        //Dispaly main characterictics of file
        public static void Attrib(string[] parameters)
        {
            string file = System.IO.Directory.GetCurrentDirectory() + @"\" + parameters[1];

            System.IO.FileInfo oFileInfo = new System.IO.FileInfo(file);
            Console.WriteLine("My File's Name: \"" + oFileInfo.Name + "\"");
            DateTime dtCreationTime = oFileInfo.CreationTime;
            Console.WriteLine("Date and Time File Created: " + dtCreationTime.ToString());
            Console.WriteLine("myFile Extension: " + oFileInfo.Extension);
            Console.WriteLine("myFile total Size: " + oFileInfo.Length.ToString());
            Console.WriteLine("myFile filepath: " + oFileInfo.DirectoryName);
            Console.WriteLine("My File's Full Name: \"" + oFileInfo.FullName + "\"");
        }

        //Read file by printing it in Console
        public static void OpenInConsole(string[] parameters)
        {
            string path = parameters[1];
            if (!File.Exists(path))
            {
                string createText = "Hello and Welcome" + Environment.NewLine;
                File.WriteAllText(path, createText);
            }
            string appendText = "This is extra text" + Environment.NewLine;
            File.AppendAllText(path, appendText);
            string readText = File.ReadAllText(path);
            Console.WriteLine(readText);
        }

        //Open file in document with different extentions
        public static void OpenDocument(string currentDirectory)
        {
            if (File.Exists(currentDirectory))
                System.Diagnostics.Process.Start(currentDirectory);
        }

        //Move file from current directory to target, that noticed in parameters
        public static void Move(string currentDirectory)
        {
            string[] parameters = new Parser(currentDirectory).ParseCommands(currentDirectory, '>');
            if (parameters.Length == 0)
                Console.WriteLine("For this action to use the special symbol '>'");
            else
            {
                if (File.Exists(parameters[1]))
                {
                    string newDirectory = parameters[2];
                    string file = Directory.GetCurrentDirectory() + @"\" + parameters[1];
                    if (File.Exists(newDirectory + @"\" + parameters))
                        File.Delete(newDirectory + @"\" + parameters);
                    File.Move(file, newDirectory);
                }
            }
        }

        //overrided metod
        public static void Move(string sourcePath, string targetPath)
        {
            if (File.Exists(sourcePath))
                File.Move(sourcePath, targetPath + @"\" + sourcePath.Substring(sourcePath.LastIndexOf("\\") + 1));
        }
        //create file
        public static void Mf(string[] parameters)
        {
            string path = parameters[1];
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (FileStream fs = File.Create(path))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                fs.Write(info, 0, info.Length);
            }
            using (StreamReader sr = File.OpenText(path))
            {
                string currentString = "";
                while ((currentString = sr.ReadLine()) != null)
                {
                    Console.WriteLine(currentString);
                }
            }
        }
        //search file
        public static void Search(string[] parameters)
        {
            string[] dirs = Directory.GetFiles(Directory.GetCurrentDirectory(), parameters[1]);
            Console.WriteLine("The number such files: {0},{1}", dirs.Length, parameters[1]);
            foreach (string file in dirs)
            {
                Console.WriteLine(file);
            }
        }

        //copy file
        public static void Copy(string sourcePath, string targetPath)
        {
            if (File.Exists(sourcePath))
            {
                targetPath = targetPath + @"\" + sourcePath.Substring(sourcePath.LastIndexOf("\\") + 1);
                File.Copy(sourcePath, targetPath);
            }

        }

        //Copy of file(overrided)
        public static void Copy(string enter)
        {
            string[] copyFile = enter.Split('>');
            if (File.Exists(Directory.GetCurrentDirectory() + @"\" + copyFile[1]))
            {
                string newDirectory = copyFile[2];
                string file = Directory.GetCurrentDirectory() + @"\" + copyFile[1];
                File.Copy(file, newDirectory);
            }
        }

        //Delete file
        public static void Delete(string[] parameters)
        {
            if (File.Exists(parameters[1]))
            {
                string path = parameters[1];
                File.Delete(path);
                Console.WriteLine("The file deleted successfully!");
            }
            else
                Console.WriteLine($"File: {parameters[1]} doesn't exist");
        }

        //overrided method, that has only path of file
        public static void Delete(string currentDirectory)
        {
            if (File.Exists(currentDirectory))
            {
                File.Delete(currentDirectory);
                Console.WriteLine("The file deleted successfully!");
            }
            else
                Console.WriteLine($"File: {currentDirectory} doesn't exist");
        }

        public static void DisplayFiles()
        {
            List<string> files = new List<string>(Directory.EnumerateFiles(Directory.GetCurrentDirectory()));
            if (files.Count != 0)
            {
                foreach (var file in files)
                {
                    Console.WriteLine("{0} \t {1} \t {2}", File.GetCreationTime(file), "<DIR>",
                        file.Substring(file.LastIndexOf(@"\") + 1));
                }
            }
            Console.WriteLine("{0} files found.", files.Count);
        }
    }
}
