﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    public class Item
    {
        const int MAX_CONSUMABLE_NUMBER = 99;

        // 장비랑 소비 아이템 공통 특성 구현(수량, 가치, 이름, 스탯)
        public string name { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int price { get; set; }
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

        public Item(string name, int atk, int def, int price, int quantity)
        {
            this.name = name;
            this.atk = atk;
            this.def = def;
            this.price = price;
            this._quantity = quantity;
        }
    }
}
