using System;
using System.Collections.Generic;

namespace FileSystemManager.Far_Manager.View
{
    class Tips
    {
        public Dictionary<int, string> tips;
        public Tips()
        {
            tips = new Dictionary<int, string>
            {
                { 1, "Help" },
                { 2, "History" },
                { 3, "Command Line" },
                { 4, "Copy" },
                { 5, "Move" },
                { 6, "Drivers" },
                { 11, "Quit" }
            };
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
