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
                pokemons[monNum].hp + ranHp + level * 3,
                pokemons[monNum].atk + ranAtk + level * 2,
                pokemons[monNum].def + ranDef + level,
                pokemons[monNum].speed + ranSpeed + level * 2,
                pokemons[monNum].exp + level * 2,
                pokemons[monNum].dropgold + level * 2,
                pokemons[monNum].critical,
                pokemons[monNum].avoidence,
                level);
            //int level = PokemonInfo.pokemon[monNum].level + ranLevel;

            //enemy = new Pokemon(PokemonInfo.pokemon[monNum].name,
            //    PokemonInfo.pokemon[monNum].hp + ranHp + level * 3,
            //    PokemonInfo.pokemon[monNum].att + ranAtt + level * 2,
            //    PokemonInfo.pokemon[monNum].def + ranDef + level,
            //    PokemonInfo.pokemon[monNum].speed + ranSpeed + level * 2,
            //    PokemonInfo.pokemon[monNum].exp + level * 2,
            //    PokemonInfo.pokemon[monNum].dropgold + level * 2,
            //    PokemonInfo.pokemon[monNum].critical,
            //    PokemonInfo.pokemon[monNum].avoidence,
            //    level);

            MeetPokemon();
        }

        public void BattleFrame()
        {
            //for(int y = 0; y<)
        }

        public void MeetPokemon()
        {
            bool isBattlePlay = true;
            while (isBattlePlay)
            {

                Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                Console.WriteLine("지나가던 {0}와 조우했다!", enemy.name);
                //Pokemon[] pokemon = new Pokemon[10];
                Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                Console.WriteLine("어떤 행동을 하시겠습니까?");
                Console.ReadKey(true);
                DialogueClear();

                string name = Menu.SelectMenu(DIALOGUE_X, DIALOGUE_Y, battle);
                //int num = int.Parse(Console.ReadLine()); // 임시로 커서로 받아올 정보대신 입력으로 넣어놨음
                switch (name)
                {
                    case "전투":
                        {
                            //Console.WriteLine("어떤 스킬을 사용하시겠습니까?");
                            if (Player.instance.speed > enemy.speed) // 플레이어의 스피드가 빠를때 플레이어의 선공
                            {
                                PlayerAttack();
                                CheckMonsterDie();
                                MonsterAttack();
                                CheckPlayerDie();
                            }
                            else // 몬스터의 스피드가 더 빠를때 몬스터의 선공
                            {
                                MonsterAttack();
                                CheckPlayerDie();
                                PlayerAttack();
                                CheckMonsterDie();
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
                            Run();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

            }
        }
        public void PlayerAttack()
        {

            Random ran = new Random();
            int avoidRan = ran.Next(100) + 1;
            int criRan = ran.Next(100) + 1;
            //데미지 계산식 = (스킬계수 * 공격력 * (레벨*2 /5 +2) / 방어 / 50 ) * 속성보정
            int playerSkillDam0 = (Player.instance.skills[0].Power * Player.instance.atk * (Player.instance.level * 2 / 5 + 2) / enemy.def / 50);
            int playerSkillDam1 = (Player.instance.skills[1].Power * Player.instance.atk * (Player.instance.level * 2 / 5 + 2) / enemy.def / 50);
            int playerSkillDam2 = (Player.instance.skills[2].Power * Player.instance.atk * (Player.instance.level * 2 / 5 + 2) / enemy.def / 50);
            int playerSkillDam3 = (Player.instance.skills[3].Power * Player.instance.atk * (Player.instance.level * 2 / 5 + 2) / enemy.def / 50);


            /* 스킬을 넣고 커서로 스킬을 정했을경우 
               스킬 4개의 각각 데미지를 int skill1Dam , skill2dam, skill3Dam skill4Dam 로 저장
             
             */


            foreach (Skill skill in Player.instance.skills)
            {
                skillsStr.Add(skill.Name);
            }

            DialogueClear();
            string skillName = Menu.SelectMenu(DIALOGUE_X, DIALOGUE_Y, skillsStr);
            DialogueClear();

            if (Player.instance.hp != 0 && enemy.hp != 0)
            {
                if (skillName == Player.instance.skills[0].Name) // 맨위 0번스킬을 사용했을때
                {
                    if (100 <= avoidRan + enemy.avoidence) //플레이어의 공격이 빗나갔을때
                    {
                        Console.Write("공격이 빗나갔다!");
                        Console.ReadKey(true);
                        Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                        DialogueClear();
                    }
                    else// 플레이어의 공격이 성공했을때
                    {
                        Console.WriteLine("{0} 의 {1}!", Player.instance.name, Player.instance.skills[0].Name);
                        if (100 <= criRan + enemy.critical) //플레이어의 공격이 크리티컬이 터졌을때
                        {
                            int criDam = playerSkillDam0 *= 2;
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", enemy.name, criDam);
                            Console.ReadKey(true);
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("급소에 맞았다!");
                            Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                            DialogueClear();
                            enemy.hp -= criDam;
                            Player.instance.skills[0].Pp -= 1;
                        }
                        else // 플레이어의 공격이 크리티컬이 안터졌을때
                        {
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", enemy.name, playerSkillDam0);
                            Console.ReadKey(true);
                            Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                            DialogueClear();
                            enemy.hp -= playerSkillDam0;
                            Player.instance.skills[0].Pp -= 1;
                        }
                    }

                }
                else if (skillName == Player.instance.skills[1].Name)
                {
                    if (100 <= avoidRan + enemy.avoidence) //플레이어의 공격이 빗나갔을때
                    {
                        Console.Write("공격이 빗나갔다!");
                        Console.ReadKey(true);
                        Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                        DialogueClear();
                    }
                    else// 플레이어의 공격이 성공했을때
                    {
                        Console.WriteLine("{0} 의 {1}!", Player.instance.name, Player.instance.skills[1].Name);
                        if (100 <= criRan + enemy.critical) //플레이어의 공격이 크리티컬이 터졌을때
                        {
                            int criDam = playerSkillDam1 *= 2;
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", enemy.name, criDam);
                            Console.ReadKey(true);
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("급소에 맞았다!");
                            Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                            DialogueClear();
                            enemy.hp -= criDam;
                            Player.instance.skills[1].Pp -= 1;
                        }
                        else // 플레이어의 공격이 크리티컬이 안터졌을때
                        {
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", enemy.name, playerSkillDam1);
                            Console.ReadKey(true);
                            Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                            DialogueClear();
                            enemy.hp -= playerSkillDam1;
                            Player.instance.skills[0].Pp -= 1;
                        }
                    }
                }

                else if (skillName == Player.instance.skills[2].Name)
                {
                    if (100 <= avoidRan + enemy.avoidence) //플레이어의 공격이 빗나갔을때
                    {
                        Console.Write("공격이 빗나갔다!");
                        Console.ReadKey(true);
                        Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                        DialogueClear();
                    }
                    else// 플레이어의 공격이 성공했을때
                    {
                        Console.WriteLine("{0} 의 {1}!", Player.instance.name, Player.instance.skills[2].Name);
                        if (100 <= criRan + enemy.critical) //플레이어의 공격이 크리티컬이 터졌을때
                        {
                            int criDam = playerSkillDam2 *= 2;
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", enemy.name, criDam);
                            Console.ReadKey(true);
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("급소에 맞았다!");
                            Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                            DialogueClear();
                            enemy.hp -= criDam;
                            Player.instance.skills[2].Pp -= 1;
                        }
                        else // 플레이어의 공격이 크리티컬이 안터졌을때
                        {
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", enemy.name, playerSkillDam2);
                            Console.ReadKey(true);
                            Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                            DialogueClear();
                            enemy.hp -= playerSkillDam2;
                            Player.instance.skills[2].Pp -= 1;
                        }
                    }
                }
                else if (skillName == Player.instance.skills[3].Name)
                {
                    if (100 <= avoidRan + enemy.avoidence) //플레이어의 공격이 빗나갔을때
                    {
                        Console.Write("공격이 빗나갔다!");
                        Console.ReadKey(true);
                        Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                        DialogueClear();
                    }
                    else// 플레이어의 공격이 성공했을때
                    {
                        Console.WriteLine("{0} 의 {1}!", Player.instance.name, Player.instance.skills[3].Name);
                        if (100 <= criRan + enemy.critical) //플레이어의 공격이 크리티컬이 터졌을때
                        {
                            int criDam = playerSkillDam3 *= 2;
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", enemy.name, criDam);
                            Console.ReadKey(true);
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("급소에 맞았다!");
                            Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                            DialogueClear();
                            enemy.hp -= criDam;
                            Player.instance.skills[3].Pp -= 1;
                        }
                        else // 플레이어의 공격이 크리티컬이 안터졌을때
                        {
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", enemy.name, playerSkillDam3);
                            Console.ReadKey(true);
                            Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                            DialogueClear();
                            enemy.hp -= playerSkillDam3;
                            Player.instance.skills[3].Pp -= 1;
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
            int avoidRan = ran.Next(100) + 1;
            int criRan = ran.Next(100) + 1;
            //데미지 계산식 = (스킬계수 * 공격력 * (레벨*2 /5 +2) / 방어 / 50 ) * 속성보정
            int monSkillDam0 = (enemy.skills[0].Power * enemy.atk * (enemy.level * 2 / 5 + 2) / enemy.def / 50);
            int monSkillDam1 = (enemy.skills[1].Power * enemy.atk * (enemy.level * 2 / 5 + 2) / enemy.def / 50);
            int monSkillDam2 = (enemy.skills[2].Power * enemy.atk * (enemy.level * 2 / 5 + 2) / enemy.def / 50);
            int monSkillDam3 = (enemy.skills[3].Power * enemy.atk * (enemy.level * 2 / 5 + 2) / enemy.def / 50);

        }
        public void ItemUse()
        {
            List<Item> tmpItem = Player.instance.inven.items;

            ItemList.SelectMenu(DIALOGUE_X, DIALOGUE_Y, tmpItem);
        }
        public void Run() // 도망치기시 80%확률로 도망에 성공,
        {
            Random random = new Random();
            int run = random.Next(100) + 1;

            if (run < 80)
            {
                Console.WriteLine("성공적으로 도망쳤다!");
                //isBattlePlay = false; (배틀 실행 종료)
                //맵에서 커서움직이게하는 bool = true (추후수정)
            }
            if (run >= 80)
            {
                Console.WriteLine("도망에 실패했습니다.");
            }
        }

        public void CheckPlayerDie() //플레이어가 죽었는지 확인하는 메서드
        {
            if (Player.instance.hp == 0)
            {
                Console.WriteLine("{0} 의 체력은 0이 되었다.", Player.instance.name);
                Console.WriteLine("눈앞이 깜깜해졌다.");
                //isPlay = false (전체 게임 실행 종료)
            }
        }
        public void CheckMonsterDie() //몬스터가 죽었는지 확인하는 메서드
        {

            if (enemy.hp == 0)
            {
                Console.WriteLine("{0}의 체력이 0이 되었다.");
                Console.WriteLine("{0}는 exp{1}과 {2}g를 얻었다!", Player.instance.name, enemy.exp, enemy.dropgold);
                //Player.instance.gold += enemy.dropgold;
                //Player.instance.exp += enemy.exp;
                //isBattlePlay = false (배틀 실행 종료)
                //맵에서 커서움직이게하는 bool = true (추후수정)
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

    }
}
