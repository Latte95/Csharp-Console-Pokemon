using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
                if (_instance == null)
                {
                    _instance = new Player();
                }
                return _instance;
            }
        }

        public string name;
        public int maxHp;
        public int hp;
        public int atk;
        public int def;
        public int speed;
        public int level;
        public int money;
        public int avoidence;
        public int critical;
        public int exp;
        public Inventory inven;
        public EquipmentSlot equipSlot;
        public int locX;
        public int locY;
        public bool isWaitingInput;
        public bool isInBattle;
        public int[] upExp;
        public char[,] characterDisplayInfo;
        public List<Skill> skills = new List<Skill>();

        private Player()
        {
            this.name = "Player";
            this.maxHp = 200;
            this.hp = 200;
            this.atk = 444;
            this.def = 30;
            this.speed = 50;
            this.level = 15;
            this.money = 10000;
            this.avoidence = 15;
            this.critical = 3;
            this.exp = 0;
            this.upExp = new int[100];
            equipSlot = new EquipmentSlot();
            inven = new Inventory();
            locX = 2;
            locY = 2;
            isWaitingInput = true;
            isInBattle = false;

            InitGetSkill();

            for (int i = 0; i < 100; i++) //플레이어의 레벨 필요경험치량
            {
                upExp[i] = 100 + ((i + 1) * 50);
            }
            this.characterDisplayInfo = new char[,]
            {
                    { '#', '#', '#', '#', '#', '#', '#', '=', '=', '=', '=', '#', '=', '=', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
                    { '#', '#', '#', '#', '#', '#', '=', '.', '.', '.', '.', '.', '.', '.', '-', '=', '#', '#', '#', '#', '#', '#', '#', '#' },
                    { '#', '#', '#', '#', '#', '.', ' ', '=', '=', '=', '-', '#', '#', '#', '-', '.', '-', '#', '#', '#', '#', '#', '#', '#' },
                    { '#', '#', '#', '#', '.', '.', '-', '-', '.', '-', '.', '-', '-', '=', '=', '#', '-', '-', '#', '#', '#', '#', '#', '#' },
                    { '#', '#', '#', '=', '.', '-', '-', '-', '-', '-', '-', '-', '.', '.', '=', '=', '=', ' ', '#', '#', '#', '#', '#', '#' },
                    { '#', '#', '#', ' ', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '.', '=', '=', ' ', ' ', '=', '#', '#', '#', '#' },
                    { '#', '#', '#', ' ', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '=', '.', ' ', '.', '#', '#', '#', '#', '#' },
                    { '#', '#', '#', ' ', '.', '-', '-', '-', '-', '-', '-', '-', '-', '.', '.', '.', ' ', ' ', '#', '#', '#', '#', '#', '#' },
                    { '#', '#', '#', ' ', ' ', ' ', '-', '-', '-', '-', '-', ' ', ' ', ' ', ' ', ' ', '#', '.', '#', '#', '#', '#', '#', '#' },
                    { '#', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '=', '#', ' ', '#', '#', '#', '#', '#', '#' },
                    { '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '=', '#', ' ', '#', '#', '#', '#', '#', '#' },
                    { '#', '#', '#', '#', '.', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '.', '-', '.', '=', '#', '#', '#', '#', '#', '#' },
                    { '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '=', '-', ' ', ' ', '#', '#', '#', '#', '#', '#', '#', '#' },
                    { '#', '#', '#', '.', '=', '=', '=', '=', '-', ' ', ' ', ' ', '.', ' ', '=', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
                    { '#', '#', ' ', '-', '-', '-', '-', '-', '-', '#', ' ', '.', '-', '.', ' ', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
                    { '#', '#', ' ', '-', '-', '-', '-', '-', '-', '.', '=', ' ', '.', '-', '#', '.', '#', '#', '#', '#', '#', '#', '#', '#' },
                    { '#', '.', '-', '-', '-', '-', '-', '-', '-', '.', '.', '#', ' ', '#', '#', '#', ' ', '#', '#', '=', '-', '=', '#', '#' },
                    { '#', '.', ' ', ' ', '.', '-', '-', '=', '.', '.', '-', '#', ' ', '#', '#', '#', '=', '-', '#', '-', '=', '=', '=', '#' },
                    { '#', '.', ' ', '-', '.', ' ', ' ', ' ', '.', '-', '.', '#', ' ', '#', '#', '#', '#', ' ', '-', '#', '#', '#', '.', '#' },
                    { '-', '#', '#', ' ', '-', '.', ' ', '-', '-', '-', '-', ' ', '#', '=', '=', '#', '#', ' ', ' ', '=', '#', '#', '=', '=' },
                    { '=', '-', '-', ' ', ' ', '=', '#', ' ', '-', '-', '.', ' ', '=', '#', '=', '-', ' ', '#', '#', '#', '#', '#', '-', '-' },
                    { '#', '.', ' ', '-', '.', '.', '-', '.', '-', '-', ' ', '-', ' ', '-', '.', '.', '#', '=', '#', '#', '#', '#', '.', '#' },
                    { '#', '.', ' ', '-', '-', '.', ' ', '-', '.', '.', '.', '=', '-', '.', '-', ' ', '#', '#', '#', '.', ' ', '.', '#', '#' },
                    { '#', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', '.', '#', '#', '#', ' ', ' ', ' ', '=', '#', '#', '#', '#' }
            };
        }
        // 플레이어 데이터 덮어 쓰는 함수
        public void UpdateProperties(Player newPlayer)
        {
            this.name = newPlayer.name;
            this.maxHp = newPlayer.maxHp;
            this.hp = newPlayer.hp;
            this.atk = newPlayer.atk;
            this.def = newPlayer.def;
            this.speed = newPlayer.speed;
            this.level = newPlayer.level;
            this.money = newPlayer.money;
            this.avoidence = newPlayer.avoidence;
            this.critical = newPlayer.critical;
            this.exp = newPlayer.exp;
            this.upExp = newPlayer.upExp;
            equipSlot = newPlayer.equipSlot;
            inven = newPlayer.inven;
            locX = newPlayer.locX;
            locY = newPlayer.locY;
            isWaitingInput = newPlayer.isWaitingInput;
            isInBattle = newPlayer.isInBattle;

            skills = newPlayer.skills;
            this.characterDisplayInfo = newPlayer.characterDisplayInfo;
        }

        private void InitGetSkill()
        {
            AddSkill(0);
            AddSkill(1);
            AddSkill(2);
            AddSkill(3);
        }
        private void AddSkill(int skillIndex)
        {
            List<Skill> tmpSkills = new List<Skill>();

            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\.\JSON\"));

            if (System.IO.File.Exists(path + "PokemonSkills.json"))
            {
                string json = System.IO.File.ReadAllText(path + "PokemonSkills.json");
                tmpSkills = JsonConvert.DeserializeObject<List<Skill>>(json);
            }
            else
            {
                Console.WriteLine("스킬정보 파일없음");
                return;
            }

            skills.Add(new Skill(
                tmpSkills[skillIndex].name,
                tmpSkills[skillIndex].power,
                tmpSkills[skillIndex].hitrate,
                tmpSkills[skillIndex].pp,
                tmpSkills[skillIndex].maxPp)
                );
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
