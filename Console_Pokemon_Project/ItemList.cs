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

            Console.SetCursorPosition(dialoqueX*2 + CURSOR_X_LENGTH + 2, dialoqueY + 1);
            if (itemIndex >= menu.Count)
            {
                Console.SetCursorPosition(dialoqueX * 2, dialoqueY + menu.Count + 2);
                Console.WriteLine("보유 아이템이 없습니다.");
                Console.ReadKey(true);
                return null;
            }

            // 아이템 출력
            PrintMenu(dialoqueX, dialoqueY, menu, itemIndex);
            // ▶ 출력
            Menu.PrintPointer(pointerY, oldPointerY, dialoqueX, dialoqueY);

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
                        if (pointerY.Equals(ITEM_LENGTH - 1) || pointerY.Equals(menu.Count - 1))
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

                Menu.PrintPointer(pointerY, oldPointerY, dialoqueX, dialoqueY);
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
            int menuLength;
            if(menu.Count>4)
            {
                menuLength = 4;
            }
            else
            {
                menuLength = menu.Count;
            }
            int maxWidth = 24;
            if((dialoqueX%2).Equals(1))
            {
                dialoqueX -= 1;
            }

            Console.SetCursorPosition(dialoqueX, dialoqueY);
            for (int y = dialoqueY; y < dialoqueY + menuLength + 2; y++)
            {
                Console.SetCursorPosition(dialoqueX, Console.CursorTop);
                for (int x = dialoqueX; x < dialoqueX + maxWidth; x++)
                {
                    if (y.Equals(dialoqueY))
                    {
                        if (x.Equals(dialoqueX))
                        {
                            Console.Write("┍");
                        }
                        else if (x.Equals(dialoqueX + maxWidth - 1))
                        {
                            Console.Write("┑");
                        }
                        else
                        {
                            Console.Write("━");
                        }
                    }
                    else if (y.Equals(dialoqueY + menuLength + 1))
                    {
                        if (x.Equals(dialoqueX))
                        {
                            Console.Write("┕");
                        }
                        else if (x.Equals(dialoqueX + maxWidth - 1))
                        {
                            Console.Write("┙");
                        }
                        else
                        {
                            Console.Write("━");
                        }
                    }
                    else
                    {
                        if (x.Equals(dialoqueX) || x.Equals(dialoqueX + maxWidth - 1))
                        {
                            Console.SetCursorPosition(x, Console.CursorTop);
                            Console.Write("│");
                        }
                    }
                }
                Console.WriteLine();
            }

            if (dialoqueX.Equals(Shop.CURSOR_X) &&
               dialoqueY.Equals(Shop.CURSOR_Y))
            {
                Shop.ClearShopContents();
            }
            else if (dialoqueX.Equals(Battle.DIALOGUE_X) &&
                dialoqueY.Equals(Battle.DIALOGUE_Y))
            {
                Battle.DialogueClear();
            }
            for (int i = 0; i < ITEM_LENGTH; i++)
            {
                if (i > menu.Count)
                {
                    break;
                }
                // ▶ 위치를 고려하여 커서의 x위치에 2를 더해줌
                Console.SetCursorPosition(dialoqueX + CURSOR_X_LENGTH + 2, dialoqueY + i + 1);
                if (itemIndex + i >= menu.Count)
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"{menu[itemIndex + i].name} - {menu[itemIndex + i].quantity}개");
                }
            }
        }
    }
}
