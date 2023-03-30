using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Newtonsoft.Json;

namespace Console_Pokemon_Project
{
    class Battle
    {
        const int WINDOW_WIDTH = 120;
        const int WINDOW_HEIGHT = 45;
        public const int DIALOGUE_X = 4;
        public const int DIALOGUE_Y = WINDOW_HEIGHT - 10;
        const int DIALOGUE_WINDOW_WIDTH = 30;
        const int DIALOGUE_WINDOW_HEIGHT = 10;

        // 몬스터와 나의 체력등을 이용해 전투종료
        // 아이템사용 스킬사용 도망치기 선택
        // 승패에 따른 결과값 반환
        public char[,] pixel = new char[60, 45];
        public List<string> battle = new List<string> { "전투", "아이템 사용", "도망" };
        public List<string> skillsStr = new List<string>();

        //public List<string> skill;
        public static Pokemon enemy;

        public Battle()
        {
            Console.Clear();
            Thread.Sleep(1);
            Random random = new Random();

            int monNum = random.Next(10);
            int ranHp = random.Next(1, 10);
            int ranAtk = random.Next(1, 5);
            int ranDef = random.Next(1, 3);
            int ranSpeed = random.Next(1, 10);
            int ranLevel = random.Next(1, 3);

            List<Pokemon> pokemons;

            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\.\JSON\"));

            if (File.Exists(path + "Pokemons.json"))
            {
                string json = File.ReadAllText(path + "Pokemons.json");
                pokemons = JsonConvert.DeserializeObject<List<Pokemon>>(json);
            }
            else
            {
                Console.WriteLine("포켓몬 정보 파일없음");
                return;
            }

            int level = pokemons[monNum].level + ranLevel;

            enemy = new Pokemon(pokemons[monNum].name,
                pokemons[monNum].maxHp + ranHp + level * 3,
                pokemons[monNum].hp + ranHp + level * 3,    // 현재체력
                pokemons[monNum].atk + ranAtk + level * 2,
                pokemons[monNum].def + ranDef + level,
                pokemons[monNum].speed + ranSpeed + level * 2,
                pokemons[monNum].exp + level * 2,
                pokemons[monNum].dropgold + level * 2,
                pokemons[monNum].critical,
                pokemons[monNum].avoidence,
                level);

            foreach (Skill skill in Player.instance.skills)
            {
                skillsStr.Add(skill.Name);
            }

        }

        public void BattleFrame()
        {
            //for(int y = 0; y<)
        }

