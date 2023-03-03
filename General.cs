﻿using Microsoft.VisualBasic.FileIO;
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
            TitleScreen();

            //String[] arr = { "Register working hours in project", "New project", "New person", "Edit project", "Edit person" , "Quit" };
            
            NavMenu(new String[] { "Register working hours in project", "New project", "New person", "Edit project", "Edit person", "Quit" });
            



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

        public static void MenuOptions()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n   Please use UP and DOWN arrows to navigate\n   Press ENTER to select\n");
            //Change foreground color in a string using this example: "\u001b[32mOption 1."
            //To reset color on the same string you can do like this: "\u001b[32mOption 1.\u001b[0m"
            Console.ResetColor();
            Console.CursorVisible = false;

            string cursor = " -> ";
            ConsoleKeyInfo key;
            (int left, int top) = Console.GetCursorPosition();
            int option = 1;
            bool selectedOption = false;
            while (!selectedOption)
            {
                Console.SetCursorPosition(left, top);

                Console.WriteLine($"{(option == 1 ? cursor : "    ")}Register working hours in project");
                Console.WriteLine($"{(option == 2 ? cursor : "    ")}New project");
                Console.WriteLine($"{(option == 3 ? cursor : "    ")}New person");
                Console.WriteLine($"{(option == 4 ? cursor : "    ")}Edit project");
                Console.WriteLine($"{(option == 5 ? cursor : "    ")}Edit person");
                Console.WriteLine($"{(option == 6 ? cursor : "    ")}Quit");
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option = (option == 6 ? 1 : option + 1);
                        break;
                    case ConsoleKey.UpArrow:
                        option = (option == 1 ? 6 : option - 1);
                        break;
                    case ConsoleKey.Enter:
                        selectedOption = true;
                        break;
                    default:
                        break;
                }
            }
            if (option == 1) Console.WriteLine("\nYou chosed Register working hours in project\n");
            if (option == 6)
            {
                Console.WriteLine("\nYou chosed Quit\nBye bye.");
            }
            //Console.WriteLine($"You choosed option {option}");

            String[] strings = {"Play","Settings","Online","Quit"};
            NavMenu(strings);

        }

        public static void NavMenu(String[] options)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n   Please use UP and DOWN arrows to navigate\n   Press ENTER to select\n");
            Console.ResetColor();

            //Change foreground color in a string using this example: "\u001b[32mOption 1."
            //To reset color on the same string you can do like this: "\u001b[32mOption 1.\u001b[0m"


            Console.CursorVisible = false;
            int option = 0;
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

        }
        
        static void RegTime()
        {
            Console.WriteLine("Register work time");
            Console.ReadKey(true);
        }
        static void NewProject()
        {
            Console.WriteLine("Add new project");
            Console.ReadKey(true);
        }
        static void NewPerson()
        { 
            Console.WriteLine("Add new person");
            Console.ReadKey(true);
        }
        static void EditProject()
        { 
            Console.WriteLine("Edit project");
            Console.ReadKey(true);
        }
        static void EditPerson()
        { 
            Console.WriteLine("Edit person");
            Console.ReadKey(true);
        }        
        static void Quit()
        { 
            Console.WriteLine("Quit");
            Console.ReadKey(true);
        }
    }
    
}