using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MiniprojectSQL
{
    public class General
    {
        public static void App()
        {
            int index = 0;
            bool appRunning = true;
            while (appRunning)
            {
                Console.Clear();
                TitleScreen();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n   Please use UP and DOWN arrows to navigate\n   Press ENTER to select\n");
                Console.ResetColor();

                /*
                The NavMenu() method is a reusable menu, just pass in a string array like below
                String[] arr = { "Register hours", "New project", "New person", "Edit project", "Edit person" , "Quit" };
                NavMenu(arr);
                OR pass the array directly as argument like below
                */

                int option = NavMenu(new String[] { "Register hours in project", "New project", "New person", "Edit project", "Edit person", "Quit" }, index);

                //NavMenu will return the index of which option the user has picked

                if (option == 0) RegTime("Register time on project");
                else if (option == 1) NewProject("Add new projectName");
                else if (option == 2) NewPerson("Add new personName");
                else if (option == 3) EditProject("Edit project");
                else if (option == 4) EditPerson("Edit person");
                else if (option == 5) Quit("Quit");
                index = option;
            }
        }


        public static void TitleScreen()
        {
            string title = @"
 __      __             __   ___________.__             ___________                     __                 
/  \    /  \___________|  | _\__    ___/|__| _____   ___\__    ___/___________    ____ |  | __ ___________ 
\   \/\/   /  _ \_  __ \  |/ / |    |   |  |/     \_/ __ \|    |  \_  __ \__  \ _/ ___\|  |/ // __ \_  __ \
 \        (  <_> )  | \/    <  |    |   |  |  Y Y  \  ___/|    |   |  | \// __ \\  \___|    <\  ___/|  | \/
  \__/\  / \____/|__|  |__|_ \ |____|   |__|__|_|  /\___  >____|   |__|  (____  /\___  >__|_ \\___  >__|   
       \/                   \/                   \/     \/                    \/     \/     \/    \/       ";
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(title);
            Console.ResetColor();
        }

        public static int NavMenu(String[] options, int option = 0)
        {
            Console.CursorVisible = false;
            //int option = 0;
            ConsoleKeyInfo key;
            (int left, int top) = Console.GetCursorPosition();
            string cursor = " -> ";
            bool selectedOption = false;
            while (!selectedOption)
            {

                Console.SetCursorPosition(left, top);

                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine($"{(option == i ? cursor : "    ")}{options[i]}");
                }

                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option = (option == options.Length - 1 ? option = 0 : option + 1);
                        break;
                    case ConsoleKey.UpArrow:
                        option = (option == 0 ? option = options.Length - 1 : option - 1);
                        break;
                    case ConsoleKey.Enter:
                        selectedOption = true;
                        break;
                }
            }
            return option;
        }

        static void OptionTitle(string optionName)
        {
            Console.WriteLine();
            Console.Write("     ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Write($" {optionName} ");
            Console.ResetColor();
            Console.WriteLine();
        }

        static int AreYouSure(int index = 0)
        {
            Console.WriteLine("\n   Are you sure?\n");
            int option = NavMenu(new string[] { "Yes", "No" }, index);
            return option;
            
        }
        
        static void PleasePressEnter()
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n   Please press ENTER to continue");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
            Console.ResetColor ();
        }

        static void RegTime(string optionName)
        {
            Console.Clear();
            TitleScreen();
            OptionTitle(optionName);
            PleasePressEnter();
        }


        static void NewProject(string optionName)
        {
            Console.Clear();
            TitleScreen();
            OptionTitle(optionName);
            Console.CursorVisible = true;

            Console.Write("\n   Project name: ");
            string projectName = Console.ReadLine();
            Console.CursorVisible = false;
            bool success = DatabaseAccess.InsertNewProject(projectName);
            if (success == true) 
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n    Project successfully added!");
                Console.ResetColor();
            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n    Unsuccessful to add new project");
                Console.ResetColor();
            }
            PleasePressEnter();
        }
        static void NewPerson(string optionName)
        {
            Console.Clear();
            TitleScreen();
            OptionTitle(optionName);
            Console.CursorVisible = true;

            Console.Write("\n   Person name: ");
            string personName = Console.ReadLine();
            Console.CursorVisible = false;

            bool success = DatabaseAccess.InsertNewPerson(personName);
            if (success == true) 
            { 
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n   Person successfully added!");
                Console.ResetColor();
            }
            else 
            { 
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("\n    Unsuccessful to add new person");
                Console.ResetColor();
            }
            PleasePressEnter();
        }
        static void EditProject(string optionName)
        {
            Console.Clear();
            TitleScreen();
            OptionTitle(optionName);
            PleasePressEnter();
        }
        static void EditPerson(string optionName)
        {
            Console.Clear();
            TitleScreen();
            OptionTitle(optionName);
            PleasePressEnter();
        }        
        static void Quit(string optionName)
        {
            Console.Clear();
            //int index = 1;
            TitleScreen();
            OptionTitle(optionName);
            int option = AreYouSure();
            if (option == 0) Environment.Exit(0);
        }
    }
}
