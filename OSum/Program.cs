using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace OSum
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }

        public static void MakeFolder(string directory)
        {
            Console.WriteLine("Want to Create Folder? if Yes then Press Y  and if you want to create File Press N");
            string answer = Console.ReadLine();
            if (answer == "y" || answer == "Y")
            {
                Console.WriteLine("Enter Folder Name");
                string foldername = Console.ReadLine();
                directory = directory + "//" + foldername;



                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    Console.WriteLine("Folder Created Successfully");
                    Console.ReadKey();
                    Console.Clear();
                    FileManagementMenu();
                }
                else
                {
                    Console.WriteLine("Directory Already Exists");
                    Console.Clear();
                    FileManagementMenu();
                }
            }
            else if (answer == "n" || answer == "N")
            {
                Console.WriteLine("Enter File Name without extension");
                string filename = Console.ReadLine();

                Console.WriteLine("Enter File Extension without dot");
                string extension = Console.ReadLine();

                string completfile = filename + "." + extension;
                directory = directory + "\\" + completfile;
                if (!File.Exists(directory))
                {
                    using (FileStream fs = File.Create(directory))
                        Console.WriteLine("File Created Successfully");
                    Console.ReadKey();
                    Console.Clear();
                    FileManagementMenu();
                }
                else
                {
                    Console.WriteLine("File Already Exists");
                    Console.Clear();
                    FileManagementMenu();
                }


            }
            else
            {
                Console.WriteLine("Invalid Input");
                Console.Clear();
                FileManagementMenu();
            }




        }           //Folder Creation
        public static void DeleteFileFolder(string directory)
        {
            Console.WriteLine("Enter File/Folder Name");
            string foldername = Console.ReadLine();
            directory = directory + foldername;


            if (!Directory.Exists(directory))
            {
                if (!File.Exists(directory))
                {
                    File.Delete(directory);
                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo(directory);
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                    Console.WriteLine("Folder Deleted Successfully");
                    Console.ReadKey();
                    Console.Clear();
                    FileManagementMenu();
                }
            }
            else
            {
                Console.WriteLine("File / Folder not found ");
                FileManagementMenu();
            }
        }

        public static void GetDiskFiles()
        {
            string disk = "";
            string directorylocation = "";
            var table2 = new Table();
            table2.SetHeaders("#", "Action");
            Console.WriteLine("Choose Which Action needs to done ");
            table2.AddRow("1", "File/Folder Create");
            table2.AddRow("2", "File/Folder Delete");
            Console.WriteLine(table2);
            int action = int.Parse(Console.ReadLine());
            Console.Clear();
            if (action == 1)
            {
                var table = new Table();

                table.SetHeaders("#", "Directories");


                Console.WriteLine("Enter Disk where Folder needs to be created: ");
                disk = Console.ReadLine();
                disk = disk + "://";

                string[] myDirs = Directory.GetDirectories(disk);
                int i = 0;
                int p = 0;
                foreach (var myDir in myDirs)
                {
                    i++;
                    table.AddRow(i.ToString(), myDir);
                }

                Console.WriteLine(table.ToString());


                Console.WriteLine("Do you want to Open the Directory? If yes then Press Y if not then press N");
                string answer = Console.ReadLine();
                directorylocation = disk;

                if (answer == "y" || answer == "Y")
                {
                    Console.WriteLine("Enter Number of Diectory Number to be Opened");
                    string direcnumber = Console.ReadLine();
                    foreach (var myDir2 in myDirs)
                    {
                        p++;
                        if (direcnumber == p.ToString())
                        {
                            directorylocation = myDir2;
                            break;
                        }

                    }
                    GetDirectoryFiles(directorylocation, disk);
                }
                else
                {
                    MakeFolder(directorylocation);
                }


            }
            else if (action == 2)
            {

                var table3 = new Table();
                table3.SetHeaders("#", "Drives");
                string[] drives = System.IO.Directory.GetLogicalDrives();
                int drivecount = 0;
                int dc = 0;
                foreach (string str in drives)
                {
                    drivecount++;
                    table3.AddRow(drivecount.ToString(), str);
                }
                Console.WriteLine(table3);
                Console.WriteLine("Enter Drive Number to Be Opened");
                int drivenumber = int.Parse(Console.ReadLine());
                foreach (var myDir2 in drives)
                {
                    dc++;
                    if (drivenumber.ToString() == dc.ToString())
                    {
                        disk = myDir2;
                        break;
                    }

                }





                var table = new Table();

                table.SetHeaders("#", "Directories");




                string[] myDirs = Directory.GetDirectories(disk);
                DirectoryInfo d = new DirectoryInfo(disk); //Assuming Test is your Folder

                FileInfo[] files = d.GetFiles();

                DirectoryInfo[] directories = d.GetDirectories(); //Getting Text files



                int i = 0;
                int p = 0;
                foreach (var myDir in myDirs)
                {
                    i++;
                    table.AddRow(i.ToString(), myDir);
                }
                foreach (FileInfo file in files)
                {
                    i++;
                    table.AddRow(i.ToString(), file.FullName);

                }



                Console.WriteLine(table.ToString());


                Console.WriteLine("Do you want to Open the Directory? If yes then Press Y if not then press N");
                string answer = Console.ReadLine();
                directorylocation = disk;


                if (answer == "y" || answer == "Y")
                {
                    Console.WriteLine("Enter Number of Diectory Number to be Opened");
                    string direcnumber = Console.ReadLine();
                    foreach (var myDir2 in myDirs)
                    {
                        p++;
                        if (direcnumber == p.ToString())
                        {
                            directorylocation = myDir2;
                            break;
                        }

                    }
                    GetDirectoryFiles(directorylocation, disk);
                }
                else
                {
                    DeleteFileFolder(directorylocation);
                }
            }
            else
            {
                Console.WriteLine("Invalid Input, Press Any Key to Try Again");
                Console.ReadKey();
                Console.Clear();
                FileManagementMenu();
            }






        }
        public static void GetDirectoryFiles(string directory, string disk)
        {

            string directorylocation = "";
            var table2 = new Table();
            table2.SetHeaders("#", "Action");
            Console.WriteLine("Choose Which Action needs to done ");
            table2.AddRow("1", "File/Folder Create");
            table2.AddRow("2", "File/Folder Delete");
            Console.WriteLine(table2);
            int action = int.Parse(Console.ReadLine());
            Console.Clear();
            if (action == 1)
            {

                var table = new Table();
                int i = 0;
                table.SetHeaders("#", "Files");


                DirectoryInfo d = new DirectoryInfo(directory); //Assuming Test is your Folder

                FileInfo[] files = d.GetFiles();

                DirectoryInfo[] directories = d.GetDirectories(); //Getting Text files
                string str = "";

                foreach (DirectoryInfo file in directories)
                {
                    str = file.Name;
                    i++;
                    table.AddRow(i.ToString(), str + " ( FOLDERS )");
                }

                foreach (FileInfo file in files)
                {
                    str = file.Name;
                    i++;
                    table.AddRow(i.ToString(), str);
                }


                Console.WriteLine(table.ToString());
                Console.WriteLine("Do you want to Open the Directory? If yes then Press Y if not then press N");
                string answer = Console.ReadLine();
                directorylocation = directory;
                string[] myDirs = Directory.GetDirectories(directory);
                int p = 0;

                if (answer == "y" || answer == "Y")
                {
                    Console.WriteLine("Enter Number of Diectory Number to be Opened");
                    string direcnumber = Console.ReadLine();
                    foreach (var myDir2 in myDirs)
                    {
                        p++;
                        if (direcnumber == p.ToString())
                        {
                            directorylocation = myDir2;
                            break;
                        }
                        else
                        {
                            directorylocation = directory;
                        }

                    }
                    GetDirectoryFiles(directorylocation, disk);
                }
                else
                {
                    MakeFolder(directorylocation);
                }
                Console.ReadKey();
            }
            else if (action == 2)
            {

                var table = new Table();
                int i = 0;
                table.SetHeaders("#", "Files");


                DirectoryInfo d = new DirectoryInfo(directory); //Assuming Test is your Folder

                FileInfo[] files = d.GetFiles();

                DirectoryInfo[] directories = d.GetDirectories(); //Getting Text files
                string str = "";

                foreach (DirectoryInfo file in directories)
                {
                    str = file.Name;
                    i++;
                    table.AddRow(i.ToString(), str + " ( FOLDERS )");
                }

                foreach (FileInfo file in files)
                {
                    str = file.Name;
                    i++;
                    table.AddRow(i.ToString(), str);
                }


                Console.WriteLine(table.ToString());
                Console.WriteLine("Do you want to Open the Directory? If yes then Press Y if not then press N");
                string answer = Console.ReadLine();
                directorylocation = directory;
                string[] myDirs = Directory.GetDirectories(directory);
                int p = 0;

                if (answer == "y" || answer == "Y")
                {
                    Console.WriteLine("Enter Number of Diectory Number to be Opened");
                    string direcnumber = Console.ReadLine();
                    foreach (var myDir2 in myDirs)
                    {
                        p++;
                        if (direcnumber == p.ToString())
                        {
                            directorylocation = myDir2;
                            break;
                        }
                        else
                        {
                            directorylocation = directory;
                        }

                    }
                    GetDirectoryFiles(directorylocation, disk);
                }
                else
                {
                    DeleteFileFolder(directorylocation);
                }
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Invalid Input, Press Any Key to Try Again");
                Console.ReadKey();
                Console.Clear();
                FileManagementMenu();
            }
        } // Disk Me Foldes

        public static void FileSearch()
        {
            int localdisk_C = 0;
            int localdisk_D = 0;
            Console.WriteLine("\t\t Enter Disk Name ");
            Console.WriteLine("\t\t");
            string path = Console.ReadLine();
            if (path == "C" || path == "c")
            {
                localdisk_C = 1;
                localdisk_D = 0;
            }
            else if (path == "D" || path == "d")
            {
                localdisk_D = 1;
                localdisk_C = 0;
            }
            else
            {
                localdisk_D = 1;
            }
            path += ":/";

            Console.WriteLine("\t\t Enter File Name ");
            string filenamewithextension = Console.ReadLine();


            string userName = Environment.UserName;
            string userpath = path + "Users/" + userName;
            IEnumerable<string> totalfiles = null;
            string programfiles = path + "Program Files";
            string[] custom = null;
            if (localdisk_C == 0)
            {
                custom = Directory.GetDirectories(path);
            }
            else
            {
                var file1 = Directory.GetDirectories(userpath);
                var file2 = Directory.GetDirectories(programfiles);

                totalfiles = file1.Concat(file2);
            }

            if (localdisk_C == 0)
            {
                foreach (var item in custom)
                {
                    if (item.ToString() != path + "System Volume Information" && item.ToString() != path + "$RECYCLE.BIN" &&
                            item.ToString() != path + "$WINRE_BACKUP_PARTITION.MARKER")
                    {
                        var files = Directory.GetFileSystemEntries(item, filenamewithextension + ".*", SearchOption.AllDirectories);
                        foreach (string file in files)
                        {
                            //Console.WriteLine(Path.GetFileName(file),Path.GetFullPath(file));
                            Console.WriteLine("The file is located in:");
                            Console.WriteLine(file);
                            FileInfo info = new FileInfo(file);
                            Console.WriteLine("FIle Name:" + info.Name);
                            Console.WriteLine("File Size" + info.Length);
                            Console.WriteLine("File Extension: " + info.Extension);
                            Console.WriteLine("\t\t\t Press Esc Key to Go To File Management Menu");
                            Console.WriteLine("\t\t\t Want to Open this file? Press Enter");
                            ConsoleKeyInfo backkey = Console.ReadKey();
                            if (backkey.Key == ConsoleKey.Enter)
                            {
                                Process.Start(file);
                                Console.Clear();
                                FileManagementMenu();
                            }

                            if (backkey.Key == ConsoleKey.Backspace)
                            {
                                MainMenu();
                            }

                            Console.ReadKey();
                            Console.Clear();
                            FileManagementMenu();
                        }
                    }
                }
            }
            else
            {
                foreach (var item in totalfiles)
                {
                    if (localdisk_D == 1)
                    {
                        if (item.ToString() != path + "System Volume Information" && item.ToString() != path + "$RECYCLE.BIN" &&
                            item.ToString() != path + "$WINRE_BACKUP_PARTITION.MARKER" &&
                            item.ToString() != path + "aow_drv.log" && item.ToString() != path + "Documents and Settings"
                            && item.ToString() != "C:\\DumpStack.log" && item.ToString() != "C:\\DumpStack.log.tmp" && item.ToString() != "C:\\hiberfil.sys"
                            )
                        {
                            var files = Directory.GetFileSystemEntries(item, filenamewithextension + ".*", SearchOption.AllDirectories);
                            foreach (string file in files)
                            {
                                //Console.WriteLine(Path.GetFileName(file),Path.GetFullPath(file));
                                Console.WriteLine("The file is located in:");
                                Console.WriteLine(file);
                                FileInfo info = new FileInfo(file);
                                Console.WriteLine("FIle Name:" + info.Name);
                                Console.WriteLine("File Size" + info.Length);
                                Console.WriteLine("File Extension: " + info.Extension + " bytes");

                                Console.WriteLine("\t\t\t Press Esc Key to Go To File Management Menu");

                                Console.WriteLine("\t\t\t Want to Open this file? Press Enter");
                                ConsoleKeyInfo backkey = Console.ReadKey();
                                if (backkey.Key == ConsoleKey.Enter)
                                {
                                    Process.Start(file);
                                    Console.Clear();
                                    FileManagementMenu();
                                }

                                if (backkey.Key == ConsoleKey.Backspace)
                                {
                                    MainMenu();
                                }
                                Console.Clear();
                                FileManagementMenu();
                            }

                        }
                    }
                    else
                    {

                        string desktoppath = path + "Users/" + userName + @"\Desktop";
                        string downloadpath = path + "Users/" + userName + @"\Downloads";
                        string picturespath = path + "Users/" + userName + @"\Pictures";
                        string programfilespath = path + "Program Files";
                        // string programfiles = path + "Program Files";

                        string startingten = item.ToString().Substring(0, 16);
                        if (item.ToString() == userpath || item.ToString() == desktoppath ||
                            item.ToString() == downloadpath || item.ToString() == picturespath || item.ToString() == programfilespath)
                        {

                            var files = Directory.GetFileSystemEntries(item, filenamewithextension + ".*", SearchOption.AllDirectories);
                            foreach (string file in files)
                            {
                                //Console.WriteLine(Path.GetFileName(file),Path.GetFullPath(file));
                                Console.WriteLine("The file is located in:");
                                Console.WriteLine(file);
                                FileInfo info = new FileInfo(file);
                                Console.WriteLine("FIle Name:" + info.Name);
                                Console.WriteLine("File Size" + info.Length);
                                Console.WriteLine("File Extension: " + info.Extension + " bytes");

                                Console.WriteLine("\t\t\t Press Any Key to Go To File Management Menu");

                                ConsoleKeyInfo backkey = Console.ReadKey();
                                if (backkey.Key == ConsoleKey.Enter)
                                {
                                    Process.Start(file);
                                    Console.Clear();
                                    FileManagementMenu();
                                }

                                if (backkey.Key == ConsoleKey.Backspace)
                                {
                                    MainMenu();
                                }
                                Console.Clear();
                                FileManagementMenu();
                            }

                        }

                        else if (startingten == path + "Program Files")
                        {
                            var pffiles = Directory.GetDirectories(programfiles);
                            foreach (var item2 in pffiles)
                            {
                                var pfffiles = Directory.GetFileSystemEntries(item, filenamewithextension + ".*", SearchOption.AllDirectories);
                                foreach (string file in pfffiles)
                                {
                                    //Console.WriteLine(Path.GetFileName(file),Path.GetFullPath(file));
                                    Console.WriteLine("The file is located in:");
                                    Console.WriteLine(file);
                                    FileInfo info = new FileInfo(file);
                                    Console.WriteLine("FIle Name:" + info.Name);
                                    Console.WriteLine("File Size" + info.Length);
                                    Console.WriteLine("File Extension: " + info.Extension + " bytes");

                                    Console.WriteLine("\t\t\t Press Any Key to Go To File Management Menu");
                                    ConsoleKeyInfo backkey = Console.ReadKey();
                                    if (backkey.Key == ConsoleKey.Enter)
                                    {
                                        Process.Start(file);
                                        Console.Clear();
                                        FileManagementMenu();
                                    }

                                    if (backkey.Key == ConsoleKey.Backspace)
                                    {
                                        MainMenu();
                                    }
                                    Console.Clear();
                                    FileManagementMenu();
                                }
                            }

                        }
                        else
                        {

                        }




                    }
                }
            }



        }           //Search
        [System.Runtime.InteropServices.DllImport("Shell32.dll")]
        private static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2); //REFRESH

        public static void FileManagementMenu()
        {
            Console.WriteLine("\t\t\t\t Welcome To OSum\n");
            Console.WriteLine("\t\tEnter Numbers for Respective Task\n");
            Console.WriteLine("\t\t 1) File Search \n\t\t 2) Folder Create \n\t\t 3) Back\n\t\t");
            ConsoleKeyInfo mainmenukey = Console.ReadKey();
            if (mainmenukey.Key == ConsoleKey.NumPad1 || mainmenukey.Key == ConsoleKey.D1)
            {
                Console.Clear();
                FileSearch();


            }
            else if (mainmenukey.Key == ConsoleKey.NumPad2 || mainmenukey.Key == ConsoleKey.D2)
            {
                Console.Clear();
                GetDiskFiles();


            }
            else if (mainmenukey.Key == ConsoleKey.NumPad3 || mainmenukey.Key == ConsoleKey.D3)
            {
                Console.Clear();
                MainMenu();
            }

            else
            {
                Console.WriteLine("Invalid Input");
                Console.Clear();
                FileManagementMenu();

            }
        }                   //File Management Menu

        public static void ListAllProcess()
        {

            var table = new Table();

            table.SetHeaders("Process Name ", "ID");


            Process[] processCollection = Process.GetProcesses();
            foreach (Process p in processCollection)
            {
                if (!string.IsNullOrEmpty(p.MainWindowTitle))
                {
                    table.AddRow(p.MainWindowTitle, p.Id.ToString());
                }
            }

            Console.WriteLine(table.ToString());
            Console.WriteLine("Press Any Key to go Back");
            Console.ReadKey();
            Console.Clear();
            ProcessManagementMenu();
        }                   //Process Listing


        public static void Installed()
        {
            var table = new Table();

            table.SetHeaders("Program Name", "Program Location");
            int i = 0;

            bool found = false;

            DataTable softwarearray = new DataTable();
            softwarearray.Columns.Add("Program");
            softwarearray.Columns.Add("Location");
            string SoftwareKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";

            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(SoftwareKey))
            {

                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        try
                        {
                            if (!(sk.GetValue("DisplayName") == null))
                            {
                                if (sk.GetValue("InstallLocation") == null)
                                {
                                }
                                else
                                {
                                    DataRow row = softwarearray.NewRow();
                                    row["Program"] = sk.GetValue("DisplayName");
                                    row["Location"] = sk.GetValue("InstallLocation");
                                    softwarearray.Rows.Add(row);
                                    table.AddRow(Convert.ToString(sk.GetValue("DisplayName")), Convert.ToString(sk.GetValue("InstallLocation")));

                                }

                            }


                        }
                        catch (Exception ex)
                        {


                        }
                    }

                }
                Console.WriteLine(table);
                Console.WriteLine("Total Apps are: " + i);
                Console.WriteLine("Enter Name to Start The Process");
                var process = Console.ReadLine();
                foreach (DataRow item in softwarearray.Rows)
                {
                    if (process != null)
                    {
                        string nameofprogram = item["Program"].ToString();
                        if (process == nameofprogram)
                        {
                            found = true;
                            Process.Start(item["Location"].ToString());
                        }

                    }
                    else
                    {
                        Console.Clear();
                        ProcessManagementMenu();
                    }
                }


            }
            if (found == false)
            {
                Console.WriteLine("Process Not Found Press Any Key to Go Back");
                Console.ReadKey();
                Console.Clear();
                ProcessManagementMenu();
            }
            else
            {
                Console.WriteLine("Process Location Found and have Opened Press any Key to Go Back");
                Console.ReadKey();
                Console.Clear();
                ProcessManagementMenu();

            }

        }                   //Program Listing


        public static void EndProcess()
        {

            var table = new Table();

            table.SetHeaders("Process Name ", "ID");

            Process[] processCollection = Process.GetProcesses();
            foreach (Process p in processCollection)
            {
                if (!string.IsNullOrEmpty(p.MainWindowTitle))
                {
                    table.AddRow(p.MainWindowTitle, p.Id.ToString());
                }
            }


            Console.WriteLine(table.ToString());
            Console.WriteLine("Enter Name to Kill the Process || Leave of .exe");
            var endID = int.Parse(Console.ReadLine());
            bool found = false;
            foreach (var process in processCollection)
            {

                if (endID == process.Id)
                {
                    found = true;
                    process.Kill();
                    break;

                }
                else
                {
                    found = false;
                }

            }
            if (found == true)
            {
                Console.WriteLine("Process Ended Successfully");
                Console.WriteLine("Press Any Key to Go to Process Menu");
                Console.ReadKey();
                Console.Clear();
                ProcessManagementMenu();
            }
            else
            {
                Console.WriteLine("Process Not Found, Press Any Key to Go Back");
                Console.ReadKey();
                Console.Clear();
                ProcessManagementMenu();
            }
            //var pr = Process.GetProcessById(endID);
            //pr.Kill();


        }           //Proess Killing

        public static void ProcessManagementMenu()
        {
            Console.WriteLine("\t\t\t\t Welcome To OSum\n");
            Console.WriteLine("\t\tEnter Numbers for Respective Task\n");
            Console.WriteLine("\t\t 1) List Process \n\t\t 2) Process End \n\t\t 3) Start Process \n\t\t 4) Back\n\t\t");
            ConsoleKeyInfo mainmenukey = Console.ReadKey();
            if (mainmenukey.Key == ConsoleKey.NumPad1 || mainmenukey.Key == ConsoleKey.D1)
            {
                Console.Clear();
                ListAllProcess();

            }
            else if (mainmenukey.Key == ConsoleKey.NumPad2 || mainmenukey.Key == ConsoleKey.D2)
            {
                Console.Clear();
                EndProcess();

            }
            else if (mainmenukey.Key == ConsoleKey.NumPad3 || mainmenukey.Key == ConsoleKey.D3)
            {
                Console.Clear();
                //ListPrograms();
                Installed();
            }
            else if (mainmenukey.Key == ConsoleKey.NumPad4 || mainmenukey.Key == ConsoleKey.D4)
            {
                Console.Clear();
                MainMenu();

            }
            else
            {
                Console.WriteLine("Invalid Input");
                Console.Clear();
                FileManagementMenu();

            }
        }           //Process Management Meny
        public static void MainMenu()
        {
            var table = new Table();
            table.SetHeaders("Welcome To OSum");
            Console.WriteLine(table.ToString());


            Console.WriteLine("Enter Numbers for Respective Task");
            Console.WriteLine("\n");


            table.SetHeaders("#", "Action");
            table.AddRow("1", "Process Management");
            table.AddRow("2", "File Management");
            table.AddRow("3", "Refresh Desktop");
            table.AddRow("4", "Shut Down/ Restart");
            Console.WriteLine(table.ToString());
            ConsoleKeyInfo mainmenukey = Console.ReadKey();
            if (mainmenukey.Key == ConsoleKey.NumPad2 || mainmenukey.Key == ConsoleKey.D2)
            {
                Console.Clear();
                FileManagementMenu();
            }

            if (mainmenukey.Key == ConsoleKey.NumPad3 || mainmenukey.Key == ConsoleKey.D3)
            {
                SHChangeNotify(0x8000000, 0x1000, IntPtr.Zero, IntPtr.Zero);
                Console.Clear();
                MainMenu();

            }




            if (mainmenukey.Key == ConsoleKey.NumPad1 || mainmenukey.Key == ConsoleKey.D1)
            {
                Console.Clear();
                ProcessManagementMenu();
            }
        }               // Main Menu


    }
}
