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
            // 소비 아이템을 삭제할 땐
            if (item is ConsumableItem)
            {
                ConsumableItem delItem = (ConsumableItem)item;
                foreach (Item existItem in items)
                {
                    // 인벤토리에 삭제할 아이템을 찾고
                    if (existItem.name == delItem.name)
                    {
                        ConsumableItem existing = (ConsumableItem)existItem;
                        // 삭제할 개수보다 많이 가지고 있으면
                        if (existing.quantity > delItem.quantity)
                        {
                            // 해당 개수만큼만 삭제하고
                            existing.quantity -= delItem.quantity;
                        }
                        // 보유한 수만큼 삭제하면
                        else if (existing.quantity.Equals(delItem.quantity))
                        {
                            // 없애고
                            items.Remove(existing);
                        }
                        // 더 많이 삭제하면?
                        else
                        {
                            // 없앨지 취소할지? 수정
                        }
                        break;
                    }
                }
            }
            // 장비 아이템 삭제시엔 그냥 삭제
            else if (item is EquipableItem)
            {
                items.Remove(item);
            }
            else
            {
                // 여기 아티팩트 구현? 수정
            }
        }
    }
}
