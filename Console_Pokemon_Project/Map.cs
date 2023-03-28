using System;
using System.Collections.Generic;
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
            WALL = '■',
            PLAYER = '●'
        }

        public const int MAP_WIDTH = 40;
        public const int MAP_HEIGHT = 45;
        

        public char[,] mapInfo = new char[MAP_WIDTH, MAP_HEIGHT]; // screen으로 출력할 맵 캐릭터 배열
        public readonly int startXLoc;
        public readonly int startYLoc;
        private int lastPlayerLocX;
        private int lastPlayerLocY;

        // 해당 Map의 위치 설정
        public Map(int startXLoc, int startYLoc)
        {
            this.startXLoc = startXLoc;
            this.startYLoc = startYLoc;
            InitMap();
        }

        public void InitMap()
        {
            for (int i = 0; i < MAP_WIDTH; i++)
            {
                for (int j = 0; j < MAP_HEIGHT; j++)
                {
                    mapInfo[i, j] = (char)TileType.GROUND;
                }
            }
            for (int i=0; i<MAP_WIDTH;i++)
            {
                mapInfo[i, 0] = (char)TileType.WALL;
                mapInfo[i, MAP_HEIGHT - 1] = (char)TileType.WALL;
            }
            for(int i=0; i<MAP_HEIGHT; i++)
            {
                mapInfo[0, i] = (char)TileType.WALL;
                mapInfo[MAP_WIDTH-1, i] = (char)TileType.WALL;
            }

            lastPlayerLocX = Player.instance.locX;
            lastPlayerLocY = Player.instance.locY;

            // 플레이어 위치
            UpdatePlayerLoc();
            
            Screen.print(mapInfo, MAP_WIDTH);
        }
        public bool UpdatePlayerLoc()
        {
            
            int playerPosX = Player.instance.locX - this.startXLoc;
            int playerPosY = Player.instance.locY - this.startYLoc;

            // 플레이어가 해당 맵에 없으면 종료
            if (playerPosX >= MAP_WIDTH || playerPosX < 0 ||
                playerPosY >= MAP_HEIGHT || playerPosY < 0)
            {
                return true;
            }
            mapInfo[lastPlayerLocX, lastPlayerLocY] = (char)TileType.GROUND;
            mapInfo[playerPosX, playerPosY] = (char)TileType.PLAYER;

            lastPlayerLocX = Player.instance.locX;
            lastPlayerLocY = Player.instance.locY;
            return false;
        }
        public void WaitPlayerInput()
        {
            while (Player.instance.isWaitingInput)
            {
                ConsoleKeyInfo keyInfo;
                keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        Player.instance.locX--;
                        break;
                    case ConsoleKey.RightArrow:
                        Player.instance.locX++;
                        break;
                    case ConsoleKey.UpArrow:
                        Player.instance.locY--;
                        break;
                    case ConsoleKey.DownArrow:
                        Player.instance.locY++;
                        break;
                    case ConsoleKey.Escape:
                        Player.instance.isWaitingInput = false;
                        break;
                }
                if(UpdatePlayerLoc() == true)
                {
                    return;
                }
                Screen.print(mapInfo, MAP_WIDTH);
            }
        }

    }
}
