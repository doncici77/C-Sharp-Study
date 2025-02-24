namespace L20250224
{
    internal class Program
    {
        class BitArray32
        {
            public uint Data;

            public void On(int position)
            {
                if(position > 0 && position <= 32)
                {
                    Data = Data | (uint)(1 << (position - 1));
                }
            }

            public void Off(int position)
            {
                if (position > 0 && position <= 32)
                {
                    Data = Data & ~(uint)(1 << (position - 1));
                }
            }

            public bool Check(uint other)
            {
                 return (int)(Data & other) > 0 ? true : false;
            }
        }

        static void Main(string[] args)
        {
            BitArray32 bitArray = new BitArray32();
            BitArray32 bitArray2 = new BitArray32();
            bitArray.On(3); // 비트 자리수
            bitArray.On(1);
            //0101 = 5
            Console.WriteLine(bitArray.Data);
            Console.WriteLine(Convert.ToString(bitArray.Data, 2));

            bitArray.Off(3);
            Console.WriteLine(bitArray.Data);
            Console.WriteLine(Convert.ToString(bitArray.Data, 2));

            bitArray.Check(bitArray2.Data);
            Console.WriteLine(bitArray.Check(bitArray2.Data).ToString());

            // << shift 연산자
            // >>

            // 0101
            // 0110 & (논리곱), and
            // 0100

            // 0011
            // 0101 | (논리합)
            // 0111

            // 0001 ~ 부정
            // 1110

            // 0101
            // 0011 ^ XOR
            // 0110

            // 0000 0000 -> 16진수
            // F    F   --> color
            // 0xFF
            // 255

            // 1111

            byte Player = 1; // => 0b0000 0001
            byte Camera = 2; // => 0b0000 0010
            byte UI =     4; // => 0b0000 0100
            byte Water =  8; // => 0b0000 1000

            byte layer = 0x00000000;
            layer = (byte)(layer | Player);

            // bit masking
            if((layer & (Camera | Player)) > 0)
            {
                    
            }

            int R = 255;
            R = 0xFF;
            R = 0b11111111;
            int a = 0; // ==> 0000 0000 // 1바이트 = 8비트
            Console.WriteLine(a);
            a = 256 >> 1; // ==> 0000 0010 // 2의 거듭 제곱
            Console.WriteLine(Convert.ToString(a , 2));
            Console.WriteLine(a);
        }
    }
}
