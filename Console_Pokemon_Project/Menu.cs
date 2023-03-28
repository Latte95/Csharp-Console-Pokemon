using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    class Menu
    {
        // 상점이면 아이템구입, 아이템판매 등등
        // 전투면 전투, 아이템사용, 도망
        // 전투면 무슨 스킬 선택할지
        // 키입력 감지해서 무슨 메뉴를 선택했는지 판단

        const int CURSOR_X_LENGTH = 2;

        // 메뉴가 출력되어야 될 위치값을 dialoqueXY로 전달받음
        public string SelectMenu<T>(int dialoqueX, int dialoqueY, T menu) where T : List<string>
        {
            // 선택 포인터(▶) 위치를 제한하기 위한 변수
            int pointerY = 0;
            // ▶가 옮겨지기 전의 위치
            int oldPointerY = 0;
            // 선택 가능한 메뉴 수
            int menuLength = menu.Count();

            // 메뉴 출력
            PrintMenu(dialoqueX, dialoqueY, menu);
            // ▶ 출력
            PrintPointer(pointerY, oldPointerY, dialoqueX, dialoqueY);

            // 키 입력에 따라 ▶ 위치를 바꿔주는 반복문
            while (true)
            {
                // 사용자가 입력한 키를 저장하는 변수
                ConsoleKey key = Console.ReadKey(true).Key;
                // 사용자가 키입력을 하기 전의 위치를 저장
                oldPointerY = pointerY;
                // 입력한 키에 따른 행동을 정하는 조건문
                switch (key)
                {
                    // ▶이동
                    // ▶가 메뉴 밖으로 나가지 않게 하기 위한 나머지 연산
                    case ConsoleKey.UpArrow:
                        // 처음 위치가 0이기 때문에 menuLength를 한번 더해줌
                        pointerY = (pointerY - 1 + menuLength) % menuLength;
                        break;
                    case ConsoleKey.DownArrow:
                        pointerY = (pointerY + 1) % menuLength;
                        break;

                    // 선택
                    // 엔터 또는 스페이스바를 눌렀을 때 메뉴가 선택됨
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        // ▶위치의 메뉴를 반환함
                        return menu[pointerY];
                }

                // ▶ 위치가 바뀌었다면 ▶ 위치를 갱신함
                if (pointerY != oldPointerY)
                {
                    // 바뀐 ▶의 위치를 새로 그려줌 
                    PrintPointer(pointerY, oldPointerY, dialoqueX, dialoqueY);
                }
            }
        }

        // 메뉴 목록만 출력
        public void PrintMenu<T>(int dialoqueX, int dialoqueY, T menu) where T : List<string>
        {
            int menuLength = menu.Count;

            for (int i = 0; i < menuLength; i++)
            {
                // 커서 위치를 고려하여 커서의 x위치에 2를 더해줌
                Console.SetCursorPosition(dialoqueX + CURSOR_X_LENGTH, dialoqueY + i);
                Console.WriteLine(menu[i]);
            }
        }

        // 커서 위치 갱신
        static void PrintPointer(int newPosition, int oldPointerY, int dialoqueX, int dialoqueY)
        {
            // 커서만 새로 그려주도록 구현
            // 원래 ▶가 있던 위치를 공백을 통해 지우고
            Console.SetCursorPosition(dialoqueX, oldPointerY + dialoqueY);
            Console.Write("  ");
            // 옮겨진 위치에 ▶ 표시
            Console.SetCursorPosition(dialoqueX, newPosition + dialoqueY);
            Console.Write("▶");
        }
    }
}
