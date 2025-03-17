using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 127.0.0.1 => 무조건 자기 자신
            // IPAddress.Parse("127.0.0.1") == IPAddress.Loopback
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4000);
            serverSocket.Connect(serverEndPoint);

            byte[] buffer;

            //String message = "박광호";
            //buffer = Encoding.UTF8.GetBytes(message);

            // 더할 두 수 세팅
            int num1 = 100;
            int num2 = 200;

            // 보낼 연산자
            char op = '+';

            // 두수를 문자열화
            String message = $"{num1}{op}{num2}";

            // 버퍼에 인코딩후 보내기
            buffer = Encoding.UTF8.GetBytes(message);
            int SendLength = serverSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);


            byte[] buffer2 = new byte[1024];
            // 서버에서 값 받아오기
            serverSocket.Receive(buffer2);

            Console.WriteLine(Encoding.UTF8.GetString(buffer2));

            serverSocket.Close();
        }
    }
}
