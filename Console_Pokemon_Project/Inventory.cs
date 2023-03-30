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

        // 아이템 추가 (구매, 착용, 드랍 등등)
        public void AddItem(Item item)
        {
            // 소비 아이템을 추가할 땐
            if (item is ConsumableItem)
            {
                ConsumableItem addItem = (ConsumableItem)item;
                bool isExist = false;
                // 인벤토리에
                foreach (Item existItem in items)
                {
                    // 추가하는 아이템이 이미 있으면 
                    if (existItem.name == addItem.name)
                    {
                        // 이미 존재하는 아이템의
                        ConsumableItem existing = (ConsumableItem)existItem;
                        // 개수를 추가하고
                        existing.quantity += addItem.quantity;
                        isExist = true;
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
                // 여기 아티팩트 구현?
            }
        }

        // 아이템 삭제 (판매, 해제, 버림, 사용 등등)
        public void RemoveItem(Item item)
        {
            if(items.Contains(item))
            {
                if(item is ConsumableItem)
                {
                    // 수량 남아있으면 하나 제거
                    if (item.quantity > 0)
                    {
                        item.quantity--;
                    }
                    // 제거 후 수량 0 이하가 되면 아이템 목록에서 제거
                    if(item.quantity <= 0)
                    {
                        items.Remove(item);
                    }
                }
                else
                {
                    items.Remove(item);
                }
            }
        }
    }
}
