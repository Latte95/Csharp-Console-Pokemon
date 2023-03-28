﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    // 콘솔창 크기 세팅
    // 몬스터 그래픽 불러와서 출력

    // 맵, 전투화면 출력하고
    // 전투에서 : 전투, 아이템사용, 도망 등 선택지(커서)
    // 스킬에서 : 스킬 뭐 선택할지 선택지(커서)

    class Screen
    {
        private enum ScreenType
        {
            맵,
            배틀,
            몬스터
        }

        const int WINDOW_WIDTH = 120;
        const int WINDOW_HEIGHT = 45;
        const string TITLE_NAME = "포켓 몬스터가 되지 못한 무언가";


        // 콘솔창 기본 설정
        public static void PreferencesWindow()
        {
            Console.Title = TITLE_NAME;
            Console.WindowWidth = WINDOW_WIDTH + 1;
            Console.WindowHeight = WINDOW_HEIGHT + 1;
            Console.CursorVisible = false;
        }

        public static void print(char[,] pixel, ConsoleKey key)
        {
            // pixel 배열의 길이 저장
            int xLength = pixel.GetLength(0);
            int yLength = pixel.GetLength(1);
            int where;
            if (xLength.Equals(Map.MAP_WIDTH) && yLength.Equals(Map.MAP_HEIGHT))
            {
                where = (int)ScreenType.맵;
            }
            else if (xLength.Equals(WINDOW_WIDTH) && yLength.Equals(WINDOW_HEIGHT))
            {
                where = (int)ScreenType.배틀;
            }

            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < WINDOW_HEIGHT; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    Console.Write(pixel[x, y]);
                }
                Console.WriteLine();
            }
            if (xLength < 100)
            {
                int menualCursorX = 90;
                int menualCursorY = 5;
                int blankYSpace = 2;
                Console.SetCursorPosition(menualCursorX, menualCursorY++);
                Console.Write("         ↑");
                Console.SetCursorPosition(menualCursorX, menualCursorY);
                Console.Write("이동 : ←↓→");
                menualCursorY += blankYSpace;
                Console.SetCursorPosition(menualCursorX, menualCursorY);
                Console.Write("선택 : 엔터");
                menualCursorY += blankYSpace;
                Console.SetCursorPosition(menualCursorX, menualCursorY);
                Console.Write("메뉴 : ESC");
            }
        }
    }
}
