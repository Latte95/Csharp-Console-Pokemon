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
        public Inventory inven;
        public EquipableItem[] equipInfo;
        public int locX;
        public int locY;
        public bool isWaitingInput = true;

        private Player()
        {

        }

        public Player(string name, int hp, int atk, int def)
        {
            this.name = name;
            this.hp = hp;
            this.atk = atk;
            this.def = def;
            equipInfo = new EquipableItem[4];
            inven = new Inventory();
            locX = 0;
            locY = 0;
            isWaitingInput = false;
        }

        public void Equip(EquipableItem item)
        {
            int type = (int)item.equipType;

            EquipableItem tmpItem = new EquipableItem(item.name, item.atk, item.def, item.equipType, item.price);
            item = equipInfo[type];
            equipInfo[type] = tmpItem;
        }
        public void EquipFromInven(int invenIndex)
        {
            EquipableItem tmpItem = this.inven.itemList[invenIndex] as EquipableItem;

            // 인벤 장착 선택한 아이템이 장비템이 아니면 종료
            if (tmpItem is null)
            {
                return;
            }
            // 선택 인덱스가 아이템 리스트크기를 넘으면 종료
            if (invenIndex >= this.inven.itemList.Count)
            {
                return;
            }

            // 현재 장착중인 장비가 없으면 인벤토리에 추가할 아이템 없음
            EquipableItem myEquipItem = this.equipInfo[(int)tmpItem.equipType];

            // 장비창에 있는 아이템은 인벤토리로 넣고
            if (myEquipItem is null == false)
            {
                this.inven.itemList.Add(equipInfo[(int)tmpItem.equipType]);
            }
            // 장착
            Equip(tmpItem);
            // 장비창으로 들어간 아이템은 인벤토리에서 삭제
            this.inven.itemList.RemoveAt(invenIndex);
        }
        public int SumEquipAtk()
        {
            int sum = 0;
            int typeCount = Enum.GetValues(typeof(EquipableItem.EQUIPTYPE)).Length; // EQUIPTYPE의 총 개수
            for (int i = 0; i < typeCount; i++)
            {
                if (this.equipInfo[i] == null)
                    continue;
                sum += this.equipInfo[i].atk;
            }
            return sum;
        }
        public int SumEquipDef()
        {
            int sum = 0;
            for (int i = 0; i < 4; i++)
            {
                if (this.equipInfo[i] == null)
                    continue;
                sum += this.equipInfo[i].def;
            }
            return sum;
        }


    }
}
