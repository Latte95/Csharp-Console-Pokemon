using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using System.IO;

namespace Console_Pokemon_Project
{
    public class Pokemon
    {
        public const int GRAPHIC_WIDTH = 24;
        public const int GRAPHIC_HEIGHT = 24;

        // 하나의 생성자로 이름값에 따라 스탯 별개
        public string name;
        public int maxHp;
        public int hp;
        public int atk;
        public int def;
        public int speed;
        public int exp;
        public int dropgold;
        public int critical;
        public int avoidence;
        public int level;
        public char[,] characterDisplayInfo;
        //public int special_Attack;
        //public int sepcial_Defence;
        public List<Skill> skills = new List<Skill>();

        public Pokemon(string name, int maxHp, int hp, int atk, int def, int speed, int exp, int dropgold, int cri, int avoid, int level, char[,] characterDisplayInfo)
        {
            this.name = name;
            this.maxHp = maxHp;
            this.hp = hp;
            this.atk = atk;
            this.def = def;
            this.speed = speed;
            this.exp = exp;
            this.dropgold = dropgold;
            this.critical = cri;
            this.avoidence = avoid;
            this.level = level;
            this.characterDisplayInfo = characterDisplayInfo;
            InitGetSkill();
        }

        public void InitGetSkill()
        {
            // 피카츄 = 0,1,2,3 파이리 = 8,9,10,11 꼬부기 = 8,12,13,14 이상해씨 = 4,5,6,7 나옹 = 8,13,15,16 
            // 푸린 = 16,17,0,28 괴력몬 = 18,19,20,21  포니타 = 10,11,21,22 팬텀 = 23,24,25,26 아쿠스타 =14,27,28,29

            switch (this.name)
            {
                case "피카츄":
                    AddSkill(0);
                    AddSkill(1);
                    AddSkill(2);
                    AddSkill(3);
                    break;
                case "파이리":
                    AddSkill(8);
                    AddSkill(9);
                    AddSkill(10);
                    AddSkill(11);
                    break;
                case "꼬부기":
                    AddSkill(8);
                    AddSkill(12);
                    AddSkill(13);
                    AddSkill(14);
                    break;
                case "이상해씨":
                    AddSkill(4);
                    AddSkill(5);
                    AddSkill(6);
                    AddSkill(7);
                    break;
                case "나옹":
                    AddSkill(8);
                    AddSkill(13);
                    AddSkill(15);
                    AddSkill(16);
                    break;
                case "푸린":
                    AddSkill(16);
                    AddSkill(17);
                    AddSkill(0);
                    AddSkill(28);
                    break;
                case "괴력몬":
                    AddSkill(18);
                    AddSkill(19);
                    AddSkill(20);
                    AddSkill(21);
                    break;
                case "포니타":
                    AddSkill(10);
                    AddSkill(11);
                    AddSkill(21);
                    AddSkill(22);
                    break;
                case "팬텀":
                    AddSkill(23);
                    AddSkill(24);
                    AddSkill(25);
                    AddSkill(26);
                    break;
                case "아쿠스타":
                    AddSkill(14);
                    AddSkill(27);
                    AddSkill(28);
                    AddSkill(29);
                    break;
            }
        }
        public void AddSkill(int skillIndex)
        {
            List<Skill> tmpSkills;

            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\.\JSON\"));

            if (File.Exists(path + "PokemonSkills.json"))
            {
                string json = File.ReadAllText(path + "PokemonSkills.json");
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
    }
}
