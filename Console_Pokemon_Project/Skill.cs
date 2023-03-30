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

        public string name { get; set; }
        public int power { get; set; }
        public int hitrate { get; set; }
        public int pp { get; set; }
        public int maxPp { get; set; }

        public Skill(string name, int power, int hitrate, int pp , int maxPp)
        {
            this.name = name;
            this.power = power;
            this.hitrate = hitrate;
            this.pp = pp;
            this.maxPp = maxPp;
        }
            public static List<Skill> PokemonSkills = new List<Skill>();

    }



        









    
}
