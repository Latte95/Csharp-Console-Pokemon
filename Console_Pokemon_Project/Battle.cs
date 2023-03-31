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
        const int BATTLE_WIDTH = Screen.WINDOW_WIDTH / 2;
        const int BATTLE_HEIGHT = Screen.WINDOW_HEIGHT;
        public const int DIALOGUE_X = 4;
        public const int DIALOGUE_Y = BATTLE_HEIGHT - 10;
        const int DIALOGUE_WINDOW_WIDTH = 30;
        const int DIALOGUE_WINDOW_HEIGHT = 10;

        // 몬스터와 나의 체력등을 이용해 전투종료
        // 아이템사용 스킬사용 도망치기 선택
        // 승패에 따른 결과값 반환
        public static char[,] pixel = new char[BATTLE_WIDTH, BATTLE_HEIGHT];
        public List<string> battle = new List<string> { "전투", "아이템 사용", "도망" };

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
                level,
                pokemons[monNum].characterDisplayInfo
                );

            foreach (Skill skill in Player.instance.skills)
            {
                skill.pp = skill.maxPp;
            }


        }

        public void MeetPokemon()
        {

            bool isBattlePlay = true; // 배틀전체 실행값
            int updateState = 0;
            BattleFrame();
            MonsterGraphic();
            PlayerGraphic();
            // MonsterGraphic();
            Screen.Print(pixel);
            Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
            Console.WriteLine("지나가던 {0}와 조우했다! 공격력 {1}", enemy.name, Player.instance.atk);
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
                        while (true)
                        {
                            bool isPp = false;
                            skillName = PlayerSelect();
                            foreach (Skill skill in Player.instance.skills)
                            {
                                if (skill.name.Equals(skillName) && skill.pp != 0)
                                {
                                    isPp = true;
                                    break;
                                }
                            }
                            if (isPp)
                            {
                                break;
                            }
                        }

                        {
                            if (Player.instance.speed > enemy.speed) // 플레이어의 스피드가 빠를때 플레이어의 선공
                            {
                                PlayerAttack(skillName);
                                isBattlePlay = CheckMonsterAlive();
                                if (isBattlePlay == false)
                                {
                                    break;
                                }
                                PlayerLevelUp(Player.instance.upExp);
                                MonsterAttack();
                                isBattlePlay = CheckPlayerAlive();
                            }
                            else // 몬스터의 스피드가 더 빠를때 몬스터의 선공
                            {
                                MonsterAttack();
                                isBattlePlay = CheckPlayerAlive();
                                if (isBattlePlay == false)
                                {
                                    break;
                                }
                                PlayerAttack(skillName);
                                isBattlePlay = CheckMonsterAlive();
                                PlayerLevelUp(Player.instance.upExp);
                            }

                            break;
                        }
                    case "아이템 사용":
                        {
                            //아이템 사용칸

                            if (ItemUse(updateState) == false)
                            {
                                DialogueClear();
                                continue;
                            }
                            MonsterAttack();

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


                Player.instance.isInBattle = false;
                Player.instance.isWaitingInput = true;
            }
        }
        public string PlayerSelect()
        {
            DialogueClear();
            string skillName = Menu.SelectMenu(DIALOGUE_X, DIALOGUE_Y, Player.instance.skills);
            return skillName;

        }
        public void PlayerAttack(string skillName)
        {

            Random ran = new Random();
            int avoidRan = ran.Next(100) + 1;
            int criRan = ran.Next(100) + 1;
            //데미지 계산식 = (스킬계수 * 공격력 * (레벨*2 /5 +2) / 방어 / 50 ) * 속성보정
            int playerSkillDam0 = (Player.instance.skills[0].power * Player.instance.atk * (Player.instance.level * 2 / 5 + 2) / enemy.def / 50);
            int playerSkillDam1 = (Player.instance.skills[1].power * Player.instance.atk * (Player.instance.level * 2 / 5 + 2) / enemy.def / 50);
            int playerSkillDam2 = (Player.instance.skills[2].power * Player.instance.atk * (Player.instance.level * 2 / 5 + 2) / enemy.def / 50);
            int playerSkillDam3 = (Player.instance.skills[3].power * Player.instance.atk * (Player.instance.level * 2 / 5 + 2) / enemy.def / 50);
            if (playerSkillDam0 < 1) { playerSkillDam0 = 1; } //각 데미지별 최소데미지를 1로 설정
            if (playerSkillDam1 < 1) { playerSkillDam1 = 1; }
            if (playerSkillDam2 < 1) { playerSkillDam2 = 1; }
            if (playerSkillDam3 < 1) { playerSkillDam3 = 1; }
            DialogueClear();


            if (Player.instance.hp != 0 && enemy.hp != 0)
            {
                DialogueClear();
                if (skillName == Player.instance.skills[0].name) // 0번스킬을 사용했을때
                {
                    if (Player.instance.skills[0].pp == 0)
                    {

                    }
                    if (Player.instance.skills[0].hitrate <= avoidRan + enemy.avoidence) //플레이어의 공격이 빗나갔을때
                    {
                        Console.Write("{0}의 공격이 빗나갔다!", Player.instance.name);
                        Console.ReadKey(true);
                        DialogueClear();
                    }
                    else// 플레이어의 공격이 성공했을때
                    {
                        Console.WriteLine("{0}의 {1}!", Player.instance.name, Player.instance.skills[0].name);
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
                            Player.instance.skills[0].pp -= 1;
                            if (enemy.hp < 0) { enemy.hp = 0; }
                        }
                        else // 플레이어의 공격이 크리티컬이 안터졌을때
                        {
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", enemy.name, playerSkillDam0);
                            Console.ReadKey(true);
                            DialogueClear();
                            enemy.hp -= playerSkillDam0;
                            Player.instance.skills[0].pp -= 1;
                            if (enemy.hp < 0) { enemy.hp = 0; }
                        }
                    }

                }
                else if (skillName == Player.instance.skills[1].name) //1번스킬을 사용했을때
                {
                    if (100 <= Player.instance.skills[1].hitrate + enemy.avoidence) //플레이어의 공격이 빗나갔을때
                    {
                        Console.Write("{0}의 공격이 빗나갔다!", Player.instance.name);
                        Console.ReadKey(true);
                        DialogueClear();
                    }
                    else// 플레이어의 공격이 성공했을때
                    {
                        Console.WriteLine("{0}의 {1}!", Player.instance.name, Player.instance.skills[1].name);
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
                            Player.instance.skills[1].pp -= 1;
                            if (enemy.hp < 0) { enemy.hp = 0; }

                        }
                        else // 플레이어의 공격이 크리티컬이 안터졌을때
                        {
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", enemy.name, playerSkillDam1);
                            Console.ReadKey(true);
                            DialogueClear();
                            enemy.hp -= playerSkillDam1;
                            Player.instance.skills[1].pp -= 1;
                            if (enemy.hp < 0) { enemy.hp = 0; }
                        }
                    }
                }

                else if (skillName == Player.instance.skills[2].name) //2번스킬을 사용했을때
                {
                    if (Player.instance.skills[2].hitrate <= avoidRan + enemy.avoidence) //플레이어의 공격이 빗나갔을때
                    {
                        Console.Write("{0}의 공격이 빗나갔다!", Player.instance.name);
                        Console.ReadKey(true);
                        DialogueClear();
                    }
                    else// 플레이어의 공격이 성공했을때
                    {
                        Console.WriteLine("{0}의 {1}!", Player.instance.name, Player.instance.skills[2].name);
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
                            Player.instance.skills[2].pp -= 1;
                            if (enemy.hp < 0) { enemy.hp = 0; }
                        }
                        else // 플레이어의 공격이 크리티컬이 안터졌을때
                        {
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", enemy.name, playerSkillDam2);
                            Console.ReadKey(true);
                            DialogueClear();
                            enemy.hp -= playerSkillDam2;
                            Player.instance.skills[2].pp -= 1;
                            if (enemy.hp < 0) { enemy.hp = 0; }
                        }
                    }
                }
                else if (skillName == Player.instance.skills[3].name) //3번스킬을 사용했을때
                {

                    if (Player.instance.skills[3].hitrate <= avoidRan + enemy.avoidence) //플레이어의 공격이 빗나갔을때
                    {
                        Console.Write("{0}의 공격이 빗나갔다!", Player.instance.name);
                        Console.ReadKey(true);
                        DialogueClear();
                    }
                    else// 플레이어의 공격이 성공했을때
                    {
                        Console.WriteLine("{0}의 {1}!", Player.instance.name, Player.instance.skills[3].name);
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
                            Player.instance.skills[3].pp -= 1;
                            if (enemy.hp < 0) { enemy.hp = 0; }
                        }
                        else // 플레이어의 공격이 크리티컬이 안터졌을때
                        {
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", enemy.name, playerSkillDam3);
                            Console.ReadKey(true);
                            DialogueClear();
                            enemy.hp -= playerSkillDam3;
                            Player.instance.skills[3].pp -= 1;
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
            int monSkillDam0 = (enemy.skills[0].power * enemy.atk * (enemy.level * 2 / 5 + 2) / enemy.def / 50);
            int monSkillDam1 = (enemy.skills[1].power * enemy.atk * (enemy.level * 2 / 5 + 2) / enemy.def / 50);
            int monSkillDam2 = (enemy.skills[2].power * enemy.atk * (enemy.level * 2 / 5 + 2) / enemy.def / 50);
            int monSkillDam3 = (enemy.skills[3].power * enemy.atk * (enemy.level * 2 / 5 + 2) / enemy.def / 50);
            if (monSkillDam0 < 1) { monSkillDam0 = 1; } //데미지별 최소데미지 1로 설정
            if (monSkillDam1 < 1) { monSkillDam1 = 1; }
            if (monSkillDam2 < 1) { monSkillDam2 = 1; }
            if (monSkillDam3 < 1) { monSkillDam3 = 1; }
            DialogueClear();

            Console.WriteLine(enemy.skills[ranSkill].name);
            if (Player.instance.hp != 0 && enemy.hp != 0)
            {
                DialogueClear();
                if (ranSkill == 0) // 0번스킬을 사용했을때
                {
                    if (enemy.skills[0].hitrate <= avoidRan + Player.instance.avoidence) //몬스터의 공격이 빗나갔을때
                    {
                        Console.Write("{0}의 공격이 빗나갔다!", enemy.name);
                        Console.ReadKey(true);
                        DialogueClear();

                    }
                    else// 플레이어의 공격이 성공했을때
                    {
                        Console.WriteLine("{0}의 {1}!", enemy.name, enemy.skills[0].name);
                        Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);

                        if (100 <= criRan + enemy.critical) //몬스터의 공격이 크리티컬이 터졌을때
                        {
                            int criDam = monSkillDam0 *= 2;
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", Player.instance.name, criDam);
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("급소에 맞았다!");
                            Console.ReadKey(true);
                            DialogueClear();
                            Player.instance.hp -= criDam;
                            enemy.skills[0].pp -= 1;
                            if (Player.instance.hp < 0) { Player.instance.hp = 0; }
                        }
                        else // 몬스터의 공격이 크리티컬이 안터졌을때
                        {
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", Player.instance.name, monSkillDam0);
                            Console.ReadKey(true);
                            DialogueClear();
                            Player.instance.hp -= monSkillDam0;
                            enemy.skills[0].pp -= 1;
                            if (Player.instance.hp < 0) { Player.instance.hp = 0; }
                        }
                    }

                }
                else if (ranSkill == 1) //1번스킬을 사용했을때
                {
                    if (enemy.skills[1].hitrate <= avoidRan + Player.instance.avoidence) //몬스터의 공격이 빗나갔을때
                    {
                        Console.Write("{0}의 공격이 빗나갔다!", enemy.name);
                        Console.ReadKey(true);
                        DialogueClear();
                    }
                    else// 플레이어의 공격이 성공했을때
                    {
                        Console.WriteLine("{0}의 {1}!", enemy.name, enemy.skills[1].name);
                        Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);

                        if (100 <= criRan + enemy.critical) //몬스터의 공격이 크리티컬이 터졌을때
                        {
                            int criDam = monSkillDam1 *= 2;
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", Player.instance.name, criDam);
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("급소에 맞았다!");
                            Console.ReadKey(true);
                            DialogueClear();
                            Player.instance.hp -= criDam;
                            enemy.skills[1].pp -= 1;
                            if (Player.instance.hp < 0) { Player.instance.hp = 0; }
                        }
                        else // 몬스터의 공격이 크리티컬이 안터졌을때
                        {
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", Player.instance.name, monSkillDam1);
                            Console.ReadKey(true);
                            DialogueClear();
                            Player.instance.hp -= monSkillDam1;
                            enemy.skills[1].pp -= 1;
                            if (Player.instance.hp < 0) { Player.instance.hp = 0; }
                        }
                    }
                }

                else if (ranSkill == 2) //2번스킬을 사용했을때
                {
                    if (enemy.skills[2].hitrate <= avoidRan + Player.instance.avoidence) //몬스터의 공격이 빗나갔을때
                    {
                        Console.Write("{0}의 공격이 빗나갔다!", enemy.name);
                        Console.ReadKey(true);
                        DialogueClear();
                    }
                    else// 플레이어의 공격이 성공했을때
                    {
                        Console.WriteLine("{0}의 {1}!", enemy.name, enemy.skills[2].name);
                        Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);

                        if (100 <= criRan + enemy.critical) //몬스터의 공격이 크리티컬이 터졌을때
                        {
                            int criDam = monSkillDam2 *= 2;
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", Player.instance.name, criDam);
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("급소에 맞았다!");
                            Console.ReadKey(true);
                            DialogueClear();
                            Player.instance.hp -= criDam;
                            enemy.skills[2].pp -= 1;
                            if (Player.instance.hp < 0) { Player.instance.hp = 0; }
                        }
                        else // 몬스터의 공격이 크리티컬이 안터졌을때
                        {
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", Player.instance.name, monSkillDam2);
                            Console.ReadKey(true);
                            DialogueClear();
                            Player.instance.hp -= monSkillDam2;
                            enemy.skills[2].pp -= 1;
                            if (Player.instance.hp < 0) { Player.instance.hp = 0; }
                        }
                    }
                }
                else if (ranSkill == 3) //3번스킬을 사용했을때
                {
                    if (enemy.skills[3].hitrate <= avoidRan + Player.instance.avoidence) //몬스터의 공격이 빗나갔을때
                    {
                        Console.Write("{0}의 공격이 빗나갔다!", enemy.name);
                        Console.ReadKey(true);
                        DialogueClear();
                    }
                    else// 플레이어의 공격이 성공했을때
                    {
                        Console.WriteLine("{0}의 {1}!", enemy.name, enemy.skills[0].name);
                        Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);

                        if (100 <= criRan + enemy.critical) //몬스터의 공격이 크리티컬이 터졌을때
                        {
                            int criDam = monSkillDam3 *= 2;
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", Player.instance.name, criDam);
                            Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                            Console.WriteLine("급소에 맞았다!");
                            Console.ReadKey(true);
                            DialogueClear();
                            Player.instance.hp -= criDam;
                            enemy.skills[3].pp -= 1;
                            if (Player.instance.hp < 0) { Player.instance.hp = 0; }
                        }
                        else // 몬스터의 공격이 크리티컬이 안터졌을때
                        {
                            Console.WriteLine("{0}은 {1}의 데미지를 입었다.", Player.instance.name, monSkillDam3);
                            Console.ReadKey(true);
                            DialogueClear();
                            Player.instance.hp -= monSkillDam3;
                            enemy.skills[3].pp -= 1;
                            if (Player.instance.hp < 0) { Player.instance.hp = 0; }
                        }
                    }
                }


            }
        }

        public bool ItemUse(int updateState)
        {
            List<Item> tmpItems = new List<Item>();
            foreach (Item item in Player.instance.inven.items)
            {
                if (item is ConsumableItem)
                {
                    tmpItems.Add(item);
                }
            }

            DialogueClear();

            string itemName = Menu.SelectMenu(DIALOGUE_X, DIALOGUE_Y, tmpItems);

            int selectedIndex = tmpItems.FindIndex(item => item.name == itemName);
            if (selectedIndex < 0)
            {
                Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                Console.WriteLine("아이템을 못찾았어요");
                Console.ReadKey(true);
                DialogueClear();
                return false;
            }
            Item selectedItem = tmpItems[selectedIndex];

            switch (itemName)
            {
                case "체력포션":
                    {
                        if (selectedItem.quantity <= 0)
                        {
                            Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                            Console.WriteLine("아이템이 없어요");
                            return false;
                        }
                        updateState = 50; // 회복양(포션에따라)
                        DialogueClear();
                        Console.WriteLine("체력 포션을 사용하였다.");
                        Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                        Player.instance.hp += 50;
                        if (Player.instance.hp > Player.instance.maxHp) // 최대체력 제한
                        {
                            updateState = updateState + Player.instance.maxHp - Player.instance.hp; //제한에 걸렸을시 출력량
                            Player.instance.hp = Player.instance.maxHp; //제한에 걸렸을이 최대체력으로 설정
                        }
                        Console.WriteLine("{0} 이 {1}만큼 회복되었다.", Player.instance.name, updateState);
                        Player.instance.inven.RemoveItem(selectedItem);
                        Console.ReadKey(true);
                        DialogueClear();
                        // 소모품 개수 닳게할공간
                        break;
                    }
            }
            return true;

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
                Console.Clear();
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
                Console.Clear();
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

                Display();
                Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                Console.WriteLine("{0}의 체력이 0이 되었다.", enemy.name);
                Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                Console.WriteLine("전투에서 승리했다!");
                Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                Console.WriteLine("{0}는 exp{1}과 {2}g를 얻었다!", Player.instance.name, enemy.exp, enemy.dropgold);
                Console.ReadKey(true);
                Player.instance.money += enemy.dropgold;
                Player.instance.exp += enemy.exp;
                //맵에서 커서움직이게하는 bool = true (추후수정)
                Console.Clear();
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

            for (int i = 0; i < DIALOGUE_WINDOW_HEIGHT - 1; i++)
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

        public static void Display() //플레이어와 몬스터의 상태창을 띄워주는 메서드 (상수좀 바꿔줘야될듯?)
        {
            int monHpBar = (enemy.maxHp / 10) ;

            Console.SetCursorPosition(44, 2);
            Console.WriteLine("이름 : {0} ", enemy.name); // 몬스터의 Lv표시
            Console.SetCursorPosition(44, 4);
            Console.WriteLine("Lv : {0} ", enemy.level); // 몬스터의 Lv표시


            Console.SetCursorPosition(44, 6);
            Console.BackgroundColor = ConsoleColor.Gray;
            for (int i=0; i< enemy.hp/monHpBar; i++)
            {
                Console.Write("　");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            for (int i=0; i<11-enemy.hp /monHpBar; i++)
            {
                Console.Write("　");
            }
            Console.ResetColor();

            // 몬스터의 hp표시
            Console.SetCursorPosition(44,7);
            Console.WriteLine("HP : {0 ,-3} / {1, -3}", enemy.hp, enemy.maxHp); 


            Console.SetCursorPosition(56, 28);
            Console.WriteLine("Lv : {0} ", Player.instance.level); //플레이어의 Lv표시
            Console.SetCursorPosition(56, 30);
            Console.WriteLine("HP : {0, -3} / {1, -3}", Player.instance.hp, Player.instance.maxHp);  //플레이어의 hp표시
            Console.SetCursorPosition(56, 32);
            Console.WriteLine("EXP : {0, -3} / {1, -3}", Player.instance.exp, Player.instance.upExp[Player.instance.level]);  //플레이어의 hp표시


        }

        public static void BattleFrame() //배틀 전체 배경 생성 메서드
        {
            for (int y = 0; y < BATTLE_HEIGHT; y++)
            {
                for (int x = 0; x < BATTLE_WIDTH; x++)
                {
                    if (y==33)
                    {
                        pixel[x, y] = '□';
                    }
                    else if (x == 0 || x == BATTLE_WIDTH - 1 ||
                       y == 0 || y == BATTLE_HEIGHT - 1)
                    {
                        pixel[x, y] = '□';
                    }
                    else
                    {
                        pixel[x, y] = '　';
                    }

                }
                
            }

        }
        public static void MonsterGraphic() //랜덤으로 호출된 몬스터를 그리는 메서드
        {
            for (int y = 0; y < 24; y++)
            {

                for (int x = 0; x < 24; x++)
                {
                    char c;
                    if (!enemy.characterDisplayInfo[y, x].Equals(' '))
                    {
                        c = (char)(enemy.characterDisplayInfo[y, x] + 0xFEE0);
                    }
                    else
                    {
                        c = '　';
                    }
                    pixel[(Screen.WINDOW_WIDTH >> 1) - 24 + x - 1, y + 1] = c;
                }
            }

        }

        public static void PlayerGraphic() //플레이어를 그리는 메서드
        {
            for (int y = 0; y < 24; y++)
            {

                for (int x = 0; x < 24; x++)
                {
                    char c;
                    if (!Player.instance.characterDisplayInfo[y, x].Equals(' '))
                    {
                        c = (char)(Player.instance.characterDisplayInfo[y, x] + 0xFEE0);
                    }
                    else
                    {
                        c = '　';
                    }
                    pixel[(Screen.WINDOW_WIDTH >> 1) - 58 + x - 1, y + 9] = c;
                }
            }
        }
        public static void CanSkill(string skillName)
        {


            Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
            Console.WriteLine("스킬의 PP가 부족하여 사용하실 수 없습니다.");

        }
        public static void PlayerLevelUp(int[] upExp)
        {
            if (Player.instance.exp >= upExp[Player.instance.level])
            {
                Player.instance.exp -= upExp[Player.instance.level];
                Player.instance.level += 1;
                DialogueClear();
                Console.WriteLine("플레이어의 레벨이 상승했다.");
                Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                Console.WriteLine("현재 플레이어의 레벨 : {0}", Player.instance.level);
                Console.ReadKey(true);
            }



        }
    }
}
