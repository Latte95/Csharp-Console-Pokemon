using System;
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
            배틀
        }

        // 콘솔 세팅 상수
        public const int WINDOW_WIDTH = 120;
        public const int WINDOW_HEIGHT = 45;
        const string TITLE_NAME = "포켓 몬스터가 되지 못한 무언가";
        // 출력 위치 상수
        const int MAP_PRINT_X = 0;
        const int MAP_PRINT_Y = 0;
        const int BATTLE_PRINT_X = 0;
        const int BATTLE_PRINT_Y = 0;

        // 콘솔창 기본 설정
        public static void PreferencesWindow()
        {
            Console.Title = TITLE_NAME;
            Console.WindowWidth = WINDOW_WIDTH + 1;
            Console.WindowHeight = WINDOW_HEIGHT + 1;
            Console.CursorVisible = false;

        }


        public static void Print(char[,] pixel)
        {
            // pixel 배열의 길이 저장
            int xLength = pixel.GetLength(0);
            int yLength = pixel.GetLength(1);
            int where;

            // pixel크기를 통해 Print를 호출한 곳을 유추
            where = Judge(xLength, yLength);

            for (int y = 0; y < WINDOW_HEIGHT; y++)
            {// 한줄 출력할 때 마다 커서의 X위치 설정
                StringBuilder line = new StringBuilder();
                for (int x = 0; x < WINDOW_WIDTH; x++)
                {
                    if (y < yLength && x < xLength)
                    {
                        line.Append(pixel[x, y]);
                    }
                    else
                    {
                        line.Append("　");
                    }
                }

                int cursorXPosition = 0;
                int cursorYPosition = y;
                switch (where)
                {
                    case (int)ScreenType.맵:
                        cursorXPosition = MAP_PRINT_X;
                        cursorYPosition += MAP_PRINT_Y;
                        break;
                    case (int)ScreenType.배틀:
                        cursorXPosition = BATTLE_PRINT_X;
                        cursorYPosition += BATTLE_PRINT_Y;
                        break;
                }
                Console.SetCursorPosition(cursorXPosition, cursorYPosition);
                Console.Write(line.ToString());
            }

        }
        //public static void Print(char[,] pixel)
        //{
        //    // pixel 배열의 길이 저장
        //    int xLength = pixel.GetLength(0);
        //    int yLength = pixel.GetLength(1);
        //    int where;

        //    // pixel크기를 통해 Print를 호출한 곳을 유추
        //    where = Judge(xLength, yLength);

        //    // 호출 위치에 맞는 커서 위치 설정
        //    switch (where)
        //    {
        //        case (int)ScreenType.맵:
        //            Console.SetCursorPosition(MAP_PRINT_X, MAP_PRINT_Y);
        //            break;
        //        case (int)ScreenType.배틀:
        //            Console.SetCursorPosition(BATTLE_PRINT_X, BATTLE_PRINT_Y);
        //            break;
        //    }

        //    for (int y = 0; y < yLength; y++)
        //    {
        //        // 한줄 출력할 때 마다 커서의 X위치 설정
        //        switch (where)
        //        {
        //            case (int)ScreenType.맵:
        //                Console.SetCursorPosition(MAP_PRINT_X, Console.CursorTop);
        //                break;
        //            case (int)ScreenType.배틀:
        //                Console.SetCursorPosition(BATTLE_PRINT_X, Console.CursorTop);
        //                break;
        //        }
        //        // 설정된 커서에서 한 줄 출력
        //        for (int x = 0; x < xLength; x++)
        //        {
        //            Console.Write(pixel[x, y]);
        //        }
        //        Console.WriteLine();
        //    }
        //}

        public static int Judge(int xLength, int yLength)
        {
            // pixel 크기로 어디서 보낸 그래픽인지 유추
            if (xLength.Equals(Map.MAP_WIDTH) && yLength.Equals(Map.MAP_HEIGHT))
            {
                return (int)ScreenType.맵;
            }
            else if (xLength.Equals(WINDOW_WIDTH) && yLength.Equals(WINDOW_HEIGHT))
            {
                return (int)ScreenType.배틀;
            }
            else
            {
                return 99;
            }
        }

        private static void DrawMenual(string mapName)
        {
            int menualCursorX = Map.MAP_WIDTH*2 + 10;
            int menualCursorY = 5;
            // 줄 간격
            int blankYSpace = 2;
            Console.SetCursorPosition(menualCursorX, 0);
            Console.Write(mapName);
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
