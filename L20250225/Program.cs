
namespace L20250225
{
    internal class Program
    {
        // 일반적인 개발 -> 프로그램 -> Try, Catch -> 추천
        // 게임 -> try, catch X -> 빨리 빨리 성능 최대화
        // OOP -> Error도 객체 -> 예외
        // Unity engine core -> C++ // contents -> C# -> script
        static void Main(string[] args)
        {
            /*Array<int> test = new Array<int>();
            test.Add(1);
            test.Add(2);
            test.Add(3);
            test.Add(4);
            test.Add(5);

            test.RemoveAt(2);
            for (int i = 0; i < test.data.Length; i++)
            {
                Console.WriteLine(test[i]);
            }
            Console.WriteLine("개수: " + test.Count);*/

            try
            {
                // Engine.Load();
                // Engine.Run();


                Test();
                // file open
                // process
                // exception
                int number = 5;
                if(number == 5)
                {
                    throw new CustomException("이건 내가만든거");
                }
            }
            catch(IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            catch(Exception e)
            {
                StreamWriter sw = new StreamWriter("log.txt");
                sw.WriteLine(e.Message);
                sw.WriteLine(e.StackTrace);
                sw.Flush();
                sw.Close();
            }
            finally
            {
                // 파일 닫기
                // 네트워크 끊기
                // 메모리 정리
                // 텍스처 언로딩
            }
        }

        static void Test()
        {
            int[] a = new int[5];
            int b = 0;
        }
    }
}
