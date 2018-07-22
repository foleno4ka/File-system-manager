using FileSystemManager.Far_Manager.DataLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemManager.Far_Manager.View
{
    class Draw
    {
        private const int WIDTH = 98;
        private const int HEIGHT = 40;

        private readonly int width;
        private readonly int height;
        //private int columnWidth;
        private readonly int columnHeight;
        private readonly int startFirstColumnX;
        private readonly int startSecondColumnX;
        private readonly int startThirdColumnX;
        private readonly int startFourthColumnX;
        private readonly int startColumnY;
        private readonly int widthEveryColumn;
        private int startIterPanel1;
        private int endIterPanel1;
        private int startIterPanel2;
        private int endIterPanel2;

        public Draw()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetWindowSize(WIDTH + 1, HEIGHT + 2);
            Console.SetBufferSize(WIDTH + 2, HEIGHT + 2);
            width = WIDTH;
            height = HEIGHT;
            startIterPanel1 = 0;
            columnHeight = height - height / 6;
            startColumnY = 3;
            endIterPanel1 = (columnHeight - startColumnY) * 2;
            startIterPanel2 = 0;
            endIterPanel2 = (columnHeight - startColumnY) * 2;
            //columnWidth = width / 4;
            widthEveryColumn = 23;
            startFirstColumnX = 1;
            startSecondColumnX = widthEveryColumn + 2;
            startThirdColumnX = widthEveryColumn * 2 + 5;
            startFourthColumnX = widthEveryColumn * 3 + 6;
        }
        public void Write(int left, int top, string item)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(item);
        }

        public void DrawTime()
        {
            DateTime time = DateTime.Now;
            Write(width - 6, 0, $"{time.Hour}:{time.Minute}");
        }

        public void DrawTop(string curDirLeft, string curDirRight)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Write(1, 0, new string(' ', width));
            Write(1, 0, new string('═', width));

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            DrawTime();

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            // Draw disk names


            //int address = widthEveryColumn * 3 + 5 - curDirRight.Length / 2;
            if (curDirLeft.Length > widthEveryColumn * 2)
                Write(2, 0, $" {curDirLeft} ".Remove(widthEveryColumn * 2 - 1));
            else
                Write((widthEveryColumn * 2 + 1 - curDirLeft.Length) / 2, 0, $" {curDirLeft} ");
            if (curDirRight.Length > widthEveryColumn * 2)
                Write(widthEveryColumn * 2 + 6, 0, $" {curDirRight} ".Remove(widthEveryColumn * 2 - 1));
            else
                Write(widthEveryColumn * 3 + 5 - curDirRight.Length / 2, 0, $" {curDirRight} ");
            // Write(width - width / 4, 0, $"{Directory.GetDirectoryRoot(Directory.GetCurrentDirectory().ToUpper())}");
            // Draw column borders
            Write(0, 0, "╔");
            Write(widthEveryColumn * 2 + 3, 0, "╗");
            Write(widthEveryColumn * 2 + 4, 0, "╔");
            Write(widthEveryColumn * 4 + 6, 0, "╗");
        }

        public void DrawColumns()
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 1; i < height - 1; i++)
            {
                Write(0, i, "║");
                Write(widthEveryColumn * 2 + 3, i, "║");
                Write(widthEveryColumn * 2 + 4, i, "║");
                Write(widthEveryColumn * 4 + 6, i, "║");
                if (i < columnHeight)
                {
                    Write(widthEveryColumn + 1, i, "│");
                    Write(widthEveryColumn * 3 + 5, i, "│");
                }
            }

            for (int i = 1; i < width; i++)
                if (i != widthEveryColumn * 2 + 3 && i != widthEveryColumn * 2 + 4)
                    Write(i, columnHeight, "─");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Write((widthEveryColumn - "Name".Length) / 2, 1, "Name");
            Write((widthEveryColumn * 3 - "Name".Length) / 2, 1, "Name");
            Write(width - width / 8, 1, "Name");
            Write(width / 8 + width / 2, 1, "Name");

            Console.ForegroundColor = ConsoleColor.White;
        }

        public void DrawBottom()
        {
            for (int i = 1; i < width; i++)
                Write(i, height - 1, "═");

            Write(0, height - 1, "╚");
            Write(widthEveryColumn * 2 + 3, height - 1, "╝");
            Write(widthEveryColumn * 2 + 4, height - 1, "╚");
            Write(widthEveryColumn * 4 + 6, height - 1, "╝");
        }
        public void DrawTips()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Tips tipItems = new Tips();
            int x = 2;
            for (int i = height; i < height + 2; i++)
                Write(0, height, new string(' ', width + 2));
            foreach (KeyValuePair<int, string> tip in tipItems.tips)
            {
                string key = $"F{tip.Key}";
                string value = $"{tip.Value}";
                Write(x, height + 1, key);
                x += key.Length;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Write(x, height + 1, value);
                x += value.Length + 1;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void ShowPanel()
        {
            Console.Clear();
            // DrawTop();
            DrawColumns();
            DrawBottom();
            DrawTips();
        }
        public void DrawItemInfo(bool isActive, int selectedIndexPanel1, int selectedIndexPanel2, string item)
        {
            if (isActive)
                WriteInfo(item, startFirstColumnX, widthEveryColumn * 2, 3);
            else
                WriteInfo(item, startThirdColumnX, widthEveryColumn * 4, 5);
        }
        public void DrawPanels(bool isActive, int selectedIndexPanel1, int selectedIndexPanel2, List<string> items)
        {
            if (isActive)
                DrawPanel(true, selectedIndexPanel1, items);
            else
                DrawPanel(false, selectedIndexPanel2, items);
        }

        public void DrawPanel(bool isLeftPanel, int selectedIndex, List<string> items)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            bool isLongList = false;
            int x = isLeftPanel ? startFirstColumnX : startThirdColumnX;
            int y = startColumnY;

            int length = items.Count;
            if (items.Count > (columnHeight - startColumnY) * 2)
            {
                if (selectedIndex == 0)
                    selectedIndex = isLeftPanel ? startIterPanel1 : startIterPanel2;
                length = isLeftPanel ? endIterPanel1 : endIterPanel2;
                isLongList = true;
            }
            for (int i = isLeftPanel ? startIterPanel1 : startIterPanel2; i < length; i++)
            {
                string item = items[i];
                if (y == columnHeight && (x == startFirstColumnX || x == startThirdColumnX))
                {
                    x = isLeftPanel ? startSecondColumnX : startFourthColumnX;
                    y = startColumnY;
                }
                string itemName = item.Substring(item.LastIndexOf("\\") + 1);
                if (itemName.Length == 0)
                    itemName = item;
                int itemLength = itemName.Length;

                if (i == selectedIndex)
                {
                    if ((isLongList) && selectedIndex == length - 1 && selectedIndex != items.Count - 1)
                    {
                        if (isLeftPanel)
                        {
                            startIterPanel1++;
                            endIterPanel1++;
                        }
                        else
                        {
                            startIterPanel2++;
                            endIterPanel2++;
                        }
                    }
                    else if ((isLongList) && (selectedIndex == startIterPanel1 || selectedIndex == startIterPanel2)
                        && i != 0)
                    {
                        if (isLeftPanel)
                        {
                            startIterPanel1--;
                            endIterPanel2--;
                        }
                        else
                        {
                            startIterPanel2--;
                            endIterPanel2--;
                        }
                    }
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                else
                    Console.BackgroundColor = ConsoleColor.Blue;

                if (itemLength > (widthEveryColumn - 1))
                {
                    Write(x, y, new string(' ', widthEveryColumn - 1));
                    Write(x, y, $"{itemName.Remove(widthEveryColumn - 1)}>");
                }
                else
                {
                    Write(x, y, new string(' ', widthEveryColumn - 1));
                    Write(x, y, $"{itemName}");
                }

                y++;
            }
        }

        public void ClearPanel(bool isActive)
        {
            int xFirst = isActive ? startFirstColumnX : startThirdColumnX;
            int xSecond = isActive ? startSecondColumnX : startFourthColumnX;
            int y = startColumnY;
            int x = xFirst;
            for (int i = 0; i < (columnHeight - startColumnY) * 2; i++)
            {
                if (y == columnHeight && x == xFirst)
                {
                    x = xSecond;
                    y = startColumnY;
                }
                Write(x, y, new string(' ', widthEveryColumn));
                y++;
            }
        }

        public void ItemsCount(string currentDirectory, ref int fileCount, ref int folderCount)
        {
            try
            {
                fileCount = Directory.EnumerateFiles(currentDirectory).Count();
                folderCount = Directory.EnumerateDirectories(currentDirectory).Count();
            }
            catch { }
        }

        public void WriteInfo(string currentDirectory, int columnX, int cWidth, int borderNumber)
        {
            int fCount = 0;
            int folCount = 0;
            Console.BackgroundColor = ConsoleColor.Blue;
            if (File.Exists(currentDirectory))
            {
                long length = new FileInfo(currentDirectory).Length;
                Write(cWidth - 21, columnHeight + 1, $"{length}");
            }
            int position;
            if (columnX < widthEveryColumn)
            {
                Write(columnX, columnHeight + 1, new string(' ', widthEveryColumn * 2));
                Write(columnX, columnHeight + 4, new string(' ', widthEveryColumn * 2));
                if (Directory.Exists(currentDirectory))
                {
                    Write(cWidth - widthEveryColumn - 1, columnHeight + 1, "Folder ");
                }
                position = (cWidth + borderNumber - ($"Bytes:{Folder.GetDirectorySize(Directory.GetParent(currentDirectory))} Files: {fCount} Folders: {folCount}".Length)) / 2;
                Write(columnX, columnHeight + 1, currentDirectory);
                Write(cWidth - 15, columnHeight + 1,
            $"{Directory.GetCreationTime(currentDirectory).ToShortDateString()} {Directory.GetCreationTime(currentDirectory).ToShortTimeString()}");
            }
            else
            {
                Write(columnX, columnHeight + 1, new string(' ', widthEveryColumn * 2));
                Write(columnX, columnHeight + 4, new string(' ', widthEveryColumn * 2));
                if (Directory.Exists(currentDirectory))
                {
                    Write(cWidth - 18, columnHeight + 1, "Folder ");
                }
                position = widthEveryColumn * 3 + 5 - ($"Bytes:{Folder.GetDirectorySize(Directory.GetParent(currentDirectory))} Files: {fCount} Folders: {folCount}".Length / 2);
                Write(columnX, columnHeight + 1, currentDirectory.Substring(currentDirectory.LastIndexOf('\\') + 1));
                Write(cWidth - 13, columnHeight + 1,
            $"{Directory.GetCreationTime(currentDirectory).ToShortDateString()} {Directory.GetCreationTime(currentDirectory).ToShortTimeString()}");
            }
            if (Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()) != Directory.GetCurrentDirectory())
            {
                ItemsCount($"{Directory.GetParent(currentDirectory)}", ref fCount, ref folCount);
                Write(position, columnHeight + 5, $"Bytes:{Folder.GetDirectorySize(Directory.GetParent(currentDirectory))} Files: {fCount} Folders: {folCount}");
            }
            else
            {
                ItemsCount(Directory.GetCurrentDirectory(), ref fCount, ref folCount);
                Write(position, columnHeight + 5, $"Bytes:{Folder.GetDirectorySize(Directory.GetParent(currentDirectory))} Files: {fCount} Folders: {folCount}");
            }
            Write(columnX, columnHeight + 1, currentDirectory.Substring(currentDirectory.LastIndexOf("\\") + 1));
        }
    }
}
