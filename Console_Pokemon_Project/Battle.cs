using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    class Battle
    {
        const int WINDOW_WIDTH = 120;
        const int WINDOW_HEIGHT = 45;

        // 몬스터와 나의 체력등을 이용해 전투종료
        // 아이템사용 스킬사용 도망치기 선택
        // 승패에 따른 결과값 반환
        public char[,] pixel = new char[60, 45];
        public int cursorX = 4;
        public int cursorY = 27;
        public List<string> battle = new List<string> { "전투", "아이템 사용", "도망" };


        public void BattleFrame()
        {
            //for(int y = 0; y<)
        }

        public void MeetPokemon()
        {
            bool isBattlePlay = true;
            while(isBattlePlay)
            { 
            Pokemon[] pokemon = new Pokemon[10];
            Random random = new Random();
            int monNum = random.Next(10);


            Console.WriteLine("지나가던 {0}와 조우했다!",pokemon[monNum].name);
            Console.WriteLine("어떤 행동을 하시겠습니까?");
                /*
                 여기서 커서로 행동값의 정보를 받아옴
                0=전투
                1=아이템사용
                2=도망
                 */
                int num = int.Parse(Console.ReadLine()); // 임시로 커서로 받아올 정보대신 입력으로 넣어놨음
                switch(num)
                {
                    case 0:
                        {
                            //Console.WriteLine("어떤 스킬을 사용하시겠습니까?");
                            if (Player.instance.speed > pokemon[monNum].speed) // 플레이어의 스피드가 빠를때 플레이어의 선공
                            {
                                PlayerAttack(monNum);
                                CheckMonsterDie(monNum);
                                MonsterAttack(monNum);
                                CheckPlayerDie();
                            }
                            else // 몬스터의 스피드가 더 빠를때 몬스터의 선공
                            {
                                MonsterAttack(monNum);
                                CheckPlayerDie();
                                PlayerAttack(monNum);
                                CheckMonsterDie(monNum);
                            }

                            break;
                        }
                    case 1:
                        {
                            Console.WriteLine("");
                            break;
                        }
                    case 2:
                        { 
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
               
            }
        }
        public void PlayerAttack(int monNum)
        {
            Pokemon[] pokemon = new Pokemon[10];
            //데미지 계산식 = (스킬계수 * 공격력 * (레벨*2 /5 +2) / 방어 / 50 ) * 속성보정
            //int dam = (skill * Player.instance.atk * (Player.instance.level * 2 / 5 + 2) / pokemon[monNum].def / 50);
            pokemon[monNum].hp = pokemon[monNum].hp - (Player.instance.atk - pokemon[monNum].def); //임시로 간략하게 계산식을 넣어둠

            /* 스킬을 넣고 커서로 스킬을 정했을경우 
               스킬 4개의 각각 데미지를 int skill1Dam , skill2dam, skill3Dam skill4Dam 로 저장
             
             */
            int num = int.Parse(Console.ReadLine()); // 임시로 커서로 받아올 정보대신 입력으로 넣어놨음
            switch (num)
            {
                case 0:
                    {
                        if (100 - pokemon[monNum].avoidence == 0 ) // 이부분은 100-avoidence < 랜덤1~100 가될때 로 할예정 or 랜덤1~100돌려서 5이상일때
                        {
                            Console.WriteLine("공격이 빗나갔다!");
                            break;
                        }
                        else
                        {
                        Console.WriteLine("{0} 의 {1} 공격!", Player.instance.name, Player.instance.atk) ;
                        
                        Console.WriteLine();
                        }
                        break; 
                    }
                case 1:
                    { 
                        break; 
                    }
                case 2:
                    { 
                        break; 
                    }
                case 3: 
                    {
                       
                        break; 
                    }
                default: // 여기서는 랭업기를 예시로 들었음
                    {
                        //여기는 랭다운기
                       // Console.WriteLine("{0} 의 {1}!", Player.instance.name, Player.instance.skill);
                       // Console.WriteLine("{0} 의 {1}이 하락했다", pokemon[monNum].name, pokemon[monNum].def);
                       // pokemon[monNum].def -= 
                       // 여기는 랭업기
                       //Console.WriteLine("{0} 의 {1}!", Player.instance.name , Player.instance.skill);
                       //Console.WriteLine("{0} 의 {1}이 상승했다",Player.instance.name, Player.instance.def );
                       //Player.instance.def += Player.instance.def /2;
                         break;
                    }
                   
            }
        }
        public void MonsterAttack(int monNum)
        {
            Pokemon[] pokemon = new Pokemon[10];
            //int dam = (skill * pokemon[monNum].att * (pokemon[monNum].att * 2 / 5 + 2) / Player.instance.def / 50);
            Player.instance.hp = Player.instance.hp - (pokemon[monNum].att - Player.instance.def);
        }
        public void ItemUse()
        {

        }
        public void Run() // 도망치기시 80%확률로 도망에 성공,
        {
            Random random = new Random();
            int run = random.Next(100)+1;

            if(run < 80)
            {
                Console.WriteLine("성공적으로 도망쳤다!");
                //isBattlePlay = false; (배틀 실행 종료)
                //맵에서 커서움직이게하는 bool = true (추후수정)
            }
            if (run >=80)
            {
                Console.WriteLine("도망에 실패했습니다.");
            }
        }

        public void CheckPlayerDie() //플레이어가 죽었는지 확인하는 메서드
        {
            if(Player.instance.hp==0)
            {
                Console.WriteLine("{0} 의 체력은 0이 되었다.", Player.instance.name) ;
                Console.WriteLine("눈앞이 깜깜해졌다.");
                //isPlay = false (전체 게임 실행 종료)
            }
        }
        public void CheckMonsterDie(int monNum) //몬스터가 죽었는지 확인하는 메서드
        {
            Pokemon[] pokemon = new Pokemon[10];
            if(pokemon[monNum].hp==0)
            {
                Console.WriteLine("{0}의 체력이 0이 되었다.");
                Console.WriteLine("{0}는 exp{1}과 {2}g를 얻었다!",Player.instance.name,pokemon[monNum].exp , pokemon[monNum].dropgold) ;
                //Player.instance.gold += pokemon[monNum].dropgold;
                //Player.instance.exp += pokemon[monNum].exp;
                //isBattlePlay = false (배틀 실행 종료)
                //맵에서 커서움직이게하는 bool = true (추후수정)
            }

        }
    }
}
