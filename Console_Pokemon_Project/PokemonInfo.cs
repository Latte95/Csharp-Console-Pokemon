﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    public static class PokemonInfo
    {
        public static List<Pokemon> pokemon = new List<Pokemon>();
        static PokemonInfo()
        {
            Random random = new Random();
            int ranHp = random.Next(1, 10);
            int ranAtt = random.Next(1, 5);
            int ranDef = random.Next(1, 3);
            int ranSpeed = random.Next(1, 10);
            int ranLevel = random.Next(1, 3);


            int mon1Level = 5 + ranLevel;
            int mon2Level = 5 + ranLevel;
            int mon3Level = 5 + ranLevel;
            int mon4Level = 5 + ranLevel;
            int mon5Level = 7 + ranLevel;
            int mon6Level = 9 + ranLevel;
            int mon7Level = 12 + ranLevel;
            int mon8Level = 8 + ranLevel;
            int mon9Level = 11 + ranLevel;
            int mon10Level = 12 + ranLevel;

            //Pokemon[] pokemon = .Add[10]; // 개체마다 기본스텟+랜덤개체값스텟+레벨비례스텟 을 부여함 
            //pokemon[0] = .Add("피카츄", 35 + ranHp + mon1Level * 3, 55 + ranAtt + mon1Level * 2, 40 + ranDef + mon1Level, 90 + ranSpeed + mon1Level * 2, 30 + level * 2, 120 + mon1Level * 2, 15, 3, mon1Level);
            pokemon.Add("꼬부기", 44 + ranHp + mon2Level * 3, 48 + ranAtt + mon2Level * 2, 65 + ranDef + mon2Level, 43 + ranSpeed + mon2Level * 2, 25 + level * 2, 100 + mon2Level * 2, 15, 3, mon2Level);
            pokemon.Add("파이리", 39 + ranHp + mon3Level * 3, 52 + ranAtt + mon3Level * 2, 43 + ranDef + mon3Level, 65 + ranSpeed + mon3Level * 2, 25 + level * 2, 100 + mon3Level * 2, 15, 3, mon3Level);
            pokemon.Add("이상해씨", 45 + ranHp + mon4Level * 3, 49 + ranAtt + mon4Level * 2, 49 + ranDef + mon4Level, 45 + ranSpeed + mon4Level * 2, 25 + level * 2, 100 + mon4Level * 2, 15, 3, mon4Level);
            pokemon.Add("나옹", 40 + ranHp + mon5Level * 3, 45 + ranAtt + mon5Level * 2, 35 + ranDef + mon5Level, 90 + ranSpeed + mon5Level * 2, 35 + level * 2, 125 + mon5Level * 2, 15, 3, mon5Level);
            pokemon.Add("푸린", 115 + ranHp + mon6Level * 3, 45 + ranAtt + mon6Level * 2, 20 + ranDef + mon6Level, 20 + ranSpeed + mon6Level * 2, 65 + level * 2, 250 + mon6Level * 2, 15, 3, mon6Level);
            pokemon.Add("괴력몬", 90 + ranHp + mon7Level * 3, 130 + ranAtt + mon7Level * 2, 80 + ranDef + mon7Level, 55 + ranSpeed + mon7Level * 2, 70 + level * 2, 300 + mon7Level * 2, 15, 3, mon7Level);
            pokemon.Add("포니타", 50 + ranHp + mon8Level * 3, 85 + ranAtt + mon8Level * 2, 55 + ranDef + mon8Level, 90 + ranSpeed + mon8Level * 2, 45 + level * 2, 150 + mon8Level * 2, 15, 3, mon8Level);
            pokemon.Add("팬텀", 60 + ranHp + mon9Level * 3, 65 + ranAtt + mon9Level * 2, 60 + ranDef + mon9Level, 110 + ranSpeed + mon9Level * 2, 60 + level * 2, 230 + mon9Level * 2, 15, 3, mon9Level);
            pokemon.Add("아쿠스타", 60 + ranHp + mon10Level * 3, 75 + ranAtt + mon10Level * 2, 85 + ranDef + mon10Level, 115 + ranSpeed + mon10Level * 2, 70 + level * 2, 300 + mon10Level * 2, 15, 3, mon10Level);

        }

    }
}
