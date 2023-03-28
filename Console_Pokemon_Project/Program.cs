using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        public static void Start()
        {
            Screen.PreferencesWindow();
            Map map = new Map(0, 0);

            // Battle battle = new Battle();

            //Screen.print(pixel);
            //if (Console.ReadKey(true).Key == ConsoleKey.Escape)
            //{
            //  Menu.SelectMenu(cursorX, cursorY, option);
            //}
        }
    }
}
