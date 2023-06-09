﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    class Menu
    {
        // List를 받아와서, 해당 List의 string을 메뉴로 출력

        // ▶의 크기. 메뉴 이름과 ▶
        const int CURSOR_X_LENGTH = 2;
        // 최대 출력 옵션 수
        const int MAX_OPTION_LENGTH = 4;


        // 메뉴가 출력되어야 될 위치값을 dialoqueXY로 전달받음
        public static string SelectMenu<T>(int dialoqueX, int dialoqueY, List<T> menu)
        {
            // ▶가 선택할 메뉴 인덱스
            int pointerY = 0;
            // ▶가 옮겨지기 전의 위치(새로 그리기 위해서)
            int oldPointerY = 0;
            // 맨위에 출력될 아이템 인덱스
            int menuIndex = 0;
            // 위를 눌렀는지 아래를 눌렀는지
            bool isScrollUp;
            bool isScrollDown;

            // 출력할 메뉴가 없으면 null 반환
            if (menu.Count.Equals(0))
            {
                return null;
            }

            // 메뉴 출력
            PrintMenu(dialoqueX, dialoqueY, menu, menuIndex);
            // ▶ 출력
            PrintPointer(pointerY, oldPointerY, dialoqueX, dialoqueY);

            // 키 입력에 따라 ▶ 위치를 바꿔주는 반복문
            while (true)
            {
                // 사용자가 입력한 키를 저장하는 변수
                ConsoleKey key = Console.ReadKey(true).Key;
                // 옮기기 전 위치 저장
                oldPointerY = pointerY;

                // 상태 초기화
                isScrollUp = false;
                isScrollDown = false;

                // 입력한 키에 따른 행동을 정하는 조건문
                switch (key)
                {
                    // ▶이동
                    case ConsoleKey.UpArrow:
                        // 맨 위면 ▶ 이동x
                        if (pointerY.Equals(0))
                        {
                            isScrollUp = true;
                        }
                        // 맨 위 아니면 ▶ 위로 이동
                        else
                        {
                            pointerY = pointerY - 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        // 맨 아래면 ▶ 이동x
                        if (pointerY.Equals(MAX_OPTION_LENGTH - 1) || pointerY.Equals(menu.Count - 1))
                        {
                            isScrollDown = true;
                        }
                        // 맨 아래 아니면 ▶ 아래로 이동
                        else
                        {
                            pointerY = pointerY + 1;
                        }
                        break;

                    // 선택
                    // 엔터 또는 스페이스바를 눌렀을 때 메뉴가 선택됨
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        // ▶ 위치의 메뉴를 반환함
                        // 아이템이면 아이템이름
                        if (menu[pointerY] is Item item)
                        {
                            return item.name;
                        }
                        // 스킬이면 스킬이름
                        else if (menu[pointerY] is Skill skill)
                        {
                            return skill.name;
                        }
                        // string List 전달 받았으면 해당 str
                        else if (menu[pointerY] is string str)
                        {
                            return str;
                        }
                        // 그외 null
                        else
                        {
                            return null;
                        }
                    // esc 누를 시 
                    case ConsoleKey.Escape:
                        // 스킬선택 (전투)일시 취소x
                        if (menu[pointerY] is Skill skill2)
                        {
                            continue;
                        }
                        // 그 외에는 메뉴 출력 취소
                        else
                        {
                            return null;
                        }
                }
                // ▶가 맨 위일 때 위를 눌렀고, 메뉴 인덱스 가 0보다 크면(맨위에 출력되는 메뉴가 첫 메뉴가 아니면)
                if (isScrollUp.Equals(true) && menuIndex > 0)
                {
                    // 메뉴 위로 스크롤
                    MenuScrollUp(ref menuIndex);
                    PrintMenu(dialoqueX, dialoqueY, menu, menuIndex);
                }
                // ▶가 맨 아래일 때 아래를 눌렀고, 메뉴 인덱스가 마지막 인덱스가 아니면
                else if (isScrollDown.Equals(true) && menuIndex < menu.Count - MAX_OPTION_LENGTH)
                {
                    // 메뉴 아래로 스크롤
                    MenuScrollDown(ref menuIndex);
                    PrintMenu(dialoqueX, dialoqueY, menu, menuIndex);
                }
                PrintPointer(pointerY, oldPointerY, dialoqueX, dialoqueY);
            }
        }

        // 메뉴 목록만 출력
        private static void PrintMenu<T>(int dialoqueX, int dialoqueY, List<T> menu, int menuIndex)
        {
            // 전각 반각 차이에 의한 *2 연산
            dialoqueX *= 2;

            // 출력할 메뉴 개수
            int menuLength = menu.Count;
            // 메뉴 팝업 너비
            int menuWidth = 0;
            // 표시할 최대 메뉴 수
            int menuHeight;
            if (menu.Count > 4)
            {
                menuHeight = 4;
            }
            else
            {
                menuHeight = menu.Count;
            }

            // 아이템 메뉴는 너비 고정
            if (menu[0] is Item item)
            {
                menuWidth = 24;
            }
            // 일반 메뉴는 메뉴 길이에 따라 너비 다르게 설정
            else if (menu[0] is string str)
            {
                // 전각이니까 *2, 테두리와 ▶를 위한 +8
                menuWidth = menu.Max(obj => (obj as string).Length) * 2 + 8;
            }

            // 전각문자 출력을 위해 출력 위치가 홀수면 짝수로 바꿔줌
            if ((dialoqueX % 2).Equals(1))
            {
                dialoqueX += 1;
            }
            ClearOption(dialoqueX, dialoqueY, menuWidth, menuHeight);

            Console.SetCursorPosition(dialoqueX, dialoqueY);
            for (int y = dialoqueY; y < dialoqueY + menuHeight + 2; y++)
            {
                for (int x = dialoqueX; x < dialoqueX + menuWidth; x += 2)
                {
                    Console.SetCursorPosition(x, y);
                    // 첫줄
                    if (y.Equals(dialoqueY))
                    {
                        if (x.Equals(dialoqueX))
                        {
                            Console.Write("┍");
                        }
                        else if (x.Equals(dialoqueX + menuWidth - 2))
                        {
                            Console.Write("┑");
                        }
                        else
                        {
                            Console.Write("━");
                        }
                    }
                    // 마지막 줄
                    else if (y.Equals(dialoqueY + menuHeight + 1))
                    {
                        if (x.Equals(dialoqueX))
                        {
                            Console.Write("┕");
                        }
                        else if (x.Equals(dialoqueX + menuWidth - 2))
                        {
                            Console.Write("┙");
                        }
                        else
                        {
                            Console.Write("━");
                        }
                    }
                    // 그외 줄
                    else
                    {
                        if (x.Equals(dialoqueX) || x.Equals(dialoqueX + menuWidth - 2))
                        {
                            Console.Write("│");
                        }
                    }
                }
            }
            ClearOption(dialoqueX, dialoqueY, menuWidth, menuHeight);
            for (int i = 0; i < menuHeight; i++)
            {
                // ▶ 위치를 고려하여 커서의 x위치에 2를 더해줌
                Console.SetCursorPosition(dialoqueX + CURSOR_X_LENGTH + 2, dialoqueY + i + 1);

                // 아이템이면 출력할 것
                if (menu[menuIndex + i] is Item itemName)
                {
                    Console.WriteLine($"{itemName.name} - {itemName.quantity}개");
                }
                // 소비템
                else if (menu[menuIndex + i] is ConsumableItem consumableItemName)
                {
                    Console.WriteLine($"{consumableItemName.name} - {consumableItemName.quantity}개");
                }
                // 스킬이면 출력할 것
                else if(menu[menuIndex + i] is Skill skillName)
                {
                    Console.WriteLine($"{skillName.name} - {skillName.pp} / {skillName.maxPp}");
                }
                // string이면 출력할 것
                else if (menu[menuIndex + i] is string str)
                {
                    Console.WriteLine(str);
                }
            }
        }

        public static void MenuScrollUp(ref int itemIndex)
        {
            itemIndex--;
        }

        public static void MenuScrollDown(ref int itemIndex)
        {
            itemIndex++;
        }

        // 커서 위치 갱신
        public static void PrintPointer(int newPosition, int oldPointerY, int dialoqueX, int dialoqueY)
        {
            dialoqueX *= 2;
            // 커서만 새로 그려주도록 구현
            // 원래 ▶가 있던 위치를 공백을 통해 지우고
            Console.SetCursorPosition(dialoqueX + 2, oldPointerY + dialoqueY + 1);
            Console.Write("  ");
            // 옮겨진 위치에 ▶ 표시
            Console.SetCursorPosition(dialoqueX + 2, newPosition + dialoqueY + 1);
            Console.Write("▶");
        }

        public static void ClearOption(int dialoqueX, int dialoqueY, int menuWidth, int menuHeight)
        {
            // 전각문자 출력을 위해 출력 위치가 홀수면 짝수로 바꿔줌
            if ((dialoqueX % 2).Equals(1))
            {
                dialoqueX += 1;
            }

            Console.SetCursorPosition(dialoqueX, dialoqueY);
            for (int y = dialoqueY + 1; y < dialoqueY + menuHeight + 1; y++)
            {
                for (int x = dialoqueX + 2; x < dialoqueX + menuWidth - 2; x += 2)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write("　");
                }
            }
        }
    }
}
