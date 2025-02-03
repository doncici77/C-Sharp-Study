using System.Drawing;

namespace L20250203
{
    internal class Program
    {
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

            /*for (int i = 1; i <= size; i++)
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
            }*/
        }
    }
}
