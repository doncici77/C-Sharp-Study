namespace L20250224_02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*// 1번
            int n = 5;
            int[] arr1 = arr1 = [9, 20, 28, 18, 11]; 
            int[] arr2 = arr2 = [30, 1, 21, 17, 28];
            int[] valueArr = new int[n];

            for (int i = 0; i < valueArr.Length; i++)
            {
                valueArr[i] = arr1[i] | arr2[i];

                string value_bit = Convert.ToString(valueArr[i], 2); // 2진수 문자열을 한글자씩 나눔
                // Console.WriteLine(Convert.ToString(valueArr[i], 2).Replace('1', '#').Replace('0', ' '));

                for(int j = 0; j < value_bit.Length; j++)
                {
                    if (value_bit[j] == '0')
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("*");
                    }
                }
                Console.WriteLine();
            }   */


            // 2번 
            int n = 3;
            long[] x = new long[n];
            long[] npot =  new long[n];
            long total = 0;
            x = [3, 5, 7];

            for(int i = 0; i < n; i++)
            {
                npot[i] = 0;
                while (x[i] > 0)
                {
                    x[i] = x[i] >> 1;
                    npot[i]++;
                }
                npot[i] = 1 << (int)npot[i]; // 0001 << n칸 이동
            }

            for(int i = 0;i < n; i++)
            {
                total = total ^ npot[i];
            }
            Console.WriteLine(total);
        }
    }
}
