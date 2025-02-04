using System.Text;

namespace L20250204
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] num = new int[52];
            int[] val = new int[8];
            Random random = new Random();
            StringBuilder output = new StringBuilder();


            for(int i = 0; i < 52; i++) // 배열에 숫자 삽입
            {
                num[i] = i + 1;
            }

            for(int i = 0; i < 8; i++) // 출력 값에 랜덤 숫자 삽입
            {
                val[i] = num[random.Next(51) + 1];

                for(int j = 0; j < 8; j++)
                {
                    if(val[i] == val[j] && i != j) // 겹치는게 있는지 확인
                    {
                        val[i] = num[random.Next(51) + 1];
                        j = 0;
                    }
                }

                output.AppendLine(val[i].ToString()); // 문자열에 값을 저장
            }

            Console.WriteLine(output.ToString());
        }
    }
}
