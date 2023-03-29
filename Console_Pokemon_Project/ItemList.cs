using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    class ItemList
    {
        const int CURSOR_X_LENGTH = 2;
        // 한번에 출력할 아이템 수
        const int ITEM_LENGTH = 4;

        public static string SelectMenu<T>(int dialoqueX, int dialoqueY, T menu) where T : List<Item>
        {
            // 선택 포인터(▶) 위치를 제한하기 위한 변수
            int pointerY = 0;
            // ▶가 옮겨지기 전의 위치
            int oldPointerY = 0;
            // 맨위에 출력될 아이템 인덱스
            int itemIndex = 0;
            // 위를 눌렀는지 아래를 눌렀는지
            bool isUp;
            bool isDown;

            // 아이템 출력
            PrintMenu(dialoqueX, dialoqueY, menu, itemIndex);
            // ▶ 출력
            PrintPointer(pointerY, oldPointerY, dialoqueX, dialoqueY);

            // 키 입력에 따라 ▶ 위치를 바꿔주는 반복문
            while (true)
            {
                // 사용자가 입력한 키를 저장하는 변수
                ConsoleKey key = Console.ReadKey(true).Key;
                isUp = false;
                isDown = false;

                // 사용자가 키입력을 하기 전의 위치를 저장
                oldPointerY = pointerY;
                // 입력한 키에 따른 행동을 정하는 조건문
                switch (key)
                {
                    // ▶이동
                    case ConsoleKey.UpArrow:
                        if (pointerY.Equals(0))
                        {
                            isUp = true;
                        }
                        else
                        {
                            pointerY = pointerY - 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (pointerY.Equals(ITEM_LENGTH - 1))
                        {
                            isDown = true;
                        }
                        else
                        {
                            pointerY = pointerY + 1;
                        }
                        break;

                    // 선택
                    // 엔터 또는 스페이스바를 눌렀을 때 메뉴가 선택됨
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        // ▶위치의 메뉴를 반환함
                        return menu[pointerY].name;
                }

                // ▶ 위치가 바뀌었다면 ▶ 위치를 갱신함
                //if (pointerY != oldPointerY)
                //{
                //    // 바뀐 ▶의 위치를 새로 그려줌 
                //}
                // 위를 눌렀는데 ▶ 위치가 그대로면 아이템 스크롤을 올려줌
                if (isUp.Equals(true) && itemIndex > 0)
                {
                    ItemScrollUp(ref itemIndex);
                    PrintMenu(dialoqueX, dialoqueY, menu, itemIndex);
                }
                // 아래를 눌렀는데 ▶ 위치가 그대로면 아이템 스크롤을 올려줌
                else if (isDown.Equals(true) && itemIndex < menu.Count - ITEM_LENGTH)
                {
                    ItemScrollDown(ref itemIndex);
                    PrintMenu(dialoqueX, dialoqueY, menu, itemIndex);
                }

                PrintPointer(pointerY, oldPointerY, dialoqueX, dialoqueY);
            }
        }

        public static void ItemScrollUp(ref int itemIndex)
        {
            itemIndex--;
        }

        public static void ItemScrollDown(ref int itemIndex)
        {
            itemIndex++;
        }

        // 메뉴 목록만 출력
        private static void PrintMenu<T>(int dialoqueX, int dialoqueY, T menu, int itemIndex) where T : List<Item>
        {
            Shop.ClearShopContents();
            for (int i = 0; i < menu.Count; i++)
            {
                // ▶ 위치를 고려하여 커서의 x위치에 2를 더해줌
                Console.SetCursorPosition(dialoqueX + CURSOR_X_LENGTH, dialoqueY + i);

                Console.WriteLine($"{menu[itemIndex + i].name} - {menu[itemIndex + i].quantity}개");
            }
        }

        // 커서 위치 갱신
        private static void PrintPointer(int newPosition, int oldPointerY, int dialoqueX, int dialoqueY)
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