        public void MeetPokemon()
        {
            bool isBattlePlay=true; // 배틀전체 실행값
            Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
            Console.WriteLine("지나가던 {0}와 조우했다!", enemy.name);
            Console.ReadKey(true);
            DialogueClear();
            while (isBattlePlay)
            {

                Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                Console.WriteLine("{0}는 지시를 기다리고있다.", Player.instance.name);
                Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                Console.WriteLine("어떤 행동을 하시겠습니까?");
                Display();
                Console.ReadKey(true);
                DialogueClear();


                string name = Menu.SelectMenu(DIALOGUE_X, DIALOGUE_Y, battle);
                string skillName;
                switch (name)
                {
                    case "전투":
                        {
                            if (Player.instance.speed > enemy.speed) // 플레이어의 스피드가 빠를때 플레이어의 선공
                            {
                                skillName = PlayerSelect();
                                PlayerAttack(skillName);
                                isBattlePlay = CheckMonsterAlive();
                                if (isBattlePlay == false) { break; }
                                MonsterAttack();
                                isBattlePlay = CheckPlayerAlive();
                            }
                            else // 몬스터의 스피드가 더 빠를때 몬스터의 선공
                            {
                                skillName = PlayerSelect();
                                MonsterAttack();
                                isBattlePlay = CheckPlayerAlive();
                                if (isBattlePlay == false) { break; }
                                PlayerAttack(skillName);
                                isBattlePlay = CheckMonsterAlive();
                            }

                            break;
                        }
                    case "아이템 사용":
                        {
                            //아이템 사용칸
                            ItemUse();
                            Console.WriteLine("");
                            break;
                        }
                    case "도망":
                        {
                            isBattlePlay = !Run();
                            break;
                        }
                    default:
                        {
                            break;
                        }

                }
                Console.Clear();
                
            }
        }
        public string PlayerSelect()
        {
            DialogueClear();
            string skillName = Menu.SelectMenu(DIALOGUE_X, DIALOGUE_Y, skillsStr);
            return skillName;

        }
        public void PlayerAttack(string skillName)
        {

            Random ran = new Random();
            int avoidRan = ran.Next(100) + 1;
            int criRan = ran.Next(100) + 1;
            //데미지 계산식 = (스킬계수 * 공격력 * (레벨*2 /5 +2) / 방어 / 50 ) * 속성보정
            int playerSkillDam0 = (Player.instance.skills[0].Power * Player.instance.atk * (Player.instance.level * 2 / 5 + 2) / enemy.def / 50);
            int playerSkillDam1 = (Player.instance.skills[1].Power * Player.instance.atk * (Player.instance.level * 2 / 5 + 2) / enemy.def / 50);
            int playerSkillDam2 = (Player.instance.skills[2].Power * Player.instance.atk * (Player.instance.level * 2 / 5 + 2) / enemy.def / 50);
            int playerSkillDam3 = (Player.instance.skills[3].Power * Player.instance.atk * (Player.instance.level * 2 / 5 + 2) / enemy.def / 50);
            if (playerSkillDam0 < 1) { playerSkillDam0 = 1; } //각 데미지별 최소데미지를 1로 설정
            if (playerSkillDam1 < 1) { playerSkillDam1 = 1; }
            if (playerSkillDam2 < 1) { playerSkillDam2 = 1; }
            if (playerSkillDam3 < 1) { playerSkillDam3 = 1; }
            DialogueClear();


            if (Player.instance.hp != 0 && enemy.hp != 0)
            {
                DialogueClear();
                Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                if (skillName == Player.instance.skills[0].Name) // 0번스킬을 사용했을때
                {
                    if (Player.instance.skills[0].Hitrate <= avoidRan + enemy.avoidence) //플레이어의 공격이 빗나갔을때
                    {
                        Console.Write("{0}의 공격이 빗나갔다!", Player.instance.name);
                        Console.ReadKey(true);
                        DialogueClear();
                    }
                    else// 플레이어의 공격이 성공했을때
                    {
                        Console.WriteLine("{0}의 {1}!", Player.instance.name, Player.instance.skills[0].Name);
                        if (100 <= criRan + enemy.critical) //플레이어의 공격이 크리티컬이 터졌을때
                        {
                            int criDam = playerSkillDam0 *= 2;
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", enemy.name, criDam);
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("급소에 맞았다!");
                            Console.ReadKey(true);
                            DialogueClear();
                            enemy.hp -= criDam;
                            Player.instance.skills[0].Pp -= 1;
                            if (enemy.hp < 0) { enemy.hp = 0; }
                        }
                        else // 플레이어의 공격이 크리티컬이 안터졌을때
                        {
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", enemy.name, playerSkillDam0);
                            Console.ReadKey(true);
                            DialogueClear();
                            enemy.hp -= playerSkillDam0;
                            Player.instance.skills[0].Pp -= 1;
                            if (enemy.hp < 0) { enemy.hp = 0; }
                        }
                    }

                }
                else if (skillName == Player.instance.skills[1].Name) //1번스킬을 사용했을때
                {
                    if (100 <= Player.instance.skills[1].Hitrate + enemy.avoidence) //플레이어의 공격이 빗나갔을때
                    {
                        Console.Write("{0}의 공격이 빗나갔다!", Player.instance.name);
                        Console.ReadKey(true);
                        DialogueClear();
                    }
                    else// 플레이어의 공격이 성공했을때
                    {
                        Console.WriteLine("{0}의 {1}!", Player.instance.name, Player.instance.skills[1].Name);
                        if (100 <= criRan + enemy.critical) //플레이어의 공격이 크리티컬이 터졌을때
                        {
                            int criDam = playerSkillDam1 *= 2;
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", enemy.name, criDam);
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("급소에 맞았다!");
                            Console.ReadKey(true);
                            DialogueClear();
                            enemy.hp -= criDam;
                            Player.instance.skills[1].Pp -= 1;
                            if (enemy.hp < 0) { enemy.hp = 0; }

                        }
                        else // 플레이어의 공격이 크리티컬이 안터졌을때
                        {
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", enemy.name, playerSkillDam1);
                            Console.ReadKey(true);
                            DialogueClear();
                            enemy.hp -= playerSkillDam1;
                            Player.instance.skills[0].Pp -= 1;
                            if (enemy.hp < 0) { enemy.hp = 0; }
                        }
                    }
                }

                else if (skillName == Player.instance.skills[2].Name) //2번스킬을 사용했을때
                {
                    if (Player.instance.skills[2].Hitrate <= avoidRan + enemy.avoidence) //플레이어의 공격이 빗나갔을때
                    {
                        Console.Write("{0}의 공격이 빗나갔다!", Player.instance.name);
                        Console.ReadKey(true);
                        DialogueClear();
                    }
                    else// 플레이어의 공격이 성공했을때
                    {
                        Console.WriteLine("{0}의 {1}!", Player.instance.name, Player.instance.skills[2].Name);
                        if (100 <= criRan + enemy.critical) //플레이어의 공격이 크리티컬이 터졌을때
                        {
                            int criDam = playerSkillDam2 *= 2;
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", enemy.name, criDam);
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("급소에 맞았다!");
                            Console.ReadKey(true);
                            DialogueClear();
                            enemy.hp -= criDam;
                            Player.instance.skills[2].Pp -= 1;
                            if (enemy.hp < 0) { enemy.hp = 0; }
                        }
                        else // 플레이어의 공격이 크리티컬이 안터졌을때
                        {
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", enemy.name, playerSkillDam2);
                            Console.ReadKey(true);
                            DialogueClear();
                            enemy.hp -= playerSkillDam2;
                            Player.instance.skills[2].Pp -= 1;
                            if (enemy.hp < 0) { enemy.hp = 0; }
                        }
                    }
                }
                else if (skillName == Player.instance.skills[3].Name) //3번스킬을 사용했을때
                {

                    if (Player.instance.skills[3].Hitrate <= avoidRan + enemy.avoidence) //플레이어의 공격이 빗나갔을때
                    {
                        Console.Write("{0}의 공격이 빗나갔다!", Player.instance.name);
                        Console.ReadKey(true);
                        DialogueClear();
                    }
                    else// 플레이어의 공격이 성공했을때
                    {
                        Console.WriteLine("{0}의 {1}!", Player.instance.name, Player.instance.skills[3].Name);
                        if (100 <= criRan + enemy.critical) //플레이어의 공격이 크리티컬이 터졌을때
                        {
                            int criDam = playerSkillDam3 *= 2;
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", enemy.name, criDam);
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("급소에 맞았다!");
                            Console.ReadKey(true);
                            DialogueClear();
                            enemy.hp -= criDam;
                            Player.instance.skills[3].Pp -= 1;
                            if (enemy.hp < 0) { enemy.hp = 0; }
                        }
                        else // 플레이어의 공격이 크리티컬이 안터졌을때
                        {
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", enemy.name, playerSkillDam3);
                            Console.ReadKey(true);
                            DialogueClear();
                            enemy.hp -= playerSkillDam3;
                            Player.instance.skills[3].Pp -= 1;
                            if (enemy.hp < 0) { enemy.hp = 0; }
                        }
                    }
                }
                // 여기서는 랭업기를 예시로 들었음

                //여기는 랭다운기
                // Console.WriteLine("{0} 의 {1}!", Player.instance.name, Player.instance.skill);
                // Console.WriteLine("{0} 의 {1}이 하락했다", enemy.name, enemy.def);
                // enemy.def -= 
                // 여기는 랭업기
                //Console.WriteLine("{0} 의 {1}!", Player.instance.name , Player.instance.skill);
                //Console.WriteLine("{0} 의 {1}이 상승했다",Player.instance.name, Player.instance.def );
                //Player.instance.def += Player.instance.def /2;


            }
        }
        public void MonsterAttack()
        {
            Random ran = new Random();
            int ranSkill = ran.Next(4);
            int avoidRan = ran.Next(100) + 1;
            int criRan = ran.Next(100) + 1;
            //데미지 계산식 = (스킬계수 * 공격력 * (레벨*2 /5 +2) / 방어 / 50 ) * 속성보정
            int monSkillDam0 = (enemy.skills[0].Power * enemy.atk * (enemy.level * 2 / 5 + 2) / enemy.def / 50);
            int monSkillDam1 = (enemy.skills[1].Power * enemy.atk * (enemy.level * 2 / 5 + 2) / enemy.def / 50);
            int monSkillDam2 = (enemy.skills[2].Power * enemy.atk * (enemy.level * 2 / 5 + 2) / enemy.def / 50);
            int monSkillDam3 = (enemy.skills[3].Power * enemy.atk * (enemy.level * 2 / 5 + 2) / enemy.def / 50);
            if (monSkillDam0 < 1) { monSkillDam0 = 1; } //데미지별 최소데미지 1로 설정
            if (monSkillDam1 < 1) { monSkillDam1 = 1; }
            if (monSkillDam2 < 1) { monSkillDam2 = 1; }
            if (monSkillDam3 < 1) { monSkillDam3 = 1; }

            Console.WriteLine(enemy.skills[ranSkill].Name);
            if (Player.instance.hp != 0 && enemy.hp != 0)
            {
                DialogueClear();
                Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                if (ranSkill == 0) // 0번스킬을 사용했을때
                {
                    if (enemy.skills[0].Hitrate <= avoidRan + Player.instance.avoidence) //몬스터의 공격이 빗나갔을때
                    {
                        Console.Write("{0}의 공격이 빗나갔다!", enemy.name);
                        Console.ReadKey(true);
                        DialogueClear();
                    }
                    else// 플레이어의 공격이 성공했을때
                    {
                        Console.WriteLine("{0}의 {1}!", enemy.name, enemy.skills[0].Name);
                        if (100 <= criRan + enemy.critical) //몬스터의 공격이 크리티컬이 터졌을때
                        {
                            int criDam = monSkillDam0 *= 2;
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", Player.instance.name, criDam);
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("급소에 맞았다!");
                            Console.ReadKey(true);
                            DialogueClear();
                            Player.instance.hp -= criDam;
                            enemy.skills[0].Pp -= 1;
                            if (Player.instance.hp < 0) { Player.instance.hp = 0; }
                        }
                        else // 몬스터의 공격이 크리티컬이 안터졌을때
                        {
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", Player.instance.name, monSkillDam0);
                            Console.ReadKey(true);
                            DialogueClear();
                            Player.instance.hp -= monSkillDam0;
                            enemy.skills[0].Pp -= 1;
                            if (Player.instance.hp < 0) { Player.instance.hp = 0; }
                        }
                    }

                }
                else if (ranSkill == 1) //1번스킬을 사용했을때
                {
                    if (enemy.skills[1].Hitrate <= avoidRan + Player.instance.avoidence) //몬스터의 공격이 빗나갔을때
                    {
                        Console.Write("{0}의 공격이 빗나갔다!", enemy.name);
                        Console.ReadKey(true);
                        DialogueClear();
                    }
                    else// 플레이어의 공격이 성공했을때
                    {
                        Console.WriteLine("{0}의 {1}!", enemy.name, enemy.skills[1].Name);
                        if (100 <= criRan + enemy.critical) //몬스터의 공격이 크리티컬이 터졌을때
                        {
                            int criDam = monSkillDam1 *= 2;
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", Player.instance.name, criDam);
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("급소에 맞았다!");
                            Console.ReadKey(true);
                            DialogueClear();
                            Player.instance.hp -= criDam;
                            enemy.skills[1].Pp -= 1;
                            if (Player.instance.hp < 0) { Player.instance.hp = 0; }
                        }
                        else // 몬스터의 공격이 크리티컬이 안터졌을때
                        {
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", Player.instance.name, monSkillDam1);
                            Console.ReadKey(true);
                            DialogueClear();
                            Player.instance.hp -= monSkillDam1;
                            enemy.skills[1].Pp -= 1;
                            if (Player.instance.hp < 0) { Player.instance.hp = 0; }
                        }
                    }
                }

                else if (ranSkill == 2) //2번스킬을 사용했을때
                {
                    if (enemy.skills[2].Hitrate <= avoidRan + Player.instance.avoidence) //몬스터의 공격이 빗나갔을때
                    {
                        Console.Write("{0}의 공격이 빗나갔다!", enemy.name);
                        Console.ReadKey(true);
                        DialogueClear();
                    }
                    else// 플레이어의 공격이 성공했을때
                    {
                        Console.WriteLine("{0}의 {1}!", enemy.name, enemy.skills[2].Name);
                        if (100 <= criRan + enemy.critical) //몬스터의 공격이 크리티컬이 터졌을때
                        {
                            int criDam = monSkillDam2 *= 2;
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", Player.instance.name, criDam);
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("급소에 맞았다!");
                            Console.ReadKey(true);
                            DialogueClear();
                            Player.instance.hp -= criDam;
                            enemy.skills[2].Pp -= 1;
                            if (Player.instance.hp < 0) { Player.instance.hp = 0; }
                        }
                        else // 몬스터의 공격이 크리티컬이 안터졌을때
                        {
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", Player.instance.name, monSkillDam2);
                            Console.ReadKey(true);
                            DialogueClear();
                            Player.instance.hp -= monSkillDam2;
                            enemy.skills[2].Pp -= 1;
                            if (Player.instance.hp < 0) { Player.instance.hp = 0; }
                        }
                    }
                }
                else if (ranSkill == 3) //3번스킬을 사용했을때
                {
                    if (enemy.skills[3].Hitrate <= avoidRan + Player.instance.avoidence) //몬스터의 공격이 빗나갔을때
                    {
                        Console.Write("{0}의 공격이 빗나갔다!", enemy.name);
                        Console.ReadKey(true);
                        DialogueClear();
                    }
                    else// 플레이어의 공격이 성공했을때
                    {
                        Console.WriteLine("{0}의 {1}!", enemy.name, enemy.skills[0].Name);
                        if (100 <= criRan + enemy.critical) //몬스터의 공격이 크리티컬이 터졌을때
                        {
                            int criDam = monSkillDam3 *= 2;
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", Player.instance.name, criDam);
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("급소에 맞았다!");
                            Console.ReadKey(true);
                            DialogueClear();
                            Player.instance.hp -= criDam;
                            enemy.skills[3].Pp -= 1;
                            if (Player.instance.hp < 0) { Player.instance.hp = 0; }
                        }
                        else // 몬스터의 공격이 크리티컬이 안터졌을때
                        {
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", Player.instance.name, monSkillDam3);
                            Console.ReadKey(true);
                            DialogueClear();
                            Player.instance.hp -= monSkillDam3;
                            enemy.skills[3].Pp -= 1;
                            if (Player.instance.hp < 0) { Player.instance.hp = 0; }
                        }
                    }
                }


            }

        }
        public void ItemUse()
        {
            List<Item> tmpItem = Player.instance.inven.items;

            ItemList.SelectMenu(DIALOGUE_X, DIALOGUE_Y, tmpItem);
        }
        public bool Run() // 도망치기시 80%확률로 도망에 성공,
        {
            Random random = new Random();
            int run = random.Next(100) + 1;

            if (run < 80)
            {
                DialogueClear();
                Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                Console.WriteLine("성공적으로 도망쳤다!");
                Console.ReadKey(true);
                return true; //(배틀 실행 종료) !run으로 처리
                //맵에서 커서움직이게하는 bool = true (추후수정)
            }
            else
            {
                DialogueClear();
                Console.WriteLine("도망에 실패했습니다.");
                Console.ReadKey(true);
                DialogueClear();
                MonsterAttack();
                return false; //배틀 실행 유지 !run으로 처리
            }

        }

