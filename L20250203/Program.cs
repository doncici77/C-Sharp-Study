using System.Drawing;

namespace L20250203
{
    internal class Program
    {

        static char wall = '*';
        static char floor = ' ';
        static int playerX = 1;
        static int playerY = 1;

        static int[,] map =               {
                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 4, 0, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
            };
        static ConsoleKeyInfo keyInfo;

        static bool IsRunning = true;
        static void Main(string[] args)
        {
            while (IsRunning)
            {
                Input();
                Update();
                Render();
            }

            Console.Clear();
            Console.WriteLine("Game over");
        }

        private static void Render()
        {
            Console.Clear();

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (x == playerX && y == playerY)
                    {
                        Console.Write('P');
                    }
                    else if (map[y, x] == 1)
                    {
                        Console.Write(wall);
                    }
                    else if (map[y, x] == 0)
                    {
                        Console.Write(floor);
                    }
                    else if (map[y, x] == 4)
                    {
                        Console.Write('M');
                    }
                }
                Console.Write('\n');
            }
        }

        private static void Update()
        {
            if (keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow)
            {
                playerY--;
            }
            else if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow)
            {
                playerY++;
            }
            else if (keyInfo.Key == ConsoleKey.A || keyInfo.Key == ConsoleKey.LeftArrow)
            {
                playerX--;
            }
            else if (keyInfo.Key == ConsoleKey.D || keyInfo.Key == ConsoleKey.RightArrow)
            {
                playerX++;
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                IsRunning = false;
            }
        }

        private static void Input()
        {
            keyInfo = Console.ReadKey();
        }
    }

    /*//매개변수, 오버로딩

    //반환형 함수명(자료형 인자1, 자료형 인자2, ...)
    //{
    //  return 자료반환;
    //}

    static float Multiply(float number, float number2)
    {
        return number * number2;
    }

    static int Multiply(int number, int number2)
    {
        return number * number2;
    }

    static void Main(string[] args)
    {
        string[] input = Console.ReadLine().Split();
        int a = 0; // 변수 선언할때 초기화 무조건 해주는 것이 좋음 
        int b = 0; // 변수 선언할때 ',' 쓰지말고 따로따로 선언하는 것이 좋음

        a = int.Parse(input[0]);
        b = int.Parse(input[1]);

        Console.WriteLine(Multiply(2.3f, 3.5f));
    }*/

    /*// 함수, 지역 변수, 전역 변수
    static int[,] data = new int[10, 10];

    static void Initialze() //PascalCase
    {
        int num = 1;

        //초기화
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                data[i, j] = num; // 들어가는 데이터 값 계산 후 초기화
                num++;
            }
        }
    }

    static void Print()
    {
        //출력
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Console.Write($"data[{i.ToString()}, {j.ToString()}] = {data[i, j].ToString()}  "); // 데이어터 생성 확인 내용 출력
            }
            Console.WriteLine();
        }
    }
    static void Main(string[] args)
    {

        Initialze();
        Print();

    }*/

    /*// 2중 반복문
    static void Main(string[] args)
    {

        for (int i = 1; i <= size; i++)
        {
            for (int j = 0; j < (size - i); j++)
            {
                Console.Write(" ");
            }

            for (int k = 0; k < i; k++)
            {
                Console.Write('*');
            }
            Console.WriteLine();
        }
        for (int i = size; i > 1; i--)
        {
            for (int j = 0; j < (size - i); j++)
            {
                Console.Write(" ");
            }

            for (int k = 0; k < i; k++)
            {
                Console.Write('*');
            }
            Console.WriteLine();
        }
    }*/
}
