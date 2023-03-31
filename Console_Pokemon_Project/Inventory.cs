using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    public class Inventory
    {
        // 아이템
        public List<Item> items {  get; private set; }

        public Inventory()
        {
            items = new List<Item>();
        }

        // 인벤토리에 아이템 추가 (구매, 착용, 드랍 등등)
        public void AddItem(Item item)
        {
            // 소비 아이템을 추가할 땐
            if (item is ConsumableItem)
            {
                ConsumableItem addItem = (ConsumableItem)item;
                bool isExist = false;
                // 인벤토리에 추가하는 아이템이 이미 있으면 
                foreach (Item existItem in items)
                {
                    if (existItem.name == addItem.name)
                    {
                        // 이미 존재하는 아이템의 개수를 추가
                        ConsumableItem existing = (ConsumableItem)existItem;
                        existing.quantity += addItem.quantity;                        
                        isExist = true;
                        // 불필요한 반복 종료
                        break;
                    }
                }
                // 없으면 새롭게 아이템을 추가
                if (!isExist)
                {
                    items.Add(addItem);
                    addItem.quantity = 1;
                }
            }
            // 장비 아이템을 추가할 땐
            else if (item is EquipableItem)
            {
                // 그냥 아이템 추가
                items.Add(item);
            }
            else
            {
                // 장비나 소비가 아닌 아이템 구현하면 추가. (아티팩트 등)
            }
        }

        // 아이템 삭제 (판매, 해제, 버림, 사용 등등)
        public void RemoveItem(Item item)
        {
            // 인벤토리에 삭제할 아이템이 있다면
            if(items.Contains(item))
            {
                // 소비 아이템
                if(item is ConsumableItem)
                {
                    // 보유 하고 있으면 1개씩 제거하고
                    if (item.quantity > 0)
                    {
                        item.quantity--;
                    }
                    // 수량이 0이 되면 완전히 제거
                    if(item.quantity <= 0)
                    {
                        items.Remove(item);
                    }
                }
                // 장비 아이템은 그냥 완전히 제거
                else
                {
                    items.Remove(item);
                }
            }
        }
    }
}
