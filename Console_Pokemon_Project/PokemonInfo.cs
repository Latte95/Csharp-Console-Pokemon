using System;
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
            int mon1Level = 5;
            int mon2Level = 5;
            int mon3Level = 5;
            int mon4Level = 5;
            int mon5Level = 7;
            int mon6Level = 9;
            int mon7Level = 12;
            int mon8Level = 8;
            int mon9Level = 11;
            int mon10Level = 12;

            //Pokemon[] pokemon = .Add[10]; // 개체마다 기본스텟+랜덤개체값스텟+레벨비례스텟 을 부여함 
            pokemon.Add(new Pokemon("피카츄", 35, 55, 40, 90, 30, 120, 15, 3, mon1Level));
            pokemon.Add(new Pokemon("꼬부기", 44, 48, 65, 43, 25, 100, 15, 3, mon2Level));
            pokemon.Add(new Pokemon("파이리", 39, 52, 43, 65, 25, 100, 15, 3, mon3Level));
            pokemon.Add(new Pokemon("이상해씨", 45, 49, 49, 45, 25, 100, 15, 3, mon4Level));
            pokemon.Add(new Pokemon("나옹", 40, 45, 35, 90, 35, 125, 15, 3, mon5Level));
            pokemon.Add(new Pokemon("푸린", 115, 45, 20, 20, 65, 250, 15, 3, mon6Level));
            pokemon.Add(new Pokemon("괴력몬", 90, 130, 80, 55, 70, 300, 15, 3, mon7Level));
            pokemon.Add(new Pokemon("포니타", 50, 85, 55, 90, 45, 150, 15, 3, mon8Level));
            pokemon.Add(new Pokemon("팬텀", 60, 65, 60, 110, 60, 230, 15, 3, mon9Level));
            pokemon.Add(new Pokemon("아쿠스타", 60, 75, 85, 115, 70, 300, 15, 3, mon10Level));
        }

    }
}
