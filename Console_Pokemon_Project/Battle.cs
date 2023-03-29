﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Console_Pokemon_Project
{
    class Battle
    {
        const int WINDOW_WIDTH = 120;
        const int WINDOW_HEIGHT = 45;
        const int DIALOGUE_X = 4;
        const int DIALOGUE_Y = WINDOW_HEIGHT-10;
        const int DIALOGUE_WINDOW_WIDTH = 20;
        const int DIALOGUE_WINDOW_HEIGHT = 5;

        // 몬스터와 나의 체력등을 이용해 전투종료
        // 아이템사용 스킬사용 도망치기 선택
        // 승패에 따른 결과값 반환
        public char[,] pixel = new char[60, 45];
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
            int ranAtt = random.Next(1, 5);
            int ranDef = random.Next(1, 3);
            int ranSpeed = random.Next(1, 10);
            int ranLevel = random.Next(1, 3);

            int level = PokemonInfo.pokemon[monNum].level + ranLevel;

            enemy = new Pokemon(PokemonInfo.pokemon[monNum].name,
                PokemonInfo.pokemon[monNum].hp + ranHp + level * 3,
                PokemonInfo.pokemon[monNum].att + ranAtt + level * 2,
                PokemonInfo.pokemon[monNum].def + ranDef + level,
                PokemonInfo.pokemon[monNum].speed + ranSpeed + level* 2,
                PokemonInfo.pokemon[monNum].exp + level* 2,
                PokemonInfo.pokemon[monNum].dropgold + level* 2,
                PokemonInfo.pokemon[monNum].critical,
                PokemonInfo.pokemon[monNum].avoidence,
                level);

            enemy.AddSkill();

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
                //Pokemon[] pokemon = new Pokemon[10];
                Console.SetCursorPosition(DIALOGUE_X, DIALOGUE_Y);
                Console.WriteLine("지나가던 {0}와 조우했다!", enemy.name);
                Console.SetCursorPosition(DIALOGUE_X, Console.CursorTop);
                Console.WriteLine("어떤 행동을 하시겠습니까?");
                /*
                 여기서 커서로 행동값의 정보를 받아옴
                0=전투
                1=아이템사용
                2=도망
                 */
                Console.ReadKey(true);

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
            //int dam = (skill * Player.instance.atk * (Player.instance.level * 2 / 5 + 2) / enemy.def / 50);
            enemy.hp = enemy.hp - (Player.instance.atk - enemy.def); //임시로 간략하게 계산식을 넣어둠

            /* 스킬을 넣고 커서로 스킬을 정했을경우 
               스킬 4개의 각각 데미지를 int skill1Dam , skill2dam, skill3Dam skill4Dam 로 저장
             
             */
            int num = int.Parse(Console.ReadLine()); // 임시로 커서로 받아올 정보대신 입력으로 넣어놨음
            switch (num)
            {
                case 0:
                    {
                        if (100 <= avoidRan + enemy.avoidence) //플레이어의 공격이 빗나갔을때
                        {
                            Console.WriteLine("공격이 빗나갔다!");
                            break;
                        }
                        else // 플레이어의 공격이 성공했을때
                        {
                            Console.WriteLine("{0} 의 {1}!", Player.instance.name, enemy.name); // {1}를 추후 스킬.네임 으로변경 
                            if (100 <= criRan + enemy.critical) //플레이어의 공격이 크리티컬이 터졌을때
                            {
                                int criDam = Player.instance.atk *= 2;
                                Console.WriteLine("{0} 는 {1}의 데미지를 입었다.", enemy.name, criDam); // {1}을 추후 데미지계산값으로 변경
                                Console.WriteLine("급소에 맞았다!");
                                enemy.hp -= criDam;
                            }
                            else // 플레이어의 공격이 크리티컬이 안터졌을때
                            {
                                Console.WriteLine("{0} 는 {1}의 데미지를 입었다.", enemy.name, Player.instance.atk); // {1}을 추후 데미지계산값으로 변경
                                enemy.hp -= Player.instance.atk;
                            }
                        }
                        break;
                    }
                case 1:
                    {
                        if (100 <= avoidRan + enemy.avoidence) //플레이어의 공격이 빗나갔을때
                        {
                            Console.WriteLine("공격이 빗나갔다!");
                            break;
                        }
                        else // 플레이어의 공격이 성공했을때
                        {
                            Console.WriteLine("{0} 의 {1}!", Player.instance.name, Player.instance.atk); // {1}를 추후 스킬.네임 으로변경 
                            if (100 <= criRan + enemy.critical) //플레이어의 공격이 크리티컬이 터졌을때
                            {
                                int criDam = Player.instance.atk *= 2;
                                Console.WriteLine("{0} 는 {1}의 데미지를 입었다.", enemy.name, criDam); // {1}을 추후 데미지계산값으로 변경
                                Console.WriteLine("급소에 맞았다!");
                                enemy.hp -= criDam;
                            }
                            else // 플레이어의 공격이 크리티컬이 안터졌을때
                            {
                                Console.WriteLine("{0} 는 {1}의 데미지를 입었다.", enemy.name, Player.instance.atk); // {1}을 추후 데미지계산값으로 변경
                                enemy.hp -= Player.instance.atk;
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        if (100 <= avoidRan + enemy.avoidence) //플레이어의 공격이 빗나갔을때
                        {
                            Console.WriteLine("공격이 빗나갔다!");
                            break;
                        }
                        else // 플레이어의 공격이 성공했을때
                        {
                            Console.WriteLine("{0} 의 {1}!", Player.instance.name, Player.instance.atk); // {1}를 추후 스킬.네임 으로변경 
                            if (100 <= criRan + enemy.critical) //플레이어의 공격이 크리티컬이 터졌을때
                            {
                                int criDam = Player.instance.atk *= 2;
                                Console.WriteLine("{0} 는 {1}의 데미지를 입었다.", enemy.name, criDam); // {1}을 추후 데미지계산값으로 변경
                                Console.WriteLine("급소에 맞았다!");
                                enemy.hp -= criDam;
                            }
                            else // 플레이어의 공격이 크리티컬이 안터졌을때
                            {
                                Console.WriteLine("{0} 는 {1}의 데미지를 입었다.", enemy.name, Player.instance.atk); // {1}을 추후 데미지계산값으로 변경
                                enemy.hp -= Player.instance.atk;
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        if (100 <= avoidRan + enemy.avoidence) //플레이어의 공격이 빗나갔을때
                        {
                            Console.WriteLine("공격이 빗나갔다!");
                            break;
                        }
                        else // 플레이어의 공격이 성공했을때
                        {
                            Console.WriteLine("{0} 의 {1}!", Player.instance.name, Player.instance.atk); // {1}를 추후 스킬.네임 으로변경 
                            if (100 <= criRan + enemy.critical) //플레이어의 공격이 크리티컬이 터졌을때
                            {
                                int criDam = Player.instance.atk *= 2;
                                Console.WriteLine("{0} 는 {1}의 데미지를 입었다.", enemy.name, criDam); // {1}을 추후 데미지계산값으로 변경
                                Console.WriteLine("급소에 맞았다!");
                                enemy.hp -= criDam;
                            }
                            else // 플레이어의 공격이 크리티컬이 안터졌을때
                            {
                                Console.WriteLine("{0} 는 {1}의 데미지를 입었다.", enemy.name, Player.instance.atk); // {1}을 추후 데미지계산값으로 변경
                                enemy.hp -= Player.instance.atk;
                            }
                        }
                        break;
                    }
                default: // 여기서는 랭업기를 예시로 들었음
                    {
                        //여기는 랭다운기
                        // Console.WriteLine("{0} 의 {1}!", Player.instance.name, Player.instance.skill);
                        // Console.WriteLine("{0} 의 {1}이 하락했다", enemy.name, enemy.def);
                        // enemy.def -= 
                        // 여기는 랭업기
                        //Console.WriteLine("{0} 의 {1}!", Player.instance.name , Player.instance.skill);
                        //Console.WriteLine("{0} 의 {1}이 상승했다",Player.instance.name, Player.instance.def );
                        //Player.instance.def += Player.instance.def /2;
                        break;
                    }

            }
        }
        public void MonsterAttack()
        {

            //int dam = (skill * enemy.att * (enemy.att * 2 / 5 + 2) / Player.instance.def / 50);
            Player.instance.hp = Player.instance.hp - (enemy.att - Player.instance.def);
        }
        public void ItemUse()
        {

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
    }
}
