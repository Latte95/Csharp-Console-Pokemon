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
            itemInfos.Add(new EquipableItem("몽둥이", 5, 1, 1000, EquipableItem.EQUIPTYPE.WEAPON));
            itemInfos.Add(new EquipableItem("칼", 5, 1, 1000, EquipableItem.EQUIPTYPE.WEAPON));
            itemInfos.Add(new EquipableItem("완드", 5, 1, 1000, EquipableItem.EQUIPTYPE.WEAPON));
            itemInfos.Add(new ConsumableItem("체력포션", 0, 0, 300, 1));
            
        }
    }
}
