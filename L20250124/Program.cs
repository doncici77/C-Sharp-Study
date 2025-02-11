using System.Numerics;

namespace L20250124
{
    class Color
    {
        public int R;
        public int G;
        public int B;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            char wall = '*';
            char floor = ' ';
            int playerX = 1;
            int playerY = 1;


            //class, OOP
            Color[,] picture = new Color[240, 640];


            int[,] map =
                {
                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
                };

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                Console.Clear();

                //process update
                if (keyInfo.Key == ConsoleKey.W)
                {
                    playerY--;
                }
                else if (keyInfo.Key == ConsoleKey.S)
                {
                    playerY++;
                }
                else if (keyInfo.Key == ConsoleKey.A)
                {
                    playerX--;
                }
                else if (keyInfo.Key == ConsoleKey.D)
                {
                    playerX++;
                }

                //Draw Map -> Render
                for (int y = 0; y < 10; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        if (x == playerX && y == playerY)
                        {
                            Console.Write('P');
                        }
                        else if (x == 7 && y == 7)
                        {
                            Console.Write('M');
                        }
                        else if (map[y, x] == 1)
                        {
                            Console.Write(wall);
                        }
                        else if (map[y, x] == 0)
                        {
                            Console.Write(floor);
                        }
                    }
                    Console.Write("\n");
                }
            }
        }
    }
}

#region 2025-01-24 필기내용
// int // -2^31 ~ 0 ~ 2^31-1 // 4byte = 8비트 * 4 = 32비트
// unsigned int // -부호가 없어져서 크기가 2^32-1 까지 가능
// byte = 1바이트, short = 2바이트, int = 4바이트, long = 8바이트
//"" 이전에 $를 넣으면 문자열에서 {}괄호안에 변수를 삽입가능

//논리연산자 논리합: AND -> &&, 논리곱: OR -> ||, NOT -> !

//array 배열
// 자료형[] 변수명 = new 자료형[개수]

//for문
//for(변수 선언 혹은 초기화; 조건문; 증감문)
//{
//}
//배열 탐색을 위한 문법

#endregion
