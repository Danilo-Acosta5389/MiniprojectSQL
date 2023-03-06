using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
                InstructionInYellow("\n   Please use UP and DOWN arrows to navigate\n   Press ENTER to select\n");
                
                try
                {
                    /*
                The NavMenu() method is a reusable menu, just pass in a string array like below
                String[] arr = { "Register hours", "New project", "New person", "Edit project", "Edit person" , "Quit" };
                NavMenu(arr);
                OR pass the array directly as argument like below
                */

                    int option = NavMenu(new List<string> { "Register hours in project", "New project", "New person", "Edit project", "Edit person", "Quit" }, index);

                    //NavMenu will return the index of which option the user has picked

                    if (option == 0) RegTime();
                    else if (option == 1) NewProject();
                    else if (option == 2) NewPerson();
                    else if (option == 3) EditProject();
                    else if (option == 4) EditPerson();
                    else if (option == 5) Quit();
                    index = option;
                }
                catch (Exception e)
                {
                    ErrorInRed(e.Message);
                    PleasePressEnter();
                }

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
            Console.WriteLine($"\x1b[5m{title}");
            Console.ResetColor();
        }

        public static int NavMenu(List<string> options, int option = 0)
        {
            Console.CursorVisible = false;
            ConsoleKeyInfo key;
            (int left, int top) = Console.GetCursorPosition();
            string cursor = " -> ";
            bool selectedOption = false;
            while (!selectedOption)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(left, top);
                for (int i = 0; i < options.Count; i++)
                {
                    Console.WriteLine($"{(option == i ? cursor : "    ")}{options[i]}");
                }
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option = (option == options.Count - 1 ? option = 0 : option + 1);
                        break;
                    case ConsoleKey.UpArrow:
                        option = (option == 0 ? option = options.Count - 1 : option - 1);
                        break;
                    case ConsoleKey.Enter:
                        selectedOption = true;
                        break;
                }
            }
            Console.ResetColor();
            return option;
        }

        public static void OptionTitleInRed(string optionName)
        {
            Console.WriteLine();
            Console.Write("     ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Write($" {optionName} ");
            Console.ResetColor();
            Console.WriteLine();
        }

        public static int AreYouSure(int index = 0)
        {
            InstructionInYellow("Are you sure?");
            int option = NavMenu(new List<string> { "Yes", "No" }, index);
            return option;
        }
        
        public static void PleasePressEnter()
        {
            Console.CursorVisible = false;

            InstructionInYellow("Please press ENTER to continue");

            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
            Console.ResetColor ();
        }

        public static void InstructionInYellow(string input)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n   {input}\n");
            Console.ResetColor();
        }

        public static void ErrorInRed(string input)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n    {input}\n");
            Console.ResetColor();
        }

        public static void SuccessInGreen(string input)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n    {input}\n");
            Console.ResetColor();
        }

        public static string IsCorrect(string optionName , string message, int index = 0)
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            InstructionInYellow("Is this correct?");
            Console.WriteLine(message);
            int yesOrNo = NavMenu(new List<string> { "Yes", "No" }, index);
            if (yesOrNo == 0) return "yes";
            else return "no";
        }

        public static string GetPerson(string optionName)
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            InstructionInYellow("Choose a person");

            List<string> personList = DatabaseAccess.GetPersonName();
            personList.Add("\u001b[0mBack to menu");

            int personIndex = NavMenu(personList);
            if (personIndex != personList.Count - 1)
            {
                return personList[personIndex];
            }
            else
            {
                Console.WriteLine($"\n   You choosed: \x1b[1m{personList[personIndex]}\x1b[0m\n");
                return "";
            }
        }

        public static string GetProject(string optionName)
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            InstructionInYellow("Choose project");

            List<string> projectList = DatabaseAccess.GetProjectName();
            projectList.Add("\u001b[0mBack to menu");

            int projectIndex = NavMenu(projectList);
            if (projectIndex != projectList.Count - 1)
            {
                return projectList[projectIndex];
            }
            else
            {
                Console.WriteLine($"\n   You choosed: \x1b[1m{projectList[projectIndex]}\x1b[0m\n");
                return "";
            }
            
        }


        public static void RegTime(string optionName = "Register time on project")
        {
            
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            bool isRunning = true;
            while (isRunning)
            {
                string person = GetPerson(optionName);
                if (person == "") { break; }

                string project = GetProject(optionName);
                if (project == "") { break; }

                InstructionInYellow("Input hours spent on project today\n   Leave blank to go back");

                Console.CursorVisible = true;
                Console.Write(" ==> ");
                int hours = int.Parse(Console.ReadLine());
                Console.CursorVisible = false;
                //Console.WriteLine($"\n   \u001b[1m{person}\u001b[0m spent \u001b[1m{hours}\u001b[0m hours on \u001b[1m{project}\u001b[0m today");
                string message = $"\n   \u001b[1m{person}\u001b[0m spent \u001b[1m{hours}\u001b[0m hours on \u001b[1m{project}\u001b[0m today\n";
                string yesNo = IsCorrect(optionName ,message);
                if (yesNo == "yes")
                {
                    bool success = DatabaseAccess.RegistrateHoursInDB(project, person, hours);
                    if (success)
                    {
                        SuccessInGreen("Great success!");
                        Console.WriteLine($"\n   \u001b[1mRegistered\u001b[0m: \u001b[1m{person}\u001b[0m spent \u001b[1m{hours}\u001b[0m hours on \u001b[1m{project}\u001b[0m today");

                    }
                    else
                    {
                        ErrorInRed("Unsuccessful to register hours");
                    }
                }
                else { Console.WriteLine($"\n   You choosed {yesNo}"); }
                
                isRunning = false;
            }
            PleasePressEnter();
        }


        static void NewProject(string optionName = "Add new project")
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            InstructionInYellow("Please input project name\n   Leave blank to exit");
            Console.CursorVisible = true;

            Console.Write("\n   Project name: ");
            string projectName = Console.ReadLine();
            Console.CursorVisible = false;
            bool success = DatabaseAccess.InsertNewProject(projectName);
            if (success == true) 
            {
                SuccessInGreen("Project successfully added!");
            }
            else 
            {
                ErrorInRed("Unsuccessful to add new project");
            }
            PleasePressEnter();
        }
        static void NewPerson(string optionName = "Add new person")
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            InstructionInYellow("Please input person name\n   Leave blank to exit");
            Console.CursorVisible = true;

            Console.Write("\n   Person name: ");
            string personName = Console.ReadLine();
            Console.CursorVisible = false;

            bool success = DatabaseAccess.InsertNewPerson(personName);
            if (success == true) 
            {
                SuccessInGreen("Person successfully added!");
            }
            else 
            {
                ErrorInRed("Unsuccessful to add new person");
            }
            PleasePressEnter();
        }
        static void EditProject(string optionName = "Edit project")
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            string project = GetProject(optionName);
            Console.WriteLine($"\n   Edit {project}");
            PleasePressEnter();
        }
        static void EditPerson(string optionName = "Edit person")
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            string person = GetPerson(optionName);
            Console.WriteLine($"\n   Edit {person}");

            PleasePressEnter();
        }        
        static void Quit(string optionName = "Close application")
        {
            Console.Clear();
            //int index = 1;
            TitleScreen();
            OptionTitleInRed(optionName);
            int option = AreYouSure();
            if (option == 0) Environment.Exit(0);
        }
    }
}
