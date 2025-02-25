
using System.Collections;

namespace L20250225
{
    public class DynamicArray<T> : IEnumerable<T>, IEnumerable
    {
        protected T[] data;
        protected int count;

        public DynamicArray()
        {
            data = new T[10];
            count = 0;
        }

        public void Add(T newData)
        {
            if (count >= data.Length)
            {
                T[] newArray = new T[data.Length * 2];
                Array.Copy(data, newArray, data.Length);
                data = newArray;
            }
            data[count] = newData;
            count++;
        }

        //[][][2][][][]
        public void RemoveAt(int index)
        {
            for (int i = index + 1; i < data.Length; ++i)
            {
                data[i - 1] = data[i];
            }
            count--;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < count; ++i)
            {
                yield return data[i];
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for (int i = 0; i < count; ++i)
            {
                yield return data[i];
            }
        }
    }

    /*public abstract class Animal
    {
        public abstract void Eat();

        public void Do()
        {

        }

        public int legs;
    } //-> 다중 상속, C# 다중 상속이 안되, 

    public interface 네발달린짐승
    {
        void Run();
    }

    public interface 새
    {
        void Fly();
    }

    class Lion : Animal, 네발달린짐승
    {
        public override void Eat()
        {

        }

        public void Run()
        {
        }
    }

    class Tiger : Animal, 네발달린짐승
    {
        public override void Eat()
        {

        }

        public void Run()
        {
        }
    }

    class Chciken : Animal, 새
    {
        public override void Eat()
        {
        }

        public void Fly()
        {
            Console.WriteLine("조금 난다.");
        }
    }

    //다중 상속, C++, Diamond, interface X
    //class Liger : Lion, Tiger
    //{

    //}

    //C#, java

    //혼자 만들면 안 씀 -> 다 같이 만든다. -> 다른 놈을 믿을 수 없다.
    public interface IItem
    {
        void Use();
    }

    public interface IEatable
    {
        void Use();
    }

    public class Position : IItem, IEatable
    {
        public void Use()
        {
            throw new NotImplementedException();
        }
    }

    public class Sword : IItem
    {
        public void Use()
        {
            throw new NotImplementedException();
        }
    }*/

    internal class Program
    {
        // 일반적인 개발 -> 프로그램 -> Try, Catch -> 추천
        // 게임 -> try, catch X -> 빨리 빨리 성능 최대화
        // OOP -> Error도 객체 -> 예외
        // Unity engine core -> C++ // contents -> C# -> script
        static void Main2 (string[] args)
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

            /*try
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
            }*/
        }

        static void Test()
        {
            int[] a = new int[5];
            int b = 0;
        }

        static int[] data = { 1, 2, 3, 4, 5 };
        static int current = 0;
        static IEnumerable GetNumbers()
        {
            while (current < data.Length)
            {
                yield return data[current++];
            }
        }

        static void Main(string[] args)
        {
            /*List<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.RemoveAt(11);

            //for (int i = 0; i < list.Count; ++i)
            //{
            //    Console.WriteLine(list[i]);
            //}

            //range for
            foreach (int value in list)
            {
                Console.WriteLine(value);
            }*/

            /*foreach (var value in GetNumbers())
            {
                Console.WriteLine(value);
            }

            return;*/

            DynamicArray<int> dynamicArray = new DynamicArray<int>();
            dynamicArray.Add(1);
            dynamicArray.Add(2);
            dynamicArray.Add(3);
            dynamicArray.Add(4);
            dynamicArray.Add(1);
            dynamicArray.Add(2);
            dynamicArray.Add(3);
            dynamicArray.Add(4);
            dynamicArray.Add(1);
            dynamicArray.Add(2);
            dynamicArray.Add(3);
            dynamicArray.Add(4);

            foreach (int value in dynamicArray)
            {
                Console.WriteLine(value);
            }
        }

        class Component
        {
            public virtual void OnTriggerEnter() { }
            public virtual void OnTriggerExt() { }
        }

        /*//함수 강제 구현, 다중 상속
        static void Main3(string[] args)
        {
            Object position = new Position();
            Type type = position.GetType();
            if (typeof(Position) == type.GetInterface("IItem"))
            {
                (position as Position).Use();
            }

            List<IItem> items = new List<IItem>();
            items.Add(new Position());
            items.Add(new Sword());
            foreach (var item in items)
            {
                item.Use();
            }
        }*/
    }
}
