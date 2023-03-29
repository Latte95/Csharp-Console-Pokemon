using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
  public class Player
  {
        // 인벤토리 클래스
        // 아이템 클래스
        // 장비
        // 현재 위치 좌표

        // 싱글톤 패턴
        private static Player _instance = null;
        public static Player instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Player();
                }
                return _instance;
            }
        }

        public string name;
        public int hp;
        public int atk;
        public int def;
        public int speed;
        public int level;
        public int money;
        public Inventory inven;
        public EquipmentSlot equipSlot;
        public int locX;
        public int locY;
        public bool isWaitingInput;
        public bool isInBattle;

        private Player()
        {
            this.name = "Player";
            this.hp = 100;
            this.atk = 5;
            this.def = 1;
            this.speed = 80;
            this.level = 1;
            this.money = 10000;
            equipSlot = new EquipmentSlot();
            inven = new Inventory();
            locX = 2;
            locY = 2;
            isWaitingInput = true;
            isInBattle = false;
        }

        private Player(string name, int hp, int atk, int def)
        {
            this.name = name;
            this.hp = hp;
            this.atk = atk;
            this.def = def;
            equipSlot = new EquipmentSlot();
            inven = new Inventory();
            locX = 2;
            locY = 2;
            isWaitingInput = true;
        }

    }
}
