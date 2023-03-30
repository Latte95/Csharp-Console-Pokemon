using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    public class Map
    {
        // 그래픽{0 0 0 0 0 1 }
        // 플레이어 이동과
        // 벽에 막혔는지
        // 엔터를 누른 위치가 상점앞인지 등의 정보를
        // screen에 전달하는 데 까지만구현하고
        // 상점창 띄우는거나 전투창 띄우는건 screen이 담당한다


        // 맵 오브젝트 타입
        public enum TileType
        {
            GROUND = '　',
            GRASS = '♣',
            RECTTILE = '▨',
            WALL = '■',
            PLAYER = '★',
            SHOP = '＠',
            MONSTER_FIELD = '※'
        }

        public const int MAP_WIDTH = 40;
        public const int MAP_HEIGHT = 45;

        public string name;
        public char[,] mapInfo = new char[MAP_WIDTH, MAP_HEIGHT]; // 맵 타일 정보
        public char[,] printInfo = new char[MAP_WIDTH, MAP_HEIGHT]; // screen으로 출력할 맵 캐릭터 배열
        public readonly int startXLoc;
        public readonly int startYLoc;
        private int lastPlayerLocX;
        private int lastPlayerLocY;
        private Shop shop;
        private GameInfo gameInfo = new GameInfo();
        private int shopLocX;
        private int shopLocY;
        private Random random = new Random();

        // 해당 Map의 위치 설정
        public Map(string name, int startXLoc, int startYLoc)
        {
            this.name = name;
            this.startXLoc = startXLoc;
            this.startYLoc = startYLoc;
            InitMap();
        }

        public void InitMap()
        {
            // 처음은 GROUND로 채움
            for (int i = 0; i < MAP_WIDTH; i++)
            {
                for (int j = 0; j < MAP_HEIGHT; j++)
                {
                    mapInfo[i, j] = (char)TileType.GROUND;
                }
            }
            // 테두리는 벽
            for (int i=0; i<MAP_WIDTH;i++)
            {
                mapInfo[i, 0] = (char)TileType.WALL;
                mapInfo[i, MAP_HEIGHT - 1] = (char)TileType.WALL;
            }
            for (int i = 0; i < MAP_HEIGHT; i++)
            {
                mapInfo[0, i] = (char)TileType.WALL;
                mapInfo[MAP_WIDTH - 1, i] = (char)TileType.WALL;
            }

            // 풀 배치
            int thisMapIndex = (this.startYLoc / MAP_HEIGHT) * 2 + (this.startXLoc / MAP_WIDTH);

            //int grassStartPosX = thisMapIndex * MAP_WIDTH / 2 + 1;
            //int grassStartPosY = thisMapIndex * MAP_HEIGHT / 2 + 1;

            int grassStartPosX;
            int grassStartPosY;

            if (thisMapIndex % 2 == 0)
            {
                grassStartPosX = 1;
            }
            else
            {
                grassStartPosX = 21;
            }
            if(thisMapIndex / 2 == 0)
            {
                grassStartPosY = 1;
            }
            else
            {
                grassStartPosY = 23;
            }
            

            for (int i = grassStartPosX; i < MAP_WIDTH / 2 + grassStartPosX-2; i++)
            {
                for (int j = grassStartPosY; j < MAP_HEIGHT / 2 + grassStartPosY - 1; j++)
                {
                    if ((i % 2 == 0) && (j % 2 == 0))
                    {
                        mapInfo[i, j] = (char)TileType.GRASS;
                    }
                    else if ((i % 2 == 1) && (j % 2 == 1))
                    {
                        mapInfo[i, j] = (char)TileType.GRASS;
                    }
                }
            }

            // 몬스터존  배치
            int monsterZoneStartPosX;
            int monsterZoneStartPosY;

            if (thisMapIndex % 2 == 0)
            {
                monsterZoneStartPosX = 21;
            }
            else
            {
                monsterZoneStartPosX = 1;
            }
            if (thisMapIndex / 2 == 0)
            {
                monsterZoneStartPosY = 23;
            }
            else
            {
                monsterZoneStartPosY = 1;
            }

            for (int i = monsterZoneStartPosX; i < MAP_WIDTH / 2 + monsterZoneStartPosX - 2; i++)
            {
                for (int j = monsterZoneStartPosY; j < MAP_HEIGHT / 2 + monsterZoneStartPosY - 1; j++)
                {
                    if ((i % 2 == 0) && (j % 2 == 0))
                    {
                        mapInfo[i, j] = (char)TileType.RECTTILE;
                    }
                    else if ((i % 2 == 1) && (j % 2 == 1))
                    {
                        mapInfo[i, j] = (char)TileType.RECTTILE;
                    }
                }
            }


            // 다음 맵으로 넘어갈 수 있는 위치
            mapInfo[MAP_WIDTH / 2, 0] = (char)TileType.GROUND;
            mapInfo[MAP_WIDTH / 2, MAP_HEIGHT - 1] = (char)TileType.GROUND;
            mapInfo[MAP_WIDTH - 1, MAP_HEIGHT / 2] = (char)TileType.GROUND;
            mapInfo[0, MAP_HEIGHT / 2] = (char)TileType.GROUND;

            // 상점 위치
            shopLocX = MAP_WIDTH / 2;
            shopLocY = MAP_HEIGHT / 2;
            mapInfo[shopLocX, shopLocY] = (char)TileType.SHOP;

            // 몬스터 출현공간
            mapInfo[4, 4] = (char)TileType.MONSTER_FIELD;

            // 상점 생성
            CreateShop();
        }
        private void CreateShop()
        {
            shop = new Shop();
            int totalMapColumeCount = 2; // 1줄에 맵 몇개 있는지
            int myMapNumber = ((startXLoc / MAP_WIDTH) + (startYLoc / MAP_HEIGHT) * totalMapColumeCount);// 몇번째 맵인지 확인

            // json 파일 있는 경로
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\.\JSON\"));

            // 타입 저장 (Item의 파생클래스 단위로 저장 가능)
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            // json파일 읽기
            string jsonFromFile = File.ReadAllText(path + "itemInfos.json");

            // json 파일 내용에서 객체로 역직렬화
            List<Item> itemsInfos = JsonConvert.DeserializeObject<List<Item>>(jsonFromFile, settings);

            // 상점 아이템 목록 생성
            // 장비 3개 먼저
            for (int i=0; i<3; i++)
            {
                shop.saleItems.Add(new EquipableItem(
                    (itemsInfos[i + (myMapNumber * 4)] as EquipableItem).name,
                    (itemsInfos[i + (myMapNumber * 4)] as EquipableItem).atk,
                    (itemsInfos[i + (myMapNumber * 4)] as EquipableItem).def,
                    (itemsInfos[i + (myMapNumber * 4)] as EquipableItem).price,
                    (itemsInfos[i + (myMapNumber * 4)] as EquipableItem).equipType,
                    (itemsInfos[i + (myMapNumber * 4)] as EquipableItem).quantity)
                    );
            }
            // 나머지 소비
            for (int i=0; i<1; i++)
            {
                shop.saleItems.Add(new ConsumableItem(
                    (itemsInfos[i + (myMapNumber * 4) + 3] as ConsumableItem).name,
                    (itemsInfos[i + (myMapNumber * 4) + 3] as ConsumableItem).atk,
                    (itemsInfos[i + (myMapNumber * 4) + 3] as ConsumableItem).def,
                    (itemsInfos[i + (myMapNumber * 4) + 3] as ConsumableItem).price,
                    (itemsInfos[i + (myMapNumber * 4) + 3] as ConsumableItem).quantity
                    ));
            }
            
        }
        // 현재 맵에 플레이어가 없으면 true 반환
        public bool UpdatePlayerLoc()
        {
            // 플레이어 표시 위치를 전체 맵에서의 위치에서 현재 맵에서의 위치로 보정
            int lastPlayerPosX = lastPlayerLocX - this.startXLoc;
            int lastPlayerPosY = lastPlayerLocY - this.startYLoc;
            int playerPosX = Player.instance.locX - this.startXLoc;
            int playerPosY = Player.instance.locY - this.startYLoc;

            printInfo = (char[,])mapInfo.Clone();

            // 플레이어가 해당 맵에 없으면 종료
            if (playerPosX >= MAP_WIDTH || playerPosX < 0 ||
                playerPosY >= MAP_HEIGHT || playerPosY < 0)
            {
                printInfo[lastPlayerPosX, lastPlayerPosY] = (char)TileType.GROUND;    // 맵 바뀌기 직전 캐릭터 흔적 삭제
                return true;
            }

            // 현재 위치가 벽이면 이전위치로
            if (CheckBlock(playerPosX, playerPosY) == true)
            {
                Player.instance.locX = lastPlayerLocX;
                Player.instance.locY = lastPlayerLocY;

                playerPosX = Player.instance.locX - this.startXLoc;
                playerPosY = Player.instance.locY - this.startYLoc;
            }
            else
            {
                lastPlayerLocX = Player.instance.locX;
                lastPlayerLocY = Player.instance.locY;
            }

            printInfo[playerPosX, playerPosY] = (char)TileType.PLAYER;
            gameInfo.ShowPlayerStat();
            gameInfo.ShowMapName(this.name);
            return false;
        }
        public bool CheckBlock(int playerPosX, int playerPosY)
        {   
            return mapInfo[playerPosX, playerPosY] == (char)TileType.WALL || 
                mapInfo[playerPosX, playerPosY] == (char)TileType.SHOP;
        }
        public bool CheckHereIsMonsterField()
        {
            // 임시 몬스터 출현 필드 검사
            int playerPosX = Player.instance.locX - this.startXLoc;
            int playerPosY = Player.instance.locY - this.startYLoc;

            if (playerPosX == 4 && playerPosY == 4)
            {
                return true;
            }

            return false;
        }
        public bool CheckHereIsShop()
        {
            bool isHereShop = false;

            int playerPosX = Player.instance.locX - this.startXLoc;
            int playerPosY = Player.instance.locY - this.startYLoc;

            // 플레이어가 상점의 상하좌우 1칸에 위치할 떄
            if(playerPosX >= shopLocX - 1 && playerPosX <= shopLocX + 1 && playerPosY == shopLocY ||
                playerPosY >= shopLocY - 1 && playerPosY <= shopLocY + 1 && playerPosX == shopLocX)
            {
                isHereShop = true;
            }

            return isHereShop;
        }
        public bool CheckThereIsMonster()
        {
            bool isThereMonster = false;

            int playerPosX = Player.instance.locX - this.startXLoc;
            int playerPosY = Player.instance.locY - this.startYLoc;

            if(mapInfo[playerPosX, playerPosY] == (char)TileType.GRASS ||
                mapInfo[playerPosX, playerPosY] == (char)TileType.RECTTILE)
            {
                int appearValue = random.Next(100);

                if (appearValue < 2)
                {
                    isThereMonster = true;
                }
            }

            return isThereMonster;
        }
        public void WaitPlayerInput()
        {
            lastPlayerLocX = Player.instance.locX;
            lastPlayerLocY = Player.instance.locY;
            UpdatePlayerLoc();
            Screen.Print(printInfo);

            while (Player.instance.isWaitingInput)
            {
                ConsoleKeyInfo keyInfo;
                keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            Player.instance.locX--;
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            Player.instance.locX++;
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            Player.instance.locY--;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            Player.instance.locY++;
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            GameInfo gameInfo = new GameInfo();
                            gameInfo.ChooseInfoMenu();
                            return;
                        }
                    case ConsoleKey.Spacebar:
                        {
                            if (CheckHereIsShop() == true)
                            {
                                shop.ShopAct();
                            }
                            if(CheckHereIsMonsterField() == true)
                            {
                                Player.instance.isInBattle = true;
                                return;
                            }
                            break;
                        }
                }
                // 맵 밖으로 넘어갔으면 true
                if (UpdatePlayerLoc() == true)
                {
                    return;
                }
                else
                {
                    if (CheckThereIsMonster() == true)
                    {
                        Player.instance.isInBattle = true;
                        return;
                    }
                }
                Screen.Print(printInfo);
            }
        }

    }
}
