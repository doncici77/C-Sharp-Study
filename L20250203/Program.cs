namespace L20250203
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //[][][][][][][][][][]
            //[][][][][][][][][][]
            //[][][][][][][][][][]
            //[][][][][][][][][][]
            //[][][][][][][][][][]
            //[][][][][][][][][][]
            //[][][][][][][][][][]
            //[][][][][][][][][][]
            //[][][][][][][][][][]
            //[][][][][][][][][][]

            int size = 10;
            int[ , ] data = new int[10, 10];
            string s = "Hello world!";

            //초기화
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    data[i , j] = 1 + j + i * size; // 들어가는 데이터 값 계산 후 초기화
                }
            }

            //출력
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write($"data[{i.ToString()}, {j.ToString()}] = {data[i, j].ToString()}  "); // 데이어터 생성 확인 내용 출력
                }
                Console.WriteLine();
            }


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
