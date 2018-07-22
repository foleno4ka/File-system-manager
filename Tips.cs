using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication63
{
    class Tips
    {
        public Dictionary<int, string> tips;
        public Tips()
        {
            tips = new Dictionary<int, string>();
            tips.Add(1, "Help");
            tips.Add(2, "History");
            tips.Add(3, "Command Line");
            tips.Add(4, "Copy");
            tips.Add(5, "Move");
            tips.Add(6, "Drivers");
            tips.Add(11, "Quit");
        }
        public void ShowTips()
        {
            
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;           
            foreach (KeyValuePair<int, string> tip in tips)
            {
                Console.WriteLine($"F{tip.Key}" + "\t-\t" + tip.Value);
            }
        }
    }
}
