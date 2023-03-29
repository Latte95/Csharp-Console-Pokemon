using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    public static class ItemInfo
    {
        //static Dictionary<string, int[]> itemInfos = new Dictionary<string, int[]>();
        public static List<Item> itemInfos = new List<Item>();

        static ItemInfo()
        {
            itemInfos.Add(new EquipableItem("몽둥이1", 5, 1, 1000, EquipableItem.EQUIPTYPE.WEAPON));
            itemInfos.Add(new EquipableItem("칼1", 5, 1, 1000, EquipableItem.EQUIPTYPE.WEAPON));
            itemInfos.Add(new EquipableItem("완드1", 5, 1, 1000, EquipableItem.EQUIPTYPE.WEAPON));
            itemInfos.Add(new ConsumableItem("체력포션1", 0, 0, 300, 1));

            itemInfos.Add(new EquipableItem("몽둥이2", 5, 1, 1000, EquipableItem.EQUIPTYPE.WEAPON));
            itemInfos.Add(new EquipableItem("칼2", 5, 1, 1000, EquipableItem.EQUIPTYPE.WEAPON));
            itemInfos.Add(new EquipableItem("완드2", 5, 1, 1000, EquipableItem.EQUIPTYPE.WEAPON));
            itemInfos.Add(new ConsumableItem("체력포션2", 0, 0, 300, 1));

            itemInfos.Add(new EquipableItem("몽둥이3", 5, 1, 1000, EquipableItem.EQUIPTYPE.WEAPON));
            itemInfos.Add(new EquipableItem("칼3", 5, 1, 1000, EquipableItem.EQUIPTYPE.WEAPON));
            itemInfos.Add(new EquipableItem("완드3", 5, 1, 1000, EquipableItem.EQUIPTYPE.WEAPON));
            itemInfos.Add(new ConsumableItem("체력포션3", 0, 0, 300, 1));

            itemInfos.Add(new EquipableItem("몽둥이4", 5, 1, 1000, EquipableItem.EQUIPTYPE.WEAPON));
            itemInfos.Add(new EquipableItem("칼4", 5, 1, 1000, EquipableItem.EQUIPTYPE.WEAPON));
            itemInfos.Add(new EquipableItem("완드4", 5, 1, 1000, EquipableItem.EQUIPTYPE.WEAPON));
            itemInfos.Add(new ConsumableItem("체력포션4", 0, 0, 300, 1));

        }
    }
}
