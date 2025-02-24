using System.Text;

namespace L20250217
{
    internal class Program
    {
        // 네트워크에 접속 했지만 비밀번호가 틀리다.
        class CustomException : Exception
        {
            public CustomException() : base("이거 내가 만든 예외")
            {

            }
        }
        class WrongPasswordException : Exception
        {
            public WrongPasswordException() : base("비번 틀림")
            {

            }
        }

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
            Engine.Instance.Load("level02.map");

            Engine.Instance.Run();

            /*// 에외 처리 예시
            StreamReader sr = null;

            try
            {
                List<string> scene = new List<string>();

                sr = new StreamReader("level03.map");
                while (!sr.EndOfStream)
                {
                    scene.Add(sr.ReadLine());
                    throw new CustomException();
                }

                throw new WrongPasswordException();
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine(e.FileName);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //catch(WrongPasswordException e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            finally
            {
                // network, 파일 입출력
                Console.WriteLine("finaly");
                sr.Close();
            }*/

            /*// 파일 입출력 예시
            string tempScene = "";
            byte[] buffer = new byte[1024];
            FileStream fs = new FileStream("level01.map", FileMode.Open);

            fs.Seek(0, SeekOrigin.End);
            long fileSize = fs.Position;
            Console.WriteLine(fileSize);

            fs.Seek(0, SeekOrigin.Begin);
            int readCount = fs.Read(buffer, 0, (int)fileSize);
            tempScene = Encoding.UTF8.GetString(buffer);
            string[] scene = tempScene.Split("\r\n");

            //while(fs.CanRead)
            //{
            //    int readCount = fs.Read(buffer, offset, 80);
            //    offset += 80;
            //    scene = scene + Encoding.UTF8.GetString(buffer);
            //}*/

            /*// 정렬 예시
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
            }*/
        }
    }
}
