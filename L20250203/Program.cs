namespace L20250203
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int size = 20;
            int[,] data = new int[1080, 1920];

            for(int i = 1; i <= size; i++)
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
        }
    }
}
