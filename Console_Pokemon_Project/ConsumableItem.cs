using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    public class ConsumableItem : Item
    {
        const int MAX_CONSUMABLE_NUMBER = 99;

        private int _quantity;
        public int quantity
        {
            get { return _quantity; }
            set
            {
                if (value > MAX_CONSUMABLE_NUMBER)
                {
                    _quantity = MAX_CONSUMABLE_NUMBER;
                }
                _quantity = value;
            }
        }



        // 소비아이템
        public ConsumableItem(string name, int atk, int def, int price, int quantity) : base(name, atk, def, price)
        {
            this.quantity = quantity;
        }
    }
}