        public bool CheckPlayerAlive() //플레이어가 죽었는지 확인하는 메서드
        {
            if (Player.instance.hp <= 0)
            {
                Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                Console.WriteLine("{0} 의 체력은 0이 되었다.", Player.instance.name);
                Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                Console.WriteLine("눈앞이 깜깜해졌다.");
                Console.ReadKey(true);
                return false;  //(전체 게임 실행 종료)
                
            }
            else
            {
                return true; // 체력이 0이 아니면 게임을 진행
            }
        }
        public bool CheckMonsterAlive() //몬스터가 죽었는지 확인하는 메서드
        {

            if (enemy.hp <= 0)
            {
                Console.Clear();
                Display();
                Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                Console.WriteLine("{0}의 체력이 0이 되었다.", enemy.name);
                Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                Console.WriteLine("전투에서 승리했다!");
                Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                Console.WriteLine("{0}는 exp{1}과 {2}g를 얻었다!", Player.instance.name, enemy.exp, enemy.dropgold);
                Console.ReadKey(true);
                Player.instance.money += enemy.dropgold;
                //Player.instance.exp += enemy.exp;
                //맵에서 커서움직이게하는 bool = true (추후수정)
                
                return false;  //배틀실행종료
            }
            else
            {
                return true; //체력이 0이 아니면 배틀을 진행
            }

        }

        public static void DialogueClear()
        {
            Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);

            for (int i = 0; i < DIALOGUE_WINDOW_HEIGHT; i++)
            {
                for (int j = 0; j < DIALOGUE_WINDOW_WIDTH; j++)
                {
                    Console.Write("　");
                }
                Console.WriteLine();
                Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
            }


            Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
        }

        public static void Display()
        {
            Console.SetCursorPosition(20, 3);
            Console.WriteLine("Lv : {0} ", enemy.level); // 몬스터의 Lv표시
            Console.SetCursorPosition(20, 5);
            Console.WriteLine("HP : {0} / {1}", enemy.hp, enemy.maxHp); // 몬스터의 hp표시
            Console.SetCursorPosition(80, 23);
            Console.WriteLine("Lv : {0} ", Player.instance.level); //플레이어의 Lv표시
            Console.SetCursorPosition(80, 25);
            Console.WriteLine("HP : {0} / {1}", Player.instance.hp, Player.instance.maxHp);  //플레이어의 hp표시
        }
    }
}
