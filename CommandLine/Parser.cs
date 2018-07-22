using System;

namespace FileSystemManager.CommandLine
{
    class Parser
    {
        public string CurrentDirectory { set; get; }
        public Parser(string _currentDirectory)
        {
            CurrentDirectory = " ";
            CurrentDirectory = _currentDirectory;
        }

        public string[] ParseCommands(string inputedValue, char specialSpliter)
        {
            string[] parameters;
            if (specialSpliter == ' ' || specialSpliter == ':' || specialSpliter == '>')
            {
                parameters = inputedValue.Split(specialSpliter);
                return parameters;
            }
            else
            {
                Console.WriteLine($"{specialSpliter}is not recognized.");
                return null;
            }
        }
    }
}
