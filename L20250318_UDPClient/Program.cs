using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace L20250318_UDPClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*IPHostEntry host = Dns.GetHostEntry("naver.com");
            foreach (IPAddress address in host.AddressList)
            {
                Console.WriteLine(address);
            }*/

            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6000);


            byte[] buffer = new byte[1024]; // 이것보다 받는 입장에서 버퍼사이즈가 커야한다.
            string message = "안녕하세요";
            buffer = Encoding.UTF8.GetBytes(message);
            int SendLength = serverSocket.SendTo(buffer, buffer.Length, SocketFlags.None, serverEndPoint);

            byte[] buffer2 = new byte[1024];
            EndPoint remoteEndPoint = serverEndPoint;
            int RecvLength = serverSocket.ReceiveFrom(buffer2, ref remoteEndPoint);
            string message2 = Encoding.UTF8.GetString(buffer2);
            Console.WriteLine("클라이언트가 받음 : " + message2);

            serverSocket.Close();
        }
    }
}
