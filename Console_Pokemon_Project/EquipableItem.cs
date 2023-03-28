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

        public EQUIPTYPE equipType { get; set; }
        public EquipableItem(string name, int atk, int def, int price, EQUIPTYPE equipType) : base(name, atk, def, price)
        {
            this.equipType = equipType;
        }
    }
}
