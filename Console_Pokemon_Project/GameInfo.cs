﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    class GameInfo
    {
        public const int CURSOR_X = 40;
        public const int CURSOR_Y = (Screen.WINDOW_HEIGHT >> 1) + 2;
        const int SHOP_X_LENGTH = Screen.WINDOW_WIDTH - CURSOR_X;
        const int SHOP_Y_LENGTH = 4;

        public List<string> infoMenu = new List<string> { "인벤토리", "장비창" };

        public GameInfo()
        {

        }

        public void ChooseInfoMenu()
        {
            // 판매 or 구매
            string whatAct;
            while (true)
            {
                whatAct = Menu.SelectMenu(CURSOR_X, CURSOR_Y, infoMenu);
                if (string.IsNullOrEmpty(whatAct))
                {
                    break;
                }
                switch (whatAct)
                {
                    case "인벤토리":
                        ShowInventory();
                        break;

                    case "장비창":
                        ShowEquipments();
                        break;
                }
                ClearInfoMenu();
            }
        }
        public void ShowPlayerStat()
        {
            Console.SetCursorPosition(80, 30);
            Console.WriteLine("ㅁㄴㅇㄻㄴㅇㄹ");
        }
        private void ShowInventory()
        {
            Console.SetCursorPosition(CURSOR_X, Console.CursorTop);
            string itemName = Menu.SelectMenu(CURSOR_X, CURSOR_Y, Player.instance.inven.items);

            int itemIndex = Player.instance.inven.items.FindIndex(item => item.name == itemName);
            if(itemIndex < 0)
            {
                return;
            }
            Item selectedItem = Player.instance.inven.items[itemIndex];

            if(selectedItem is EquipableItem)
            {
                Player.instance.equipSlot.EquipItem(selectedItem as EquipableItem);
            }
            else if(selectedItem is ConsumableItem)
            {
                //미구현
                //selectedItem.quantity--;
            }
        }
        private void ShowEquipments()
        {
            Console.SetCursorPosition(CURSOR_X, CURSOR_Y);
            //Console.SetCursorPosition(CURSOR_X, Console.CursorTop);

            List<EquipableItem.EQUIPTYPE> equipments = new List<EquipableItem.EQUIPTYPE>(Player.instance.equipSlot.equipSlots.Keys);

            List<string> equipmentNames = new List<string>();

            for (int i = 0; i < equipments.Count; i++)
            {
                if (Player.instance.equipSlot.equipSlots[equipments[i]] is null)
                {
                    continue;
                }
                else
                {
                    equipmentNames.Add(Player.instance.equipSlot.equipSlots[equipments[i]].name);
                }
            }
            if(equipmentNames.Count <= 0)
            {
                Console.SetCursorPosition(CURSOR_X*2, CURSOR_Y+ infoMenu.Count+2);
                Console.WriteLine("착용한 장비없음");
                Console.ReadKey(true);
                return;
            }

            Menu.SelectMenu(CURSOR_X, CURSOR_Y, equipmentNames);
        }
        private void ClearInfoMenu()
        {
            for (int y = CURSOR_Y; y < Screen.WINDOW_HEIGHT; y++)
            {
                for (int x = CURSOR_X*2; x < Screen.WINDOW_WIDTH; x += 2)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write("　");
                }
            }
        }
    }
}
