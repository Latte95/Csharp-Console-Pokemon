﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Console_Pokemon_Project
{
    public class Pokemon
    {
        public const int GRAPHIC_WIDTH = 24;
        public const int GRAPHIC_HEIGHT = 24;

        // 하나의 생성자로 이름값에 따라 스탯 별개
        public string name;
        public int hp;
        public int att;
        public int def;
        public int speed;
        public int exp;
        public int dropgold;
        public int critical;
        public int avoidence;
        public int level;
        public char[] characterDisplayInfo;
        //public int special_Attack;
        //public int sepcial_Defence;
        List<Skill> skill = Skill.PokemonSkills;


        public Pokemon(string name, int hp, int att, int def, int speed, int exp, int dropgold, int cri, int avoid, int level)
        {
            this.name = name;
            this.hp = hp;
            this.att = att;
            this.def = def;
            this.speed = speed;
            this.exp = exp;
            this.dropgold = dropgold;
            this.critical = cri;
            this.avoidence = avoid;
            this.level = level;
            
        }



        readonly static char[][,] MONSTER_DISPLAY_INFO_LIST = new char[][,]
        {

            new char[,]
            {
                    {'=','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','=','-',' ',' '},
                    {'-',' ','#','-','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','=','.',' ','.'},
                    {'#',' ','=','=','-','#','#','#','#','#','#','#','#','#','#','#','#','#','#','-','=','-',' ','#'},
                    {'#','.','-','=','=','-','#','#','#','#','#','#','#','#','#','#','#','#','-','=','=','.',' ','#'},
                    {'#','#','.','=','=','=','-','#','#','#','#','#','#','#','#','#','#','-','=','=','=',' ','#','#'},
                    {'#','#','=','=','=','=','=','#','#','#','#','#','#','#','#','#','-','=','=','=','=','.','#','#'},
                    {'#','#','#','-','=','=','=','-','#','=','-','-','-','=','=','=','=','=','=','=','-','#','#','='},
                    {'#','#','#','#','-','=','=','=','-','=','=','=','=','=','=','-','=','=','=','-','#','=','-','='},
                    {'#','#','#','#','#','-','=','=','=','=','=','=','=','=','=','=','=','=','=','#','-','=','=','='},
                    {'#','#','#','#','#','#','-','=','=','=','=','=','=','=','=','=','=','=','=','-','=','=','=','='},
                    {'#','#','#','#','#','#','-','=','=','=','=','=','=','=','=','=','=','-','=','=','=','=','-','-'},
                    {'#','#','#','#','#','#','=','=','=','=','=','=','=','=','=','=','=','-','=','=','=','=','=','='},
                    {'#','#','#','#','#','#','=','-','-','=','=','=','=','=','=','.','=','-','=','=','=','=','=','-'},
                    {'#','#','#','#','#','#','=',' ','#','-','=','=','=','=','=','-','.','-','=','=','=','=','=','='},
                    {'#','#','#','#','#','#','=','.',' ','-','=','=','=','=','.',' ','.','-','=','=','=','=','=','-'},
                    {'#','#','#','#','#','=','#','-','.','=','=','-','-','=','=','.','=','=','-','=','=','-','=','#'},
                    {'#','#','#','#','#','=','.','=','=','=','=','=','=','=','=','=','=','-','-','=','=','#','#','#'},
                    {'#','#','#','#','#','-','-','-','=','=','-','-','-','-','=','=','-','-','-','=','-','#','#','#'},
                    {'#','#','#','#','#','=','-','-','=','=','=','=','=','=','=','-','-','-','-','=','=','=','#','#'},
                    {'#','#','#','#','#','#','-','-','=','=','=','-','=','=','=','=','-','-','-','=','=','-','#','#'},
                    {'#','#','#','#','#','#','-','=','=','=','=','-','=','=','=','=','=','-','#','-','=','=','-','#'},
                    {'#','#','#','#','#','#','=','-','=','=','=','=','=','=','=','=','-','=','=','=','=','-','=','#'},
                    {'#','#','#','#','#','#','=','=','-','=','=','=','=','=','-','-','=','-','=','=','=','#','#','#'},
                    {'#','#','#','#','#','#','-','=','=','=','-','-','-','=','=','=','=','-','-','-','#','#','#','#'}
            }
            ,
            new char[,]
            {
                    {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','#','#','=','=','=','=','=','=','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','=','=','=','=','=','=','=','=','=','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','=','=','=','=','=','=','=','=','=','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','=','=','=','=','=','=','-','=','=','=','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','=','.','=','=','=','=','=','-','-','#','=','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','=',' ','=','=','=','#','-',' ','-','#','=','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','=','-','=','#','=','=','=','.','=','=','=','#','#','#','#','#','#','#','#'},
                    {'#','#','=','=','=','=','#','#','=','=','=','#','#','=','=','#','#','=','#','#','#','#','#','#'},
                    {'#','#','=','=','=','=','=','=','=','=','=','=','=','=','#','#','#','.','#','#','#','#','#','#'},
                    {'#','#','#','=','=','=','#','=','=','=','=','=','=','#','=','=','=','=','=','#','#','#','#','#'},
                    {'#','#','#','#','#','=','=','=','#','=','=','#','#','=','=','=','=','=','-','#','=','=','=','#'},
                    {'#','#','#','#','#','#','=','=','=','=','=','=','=','=','=','=','=','=','-','=','#','#','-','='},
                    {'#','#','#','#','#','=','#','=','=','=','=','=','#','=','=','=','-','=','-','=','=','=','-','='},
                    {'#','#','#','#','#','=','=','=','=','=','=','=','=','#','=','=','#','-','-','=','=','-','-','='},
                    {'#','#','#','#','#','=','=','=','=','=','=','=','=','=','#','=','#','-','-','=','-','-','-','#'},
                    {'#','#','#','#','#','#','=','=','=','=','=','=','=','=','=','=','#','-','-','=','-','-','#','#'},
                    {'#','#','#','#','#','#','#','=','=','=','=','=','=','=','#','#','#','#','-','-','-','#','#','#'},
                    {'#','#','#','#','#','=','=','#','=','=','=','=','=','=','=','=','=','#','-','=','#','#','#','#'},
                    {'#','#','#','#','#','=','=','=','=','=','=','#','#','=','=','=','=','=','#','#','#','#','#','#'},
                    {'#','#','#','#','=','=','=','-','=','=','-','-','=','=','=','=','=','#','#','#','#','#','#','#'},
                    {'#','#','#','#','=','=','-','-','=','#','=','=','#','-','=','=','=','#','#','#','#','#','#','#'},
                    {'#','#','#','#','-','-','-','=','#','#','#','#','#','=','-','-','-','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','=','#','#','#','#','#','#','#','#'}
                                 
            },

            new char[,]
            {
                    {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','#','#','=','-','-','-','-','=','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','#','.','=','=','=','=','=','-','=','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','-','=','-','-','=','-','.','=','=','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','=','-','-','-','-','-','=','-','=','=','=','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','=','=','=','=','=','-','-','-','-','-','=','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','=','=','-','-','=','=','=','=','=','-','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','-','-','-','=','=','=','=','=','-','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','=','-','=','-','=','=','=','-','=','=','#','#','#','#','#','-','-','='},
                    {'#','#','#','#','-','-','=','#','#','#','=','-','-','-','-','=','-','=','#','#','#','#','=','-'},
                    {'#','#','#','-','-','-','#','#','#','#','#','=','-','-','=','=','=','-','=','#','#','#','#','-'},
                    {'#','#','=','=','-','=','#','#','#','#','#','#','-','-','-','-','=','=','-','#','#','#','#','.'},
                    {'#','#','#','-','-','#','#','#','#','#','#','#','=','-','-','=','=','-','=','#','#','#','=','#'},
                    {'#','#','#','#','-','#','#','#','#','#','#','#','#','-','-','-','=','#','#','#','#','#','-','#'},
                    {'#','#','#','#','#','#','#','#','#','#','#','#','#','-','=','-','#','#','#','#','#','=','=','#'},
                    {'#','#','#','#','#','#','#','#','#','#','#','#','#','-','=','-','#','#','#','#','=','-','#','#'},
                    {'#','#','#','#','#','#','#','#','#','#','#','#','#','-','-','-','#','#','#','=','=','-','#','#'},
                    {'#','#','#','#','#','#','#','#','#','#','#','#','#','-','=','=','=','=','=','-','=','#','#','#'},
                    {'#','#','#','#','-','-','#','#','#','#','#','#','=','-','=','=','-','=','-','=','#','#','#','#'},
                    {'#','#','#','=','=','=','-','-','-','=','=','=','.','.','-','=','=','-','=','#','#','#','#','#'},
                    {'#','#','#','#','-','-','.','.','-','#','#','=','-','.','.','-','=','=','#','#','#','#','#','#'},
                    {'#','#','#','#','-','=','=','.','#','#','#','#','#','=','-','=','=','=','#','#','#','#','#','#'},
                    {'#','#','=','-','=','=','=','.','#','#','#','#','#','=','=','=','=','-','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','=','#','=','#','#','#','#','#','#'}

            },
            new char[,]
            {

                    {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','=','-','-','#','#','#'},
                    {'#','#','#','#','#','#','#','#','#','#','#','#','=','=','-','=','-','=','=','=','=','-','#','#'},
                    {'#','#','#','#','#','#','#','#','#','#','=','=','=','=','=','=','=','=','=','=','-','=','#','#'},
                    {'#','#','#','#','#','#','#','#','#','-','=','=','=','-','#','=','=','=','=','=','-','=','#','#'},
                    {'#','#','#','=','=','-','#','=','=','=','=','=','=','=','-','=','=','=','=','=','-','-','#','#'},
                    {'#','#','=','=','#','#','#','=','=','=','=','#','#','#','=','=','=','=','=','=','-','=','#','#'},
                    {'#','#','-','#','#','=','-','-','-','=','=','=','=','=','-','=','=','=','=','-','-','-','-','#'},
                    {'#','#','=','#','#','-','-','-','=','=','=','=','=','=','=','-','=','-','-','-','=','-','=','#'},
                    {'#','=','=','=','-','#','=','=','=','=','#','=','#','=','=','-','-','-','-','-','-','-','=','='},
                    {'#','#','-','=','=','#','-','=','=','#','=','-','-','=','=','-','-','=','-','-','=','-','=','-'},
                    {'=','#','=','=','#','-','.','#','=','=','#','#','-','=','=','-','-','-','=','=','-','=','=','-'},
                    {'#','#','#','#','=','=','-','#','=','#','=','#','=','=','=','=','-','-','-','-','-','-','=','-'},
                    {'=','=','=','#','=','=','=','=','=','#','#','-','-','=','=','=','=','-','-','-','-','=','=','='},
                    {'-','=','#','#','=','=','=','=','=','=','#','=','=','=','=','=','=','-','-','-','-','-','-','#'},
                    {'#','-','-','=','#','=','#','#','=','=','=','-','-','=','=','=','=','=','=','=','=','-','=','#'},
                    {'#','#','=','-','-','=','=','-','-','-','-','=','=','=','-','=','=','=','=','=','=','-','#','#'},
                    {'#','#','#','-','-','=','=','=','=','-','=','=','-','=','#','=','#','-','=','=','-','=','=','#'},
                    {'#','#','#','=','=','-','-','-','-','-','-','-','-','#','-','#','=','-','=','-','-','=','=','#'},
                    {'#','#','#','=','-','-','=','-','=','-','-','-','=','=','-','#','=','=','=','-','-','=','=','#'},
                    {'#','#','#','=','=','-','=','=','-','=','-','-','=','-','#','=','=','-','=','-','-','=','=','#'},
                    {'#','#','#','#','-','=','=','=','#','#','=','-','-','=','#','=','-','-','=','=','=','=','=','#'},
                    {'#','#','#','#','-','=','=','=','#','#','#','=','=','#','=','-','#','#','-','=','=','-','#','#'},
                    {'#','#','#','#','#','#','#','#','#','#','#','=','=','=','=','#','#','#','#','=','=','#','#','#'}
            },
            
            new char[,]
            {

                    {'#','#','#','#','#','#','#','#','#','#','#','#','#','=','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','.','#','#','-','#','#','#','#','-',' ','-','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','-','.',' ','=','=','=','=','=','.','.','-','.','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#',' ','=','=',' ','.','=','=','-',' ','#','=',' ','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#',' ','#','=','#','#','-','=','-','#','=','-',' ','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#',' ','-','#','#','#','-','-','#','#','#','#',' ','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','-','#','#','#','#','#','=','#','=','#','#','=','=','-','=','=','=','#','#'},
                    {'#','=','=','=','-','#','#','#','-','#','#','#','#','=','#','#','#','#','#','=','=','#','#','#'},
                    {'#','#','#','#','#','#','#','#','#','#','#','#','=','=','#','#','#','-','-','-','.','-','#','#'},
                    {'#','#','=','-','-','=','#','#','#','#','#','#','#','#','#','#','#','=','-','-','-','.','=','#'},
                    {'#','=','#','=','#','=','#','#','=','#','#','#','#','=','=','#','=','#','#','=','=','-','=','#'},
                    {'#','#','#','-','=','#','-','#','#','#','#','#','#','#','#','#','=','=','=','=','-','=','=','#'},
                    {'#','#','#','#','=','=','#','#','=','#','#','#','#','=','-','#','#','#','#','#','-','=','=','#'},
                    {'#','#','#','#','#','#','#','=','#','#','=','=','#','#','#','#','#','=','#','#','-','-','#','#'},
                    {'#','#','#','#','#','#','=','#','=','#','#','#','#','#','=','#','=','#','#','=','=','=','#','#'},
                    {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','=','=','-','#','#','#'},
                    {'#','#','#','#','#','#','#','#','=','#','#','#','#','#','#','#','#','=','#','=','#','#','#','#'},
                    {'#','#','#','#','#','#','#','#','=','#','#','#','#','#','=','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','#','#','#','#','=','=','=','#','=','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','#','#','=','=','#','#','#','=','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','#','#','=','=','#','#','#','=','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','-','.','.','-','-','#','#','=','#','=','#','#','.','-','-','.','-','#','#','#','#'},
                    {'#','#','#','-','.','-','-','-','-','=','=','#','=','=','-','-','-','-','.','-','#','#','#','#'},
                    {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'}
            },

            new char[,]
            {

                    {'#','#','#','#','#','#','=','=','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','=','=','=','=','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','.',' ','#','=','=','#','#','=','=','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','=',' ',' ','-','#','=','=','=','#','=','=','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','=','.',' ','-','#','=','#','#','=','#','#','=','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','=','-','=','#','=','#','#','#','#','#','=','=','=','#','#','#','#','#','#'},
                    {'#','#','#','#','#','=','#','#','#','=','#','=','=','=','=','=','=','=','=','=','#','#','#','#'},
                    {'#','#','#','#','=','#','#','#','=','=','=','=','#','#','#','#','=','=','=','#','=','=','=','#'},
                    {'#','#','#','#','=','=','=','#','=','=','=','=','=','=','=','=','#','=','#','-','-','=','=','='},
                    {'#','#','#','=','=','=','=','.','=','=','#','#','=','=','=','#','=','=','#','.',' ',' ','.','#'},
                    {'#','#','#','=','=','#','.','.','=','=','=','=','=','=','#','=','=','#','=','=','.','.','=','#'},
                    {'#','#','#','=','=','=','-','-','.','=','=','=','=','=','#','=','=','=','=','=','.','=','=','#'},
                    {'#','#','#','=','=','#','=','=','=','=','=','=','=','#','-','-','=','=','=','=','=','=','#','#'},
                    {'#','#','=','=','#','=','=','=','=','=','=','=','=','=','.','.','-','=','#','=','=','#','#','#'},
                    {'#','=','#','=','=','#','=','=','=','=','=','=','=','=','-','.','.','=','=','=','#','#','#','#'},
                    {'#','#','=','=','=','=','=','=','=','=','=','=','=','=','=','=','-','=','=','=','#','#','#','#'},
                    {'#','#','#','=','=','=','=','=','=','=','#','=','=','=','=','=','=','=','=','=','#','#','#','#'},
                    {'#','#','#','#','=','=','=','=','=','=','=','=','=','=','=','=','#','=','=','#','#','#','#','#'},
                    {'#','#','#','#','#','=','=','=','=','=','=','=','=','=','=','=','=','=','=','#','#','#','#','#'},
                    {'#','#','#','#','#','=','=','=','=','=','=','=','=','=','=','#','=','=','#','#','#','#','#','#'},
                    {'#','#','#','#','=','=','=','=','=','=','=','=','=','=','=','=','=','#','#','#','#','#','#','#'},
                    {'#','#','#','=','#','#','=','=','=','=','=','=','=','=','=','=','=','#','#','#','#','#','#','#'},
                    {'#','#','#','#','=','=','=','#','#','#','#','=','#','=','=','#','#','=','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','=','=','=','=','#','#','#','#','#','#'},

            },
            new char[,]
            {
                    {'#','#','#','=','=','=','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','=','=','=','=','=','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','=','=','=','-','=','#','#','#','#','#','#','#','#','#','=','=','=','#','#','#','#','#'},
                    {'#','#','=','-','-','=','#','#','#','#','#','#','#','#','#','=','=','=','=','=','#','#','#','#'},
                    {'#','#','=','=','=','=','#','#','#','#','#','#','#','#','#','=','-','-','-','-','#','#','#','#'},
                    {'#','#','#','=','=','=','=','#','#','#','#','#','#','#','#','#','=','=','=','-','#','#','#','#'},
                    {'#','#','#','=','=','=','-','=','=','#','#','=','=','=','#','#','#','=','=','=','=','#','#','#'},
                    {'#','#','#','#','-','-','-','=','=','-','-','=','=','=','=','#','=','=','=','=','=','#','#','#'},
                    {'#','#','#','#','#','=','=','=','=','=','=','=','=','=','-','=','=','=','=','=','=','#','#','#'},
                    {'#','#','#','=','=','=','=','=','=','=','=','=','=','=','-','-','-','-','=','=','#','#','#','#'},
                    {'#','=','=','=','=','=','=','=','=','=','=','=','=','=','=','=','-','=','#','#','#','#','#','#'},
                    {'#','=','=','=','=','-','=','-','=','=','=','=','=','-','=','=','=','=','#','#','#','#','#','#'},
                    {'#','=','=','=','=','-','-','.','=','=','=','=','-','=','=','=','=','=','=','=','#','#','#','#'},
                    {'#','#','=','=','-','-','=','=','=','=','=','=','=','=','-','=','=','=','=','=','=','#','#','#'},
                    {'#','#','#','=','=','=','=','#','=','-','=','-','=','-','=','#','#','=','=','=','=','=','#','#'},
                    {'#','#','#','#','#','#','#','=','-','-','=','=',' ','#','#','#','#','#','=','=','=','=','=','#'},
                    {'#','#','#','#','#','#','=','=','=','-','.',' ','-','=','=','#','#','#','#','=','=','=','=','='},
                    {'#','#','#','#','#','=','=','=','=','.',' ','.','=','=','=','=','=','#','#','#','=','=','=','='},
                    {'#','#','#','#','=','=','=','=','=','=','#','=','=','=','=','=','=','#','#','#','#','#','#','#'},
                    {'#','#','#','=','=','=','=','=','#','#','#','#','#','.','-','-','#','#','#','#','#','#','#','#'},
                    {'#','#','#','=','=','=','=','#','#','#','#','#','#','-','-','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','=','=','=','#','#','#','#','#','#','=','-','-','-','#','#','#','#','#','#','#','#'},
                    {'#','#','=','=','=','=','#','#','#','#','#','#','#','#','=','=','=','=','#','#','#','#','#','#'},
                    {'#','=','=','=','=','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'}
                    

            },
            new char[,]
            {
                    {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','#','#','#','#','#','#','=','=','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','=','=','=','=','=','=','=','=','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','=','=','=','=','=','=','=','-','=','=','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','=','=','=','=','-','-','=','-','=','#','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','=','=','=','-','=','=','=','=','#','#','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','=','=','=','=','=','-','-','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','=','#','=',' ','=','=','=','=','#','#','#','#','#','#','#','#','#','#','#','=','#'},
                    {'#','#','=','#','=','=','#','=','=','=','-','#','#','#','#','=','=','=','=','=','=','=','=','='},
                    {'#','#','#','=','=','#','=','=','=','=','=','=','=','#','#','#','=','=','=','=','=','=','=','='},
                    {'#','#','#','#','#','#','=','=','=','=','=','=','=','=','=','#','=','=','=','=','=','=','-','#'},
                    {'#','#','#','#','#','#','#','=','=','=','=','=','=','=','=','=','-','=','=','=','=','=','=','#'},
                    {'#','#','#','#','#','#','=','#','=','=','=','=','#','#','#','#','=','-','=','=','=','#','#','#'},
                    {'#','#','#','#','#','#','=','#','#','=','=','=','=','=','=','#','=','=','=','=','=','#','#','#'},
                    {'#','#','#','#','#','#','=','=','#','=','=','=','=','=','#','=','=','=','=','=','#','#','#','#'},
                    {'#','#','#','#','#','#','=','#','=','=','#','=','=','=','=','=','=','=','-','=','#','#','#','#'},
                    {'#','#','#','#','#','#','#','=','=','=','#','=','=','=','=','=','=','=','=','=','#','#','#','#'},
                    {'#','#','#','#','#','#','#','=','=','-','#','=','=','=','#','=','=','=','=','=','#','#','#','#'},
                    {'#','#','#','#','#','#','#','=','=','=','=','=','=','=','#','#','=','=','=','#','#','#','#','#'},
                    {'#','#','#','#','#','#','#','=','=','#','=','=','=','=','#','#','=','=','=','#','#','#','#','#'},
                    {'#','#','#','#','#','#','#','=','=','#','=','=','=','=','#','#','=','=','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','#','=','=','#','#','=','-','#','#','#','=','=','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','=','.','=','#','=','=','#','#','#','=','.','=','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','#','#','#','=','-','=','#','#','#','#','#','#','#','#','#','#','#','#'},

            },
       
            new char[,]
            {
                
                    {'#','#','#','#','=','=','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','=','-','=','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','-','=','=','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','-','=','=','-','#','-','=','#','=','=','.','=','#','#','#','#','#','#','#'},
                    {'#','=','#','#','#','-','-','-','-','-','-','-','-','-','-','=','#','#','#','#','#','#','#','#'},
                    {'=','-','=','#','#','-','-','-','=','=','=','=','=','=','-','-','.','#','#','#','=','=','#','#'},
                    {'=','-','=','=','=','-','-','=','=','=','=','=','=','=','-','-','-','=','=','=','=','=','=','-'},
                    {'#','-','-','=','=','-','=','=','=','=','=','=','=','=','=','-','-','-','=','=','=','-','-','#'},
                    {'#','#','-','-','-','-','=','=','=','=','=','=','=','=','-','-','-','-','-','-','-','=','#','#'},
                    {'#','#','=','-','-','-','-','=','=','=','=','=','=','-','-','-','=','-','-','-','=','#','#','#'},
                    {'#','#','#','-','-','-','=','=','-','-','=','=','-','-','-','-','-','-','-','=','#','#','#','#'},
                    {'#','#','=','-','-','-','-','=','-','-','-','-','-','-','=','-','-','-','=','=','=','=','#','#'},
                    {'#','#','=','-','-','=','-','-','=','-','-','-','=','=','=','-','-','-','-','-','-','-','-','='},
                    {'#','#','=','=','-','#','=','-','-','-','-','-','=','=','-','-','-','-','-','-','-','-','-','='},
                    {'#','#','=','-','-','-','#','#','=','=','-','-','-','-','-','-','-','-','-','-','-','=','=','#'},
                    {'#','#','-','-','-','.','-','=','#','#','=','#','#','#','-','-','-','.','=','=','#','#','#','#'},
                    {'#','=','.','-','.','.','.','.','=','#','=','#','=','-','.','.','.','=','#','#','#','#','#','#'},
                    {'#','=','-','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#','#','#','#','#','#','#'},
                    {'#','=','-','-','.','.','.','.','.','.','.','.','.','-','-','.','=','#','#','#','#','#','#','#'},
                    {'#','#','-','.','.','.','.','.','.','.','.','.','-','-','-','.','#','#','#','#','#','#','#','#'},
                    {'#','#','#','-',' ','-','-','.','.','.','.','-','-','-','-','-','=','#','#','#','#','#','#','#'},
                    {'#','#','#','#','=','#','#','=','.','.','=','-','-','-','-','-','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','#','#','=','-','#','=','.','.','.','=','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','#','#','#','#','#','#','.','.','=','#','#','#','#','#','#','#','#','#'}

            },
             new char[,]
            {
                    {'#','#','#','#','#','#','#','#','#','#','#','#','=','#','#','#','#','#','#','#','#','#','#','#'},
                    {'#','#','#','#','#','#','#','#','#','#','#','=','=','-','#','#','#','#','#','#','=','=','#','#'},
                    {'#','#','#','#','=','=','#','#','#','#','#','=','=','=','#','#','#','#','=','=','=','-','#','#'},
                    {'#','#','#','#','-','=','=','#','#','#','=','=','-','=','=','#','=','=','=','=','-','=','#','#'},
                    {'#','#','#','#','=','=','=','=','=','#','=','=','=','=','=','=','=','=','=','-','-','#','#','#'},
                    {'#','#','#','#','=','-','=','=','=','-','=','=','-','=','=','-','=','=','-','-','=','#','#','#'},
                    {'#','#','#','#','=','-','-','=','=','=','=','#','-','=','=','-','=','-','=','-','#','#','#','#'},
                    {'#','#','#','#','#','-','-','=','-','=','=','#','=','=','-','=','-','-','-','=','#','#','#','#'},
                    {'#','#','#','#','#','=','-','=','=','=','=','=','#','=','=','=','-','-','-','#','#','#','#','#'},
                    {'#','#','#','#','#','=','-','-','=','=','#','=','=','=','=','-','=','=','-','#','#','#','#','#'},
                    {'#','#','#','#','#','=','=','=','=','#','-','=','=','=','=','=','=','=','=','=','#','#','#','#'},
                    {'#','#','#','#','=','=','=','=','#','=','=','#','=','=','=','=','=','=','=','=','=','#','#','#'},
                    {'#','#','#','=','=','=','=','#','=','=','=','=','=','-','=','=','=','=','-','=','=','=','#','#'},
                    {'#','#','=','=','=','=','-','=','-','-','=','=','=','=','-','=','-','-','=','=','=','=','=','#'},
                    {'#','#','-','-','=','=','=','-','=','=','=','=','=','=','=','=','=','=','=','=','=','-','=','#'},
                    {'#','#','#','#','-','-','-','=','#','=','=','=','-','=','=','-','=','-','-','-','-','=','#','#'},
                    {'#','#','#','=','=','-','-','=','=','-','=','=','=','=','=','=','=','=','.','-','=','=','=','#'},
                    {'#','#','=','=','-','-','=','=','-','=','-','=','-','=','=','=','=','=','-','-','-','=','-','#'},
                    {'#','=','=','-','-','-','=','=','=','=','-','=','=','=','=','=','=','=','-','-','-','-','-','='},
                    {'=','-','.','-','=','=','=','=','=','=','-','=','.','=','=','=','=','=','=','=','=','-','.','-'},
                    {'#','#','#','#','#','#','=','=','=','=','-','-','-','-','=','=','=','=','=','#','#','#','#','#'},
                    {'#','#','#','#','#','=','=','=','=','#','=','-','-','=','#','=','=','-','-','#','#','#','#','#'},
                    {'#','#','#','#','#','=','=','-','#','#','#','-','.','#','#','#','=','=','-','#','#','#','#','#'},
                    {'#','#','#','#','#','#','-','=','#','#','#','=','=','#','#','#','#','=','-','#','#','#','#','#'}


            },


        };
       





    }
}
