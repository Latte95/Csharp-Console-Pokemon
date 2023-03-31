using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    class GameInfo
    {
        public const int CURSOR_X = 40+2;
        public const int CURSOR_Y = (Screen.WINDOW_HEIGHT >> 1) + 2;
        const int SHOP_X_LENGTH = Screen.WINDOW_WIDTH - CURSOR_X;
        const int SHOP_Y_LENGTH = 4;
        public const int PLAYER_OPTION_MENU_POS_X = 83;
        public const int PLAYER_OPTION_MENU_POS_Y = 1;
        

        public List<string> infoMenu = new List<string> { "인벤토리", "장비창", "저장" };

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
                switch (whatAct)
                {
                    case "인벤토리":
                        ClearInfoMenu();
                        ShowInventory();
                        break;

                    case "장비창":
                        ClearInfoMenu();
                        ShowEquipments();
                        break;
                    case "저장":
                        ClearInfoMenu();
                        DataManager.Save(Player.instance);
                        break;

                }
                ClearInfoMenu();
                if (string.IsNullOrEmpty(whatAct))
                {
                    break;
                }
            }
        }
        public void ShowMapName(string name)
        {
            Console.SetCursorPosition(PLAYER_OPTION_MENU_POS_X + 14, PLAYER_OPTION_MENU_POS_Y);
            Console.WriteLine(name);
        }
        public void ShowPlayerStat()
        {
            Console.SetCursorPosition(PLAYER_OPTION_MENU_POS_X, PLAYER_OPTION_MENU_POS_Y+3);
            DisplayWithBlank($"{Player.instance.name}의 스탯");
            
            Console.SetCursorPosition(PLAYER_OPTION_MENU_POS_X, PLAYER_OPTION_MENU_POS_Y + 4);
            DisplayWithBlank($"HP:{Player.instance.hp,3}/{Player.instance.maxHp}");

            Console.SetCursorPosition(PLAYER_OPTION_MENU_POS_X, PLAYER_OPTION_MENU_POS_Y + 5);
            DisplayWithBlank($"ATK:{Player.instance.atk,3}");

            Console.SetCursorPosition(PLAYER_OPTION_MENU_POS_X, PLAYER_OPTION_MENU_POS_Y + 6);
            DisplayWithBlank($"DEF:{Player.instance.def,3}");

            Console.SetCursorPosition(PLAYER_OPTION_MENU_POS_X, PLAYER_OPTION_MENU_POS_Y + 7);
            DisplayWithBlank($"SPEED:{Player.instance.speed,3}");

            Console.SetCursorPosition(PLAYER_OPTION_MENU_POS_X, PLAYER_OPTION_MENU_POS_Y + 8);
            DisplayWithBlank($"LEVEL:{Player.instance.level,3}");

            Console.SetCursorPosition(PLAYER_OPTION_MENU_POS_X, PLAYER_OPTION_MENU_POS_Y + 9);
            DisplayWithBlank($"MONEY:{Player.instance.money,6}");

            Console.SetCursorPosition(PLAYER_OPTION_MENU_POS_X + 2, Screen.WINDOW_HEIGHT / 2);
            Console.Write("================================");

            Screen.DrawMenual();
        }
        private void DisplayWithBlank(string str)
        {
            // 반각문자 숫자까지 고려해서 여백 처리
            int strSize = Encoding.Default.GetBytes(str).Length;
            int halfCharCount = str.Length * 2 - strSize;
            
            Console.Write(str.PadRight(9 + halfCharCount/2, '　'));
            if(halfCharCount %2 == 1)
            {
                Console.Write(" ");
            }
            Console.Write("│");
        }
        private void ShowInventory()
        {
            Console.SetCursorPosition(CURSOR_X, Console.CursorTop);

            // 인벤토리의 아이템 목록을 보여주고 선택한 내용 문자열로 받음
            string itemName = Menu.SelectMenu(CURSOR_X, CURSOR_Y, Player.instance.inven.items);

            // 해당 문자열 (아이템이름)이 플레이어 인벤토리의 몇번째 인덱스인지 탐색
            int itemIndex = Player.instance.inven.items.FindIndex(item => item.name == itemName);

            // 플레이어 인벤토리에 아이템이 없을 때
            if(Player.instance.inven.items.Count <= 0)
            {
                Console.SetCursorPosition(CURSOR_X * 2, CURSOR_Y + infoMenu.Count - 1);
                Console.WriteLine("아이템 없음");
                Console.ReadKey(true);
                return;
            }
            // 선택한 아이템이 없을 때 (선택 안하고 나왔을 때)
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
                if(selectedItem.quantity > 0)
                {
                    selectedItem.quantity--;
                    Player.instance.hp += 50;
                    if (Player.instance.hp > Player.instance.maxHp)
                    {
                        Player.instance.hp = Player.instance.maxHp;
                    }
                }
                if(selectedItem.quantity <= 0)
                {
                    Player.instance.inven.RemoveItem(selectedItem);
                }
                
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
                    equipmentNames.Add($"{equipments[i]}\t- 없음");
                    continue;
                }
                else
                {
                    equipmentNames.Add($"{equipments[i]}\t- " + Player.instance.equipSlot.equipSlots[equipments[i]].name);
                }
            }
            if(equipmentNames.Count <= 0)
            {
                Console.SetCursorPosition(CURSOR_X*2, CURSOR_Y + infoMenu.Count - 1);
                Console.WriteLine("착용한 장비없음");
                Console.ReadKey(true);
                return;
            }

            string selectedType = Menu.SelectMenu(CURSOR_X, CURSOR_Y, equipmentNames);
            if(selectedType == null)
            {
                return;
            }
            if(selectedType.StartsWith(EquipableItem.EQUIPTYPE.HEAD.ToString()))
            {
                EquipableItem item = Player.instance.equipSlot.equipSlots[EquipableItem.EQUIPTYPE.HEAD];
                if(item is null)
                {
                    return;
                }
                Player.instance.equipSlot.UnequipItem(item);
            }
            else if(selectedType.StartsWith(EquipableItem.EQUIPTYPE.BODY.ToString()))
            {
                EquipableItem item = Player.instance.equipSlot.equipSlots[EquipableItem.EQUIPTYPE.BODY];
                if(item is null)
                {
                    return;
                }
                Player.instance.equipSlot.UnequipItem(item);
            }
            else if (selectedType.StartsWith(EquipableItem.EQUIPTYPE.WEAPON.ToString()))
            {
                EquipableItem item = Player.instance.equipSlot.equipSlots[EquipableItem.EQUIPTYPE.WEAPON];
                if(item is null)
                {
                    return;
                }
                Player.instance.equipSlot.UnequipItem(item);
            }
            else if(selectedType.StartsWith(EquipableItem.EQUIPTYPE.FOOT.ToString()))
            {
                EquipableItem item = Player.instance.equipSlot.equipSlots[EquipableItem.EQUIPTYPE.FOOT];
                if(item is null)
                {
                    return;
                }
                Player.instance.equipSlot.UnequipItem(item);
            }
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
