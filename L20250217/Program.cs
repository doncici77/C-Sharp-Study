﻿using System.Reflection;
using System.Text;

namespace L20250217
{
    public class Sample
    {
        public delegate int Command(int a, int b);

        public Command command;

        public void Sort()
        {
            if(command(1, 2) > 0)
            {
                // 교환
            }
        }
    }

    public class EvenetClass
    {
        public delegate void DelegateSample();
        public event DelegateSample EventSample;

        public void Do()
        {
            EventSample?.Invoke();
        }
    }

    public class Program
    {
        static int Add(int A, int B)
        {
            return A + B;
        }

        static int Sub(int A, int B)
        {
            return A - B;
        }

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

        class Data
        {
            public void Count()
            {
                Console.WriteLine("Count");
            }

            private void FuncA()
            {
                Console.WriteLine("private");
            }

            protected void Sum()
            {
                Console.WriteLine("protected");
            }

            public static void StaticFunction()
            {
                Console.WriteLine("StaticFunction StaticFunction");
            }

            public static void Add(int A, int B)
            {
                Console.WriteLine($"{A} + {B} = {A + B}");
            }

            public int Gold = 1;

            protected int Money = -1000;

            private float Hp = -10.5f;

            public int MP
            {
                get;
                set;
            }
        }

        public static int Compare(GameObject first, GameObject second)
        {
            SpriteRenderer spriteRenderer1 = first.GetComponent<SpriteRenderer>();
            SpriteRenderer spriteRenderer2 = second.GetComponent<SpriteRenderer>();
            if (spriteRenderer1 == null || spriteRenderer2 == null)
            {
                return 0;
            }

            return spriteRenderer1.orderlayer - spriteRenderer2.orderlayer;
        }

        public static void Test()
        {
            Console.WriteLine("Test");
        }

        static void Main(string[] args)
        {
            /*// 리플렉션 예시
            Data d = new Data();
            Type classType = d.GetType();

            Console.WriteLine(classType.Name);

            MethodInfo[] methods = classType.GetMethods(BindingFlags.Public |
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            foreach (MethodInfo info in methods)
            {
                //Console.WriteLine($"{info.Name}");
                if (info.Name.CompareTo("Add") == 0)
                {
                    ParameterInfo[] paramInfos = info.GetParameters();
                    foreach (ParameterInfo paramInfo in paramInfos)
                    {
                        Console.WriteLine(paramInfo.Name);
                    }

                    Object[] param = { 3, 5 };
                    info.Invoke(d, param);
                }
            }

            FieldInfo[] fields = classType.GetFields(BindingFlags.Public |
                BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                Console.WriteLine($"{field.FieldType} , {field.Name} , {field.GetValue(d)}");
                field.SetValue(d, 10);
                Console.WriteLine($"{field.FieldType} , {field.Name} , {field.GetValue(d)}");
            }

            PropertyInfo[] propertyInfos = classType.GetProperties(BindingFlags.Public |
                BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                Console.WriteLine($"{propertyInfo.Name} , {propertyInfo.GetValue(d)}");
            }*/

            /*// 델리게이트 예제
            Sample.Command command = new Sample.Command((int A, int B) => { return A * B; });
            Console.WriteLine(command(1, 2));

            Sample sample = new Sample();
            sample.command = Add; // 다른클래스의 델리게이트 변수에 프로그램 클래스의 함수를 저장 가능.
            sample.Sort();*/

            /*// 이벤트 델리게이트 예제
            EvenetClass evenetClass = new EvenetClass();
            evenetClass.EventSample += Test;
            evenetClass.Do();*/

            /*// Action 과 Func 델리게이트 자료형 예제
            // Action : 반환형이 없는 델리게이트를 줄인것
            // Func<int, int> : 반환형이 있는 델리게이트를 줄인것, 마지막 인자가 반환형임
            Action helloAction = Test;
            Func<int, int> f = (int a) => { return a; };
            f(2);*/

            Engine.Instance.Init();
            Engine.Instance.SetSortCompare(Compare);

            Engine.Instance.Load("level01.map");
            Engine.Instance.Run();

            Engine.Instance.Quit();

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
