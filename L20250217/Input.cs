﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250217
{
    public class Input
    {
        public Input()
        {

        }

        static public void Process()
        {
            if(Console.KeyAvailable) // 키 입력이 있을때 true
            {
                keyInfo = Console.ReadKey(true);
            }
        }

        static protected ConsoleKeyInfo keyInfo; // 키보드 입력을 받기 위한 변수

        static public bool GetKeyDown(ConsoleKey key)
        {
            return (keyInfo.Key == key);
        }

        public static void ClearInput()
        {
            keyInfo = new ConsoleKeyInfo();
        }
    }
}
