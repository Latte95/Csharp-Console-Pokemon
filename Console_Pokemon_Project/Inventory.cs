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
        public List<Item> itemList;
        public Inventory()
        {
            itemList = new List<Item>();
        }
    }
}
