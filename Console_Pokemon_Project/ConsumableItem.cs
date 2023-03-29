using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    public class ConsumableItem : Item
    {
        // 소비아이템
        public ConsumableItem(string name, int atk, int def, int price, int quantity) : base(name, atk, def, price, quantity)
        {
            this.quantity = quantity;
        }
    }
}
