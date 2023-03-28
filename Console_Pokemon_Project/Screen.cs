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
        const int WINDOW_WIDTH = 120;
        const int WINDOW_HEIGHT = 45;
        const string TITLE_NAME = "포켓 몬스터가 되지 못한 무언가";
        // 출력 위치 상수
        const int MAP_PRINT_X = 0;
        const int MAP_PRINT_Y = 0;
        const int MAP_MENU_X = Map.MAP_WIDTH + 2;
        const int MAP_MENU_Y = WINDOW_HEIGHT / 2 + 2;
        const int BATTLE_DIALOGUE_PRINT_X = 4;
        const int BATTLE_DIALOGUE_PRINT_Y = Pokemon.GRAPHIC_HEIGHT + 2;
        const int ENEMY_PRINT_X = WINDOW_WIDTH - Pokemon.GRAPHIC_WIDTH - 1;
        const int ENEMY_PRINT_Y = 1;
        const int PLAYER_PRINT_X = 2;
        const int PLAYER_PRINT_Y = Pokemon.GRAPHIC_HEIGHT / 2;

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

            Judge(xLength, yLength);

            for (int y = 0; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    Console.Write(pixel[x, y]);
                }
                Console.WriteLine();
            }
        }

        public static void Judge(int xLength, int yLength)
        {
            int where;

            #region pixel 크기로 어디서 준 정보인지 판단
            if (xLength.Equals(Map.MAP_WIDTH) && yLength.Equals(Map.MAP_HEIGHT))
            {
                where = (int)ScreenType.맵;
            }
            else if (xLength.Equals(WINDOW_WIDTH) && yLength.Equals(WINDOW_HEIGHT))
            {
                where = (int)ScreenType.배틀;
            }
            else
            {
                where = 99;
            }
            #endregion

            switch (where)
            {
                case (int)ScreenType.맵:
                    DrawMenual();
                    Console.SetCursorPosition(MAP_PRINT_X, MAP_PRINT_Y);
                    break;
                case (int)ScreenType.배틀:
                    Console.SetCursorPosition(MAP_PRINT_X, MAP_PRINT_Y);
                    break;
            }
        }

        private static void DrawMenual()
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
