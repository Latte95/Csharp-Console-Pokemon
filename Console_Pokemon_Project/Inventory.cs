﻿using System;
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
            if (item is ConsumableItem)
            {
                // 같은 이름을 가진 아이템이 있는지 찾기
                int itemIndex = items.FindIndex(invenItem => invenItem.name == item.name);
                
                // 아이템이 있으면 수량증가
                if (itemIndex >= 0)
                {
                    Player.instance.inven.items[itemIndex].quantity++;
                    return;
                }
                // 아이템이 없으면 인벤토리에 추가
                else
                {
                    items.Add(item);
                    item.quantity = 1;
                }
            }
            // 장비 아이템을 추가할 땐
            else if (item is EquipableItem)
            {
                // 그냥 아이템 추가
                items.Add(item);
                item.quantity = 1;
            }
            else
            {
                // 장비나 소비가 아닌 아이템 구현하면 추가. (아티팩트 등)
            }
        }

        // 아이템 삭제 (판매, 해제, 버림, 사용 등등)
        public void RemoveItem(Item item)
        {
            // 같은 이름을 가진 아이템이 있는지 찾기
            int itemIndex = items.FindIndex(invenItem => invenItem.name == item.name);

            // 인벤토리에 삭제할 아이템이 있다면
            if (itemIndex >= 0)
            {
                // 소비 아이템
                if(item is ConsumableItem)
                {
                    // 보유 하고 있으면 1개씩 제거하고
                    if (items[itemIndex].quantity > 0)
                    {
                        items[itemIndex].quantity--;
                    }
                    // 수량이 0이 되면 완전히 제거
                    if(items[itemIndex].quantity <= 0)
                    {
                        items.RemoveAt(itemIndex);
                    }
                }
                // 장비 아이템은 그냥 완전히 제거
                else
                {
                    items.RemoveAt(itemIndex);
                }
            }
        }
    }
}
