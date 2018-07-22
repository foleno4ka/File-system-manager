using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication63
{
    class CommandLine
    {

        private string currentDirectory;


        public void Date()
        {
            DateTime localDate = DateTime.Now;
            String[] cultureNames = { "en-US", "en-GB", "fr-FR",
                                "de-DE", "ru-RU" };

            foreach (var cultureName in cultureNames)
            {
                var culture = new CultureInfo(cultureName);
                Console.WriteLine("{0}: {1}", cultureName,
                                  localDate.ToString(culture));
            }
        }

        public void Run()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("<version 1.0>,\nby Fedai O, 2017>");
                Directory.SetCurrentDirectory(@"C:\");
                this.currentDirectory = Directory.GetCurrentDirectory();
                Console.Write(currentDirectory + "> ");
                History historyList = new History();
                do
                {
                    string enter = Console.ReadLine();
                    string[] parameters = new Parser(enter).ParseCommands(enter, ' ');
                    if(parameters.Length==1)
                       parameters = new Parser(enter).ParseCommands(enter, '>');
                    if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), parameters[0])) && Path.IsPathRooted(parameters[0]) && parameters[0].Length <=2)
                        currentDirectory = Path.Combine(Directory.GetCurrentDirectory(), parameters[0]).ToUpper();
                    else if (enter == "")
                    { }
                    else
                    {
                        switch (parameters[0].ToLowerInvariant())
                        {
                            case "help":
                                new Help().DisplayHelpItems(parameters);
                                break;
                            case "history":
                                historyList.ShowHistory();
                                break;
                            case "attrib":
                                Files.Attrib(parameters);
                                break;
                            case "cls":
                                Console.Clear();
                                break;
                            case "dir":
                                Folder.Dir();
                                break;
                            case "cd":
                                currentDirectory = Folder.Cd(parameters);
                                break;
                            case "date":
                                Date();
                                break;
                            case "rd":
                                Folder.Rd(parameters);
                                break;
                            case "exit":
                                Environment.Exit(0);
                                break;
                            case "search":
                                Files.Search(parameters);
                                break;
                            case "copy":
                                Files.Copy(enter);
                                Folder.Copy(enter);
                                break;
                            case "type":
                                Files.OpenInConsole(parameters);
                                break;
                            case "md":
                                Folder.Md(parameters);
                                break;
                            case "del":
                                Files.Delete(parameters);
                                break;
                            case "open":
                                if(File.Exists(enter))
                                Files.OpenDocument(enter);
                                else
                                    Console.WriteLine("File doesn't exists");
                                break;
                            case "mf":
                                Files.Mf(parameters);
                                break;
                            case "move":
                                Folder.Move(enter);
                                Files.Move(enter);
                                break;
                            case "rename":
                                Files.GroupRename();
                                break;
                            default:
                                Console.WriteLine($"{enter} is not recognized as an internal or external command, operable program batch file.");
                                break;
                        }
                    }
                    //Console.ReadKey();
                    Directory.SetCurrentDirectory(currentDirectory);
                    Console.Write(Path.GetFullPath(Directory.GetCurrentDirectory()) + "> ");
                    historyList.AddCommand(enter);
                } while (true);
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
