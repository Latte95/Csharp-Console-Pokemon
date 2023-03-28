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
        private Dictionary<EquipableItem.EQUIPTYPE, EquipableItem> equipSlots;

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
            // 인벤토리에서 해당 아이템은 제거
            inventory.RemoveItem(item);

        }

        public void UnequipItem(EquipableItem item)
        {
            Inventory inventory = Player.instance.inven;
            // 장비하고 있는 슬롯을 비우고
            equipSlots[item.equipType] = null;
            // 인벤토리에 해당 장비를 추가
            inventory.AddItem(item);
        }


            //public void Equip(EquipableItem item)
            //{
            //    int type = (int)item.equipType;

            //    EquipableItem tmpItem = new EquipableItem(item.name, item.atk, item.def, item.equipType, item.price);
            //    item = equipInfo[type];
            //    equipInfo[type] = tmpItem;
            //}
            //public void EquipFromInven(int invenIndex)
            //{
            //    EquipableItem tmpItem = this.inven.itemList[invenIndex] as EquipableItem;

            //    // 인벤 장착 선택한 아이템이 장비템이 아니면 종료
            //    if (tmpItem is null)
            //    {
            //        return;
            //    }
            //    // 선택 인덱스가 아이템 리스트크기를 넘으면 종료
            //    if (invenIndex >= this.inven.itemList.Count)
            //    {
            //        return;
            //    }

            //    // 현재 장착중인 장비가 없으면 인벤토리에 추가할 아이템 없음
            //    EquipableItem myEquipItem = this.equipInfo[(int)tmpItem.equipType];

            //    // 장비창에 있는 아이템은 인벤토리로 넣고
            //    if (myEquipItem is null == false)
            //    {
            //        this.inven.itemList.Add(equipInfo[(int)tmpItem.equipType]);
            //    }
            //    // 장착
            //    Equip(tmpItem);
            //    // 장비창으로 들어간 아이템은 인벤토리에서 삭제
            //    this.inven.itemList.RemoveAt(invenIndex);
            //}
            //public int SumEquipAtk()
            //{
            //    int sum = 0;
            //    int typeCount = Enum.GetValues(typeof(EquipableItem.EQUIPTYPE)).Length; // EQUIPTYPE의 총 개수
            //    for (int i = 0; i < typeCount; i++)
            //    {
            //        if (this.equipInfo[i] == null)
            //            continue;
            //        sum += this.equipInfo[i].atk;
            //    }
            //    return sum;
            //}
            //public int SumEquipDef()
            //{
            //    int sum = 0;
            //    for (int i = 0; i < 4; i++)
            //    {
            //        if (this.equipInfo[i] == null)
            //            continue;
            //        sum += this.equipInfo[i].def;
            //    }
            //    return sum;
            //}
        }
    }
