using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    class Shop
    {
        const int CURSOR_X = Map.MAP_WIDTH + 2;
        const int CURSOR_Y = (Screen.WINDOW_HEIGHT >> 1) + 2;
        const int SHOP_X_LENGTH = Screen.WINDOW_WIDTH - CURSOR_X;
        const int SHOP_Y_LENGTH = 4;

        public List<string> shopOption = new List<string> { "아이템 구매", "아이템 판매" };
        public List<Item> saleItems;

        public Shop(List<Item> saleItems)
        {
            this.saleItems = saleItems;
            //ShopAct();
        }

        public Shop() : this(new List<Item>())
        {

        }

        public void ShopAct()
        {
            // 판매 or 구매
            string whatAct;

            whatAct = Menu.SelectMenu(CURSOR_X, CURSOR_Y, shopOption);
            switch (whatAct)
            {
                case "아이템 구매":
                    ShowSaleItems();
                    break;

                case "아이템 판매":
                    ShowBuyItems();
                    break;
            }
        }

        // 판매 아이템 출력 및 구매 아이템 선택
        public void ShowSaleItems()
        {
            Console.SetCursorPosition(CURSOR_X, CURSOR_Y);
            Console.SetCursorPosition(CURSOR_X, Console.CursorTop);
            string choiceItem = ItemList.SelectMenu(CURSOR_X, CURSOR_Y, saleItems);

            BuyItem(choiceItem);
        }

        // 사용자 인벤토리 출력 및 판매 아이템 선택
        public void ShowBuyItems()
        {
            ClearShopContents();
            Console.SetCursorPosition(CURSOR_X, CURSOR_Y);
            string choiceItem = ItemList.SelectMenu(CURSOR_X, CURSOR_Y, Player.instance.inven.items);
            SellItem(choiceItem);
        }

        public void BuyItem(string itemName)
        {
            int itemIndex = saleItems.FindIndex(item => item.name == itemName);

            Item tmpItem = saleItems[itemIndex]; // 현재 살 아이템
            if(tmpItem.quantity <= 0)
            {
                Console.WriteLine("수량부족");
                return;
            }
            else if(Player.instance.money < tmpItem.price)
            {
                Console.WriteLine("돈 부족");
                return;
            }
            if(tmpItem is EquipableItem)
            {
                Player.instance.inven.AddItem(new EquipableItem(
                (tmpItem as EquipableItem).name,
                (tmpItem as EquipableItem).atk,
                (tmpItem as EquipableItem).def,
                (tmpItem as EquipableItem).price,
                (tmpItem as EquipableItem).equipType,
                                                    1));
            }
            else if(tmpItem is ConsumableItem)
            {
                Player.instance.inven.AddItem(new ConsumableItem(
                    (tmpItem as ConsumableItem).name,
                    (tmpItem as ConsumableItem).atk,
                    (tmpItem as ConsumableItem).def,
                    (tmpItem as ConsumableItem).price,
                                                    1));
            }
            Player.instance.money -= tmpItem.price;
            tmpItem.quantity--;
        }
        public void SellItem(string itemName)
        {
            int itemIndex = Player.instance.inven.items.FindIndex(item => item.name == itemName);

            Item tmpItem = Player.instance.inven.items[itemIndex]; // 현재 팔 아이템

            if(tmpItem.quantity <= 0)
            {
                Console.WriteLine("수량부족");
                return;
            }
            tmpItem.quantity--;
            Player.instance.money += tmpItem.price / 2;
            Player.instance.inven.RemoveItem(tmpItem);
        }

        // 상점 화면 초기화
        public static void ClearShopContents()
        {
            Console.SetCursorPosition(CURSOR_X, CURSOR_Y);
            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(CURSOR_X, Console.CursorTop);
                Console.WriteLine(new string('　', SHOP_X_LENGTH / 2 - 2));
            }
        }
    }
}
