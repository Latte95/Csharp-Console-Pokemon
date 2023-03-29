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
            ShopAct();
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
            ItemList.SelectMenu(CURSOR_X, CURSOR_Y, saleItems);
        }

        // 사용자 인벤토리 출력 및 판매 아이템 선택
        public void ShowBuyItems()
        {
            ClearShopContents();
            Console.SetCursorPosition(CURSOR_X, CURSOR_Y);
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
