using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
    class SkillInfo
    {
        public static List<Skill> PokemonSkills = new List<Skill>();

        static SkillInfo()
        {
            PokemonSkills.Add(new Skill("10만볼트", 90, 100, 15));
            PokemonSkills.Add(new Skill("아이언테일", 90, 100, 15));
            PokemonSkills.Add(new Skill("번개", 90, 100, 15));
            PokemonSkills.Add(new Skill("볼부비부비", 90, 100, 15));
            /*
            new Skill("10만볼트", 90, 100, 15), 
            new Skill("아이언테일", 100, 75, 15),
            new Skill("번개", 110, 70, 10),
            new Skill("볼부비부비", 20, 100, 20),
            new Skill("잎날가르기", 55, 95, 25),
            new Skill("덩굴채찍", 45, 100, 25),
            new Skill("매지컬리프", 60, 100, 20), //필중기
            new Skill("기가드레인", 75, 100, 10), //데미지절반 피흡
            new Skill("할퀴기", 40, 100, 35), 
            new Skill("불꽃세례", 40, 100, 25), 
            new Skill("불꽃엄니", 65, 95, 15), 
            new Skill("화염방사", 90, 100, 15), 
            new Skill("물대포", 40, 100, 25), 
            new Skill("물기", 60, 100, 25), 
            new Skill("하이드로펌프", 110, 80, 5), 
            new Skill("고양이돈받기", 40, 100, 20), 
            new Skill("치근거리기", 90, 90, 10), 
            new Skill("막치기", 110, 80, 5), 
            new Skill("스톤샤워", 75, 90, 10), 
            new Skill("깨트리다", 75, 100, 15), 
            new Skill("암석봉인", 60, 95, 15), 
            new Skill("화염자동차", 60, 100, 25), 
            new Skill("짓밟기", 65, 100, 20), 
            new Skill("섀도볼", 80, 100, 15), 
            new Skill("악의파동", 80, 100, 15), 
            new Skill("핥기", 30, 100, 30), 
            new Skill("섀도클로", 70, 100, 15), 
            new Skill("환상빔", 65, 100, 20), 
            new Skill("사이코키네시스", 90, 100, 10), 
            new Skill("파도타기", 90, 100, 15), 
            */
        }

    }
}
