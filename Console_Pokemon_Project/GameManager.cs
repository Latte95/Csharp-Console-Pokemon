using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Console_Pokemon_Project.Map;

namespace Console_Pokemon_Project
{
    class GameManager
    {
        // 맵 정사각형 모양으로 배치했을 때 몇 줄인지 => ROW * COL개
        const int MAX_MAP_ROW_COUNT = 2;
        const int MAX_MAP_COL_COUNT = 2;
        public static void Start()
        {
            //Screen.PreferencesWindow();
            List<Map> mapList = new List<Map>();
            for (int i = 0; i < MAX_MAP_ROW_COUNT; i++)
            {
                for (int j = 0; j < MAX_MAP_COL_COUNT; j++)
                {
                    mapList.Add(new Map($"{(i*MAX_MAP_COL_COUNT) + j+1}번째 맵", j * MAP_WIDTH, i * MAP_HEIGHT));
                }
            }
            while (true)
            {
                if (Player.instance.isInBattle == false)
                {
                    if (Player.instance.locX < 0)
                    {
                        Player.instance.locX = 0;
                    }
                    else if (Player.instance.locY < 0)
                    {
                        Player.instance.locY = 0;
                    }
                    else if (Player.instance.locX > MAP_WIDTH * MAX_MAP_COL_COUNT - 1)
                    {
                        Player.instance.locX = MAP_WIDTH * MAX_MAP_COL_COUNT - 1;
                    }
                    else if (Player.instance.locY > MAP_HEIGHT * MAX_MAP_ROW_COUNT - 1)
                    {
                        Player.instance.locY = MAP_HEIGHT * MAX_MAP_ROW_COUNT - 1;
                    }

                    for (int i = 0; i < MAX_MAP_COL_COUNT * MAX_MAP_ROW_COUNT; i++)
                    {
                        if (Player.instance.locX >= mapList[i].startXLoc &&
                            Player.instance.locX < mapList[i].startXLoc + MAP_WIDTH &&
                            Player.instance.locY >= mapList[i].startYLoc &&
                            Player.instance.locY < mapList[i].startYLoc + MAP_HEIGHT)
                        {
                            mapList[i].WaitPlayerInput();
                        }
                    }
                }
                if (Player.instance.isInBattle == true)
                {
                    Battle ga = new Battle();
                    ga.MeetPokemon();
                }
                
                // 여기부터는 배틀차례
                //if(Player.instance.isInBattle == true)
                //{

                //}
                // Battle battle = new Battle();

                //Screen.print(pixel);
                //if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                //{
                //  Menu.SelectMenu(cursorX, cursorY, option);
                //}
            }
        }
    }
}
