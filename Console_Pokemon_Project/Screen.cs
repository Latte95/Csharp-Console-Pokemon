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

        public static void print(char[,] pixel, int xLength)
        {
            for (int y = 0; y < WINDOW_HEIGHT; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    Console.Write(pixel[x, y]);
                }
                Console.WriteLine();
            }
        }
    }
}
