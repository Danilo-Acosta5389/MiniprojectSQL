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
            int index = 0;  //THIS VARIABLE IS HERE SO THAT THE MENU ALWAYS STAYS IN THE POSITION WHERE WE LAST LEFT IT
            bool appRunning = true;
            while (appRunning)
            {
                //IN ORDER FOR THE MENUS THROUGHOUT THE APP TO NOT CRASH OR BUG, THERE WILL BE MANY Console.Clear();
                Console.Clear();
                TitleScreen();
                InstructionInYellow("\n   Please use UP and DOWN arrows to navigate\n   Press ENTER to select\n");
                try
                {
                    /*
                    
                    The NavMenu() method is a reusable menu, just pass in a list like below
                    List<string> option = new List<string> { "Register hours", "New project", "New person", "Edit project", "Edit person" , "Close" };
                    NavMenu(options);
                    OR pass the List directly as an argument like below
                    
                     */

                    int option = NavMenu(new List<string> { "Work Time Tracker" , "Register hours in project", "New project", "New person", "Edit registered hours", "Edit project", "Edit person", "\x1b[31mDelete project\u001b[0m", "\u001b[31mDelete person\u001b[0m", "\u001b[1mClose" },index);

                    //NavMenu will returns the index of which option the user has picked

                    if (option == 0) WorkTimeTrack();
                    else if (option == 1) RegTime();
                    else if (option == 2) NewProject();
                    else if (option == 3) NewPerson();
                    else if (option == 4) EditRegTime();
                    else if (option == 5) EditProject();
                    else if (option == 6) EditPerson();
                    else if (option == 7) DeleteProject();
                    else if (option == 8) DeletePerson();
                    else if (option == 9) Quit();
                    index = option; //HERE IS THE INDEX VARIABLE, IT HAS BEEN PASSED INTO THE NavMenu() SO THAT THE CURSOR IS ALWAYS WHERE YOU LEFT IT
                    //HOWEVER THIS HAS NOT WORKED IN EVERY PART OF THE CODE.
                }
                catch (Exception e)
                {
                    ErrorInRed(e.Message);
                    PleasePressEnter();
                }

            }
        }


        /*
         

          FOLLOWING ARE THE METHODS THAT MOSTLY ARE THERE TO MAKE THE APP MORE USER FRIENDLY, DESIGNWISE THAT IS.
         
        
        */
         


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
            Console.WriteLine($"\x1b[5m{title}");  //YOU CAN LOOK HERE, THE ESCAPE CODE IN THE WriteLine IS WHAT MAKES THE SIGN BLINK
            Console.ResetColor();
        }

        public static int NavMenu( List<string> options, int option = 0) //THIS IS THE MENU THAT IS MOSTLY REUSED
        {
            Console.CursorVisible = false; //THIS TURNS OF THE CURSOR VISIBILITY
            ConsoleKeyInfo key;  //ConsoleKeyInfo variable will read the key pressed by the user
            (int left, int top) = Console.GetCursorPosition(); //THIS WILL SET A CURSOR POSITION SO THE MENU REWRITES ON THE SAME POSITIOIN EVERYTIME YOU GO UPP OR DOWN
            string cursor = " -> ";
            bool selectedOption = false;
            while (!selectedOption)
            {
                Console.ForegroundColor = ConsoleColor.White;  //FOREGROUND COLOR IS SET QUITE OFTEN TO EMPHESIZE TEXT
                Console.SetCursorPosition(left, top); //EVERY TIME THE USER PRESSES UP OR DOWN THIS WHILE LOOP WILL RUN AGAIN AND THIS METHOD HERE WILL REWRITE THE MENU ON TOP OF THE OLD ONE
                for (int i = 0; i < options.Count; i++)
                {
                    Console.WriteLine($"{(option == i ? cursor : "    ")}{options[i]}");  //THIS IS WHERE THE MENU GETS OUTPUTTED
                }
                key = Console.ReadKey(true); //THE ConsoleKeyInfo VARIABLE THAT WAS DECLARED EARLIER IS SET TO Console.ReadKey(true) HERE
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option = (option == options.Count - 1 ? option = 0 : option + 1);  //THE SWITCH CASE TAKES IN the ConsoleKeyInfo variable 'key'
                        break;
                    case ConsoleKey.UpArrow:
                        option = (option == 0 ? option = options.Count - 1 : option - 1);
                        break;
                    case ConsoleKey.Enter:
                        selectedOption = true;
                        break;
                }
            }
            Console.ResetColor(); //THIS METHOD RESETS THE COLOR ON THE TEXT
            return option; //AN INT IS RETURNED
        }


        public static int CustomNavMenu(List<ProjectPersonModel> options = null, int option = 0) //THIS IS THE LATER MENU WHERE I REALIZED IT MIGHT ASWELL TAKE IN A WHOLE OBJECT LIST OR ARRAY
        {                                                      //THIS MENU WAS VERY MUCH NEEDED FOR THE FUNCTION OF editing registered hours.
            Console.CursorVisible = false;
            ConsoleKeyInfo key;
            (int left, int top) = Console.GetCursorPosition();
            var last = options.Last();
            string cursor = " -> ";
            bool selectedOption = false;
            while (!selectedOption)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(left, top);
                Console.WriteLine("    {0,-3}  {1, -10}  {2,-15}  {3,-5}\n", "(i)", "Person", "Project", "Hours"); //THIS MENU WAS CUSTOM MADE FOR THE METHOD THAT EDITS REGISTERED HOURS
                for (int i = 0; i < options.Count; i++)                                       //WHICH IS THE METHOD THAT CALLS THE DATABASE TABLE dac_project_person
                {
                    if (options[i] != last)
                        Console.WriteLine((option == i ? cursor : "    ") + " " + (i + 1) + ".  {0, -10}  {1,-15}   {2,-5}", options[i].person_name, options[i].project_name, options[i].hours);
                    else Console.WriteLine((option == i ? cursor : "    ") + $" \u001b[0m{options[i].person_name}");
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


        //SOME METHODS ONLY EXIST SO THAT I DON'T NEED TO REWRITE THE SAM PEACE OF CODE AND THAT ALSO MAKES SURE THAT IT ALWAYS LOOKS SIMILAR IN EVERY PART OF THE PROGRAM
        // ALL THE TITLES WHEN ENTERING AN OPTION WILL BE USING THIS METHOD
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

        //ARE YOU SURE, SOMETHING I FREQUENTLY USE
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
        
        //JUST A TEXT ASKING THE USER TO PRESS ENTER, NO OTHER KEY CAN BE PRESSED.
        public static void PleasePressEnter()
        {
            Console.CursorVisible = false;

            InstructionInYellow("Please press ENTER to continue");

            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
            Console.ResetColor ();
        }

        //THE METHOD BELOW IS USED ALOT, IT IS ASOCIATED WITH INSTRUCTIONS TO THE USER
        public static void InstructionInYellow(string input)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n   {input}\n");
            Console.ResetColor();
        }


        //THE METHOD BELOW ONLY OUTPUTS TEXT IN RED AS A WARNING OR ERROR.
        public static void ErrorInRed(string input)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n    {input}\n");
            Console.ResetColor();
        }


        //WHEN AN OPERATION WHENT WELL A GREEN TEXT WILL APPEAR
        public static void SuccessInGreen(string input)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n    {input}\n");
            Console.ResetColor();
        }

        //QUITE LIKE THE ARE YOU SURE
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

        /*
          
        NOW WE LEAVE ALL THE DESIGN METHODS AND ENTER THE MORE FUNCTIONAL ONES
        
         */

        //THIS METHOD BELOW GETS ONLY THE NAMES FROM THE dac_person TABLE IN THE DATABASE
        //IT CALLS THE SQL METHOD AND STORES ALL THE DATA IN A List<string> VARIABLE
        //IT ALSO CALLS THE NavMenu() METHOD WITH THE LIST AS ARGUMENT

        public static string GetPerson(string optionName, string message = "Choose person")
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            InstructionInYellow(message);

            List<string> personList = DatabaseAccess.GetPersonName();
            personList.Add("\u001b[0mBack to menu");    //NOTE HERE THAT Back to menu IS ADDED TO THE LIST OF PERSONS, THIS IS SO THAT THE USER CAN CHOOSE AN OPTION THAT WILL TAKE HIM/HER BACK

            int personIndex = NavMenu(personList);
            if (personIndex != personList.Count - 1)
            {
                return personList[personIndex];
            }
            else
            {
                Console.WriteLine($"\n   You choosed: \x1b[1m{personList[personIndex]}\x1b[0m\n");  //THE NavMenu() METHOD RETURNS THE INDEX OF THE NAME IN THE LIST
                return "";                                                          //THE INDEX IS LATER PASSED INTO THE LIST AND THATS HOW WE DISPLAY THE CORRECT NAME ALWAYS
            }                                                                       //IT WOULD HAVE BEEN BETTER TO USE id INSTEAD OF ONLY THE NAME BUT I MIGHT DO THIS CHANGE LATER ON
        }                                                                   //IF THE LAST OPTION ON THE LIST IS CHOOSEN, IT WILL RETURN "", WHICH WILL MEAN GO BACK IN THE METHOD THAT CALLS THIS ONE

        //THE FOLLOWING METHOD DOES THE EXACT SAME THING AS THE ABOVE ONE EXCEPT IT GETS DATA FROM THE project_name TABLE IN THE DATABASE 

        public static string GetProject(string optionName, string message = "Choose project", int index = 0)
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            InstructionInYellow(message);

            List<string> projectList = DatabaseAccess.GetProjectName(); // LIKE THE ABOVE METHOD, Back to menu IS ADDED HERE AT THE END OF THE LIST SO THAT USERS CAN GO BACK
            projectList.Add("\u001b[0mBack to menu");  // NOTE TO SELF, INSTEAD OF ADDING THIS, ONE COULD MAKE Esc button for going back ? Maybe...

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




        /*

        FOLLOWING IS WHAT YOU WOULD FIND INSIDE THE FIRST OPTION IN THE APPLICATION - Work Time Tracker

        THE FOLLOWING FOUR METHODS MAKE CALLS TO THE METHODS IN DatabaseAccess CLASS
        WHERE THEY GET THE DATA TO DISPLAY FOR THE USER

        THE FIFTH METHOD IS THE MENU WHERE THE USER CAN CHOOSE WHAT HE/SHE WHANTS TO SEE

        */



        //THIS WILL GET CALL THE METHOD FROM THE DatabaseAccess CLASS THAT GETS ONE PERSON ON ONE PROJECT FROM THE SQL DATABASE
        static void PersonOnAProject(string optionName)
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            string person = GetPerson(optionName, "Person on a project\n   Choose person");
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
                        InstructionInYellow($"\n   Table of {person} and hours registered on the {project} project\n   NOTE: Table order is from first (above) to last (below) registry");
                        List<ProjectPersonModel> personProjectList = DatabaseAccess.GetPersonOnOneProject(person, project);
                        var x = personProjectList;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("    __________________________________________");
                        Console.WriteLine("   | {0,-10}  | {1, -15}  |  {2,-5}  |", "Person", "Project", "Hours");              //HERE IN ORDER TO DISPLAY A TABLE PROPERLY I USE THE TECHNIQUE IN THE WriteLine()
                        Console.WriteLine("   |-------------|------------------|---------|");                               //HOWEVER IF THE STRING IS LONGER THAN THE LEFT NUMBER IN THE {0,-10} THEN THE ASCII WILL LOOK BAD
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


        //THE METHOD BELOW CALLS THE METHOD IN THE DatabaseAccess CLASS THAT GETS ONE PERSON ON MULTIPLE PROJECTS
        //THIS METHOD ONLY DISPLAYS THE DATA, LIKE THE METHOD ABOVE
        static void PersonOnMultipleProjects(string optionName)
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            while (true)
            {
                string person = GetPerson(optionName, "Person on multiple projects\n   Choose person");
                if (person != "")
                {
                    Console.Clear();
                    TitleScreen();
                    OptionTitleInRed(optionName);
                    InstructionInYellow($"\n   Table of {person} and hours registered on different projects\n   NOTE: Table order is from first (above) to last (below) registry");
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

        //THIS METHOD LIKE THE ABOVE ONE MAKES A CALL TO THE METHOD IN DatabaseAccess CLASS
        //IT DISPLAYS ONE PROJECT WITH MULTIPLE USERS ON IT
        static void ProjectWithMultiplePersons(string optionName)
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            int index = 0;
            while (true)
            {
                string project = GetProject(optionName, "Project with multiple person\n   Choose project", index);
                if (project != "")
                {
                    Console.Clear();
                    TitleScreen();
                    OptionTitleInRed(optionName);
                    InstructionInYellow($"\n   Table of the {project} project\n   and everyone that has registered hours on it\n   NOTE: Table order is from first (above) to last (below) registry");
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


        //THIS METHOD DISPLAYS ALL PROJECTS AND ALL USERS AND ALL REGISTERED HOURS, IF ONE WANTS TO KNOW THAT
        static void AllProjectsAllPersons(string optionName)
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            InstructionInYellow("\n   Table of all projects, persons and registered hours\n   Total amount of registered hours at the bottom\n   NOTE: Table order is from first (above) to last (below) registry");
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

        public static void WorkTimeTrack(string optionName = "Work Time Tracker") //THE METHOD THAT LETS YOU CHOOSE BETWEEN THE FUNCTIONS ABOVE
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


        /*
          
          THE FOLLOWING METHOD IS THE SECCOND OPTION IN THE APPLICATION, WHERE THE USER REGISTERES HOURS ON A PROJECT
         
         */

        public static void RegTime(string optionName = "Register time on project")
        {
            
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            bool isRunning = true;
            while (isRunning)
            {
                string person = GetPerson(optionName);  //CALLS THE METHOD THAT GETS ALL USERS
                if (person == "") { break; }

                string project = GetProject(optionName); //CALLS THE METHOD THAT GETS ALL PROJECTS
                if (project == "") { break; }

                InstructionInYellow("Input hours spent on project today\n   Leave blank to cancel");

                Console.CursorVisible = true;
                Console.Write(" ==> ");
                string hours = Console.ReadLine();             //USER GETS TO INSERT AMOUNT OF HOURS AS A STRING
                if (hours == "")
                {
                    Console.WriteLine("\n    Canceling");    //IF THE STRING IS EMPTY THE USER CAN CANCEL
                    break;
                }
                int newHours = Convert.ToInt32(hours);    //IF THE USER INSERTED A VALUE AND THE FORMAT WAS CORRECT THIS METHOD WILL CONVERT THE string TO AN int
                Console.CursorVisible = false;


                //Console.WriteLine($"\n   \u001b[1m{person}\u001b[0m spent \u001b[1m{hours}\u001b[0m hours on \u001b[1m{project}\u001b[0m today");
                string message = $"\n   \u001b[1m{person}\u001b[0m spent \u001b[1m{newHours}\u001b[0m hours on \u001b[1m{project}\u001b[0m today\n";
                string yesNo = IsCorrect(optionName ,message);
                if (yesNo == "yes")                                                 //USER GETS TO MAKE SURE HE/SHE TYPED CORRECTLY
                {
                    bool success = DatabaseAccess.RegistrateHoursInDB(project, person, newHours);
                    if (success)
                    {
                        SuccessInGreen("Great success!");
                        Console.WriteLine($"\n   \u001b[1mRegistered\u001b[0m: \u001b[1m{person}\u001b[0m spent \u001b[1m{newHours}\u001b[0m hours on \u001b[1m{project}\u001b[0m today");

                    }
                    else                                        //IF THE OPERATION WHENT WELL, A TEXT WILL BE DISPLAYED IN GREEN OR ELSE IT WILL BE DISPLAYED IN RED
                    {
                        ErrorInRed("Unsuccessful to register hours");
                    }
                }
                else { Console.WriteLine($"\n   You choosed {yesNo}"); }
                
                isRunning = false;
            }
            PleasePressEnter();
        }

        /*
          
         END OF THE SECCOND OPTION IN THE MAIN MENU

        FOLLOWING ARE THE TWO NEXT OPTIONS IN THE MAIN MENU New project AND New person.
         
        THESE METHODS MAKE CALLS TO THE METHODS IN THE DatabaseAccess CLASS THAT USE SQL INSERT CODE IN THE DATABASE

        */


        //THE BELOW METHOD INSERTS A NEW PROJECT NAME OF THE USERS CHOOSING
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

        //LIKE THE ABOVE METHOD THE BELOW ONE DOES THE SAME BUT INSERTS A NEW PERSON NAME INSTEAD 
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


        /*
         * END OF OPTION THREE AND FOUR
         * 
         * 
         * FOLLWING ARE OPTIONS FIVE, SIX AND SEVEN. THE OPTIONS THAT ALTER/EDIT DATA ALREADY PUTTED IN
         * 
         */


        //THE FOLLOWING TWO METHODS MIGHT BE A BIT TRICKY TO UNDESTAND
        //ONE IS THE OPTION IN THE MAIN MENU, THE OTHER ONE IS THE FUNCTIONALITY INSIDE OF IT

        //THE BELOW METHOD IS WHAT MAKES IT POSSIBLE TO ADIT REGISTERED HOURS, THE METHOD MAKES CALL TO THE METHOD IN DatabaseAccess.cs THAT ALTERS A ROW IN dac_project_person,
        //DEPENDING ON THE id THAT GETS PASSED IN
        static void EditRegTimeOnPerson(List<ProjectPersonModel> list, string optionName, string person) //A LIST OF THE ProjectPersonModel OBJECT GETS PASSED IN
        {                                                                                   //THE string person WILL BE AN OPTION MADE IN THE SCREEN BEFORE THIS ONE
            while (true)                                                                    //WHERE THE USER MUST CHOOSE THE PERSON WHOM THIS WILL AFFECT
            {                                                     //THE string optionName IS ONLY SO THAT THIS SCREEN LOOKS LIKE THE FORMER ONE
                Console.Clear();
                TitleScreen();
                OptionTitleInRed(optionName);
                InstructionInYellow("\n   Please choose the registry that you wish to alter\n   NOTE: Index (i) indicates order from oldest to latest");
                int option = CustomNavMenu(list);  //THE CUSTOM MENY CustomNavMenu THAT TAKES IN AN OBJECT LIST AS ARGUMENT GETS CALLED HERE
                if (option == list.Count - 1)
                {
                    Console.WriteLine($"\n   You choosed: {list[option].person_name}"); //THE LAST OPTIOIN ON THAT LIST WAS THE TEXT Back to menu, IF THE USER CHOOSES THAT, THE LOOP BREAKS
                    break;
                }
                else
                {
                    Console.WriteLine($"\n   You choosed: \u001b[1m{option + 1}. {list[option].person_name}   {list[option].project_name}  {list[option].hours}\u001b[0m hours");
                }

                InstructionInYellow("Please enter new amount of hours\n   Leave blank to cancel");
                Console.CursorVisible = true;
                Console.Write(" ==> ");
                string hours = Console.ReadLine();
                Console.CursorVisible = false;
                if (hours == "")
                {
                    Console.WriteLine("\n   Canceling");
                    break;
                }
                int newHours = Convert.ToInt32(hours);
                string yesNo = IsCorrect(optionName, $"\n   Alter hours on index \u001b[1m{option + 1}\u001b[0m from \u001b[1m{list[option].hours}\u001b[0m to \u001b[1m{newHours}\u001b[0m\n");
                if (yesNo == "yes")
                {
                    bool success = DatabaseAccess.EditRegisteredHours(list[option].id, newHours);  //THE RIGHT ID GETS PASSED IN HERE TO THE METHOD IN THE DatabaseAccess CLASS
                    if (success == true)                                                        //ALSO THE NEW AMOUNT OF HOURS OF COURSE
                    {                                                                          //THE USER IS NEVER AWARE OF THE ID'S OF THE ALL THE PERSONS, PROJECTS OR dac_project_person TABLE IN THE APP OR DATABASE
                        SuccessInGreen("Greate success!");
                        break;
                    }
                    else 
                    { 
                        ErrorInRed("Unable to alter hours"); 
                        break;
                    }
                }
            }
        }

        //THE BELOW METHOD IS LINKED TO THE ABOVE ONE, THIS IS WHAT YOU WILL GET WHEN YOU ENTER OPTION FIVE IN THE MAIN MENU
        //THIS METHOD GETS ALL THE PERSONS SO THE USER CAN CHOOSE WHOM IT WANTS TO AFFECT
        static void EditRegTime(string optionName = "Edit registered hours")
        {
            while (true)
            {
                Console.Clear();
                TitleScreen();
                OptionTitleInRed(optionName);
                string person = GetPerson(optionName, "Choose person");
                if (person == "") { PleasePressEnter(); break; }
                List<ProjectPersonModel> PersonOnAProjectList = DatabaseAccess.GetPersonWithManyProjects(person);
                PersonOnAProjectList.Add(new ProjectPersonModel { person_name = "Back to menu" });
                //PersonOnAProjectList.Insert(0, new ProjectPersonModel { person_name = "Back to menu" });

                EditRegTimeOnPerson(PersonOnAProjectList, optionName, person);

                PleasePressEnter();
                break;
            }
        }

        //THE FOLLOWING METHOD IS OPTION NUMBER SIX IN THE MAIN MENU
        //THIS IS WHERE THE USER CHANGES THE NAME OF A PROJECT
        // THIS METHODS THEN CALLS A METHOD IN DatabaseAccess.cs WITH THE NEW NAME AND THAT METHOD WILL MAKE THE SQL CALL WITH THE INSERT CODE
        static void EditProject(string optionName = "Edit project")
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);
            bool isRunning = true;
            while (isRunning)
            {
                string projectName = GetProject(optionName);  // USER GETS TO CHOOSE WHICH PROJECT TO AFFECT FROM A LIST IN THIS METHOD, 
                if (projectName == "") { break; }      //THE NAME OF THE PROJECT GETS RETURNED, IF THE LAST OPTION ON THE LIST, THEN "" WILL BE RETURNED, WHICH MEANS TO GO BACK TO MENU
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
                    bool success = DatabaseAccess.EditProjectName(projectName ,newName);  //HERE IS WHERE THE NEW AND THE OLD NAME GETS PASSED INTO A METHOD THAT PERFORMES THE OPERATION IN THE SQL DATABASE
                    if (success == true) 
                    { 
                        SuccessInGreen("Project name was successfully changed!");  //IF SUCCESS, MESSAGE IN GREEN, IF FAIL, MESSAGE IN RED
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


        //THE FOLLOWING CODE DOES THE SAME AS ABOVE EXCEPT IT DOES IT FOR PERSON NAMES ONLY
        //THE STRUCTURE IS PRETTY MUCH THE SAME AS THE ABOVE ONE
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

        /*
         * 
         * AND SO THE OPTIONS FIVE, SIX AND SEVEN ENDS HERE
         * 
         * 
         * NEXT ARE THE OPTIONS EIGHT AND NINE, THE DELETE OPTIONS
         * 
         * THESE OPTIONS ARE PRETTY LIMITED, THEY CAN ONLY DELETE PROJECTS THAT HAVE NO HOURS REGISTERED TO THEM
         * THE SAME WITH PERSONS, ONLY PERSON THAT HAS NOT REGISTERED ANY HOURS CAN BE DELETED
         * THIS HAS TO DO WITH VIOLATION OF FOREIGN KEY CONSTRAIN IN THE dac_project_person TABLE IN THE DATABASE
         * 
         * I want to - however - make deletion method for rows in dac_project_person table and also delete all the data from the tables to start over
         * 
         */


        //THE FOLLOWING METHOD DELETES A PROJECT THAT HAS NO HOURS REGISTERED TO IT
        static void DeleteProject(string optionName = "Delete project")
        {
            Console.Clear();
            TitleScreen();
            OptionTitleInRed(optionName);

            while (true)
            {               //THE USER CHOOSES THE PROJECT NAME FROM A LIST, WHICH THE METHOD GetProject() PROVIDES
                string project = GetProject(optionName, "Choose project to DELETE\n   Project with registered hours cannot be deleted\n\n   \u001b[31mWARNING: DELETION IS PERMANENT!");
                if (project == "") break;
                int yesNo = AreYouSure(optionName, $"   Do you wish to DELETE {project}?\n");
                if (yesNo == 0)
                {
                    bool success = DatabaseAccess.DeleteProjectName(project);  //IF THE PROJECT CAN BE DELETED, IT WILL BE DELETED BY NAME IN THE DATABASE AND THE METHOD CALLED ON THIS LINE WILL MAKE IT SO
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
        
        //THE BELOW METHOD IS JUST LIKE THE ABOVE METHOD, EXCEPT.... YOU GUESSED IT.... IT DOES IT FOR PERSON INSTEAD OF PROJECTS
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
                    bool success = DatabaseAccess.DeletePersonName(person);         //IT'S EXACTLY THE SAME THING AS ABOVE
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
        

        /*
         * 
         * END OF THE DELETION METHODS
         * 
         * AND LASTLY THE QUITTING OR CLOSING OF THE APPLICATION
         * 
         * I KNOW WHAT YOU ARE THINKING, "BUT WHY Enviroment.Exit(0)?" WELL, BECAUSE, IT WORKS WELL, mKAY?..
         * 
         */

        static void Quit(string optionName = "Close application")
        {
            Console.Clear();
            //int index = 1;
            TitleScreen();
            OptionTitleInRed(optionName);
            int option = AreYouSure(optionName, "   This will close the application\n");
            if (option == 0) Environment.Exit(0);
        }

        /*
         * 
         * THE END
         * 
         */


    }
}


