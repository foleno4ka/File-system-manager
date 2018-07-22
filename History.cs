using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication63
{
    class History
    {
        public List<string> historyCommands;

        public History()
        {
            historyCommands = new List<string>();
        }
        public void AddCommand(string command)
        {
            historyCommands.Add(command);

        }

        public void ShowHistory()
        {
            foreach (String command in historyCommands)
            {
                Console.WriteLine(command + " ");
            }
        }
    }
}
