using System.Text;

namespace L20250204
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] num = new int[52];
            Random random = new Random();
            StringBuilder output = new StringBuilder();

            // 1 - 13 -> Heart, 1 -> A, 11 -> J, 12 -> Q, 13 -> K
            // 14 - 26 -> Diamond
            // 27 - 39 -> Clover
            // 40 - 52 -> Spade

            for(int i = 0; i < num.Length; i++)
            {
                num[i] = i + 1;
            }

            /*//시간복잡도(N^2)
            for (int i = 0; i < outNum; i++) // 출력 값에 랜덥값 넣기
            {
                val[i] = num[random.Next(1, size + 1)];

                for (int j = 0; j < i; j++)
                {
                    if (val[i] == val[j] && i != j) // 겹치는게 있는지 확인
                    {
                        val[i] = num[random.Next(1, size + 1)];
                        j = 0;
                    }
                }

                output.AppendLine(val[i].ToString()); // 문자열에 값을 저장
            }*/

            // 시간복잡도 (N)
            // Fisher-Yates shuffle (배열 섞기)
            for (int i = num.Length - 1; i > 0; i--)
            {
                // 랜덤 인덱스 선택
                int j = random.Next(i + 1);

                // 현재 인덱스와 랜덤 인덱스 값을 교환
                int temp = num[i];
                num[i] = num[j];
                num[j] = temp;
            }

            // 섞인 배열에서 첫 8개 숫자 출력
            for (int i = 0; i < 8; i++)
            {
                if (num[i] < 14)
                {
                    output.Append(num[i].ToString() + " Heart");
                    if((num[i] % 13) == 1)
                    {
                        output.AppendLine(" A");
                    }
                    else if ((num[i] % 13) == 11)
                    {
                        output.AppendLine(" J");
                    }
                    else if ((num[i] % 13) == 12)
                    {
                        output.AppendLine(" Q");
                    }
                    else if ((num[i] % 13) == 0)
                    {
                        output.AppendLine(" K");
                    }
                    else
                    {
                        output.AppendLine();
                    }
                }
                else if (14 <= num[i] && num[i] < 27)
                {
                    output.Append(num[i].ToString() + " Diamond");
                    if ((num[i] % 13) == 1)
                    {
                        output.AppendLine(" A");
                    }
                    else if ((num[i] % 13) == 11)
                    {
                        output.AppendLine(" J");
                    }
                    else if ((num[i] % 13) == 12)
                    {
                        output.AppendLine(" Q");
                    }
                    else if ((num[i] % 13) == 0)
                    {
                        output.AppendLine(" K");
                    }
                    else
                    {
                        output.AppendLine();
                    }
                }
                else if (27 <= num[i] && num[i] < 40)
                {
                    output.Append(num[i].ToString() + " Clover");
                    if ((num[i] % 13) == 1)
                    {
                        output.AppendLine(" A");
                    }
                    else if ((num[i] % 13) == 11)
                    {
                        output.AppendLine(" J");
                    }
                    else if ((num[i] % 13) == 12)
                    {
                        output.AppendLine(" Q");
                    }
                    else if ((num[i] % 13) == 0)
                    {
                        output.AppendLine(" K");
                    }
                    else
                    {
                        output.AppendLine();
                    }
                }
                else if (40 <= num[i] && num[i] < 53)
                {
                    output.Append(num[i].ToString() + " Spade");
                    if ((num[i] % 13) == 1)
                    {
                        output.AppendLine(" A");
                    }
                    else if ((num[i] % 13) == 11)
                    {
                        output.AppendLine(" J");
                    }
                    else if ((num[i] % 13) == 12)
                    {
                        output.AppendLine(" Q");
                    }
                    else if ((num[i] % 13) == 0)
                    {
                        output.AppendLine(" K");
                    }
                    else
                    {
                        output.AppendLine();
                    }
                }
            }

            Console.WriteLine(output.ToString());
        }
    }
}
