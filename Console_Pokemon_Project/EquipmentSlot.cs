using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    public class EquipmentSlot
    {
        // 딕셔너리를 쓴 이유 : 키값을 하나밖에 못 갖으니까, 키값을 슬롯으로 활용
        public Dictionary<EquipableItem.EQUIPTYPE, EquipableItem> equipSlots;

        public EquipmentSlot()
        {
            equipSlots = new Dictionary<EquipableItem.EQUIPTYPE, EquipableItem>();
            // EQUIPTYPE에 있는 슬롯마다 null로 초기화
            foreach (EquipableItem.EQUIPTYPE type in Enum.GetValues(typeof(EquipableItem.EQUIPTYPE)))
            {
                equipSlots.Add(type, null);
            }
        }

        // 장비 착용
        public void EquipItem(EquipableItem item)
        {
            Inventory inventory = Player.instance.inven;
            // 착용할 장비의 부위에
            EquipableItem.EQUIPTYPE type = item.equipType;
            // 이미 다른 아이템이 착용되어 있으면
            if (equipSlots[type] != null)
            {
                // 착용되어 있던 장비는 해제하고
                UnequipItem(equipSlots[type]);
            }
            // 새로운 아이템 착용한 뒤
            equipSlots[type] = item;
            Player.instance.atk += item.atk;
            Player.instance.def += item.def;
            // 인벤토리에서 해당 아이템은 제거
            inventory.RemoveItem(item);

        }

        public void UnequipItem(EquipableItem item)
        {
            Player.instance.atk -= item.atk;
            Player.instance.def -= item.def;
            Inventory inventory = Player.instance.inven;
            // 장비하고 있는 슬롯을 비우고
            equipSlots[item.equipType] = null;
            // 인벤토리에 해당 장비를 추가
            inventory.AddItem(item);
        }
        }
    }
