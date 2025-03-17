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

            String message = "Hello World!";
            buffer = Encoding.UTF8.GetBytes(message);
            int SendLength = serverSocket.Send(buffer);

            byte[] buffer2 = new byte[1024];
            serverSocket.Receive(buffer2);

            Console.WriteLine(Encoding.UTF8.GetString(buffer2));

            serverSocket.Close();
        }
    }
}
