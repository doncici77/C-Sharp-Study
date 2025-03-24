using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace L20250324_Thread
{
    internal class Program
    {
        static Object _lock = new Object(); // 동기화 객체

        // atomic, 공유영역 작업은 원자성, 중간 끊지 말라고
        static int Money = 0;

        static bool lockTaken = false;

        static void Add()

        {
            for (int i = 0; i < 100000; i++)
            {
                //Interlocked.Increment(ref Money);
                //_spinLock.Enter(ref lockTaken);
                lock (_lock)
                {
                    Money++;
                }
                //_spinLock.Exit();
            }
        }

        static void Remove()
        {
            for (int i = 0; i < 100000; i++)
            {
                // Interlocked.Decrement(ref Money);
                //_spinLock.Enter(ref lockTaken);
                lock (_lock)
                {   
                    Money--;
                }
                //_spinLock.Exit();
            }
        }

        public void Test()
        {
            Console.WriteLine("HI");
        }

        // foreground, main thread 종료 되면 나머지 쓰레드는 다 종료
        static void Main(string[] args)
        {
            // OS B 함수 등록해줘 -> Instance
            Thread thread1 = new Thread(new ThreadStart(Add));
            Thread thread2 = new Thread(new ThreadStart(Remove));

            Program program = new Program(); // 클래스 객채생성 후
            Thread thread3 = new Thread(new ThreadStart(program.Test)); // 델리게이트에 대입

            // B 함수 따로 실행 시켜줘 (Thread) -> OS 부탁
            thread1.IsBackground = true;
            thread1.Start();
            thread2.IsBackground = true;
            thread2.Start();
            thread3.IsBackground = true;
            thread3.Start();

            thread1.Join(); // 스레드가 끝나야 아래의 코드를 진행 시킴.
            thread2.Join();
            thread3.Join();

            Console.WriteLine(Money);

            Console.WriteLine("Hello World");
        }
    }
}
