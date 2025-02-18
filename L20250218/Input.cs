using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250218
{
    public class Input
    {
        public Input() { }

        static protected ConsoleKeyInfo keyInfo; // 키의 문자와 Shift, Alt, Ctrl 보조키 상태도 포함 

        static public void Process()
        {
            keyInfo = Console.ReadKey(); // 사용자가 누른 키 한 문자 정보를 리턴
        }

        static public bool GetDownKey(ConsoleKey key) // 키가 눌렸는지 확인
        {
            return keyInfo.Key == key; // 눌린 키가 key와 같은지 확인
        }
    }
}
