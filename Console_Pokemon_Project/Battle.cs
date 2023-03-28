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
    }
}
