namespace L20250217
{
    internal class Program
    {
        class Singleton
        {
            private Singleton()
            {

            }

            static Singleton instance;
            static public Singleton Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                    return instance;
                }
            }
        }
        static void Main(string[] args)
        {
            //Engine.Instance.Load();

            //Engine.Instance.Run();

            // 1 --> 10 올림차순, ascending
            // 10 --> 1 내림차순, descending

            int[] numbers = { 6, 5, 2, 3, 1, 7, 8, 10, 9 };

            for(int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers.Length; j++)
                {
                    if(numbers[i] < numbers[j])
                    {
                        int temp = numbers[i];
                        numbers[i] = numbers[j];
                        numbers[j] = temp;
                    }
                }
            }

            for(int i = 0;i < numbers.Length; i++)
            {
                Console.Write(numbers[i] + ", ");
            }
        }
    }
}
