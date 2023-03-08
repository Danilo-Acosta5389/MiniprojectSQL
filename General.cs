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

                    int option = NavMenu(new List<string> { "Work Time Tracker" , "Register hours in project", "New project", "New person", "Edit project", "Edit person", "\x1b[31mDelete project\u001b[0m", "\u001b[31mDelete person\u001b[0m", "\u001b[1mQuit" },index);

                    //NavMenu will return the index of which option the user has picked
                    if (option == 0) WorkTimeTrack();
                    else if (option == 1) RegTime();
                    else if (option == 2) NewProject();
                    else if (option == 3) NewPerson();
                    else if (option == 4) EditProject();
                    else if (option == 5) EditPerson();
                    else if (option == 6) DeleteProject();
                    else if (option == 7) DeletePerson();
                    else if (option == 8) Quit();
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

        public static int NavMenu( List<string> options, int option = 0)
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

        public static int AreYouSure(string optionName, string message, int index = 0)
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            InstructionInYellow("Are you sure?");
            Console.WriteLine(message);
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

        public static string IsCorrect(string optionName , string message = "[Insert message]", int index = 0)
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

        public static string GetPerson(string optionName, string message = "Choose person")
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            InstructionInYellow(message);

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

        public static string GetProject(string optionName, string message = "Choose project", int index = 0)
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            InstructionInYellow(message);

            List<string> projectList = DatabaseAccess.GetProjectName();
            projectList.Add("\u001b[0mBack to menu");

            int projectIndex = NavMenu(projectList, index);
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

        static void PersonOnAProject(string optionName)
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            string person = GetPerson(optionName, "\n   Choose person");
            if (person != "")
            {
                while (true)
                {
                    string project = GetProject(optionName, "\n   Choose project");
                    if (project != "")
                    {
                        Console.Clear();
                        TitleScreen();
                        OptionTitleInRed(optionName);
                        InstructionInYellow($"\n   Table of {person} and hours registered on the {project} project");
                        List<ProjectPersonModel> personProjectList = DatabaseAccess.GetPersonOnOneProject(person, project);
                        var x = personProjectList;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("    __________________________________________");
                        Console.WriteLine("   | {0,-10}  | {1, -15}  |  {2,-5}  |", "Person", "Project", "Hours");
                        Console.WriteLine("   |-------------|------------------|---------|");
                        int sum = 0;
                        for (int i = 0; i < x.Count; i++)
                        {
                            Console.WriteLine("   | {0,-10}  | {1, -15}  |   {2,-5} |", x[i].person_name, x[i].project_name, x[i].hours);
                            Console.WriteLine("   |-------------|------------------|---------|");
                            sum = sum += x[i].hours;
                        }
                        Console.WriteLine("   | {0,-10}  | {1, -10}       |   {2,-5} |", "TOTAL", "hours:", sum, "");
                        Console.WriteLine("   |__________________________________________|");
                        Console.ResetColor();
                        PleasePressEnter();
                    }
                    else break;
                }
            }

        }

        static void PersonOnMultipleProjects(string optionName)
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            while (true)
            {
                string person = GetPerson(optionName, "\n   Choose person");
                if (person != "")
                {
                    Console.Clear();
                    TitleScreen();
                    OptionTitleInRed(optionName);
                    InstructionInYellow($"\n   Table of {person} and hours registered on different projects");
                    List<ProjectPersonModel> personMultiProjectList = DatabaseAccess.GetPersonWithManyProjects(person);
                    var x = personMultiProjectList;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("    __________________________________________");
                    Console.WriteLine("   | {0,-10}  | {1, -15}  |  {2,-5}  |", "Person", "Project", "Hours");
                    Console.WriteLine("   |-------------|------------------|---------|");
                    int sum = 0;
                    for (int i = 0; i < x.Count; i++)
                    {
                        Console.WriteLine("   | {0,-10}  | {1, -15}  |   {2,-5} |", x[i].person_name, x[i].project_name, x[i].hours);
                        Console.WriteLine("   |-------------|------------------|---------|");
                        sum = sum += x[i].hours;
                    }
                    Console.WriteLine("   | {0,-10}  | {1, -10}       |   {2,-5} |", "TOTAL", "hours:", sum, "");
                    Console.WriteLine("   |__________________________________________|");
                    Console.ResetColor();
                    PleasePressEnter();
                    
                }
                else break;
            }

        }

        static void ProjectWithMultiplePersons(string optionName)
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            int index = 0;
            while (true)
            {
                string project = GetProject(optionName, "Choose project", index);
                if (project != "")
                {
                    Console.Clear();
                    TitleScreen();
                    OptionTitleInRed(optionName);
                    InstructionInYellow($"\n   Table of the {project} project\n   and everyone that has registered hours on it");
                    Console.ForegroundColor = ConsoleColor.White;
                    List<ProjectPersonModel> projectMultiPersonList = DatabaseAccess.GetProjectWithMany(project);
                    var x = projectMultiPersonList;
                    Console.WriteLine("    __________________________________________");
                    Console.WriteLine("   | {0,-15}  | {1, -10}  |  {2,-5}  |", "Project", "Person", "Hours");
                    Console.WriteLine("   |------------------|-------------|---------|");
                    int sum = 0;
                    for (int i = 0; i < x.Count; i++)
                    {
                        Console.WriteLine("   | {0,-15}  | {1, -10}  |   {2,-5} |", x[i].project_name, x[i].person_name, x[i].hours);
                        Console.WriteLine("   |------------------|-------------|---------|");
                        sum = sum += x[i].hours;
                    }
                    Console.WriteLine("   | {0,-15}  | {1, -10}  |   {2,-5} |", "TOTAL", "hours:", sum, "");
                    Console.WriteLine("   |__________________________________________|");
                    Console.ResetColor();
                    PleasePressEnter();
                }
                else break;


            }
            PleasePressEnter();
            
        }

        static void AllProjectsAllPersons(string optionName)
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            InstructionInYellow("\n   Table of all projects, persons and working hours\n   Total amount of working hours at the bottom");
            List<ProjectPersonModel> workTrackList = DatabaseAccess.GetProjectPersonList();
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                Console.WriteLine("\n    __________________________________________");
                Console.WriteLine("   | {0,-15}  | {1, -10}  |  {2,-5}  |", "Project", "Person", "Hours");
                Console.WriteLine("   |------------------|-------------|---------|");
                int sum = 0;
                for (int i = 0; i < workTrackList.Count; i++)
                {
                    Console.WriteLine("   | {0,-15}  | {1, -10}  |   {2,-5} |", workTrackList[i].project_name, workTrackList[i].person_name, workTrackList[i].hours);
                    Console.WriteLine("   |------------------|-------------|---------|");
                    sum = sum += workTrackList[i].hours;
                }
                Console.WriteLine("   | {0,-15}  | {1, -10}  |   {2,-5} |", "TOTAL", "hours:", sum, "");
                Console.WriteLine("   |__________________________________________|");

                break;
            }
            Console.ResetColor();
            PleasePressEnter();
        }

        public static void WorkTimeTrack(string optionName = "Work Time Tracker")
        {
            int index = 0;
            while (true)
            {
                Console.Clear();
                TitleScreen();
                OptionTitleInRed(optionName);
                InstructionInYellow("\n   Track hours spent on project\n");

                int option = NavMenu(new List<string> { "Person on a project", "Person on multiple projects", "Project with multiple person", "All projects and persons", "\u001b[0mBack to menu" }, index);
                if (option == 0) PersonOnAProject(optionName);
                else if (option == 1) PersonOnMultipleProjects(optionName);
                else if (option == 2) ProjectWithMultiplePersons(optionName);
                else if (option == 3) AllProjectsAllPersons(optionName);
                else if (option == 4) break;
                index = option;
            }
            PleasePressEnter();

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

                InstructionInYellow("Input hours spent on project today\n   Leave blank to cancel");

                Console.CursorVisible = true;
                Console.Write(" ==> ");
                string hours = Console.ReadLine();
                if (hours == "")
                {
                    Console.WriteLine("\n    Canceling");
                    break;
                }
                int newHours = Convert.ToInt32(hours);
                Console.CursorVisible = false;


                //Console.WriteLine($"\n   \u001b[1m{person}\u001b[0m spent \u001b[1m{hours}\u001b[0m hours on \u001b[1m{project}\u001b[0m today");
                string message = $"\n   \u001b[1m{person}\u001b[0m spent \u001b[1m{newHours}\u001b[0m hours on \u001b[1m{project}\u001b[0m today\n";
                string yesNo = IsCorrect(optionName ,message);
                if (yesNo == "yes")
                {
                    bool success = DatabaseAccess.RegistrateHoursInDB(project, person, newHours);
                    if (success)
                    {
                        SuccessInGreen("Great success!");
                        Console.WriteLine($"\n   \u001b[1mRegistered\u001b[0m: \u001b[1m{person}\u001b[0m spent \u001b[1m{newHours}\u001b[0m hours on \u001b[1m{project}\u001b[0m today");

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
            InstructionInYellow("Please input project name\n   Leave blank to cancel");
            while (true)
            {
                Console.CursorVisible = true;
                Console.Write("\n   Project name: ");
                string projectName = Console.ReadLine();
                if (projectName == "")
                {
                    Console.WriteLine("\n   Canceling");
                    break;
                }
                Console.CursorVisible = false;
                bool success = DatabaseAccess.InsertNewProject(projectName);
                if (success == true)
                {
                    SuccessInGreen("Project successfully added!");
                    break;
                }
                else
                {
                    ErrorInRed("Unsuccessful to add new project");
                    break;
                }
            }
            
            PleasePressEnter();
        }
        static void NewPerson(string optionName = "Add new person")
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            InstructionInYellow("Please input person name\n   Leave blank to cancel");

            while(true)
            {
                Console.CursorVisible = true;
                Console.Write("\n   Person name: ");
                string personName = Console.ReadLine();
                if (personName == "")
                {
                    Console.WriteLine("\n   Canceling");
                    break;
                }
                Console.CursorVisible = false;

                bool success = DatabaseAccess.InsertNewPerson(personName);
                if (success == true)
                {
                    SuccessInGreen("Person successfully added!");
                    break;
                }
                else
                {
                    ErrorInRed("Unsuccessful to add new person");
                    break;
                }
            }

            PleasePressEnter();
        }
        static void EditProject(string optionName = "Edit project")
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            bool isRunning = true;
            while (isRunning)
            {
                string projectName = GetProject(optionName);
                if (projectName == "") { break; }
                Console.WriteLine($"\n   Edit \u001b[1m{projectName}\u001b[0m");

                InstructionInYellow("Please input new project name\n   Leave blank to cancel");
                Console.CursorVisible = true;
                Console.Write(" ==> ");
                string newName = Console.ReadLine();
                Console.CursorVisible = false;
                if (newName == "")
                {
                    Console.WriteLine($"\n   Canceling");
                    break;
                }
                string message = $"\n   Change \u001b[1m{projectName}\u001b[0m to \u001b[1m{newName}\u001b[0m\n";
                string yesNo = IsCorrect(optionName, message);
                if(yesNo == "yes")
                {
                    bool success = DatabaseAccess.EditProjectName(projectName ,newName);
                    if (success == true) 
                    { 
                        SuccessInGreen("Project name was successfully changed!");
                        isRunning = false;
                    }
                    else
                    {
                        ErrorInRed("Project name was not changed");
                    }
                }
            }
            
            
            //
            PleasePressEnter();
        }
        static void EditPerson(string optionName = "Edit person")
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            bool isRunning = true;
            while (isRunning)
            {
                string personName = GetPerson(optionName);
                if (personName == "") { break; }
                Console.WriteLine($"\n   Edit \u001b[1m{personName}\u001b[0m");

                InstructionInYellow("Please input new person name\n   Leave blank to cancel");
                Console.CursorVisible = true;
                Console.Write(" ==> ");
                string newName = Console.ReadLine();
                Console.CursorVisible = false;
                if (newName == "")
                {
                    Console.WriteLine($"\n   Canceling");
                    break;
                }
                string message = $"\n   Change \u001b[1m{personName}\u001b[0m to \u001b[1m{newName}\u001b[0m\n";
                string yesNo = IsCorrect(optionName, message);
                if (yesNo == "yes")
                {
                    bool success = DatabaseAccess.EditPersontName(personName, newName);
                    if (success == true)
                    {
                        SuccessInGreen("Project name was successfully changed!");
                        isRunning = false;
                    }
                    else
                    {
                        ErrorInRed("Project name was not changed");
                    }
                }
            }

                PleasePressEnter();
        }


        static void DeleteProject(string optionName = "Delete project")
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);

            while (true)
            {
                string project = GetProject(optionName, "Choose project to DELETE\n   Project with registered hours cannot be deleted\n\n   \u001b[31mWARNING: DELETION IS PERMANENT!");
                if (project == "") break;
                int yesNo = AreYouSure(optionName, $"   Do you wish to DELETE {project}?\n");
                if (yesNo == 0)
                {
                    bool success = DatabaseAccess.DeleteProjectName(project);
                    if (success == true)
                    {
                        SuccessInGreen("Greate success!");
                        break;
                    }
                    else
                    {
                        ErrorInRed("Could not DELETE project");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine($"\n   Canceling");
                    break;
                }
            }
            PleasePressEnter();
        }
        

        static void DeletePerson(string optionName = "Delete person")
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);

            while (true)
            {
                string person = GetPerson(optionName, "Choose person to DELETE\n   Person with registered hours cannot be deleted\n\n   \u001b[31mWARNING: DELETION IS PERMANENT!");
                if (person == "") break;
                int yesNo = AreYouSure(optionName, $"   Do you wish to DELETE {person}?\n");
                if (yesNo == 0)
                {
                    bool success = DatabaseAccess.DeletePersonName(person);
                    if (success == true)
                    {
                        SuccessInGreen("Greate success!");
                        break;
                    }
                    else
                    {
                        ErrorInRed("Could not DELETE person");
                        break;
                    }
                    
                }
                else
                {
                    Console.WriteLine($"\n   Canceling");
                    break;
                }
            }

            PleasePressEnter();
        }
        

        static void Quit(string optionName = "Quit")
        {
            Console.Clear();
            //int index = 1;
            TitleScreen();
            OptionTitleInRed(optionName);
            int option = AreYouSure(optionName, "   Close the application?\n");
            if (option == 0) Environment.Exit(0);
        }
    }
}
