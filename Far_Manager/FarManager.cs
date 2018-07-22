using FileSystemManager._CommandLine;
using FileSystemManager.Far_Manager.DataLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemManager.Far_Manager.View
{
    class FarManager
    {
        public Draw draw;
        public bool isLeftPanel;
        public bool AreDrivesLeft;
        public bool AreDrivesRight;

        public FarManager()
        {
            draw = new Draw();
            Console.Title = "Far Manager";
        }
        public void Cmd(string path)//create method of drawing cmd;
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            draw.Write(1, Console.WindowHeight - 2, new string(' ', Console.WindowWidth));
            draw.Write(1, Console.WindowHeight - 2, path);
            Console.BackgroundColor = ConsoleColor.Blue;
        }

        public List<string> GetDrives()
        {
            List<string> driverList = new List<string>();
            DriveInfo[] drivers = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drivers)
            {
                driverList.Add(drive.Name);
            }
            return driverList;
        }



        public void Run()
        {
            string currentDirectoryLeft = Directory.GetCurrentDirectory();
            string currentDirectoryRight = Directory.GetCurrentDirectory();
            int selectedIndexPanel1 = 0;
            int selectedIndexPanel2 = 0;
            History history = new History();
            draw.ShowPanel();
            int selectedIndex = 0;
            isLeftPanel = true;
            bool isDrivesList = false;

            draw.DrawPanels(false, selectedIndexPanel1, selectedIndexPanel2, Folder.GetDirectoryItems(currentDirectoryRight));
            ConsoleKeyInfo choice;
            try
            {
                do
                {
                    string curDir = isLeftPanel ? currentDirectoryLeft : currentDirectoryRight;
                    Directory.SetCurrentDirectory(curDir);
                    List<string> leftPanelItems = Folder.GetDirectoryItems(currentDirectoryLeft);
                    List<string> rightPanelItems = Folder.GetDirectoryItems(currentDirectoryRight);
                    if (isLeftPanel && AreDrivesLeft)
                        leftPanelItems = GetDrives();
                    else if (!isLeftPanel && AreDrivesRight)
                        rightPanelItems = GetDrives();
                    else
                        isDrivesList = false;
                    List<string> currentDirectoryItems = isLeftPanel ? leftPanelItems : rightPanelItems;
                    string selectedItem = currentDirectoryItems[selectedIndex];
                    draw.DrawTop(currentDirectoryLeft, currentDirectoryRight);
                    //draw.ShowPanel();

                    draw.DrawPanels(isLeftPanel, selectedIndexPanel1, selectedIndexPanel2, currentDirectoryItems);
                    draw.DrawItemInfo(true, selectedIndexPanel1, selectedIndexPanel2, currentDirectoryItems[selectedIndexPanel1]);
                    draw.DrawItemInfo(false, selectedIndexPanel1, selectedIndexPanel2, currentDirectoryItems[selectedIndexPanel2]);

                    bool isCurrentDirectoryRoot = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()) == Directory.GetCurrentDirectory();
                    Cmd(curDir);


                    choice = Console.ReadKey();
                    int leftCount = leftPanelItems.Count();
                    int rightCount = rightPanelItems.Count();
                    int curDirCount = isLeftPanel ? leftCount : rightCount;
                    switch (choice.Key)
                    {
                        case ConsoleKey.DownArrow:
                            if (selectedIndex == curDirCount - 1)
                                selectedIndex = 0;
                            else
                                selectedIndex++;
                            break;
                        case ConsoleKey.UpArrow:
                            if (selectedIndex == 0)
                                selectedIndex = curDirCount - 1;
                            else
                                selectedIndex--;
                            break;
                        case ConsoleKey.LeftArrow:
                            selectedIndex = 0;
                            break;
                        case ConsoleKey.RightArrow:
                            selectedIndex = curDirCount - 1;
                            break;
                        case ConsoleKey.F1:
                            new Tips().ShowTips();
                            Console.ReadKey();
                            Console.Clear();
                            draw.ShowPanel();
                            draw.DrawPanels(isLeftPanel, selectedIndexPanel1, selectedIndexPanel2, currentDirectoryItems);
                            draw.DrawPanels(false, selectedIndexPanel1, selectedIndexPanel2, Folder.GetDirectoryItems(currentDirectoryRight));
                            break;
                        case ConsoleKey.F2:
                            Console.Clear();
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.White;
                            history.ShowHistory();
                            Console.ReadKey();
                            Console.Clear();
                            draw.ShowPanel();
                            draw.DrawPanels(isLeftPanel, selectedIndexPanel1, selectedIndexPanel2, currentDirectoryItems);
                            draw.DrawPanels(false, selectedIndexPanel1, selectedIndexPanel2, Folder.GetDirectoryItems(currentDirectoryRight));
                            break;
                        case ConsoleKey.F3:
                            new CommandLine().Run();
                            break;
                        case ConsoleKey.F4:
                            {
                                if (isLeftPanel)
                                {
                                    isLeftPanel = false;
                                    Files.Copy(currentDirectoryItems[selectedIndexPanel1], currentDirectoryRight);
                                    Folder.Copy(currentDirectoryItems[selectedIndexPanel1], currentDirectoryRight);

                                }
                                else
                                {
                                    Files.Copy(currentDirectoryItems[selectedIndexPanel2], currentDirectoryLeft);
                                    Folder.Copy(currentDirectoryItems[selectedIndexPanel2], currentDirectoryLeft);
                                }
                                break;
                            }
                        case ConsoleKey.F5:
                            {
                                if (isLeftPanel)
                                {
                                    isLeftPanel = false;
                                    Files.Move(currentDirectoryItems[selectedIndexPanel1], currentDirectoryRight);
                                    Folder.Move(currentDirectoryItems[selectedIndexPanel1], currentDirectoryRight);
                                }

                                else
                                {
                                    Files.Move(currentDirectoryItems[selectedIndexPanel2], currentDirectoryLeft);
                                    Folder.Move(currentDirectoryItems[selectedIndexPanel2], currentDirectoryLeft);
                                }
                            }
                            break;
                        case ConsoleKey.F6:
                            {
                                List<string> drives = GetDrives();
                                isDrivesList = true;
                                if (isLeftPanel)
                                {
                                    leftPanelItems = drives;
                                    currentDirectoryLeft = drives[0];
                                    selectedIndexPanel1 = 0;
                                }
                                else
                                {
                                    rightPanelItems = drives;
                                    currentDirectoryRight = drives[0];
                                    selectedIndexPanel2 = 0;
                                }
                                curDir = isLeftPanel ? currentDirectoryLeft : currentDirectoryRight;
                                currentDirectoryItems = isLeftPanel ? leftPanelItems : rightPanelItems;
                                draw.ClearPanel(isLeftPanel);
                                draw.DrawTop(leftPanelItems[selectedIndexPanel1], rightPanelItems[selectedIndexPanel2]);
                                draw.DrawPanels(isLeftPanel, selectedIndexPanel1, selectedIndexPanel2, drives);
                                //Console.ReadKey();
                                break;
                            }
                        case ConsoleKey.F11:
                            Console.Clear();
                            Environment.Exit(0);
                            break;
                        case ConsoleKey.Enter:
                            {
                                isDrivesList = false;
                                string newPath = Directory.GetCurrentDirectory();
                                if (!isCurrentDirectoryRoot && selectedIndex == 0)
                                    newPath = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
                                else if (Directory.Exists(selectedItem))
                                {
                                    newPath = selectedItem;
                                    selectedIndex = 0;
                                }
                                else
                                    Files.OpenDocument(selectedItem);

                                if (isLeftPanel)
                                    currentDirectoryLeft = newPath;
                                else
                                    currentDirectoryRight = newPath;
                                break;
                            }
                        case ConsoleKey.Tab:
                            isLeftPanel = !isLeftPanel;
                            if (isDrivesList && AreDrivesLeft && !AreDrivesRight && !isLeftPanel)
                                isDrivesList = !isDrivesList;
                            else if (isDrivesList && AreDrivesRight && !AreDrivesLeft && isLeftPanel)
                                isDrivesList = !isDrivesList;
                            else if (isDrivesList && AreDrivesRight && AreDrivesLeft)
                                isDrivesList = true;
                            else if (!isDrivesList && AreDrivesLeft && !AreDrivesRight && isLeftPanel)
                                isDrivesList = true;
                            else if (!isDrivesList && !AreDrivesLeft && AreDrivesRight && isLeftPanel)
                                isDrivesList = true;
                            selectedIndex = isLeftPanel ? selectedIndexPanel1 : selectedIndexPanel2;
                            break;
                    }

                    if (isLeftPanel)
                    {
                        selectedIndexPanel1 = selectedIndex;
                        AreDrivesLeft = isDrivesList;
                    }
                    else
                    {
                        selectedIndexPanel2 = selectedIndex;
                        AreDrivesRight = isDrivesList;
                    }
                    draw.ClearPanel(isLeftPanel);
                    history.AddCommand(choice.Key.ToString());
                } while (true);
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

        }
    }
}
