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
            PokemonSkills.Add(new Skill("아이언테일", 100, 75, 15));
            PokemonSkills.Add(new Skill("번개", 110, 70, 10));
            PokemonSkills.Add(new Skill("잎날가르기", 55, 95, 25));
            PokemonSkills.Add(new Skill("볼부비부비", 20, 100, 20));
            PokemonSkills.Add(new Skill("덩굴채찍", 45, 100, 25));
            PokemonSkills.Add(new Skill("매지컬리프", 60, 100, 20));
            PokemonSkills.Add(new Skill("기가드레인", 75, 100, 10));
            PokemonSkills.Add(new Skill("할퀴기", 40, 100, 35));
            PokemonSkills.Add(new Skill("불꽃세례", 40, 100, 25));
            PokemonSkills.Add(new Skill("불꽃엄니", 65, 95, 15));
            PokemonSkills.Add(new Skill("화염방사", 90, 100, 15));
            PokemonSkills.Add(new Skill("물대포", 40, 100, 25));
            PokemonSkills.Add(new Skill("물기", 60, 100, 25));
            PokemonSkills.Add(new Skill("하이드로펌프", 110, 80, 5));
            PokemonSkills.Add(new Skill("고양이돈받기", 40, 100, 20));
            PokemonSkills.Add(new Skill("치근거리기", 90, 90, 10));
            PokemonSkills.Add(new Skill("막치기", 40, 100, 35));
            PokemonSkills.Add(new Skill("스톤샤워", 75, 90, 10));
            PokemonSkills.Add(new Skill("깨트리다", 75, 100, 15));
            PokemonSkills.Add(new Skill("암석봉인", 60, 95, 15));
            PokemonSkills.Add(new Skill("화염자동차", 60, 100, 25));
            PokemonSkills.Add(new Skill("짓밟기", 65, 100, 20));
            PokemonSkills.Add(new Skill("섀도볼", 80, 100, 15));
            PokemonSkills.Add(new Skill("악의파동", 80, 100, 15));
            PokemonSkills.Add(new Skill("핥기", 30, 100, 30));
            PokemonSkills.Add(new Skill("섀도클로", 70, 100, 15));
            PokemonSkills.Add(new Skill("환상빔", 65, 100, 20));
            PokemonSkills.Add(new Skill("사이코키네시스", 90, 100, 10));
            PokemonSkills.Add(new Skill("파도타기", 90, 100, 15));

            /*a 
         * 0.10만볼트 
         * 1.아이언테일
         * 2.번개
         * 3.볼부비부비
         * 4.잎날가르기
         * 5.덩굴채찍
         * 6.매지컬리프
         * 7.기가드레인
         * 8,할퀴기
         * 9.불꽃세례
         * 10.불꽃엄니
         * 11.화염방사
         * 12.물대포
         * 13.물기
         * 14.하이드로펌프
         * 15.고양이돈받기
         * 16.치근거리기
         * 17.막치기
         * 18.스톤샤워
         * 19.깨트리다
         * 20.암석봉인
         * 21.화염자동차
         * 22.짓밟기
         * 23.섀도볼
         * 24.악의파동
         * 25.핥기
         * 26.섀도클로
         * 27.물의파동
         * 28.사이코키네시스
         * 29.파도타기
         */
            // 피카츄 = 0,1,2,3 파이리 = 8,9,10,11 꼬부기 = 8,12,13,14 이상해씨 = 4,5,6,7 나옹 = 8,13,15,16 
            // 푸린 = 16,17,11,0 괴력몬 = 18,19,20,21  포니타 = 10,11,21,22 팬텀 = 23,24,25,26 아쿠스타 =14,27,28,29
        }

    }
}
