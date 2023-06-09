﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Console_Pokemon_Project
{
    public class DataManager
    {
        // 암호화 구현하려다 미뤄진 변수들...
        private static int keySizes = 32;
        private byte[] key;
        private static readonly byte[] salt = new byte[] { 0x26, 0x19, 0x36, 0x29, 0x3F, 0x10, 0x01, 0x1A };
        private const int Iterations = 10000;

        // 불러오기
        public static Player Load()
        {
            // 파일 저장 위치
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\.\JSON\" + "saveData.json"));
            
                // 파일이 존재하면
                if (File.Exists(path))
                {
                    // json 파일을 읽어와서
                    string jsonFromFile = File.ReadAllText(path);
                    // 직렬화 된 jsonFromFile를 역직렬화하여 플레이어 객체로 저장
                    Player deserializedPlayer = JsonConvert.DeserializeObject<Player>(jsonFromFile);
                    // 이를 현재 player 데이터로 덮씌움
                    Player.instance.UpdateProperties(deserializedPlayer);
                }
            
            return Player.instance;
        }

        // 저장하기
        public static void Save(Player data)
        {           
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\.\JSON\"));
            // 해당 경로가 없으면 새로 생성
            Directory.CreateDirectory(Path.GetDirectoryName(path+ "saveData.json"));

            string playerJson = JsonConvert.SerializeObject(Player.instance, Formatting.Indented);
            File.WriteAllText(path + "saveData.json", playerJson);
        }
    }
}
