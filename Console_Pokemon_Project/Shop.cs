using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    class Shop
    {
        public List<string> shopOption = new List<string> { "아이템 구매", "아이템 판매" };

        public Shop()
        {
            Menu.SelectMenu(45, 45, shopOption);
        }
    }
}
