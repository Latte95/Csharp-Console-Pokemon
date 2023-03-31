using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    class Shop
    {
        public const int CURSOR_X = Map.MAP_WIDTH + 2;
        public const int CURSOR_Y = (Screen.WINDOW_HEIGHT >> 1) + 2;

        public List<string> shopOption = new List<string> { "아이템 구매", "아이템 판매"};
        public List<Item> saleItems;

        public Shop()
        {
            this.saleItems = new List<Item>();
        }

        public void ShopAct()
        {
            // 판매 or 구매
            string whatAct;

            // 상점 메뉴 출력
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
            ClearShopContents();
        }

        // 판매 아이템 출력 및 구매 아이템 선택
        public void ShowSaleItems()
        {
            // Shop이 현재 보유중인 (판매중인) 아이템 목록 표시
            Console.SetCursorPosition(CURSOR_X, CURSOR_Y);
            Console.SetCursorPosition(CURSOR_X, Console.CursorTop);
            string choiceItem = Menu.SelectMenu(CURSOR_X, CURSOR_Y, saleItems);

            // 선택한 아이템 구매
            BuyItem(choiceItem);
        }

        // 사용자 인벤토리 출력 및 판매 아이템 선택
        public void ShowBuyItems()
        {
            if (Player.instance.inven.items.Count <= 0)
            {
                Console.SetCursorPosition(CURSOR_X * 2, CURSOR_Y + shopOption.Count + 2);
                Console.WriteLine("보유 아이템이 없습니다.");
                Console.ReadKey(true);
            }
            else
            {
                ClearShopContents();
            }
            // 플레이어의 인벤토리에 있는 아이템 목록 표시 후 선택한 아이템 문자열로 가져오기
            Console.SetCursorPosition(CURSOR_X, CURSOR_Y);
            string choiceItem = Menu.SelectMenu(CURSOR_X, CURSOR_Y, Player.instance.inven.items);
            SellItem(choiceItem);
        }

        public void BuyItem(string itemName)
        {
            int itemIndex = saleItems.FindIndex(item => item.name == itemName);
            // 플레이어가 고른 아이템이 상점 목록에 없으면(고르지 않으면) 리턴
            if(itemIndex < 0)
            {
                return;
            }

            Item tmpItem = saleItems[itemIndex]; // 현재 살 아이템
            if (tmpItem.quantity <= 0)
            {
                return;
            }
            else if (Player.instance.money < tmpItem.price)
            {
                Console.WriteLine("소지금이 부족합니다.");
                return;
            }

            // 아이템 타입 검사해서 해당 타입으로 플레이어 인벤토리에 추가 (아이템 새로 생성해서 인벤토리에 추가)
            if (tmpItem is EquipableItem)
            {
                Player.instance.inven.AddItem(new EquipableItem(
                (tmpItem as EquipableItem).name,
                (tmpItem as EquipableItem).atk,
                (tmpItem as EquipableItem).def,
                (tmpItem as EquipableItem).price,
                (tmpItem as EquipableItem).equipType,
                                                    1));
            }
            else if (tmpItem is ConsumableItem)
            {
                Player.instance.inven.AddItem(new ConsumableItem(
                    (tmpItem as ConsumableItem).name,
                    (tmpItem as ConsumableItem).atk,
                    (tmpItem as ConsumableItem).def,
                    (tmpItem as ConsumableItem).price,
                                                    1));
            }
            Player.instance.money -= tmpItem.price;

            // 상점에서 수량 차감
            tmpItem.quantity--;
        }
        public void SellItem(string itemName)
        {
            // 플레이어가 선택한 아이템이 아이템리스트에 존재하는지 (혹은 선택을 했는지) 확인 후 판매
            int itemIndex = Player.instance.inven.items.FindIndex(item => item.name == itemName);
            if (itemIndex < 0)
            {
                Console.WriteLine("아이템 없음");
                return;
            }
            Item tmpItem = Player.instance.inven.items[itemIndex]; // 현재 팔 아이템

            if (tmpItem.quantity <= 0)
            {
                Console.WriteLine("수량부족");
                return;
            }

            Player.instance.money += tmpItem.price / 2;
            Player.instance.inven.RemoveItem(tmpItem);
        }

        // 상점 화면 초기화
        public static void ClearShopContents()
        {
            for (int y = CURSOR_Y; y < Screen.WINDOW_HEIGHT; y++)
            {
                for (int x = CURSOR_X * 2; x < Screen.WINDOW_WIDTH; x += 2)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write("　");
                }
            }
        }
    }
}
