using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Net;

namespace L20250318_UDPServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 6000);

            serverSocket.Bind(serverEndPoint);

            byte[] buffer = new byte[1024]; // 이것보다 받는 입장에서 버퍼사이즈가 커야한다.
            EndPoint clientEndPoint = new IPEndPoint();
            serverSocket.ReceiveFrom(buffer, ref clientEndPoint);
        }
    }
}
