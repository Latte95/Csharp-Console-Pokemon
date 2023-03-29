using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    public class Skill
    {
        // 계산 없이
        // 계수
        // 명중률 같은 수치만 설정

        public string Name { get; set; }
        public int Power { get; set; }
        public int Hitrate { get; set; }
        public int Pp { get; set; }

        public Skill(string name, int power, int hitrate, int pp)
        {

        }
            public static List<Skill> PokemonSkills = new List<Skill>();

    }



        









    
}
