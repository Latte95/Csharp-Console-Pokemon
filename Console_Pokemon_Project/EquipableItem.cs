using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    public class EquipableItem : Item
    {
        public enum EQUIPTYPE
        {
            HEAD,
            BODY,
            WEAPON,
            FOOT,
        }

        public EQUIPTYPE equipType;

        public EquipableItem(string name, int atk, int def, EQUIPTYPE equipType, int price)
        {
            this.name = name;
            this.atk = atk;
            this.def = def;
            this.equipType = equipType;
            this.price = price;
        }
    }
}
