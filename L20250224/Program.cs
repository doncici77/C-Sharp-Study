namespace L20250224
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
