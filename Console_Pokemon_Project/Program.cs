using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Console_Pokemon_Project
{
    class Program
    {
        public static List<string> titleOption = new List<string> { "새로하기", "불러오기", "게임 종료", "게임 종료", "게임 종료", "게임 종료" };

        static void Main(string[] args)
        {
            Screen.PreferencesWindow();
            Title();
        }

        public static void Title()
        {
            string select;

            select = Menu.SelectMenu((Screen.WINDOW_WIDTH >> 2)-10, 30, titleOption);
            switch (select)
            {
                case "새로하기":
                    Console.Clear();
                    GameManager.Start();
                    break;
                case "불러오기":
                    Console.Clear();
                    //if()
                    GameManager.Start();
                    break;
                case "게임 종료":
                    return;
            }
        }
    }
}
