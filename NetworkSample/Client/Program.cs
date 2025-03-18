using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class MessageDataClient
{
    public MessageDataClient(string inMessage)
    {
        message = inMessage;
    }

    public string message;
}

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4000);
            serverSocket.Connect(serverEndPoint);

            FileStream fsOutput = new FileStream("1_copy.webp", FileMode.Create);
            int RecvLength = 0;
            do
            {
                byte[] buffer = new byte[1024];
                RecvLength = serverSocket.Receive(buffer);
                fsOutput.Write(buffer, 0, RecvLength);

            } while (RecvLength > 0);

            fsOutput.Close();
            serverSocket.Close();
        }

        /// <summary>
        /// 이미지 받고 저장 클라이언트 GPT버전
        /// </summary>
        /// <param name="args"></param>
        static void ImageSocketClientGPTver(string[] args)
        {
            // 1. 서버 연결
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4000);
            serverSocket.Connect(serverEndPoint);

            // 2. 서버에 메시지 전송
            byte[] buffer;
            string message = "이미지 요청";
            buffer = Encoding.UTF8.GetBytes(message);
            int sendLength = serverSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
            Console.WriteLine($"보낸 데이터 크기: {sendLength} 바이트");

            // 3. 서버에서 이미지 크기(4바이트) 수신
            byte[] imageSizeBytes = new byte[4];
            serverSocket.Receive(imageSizeBytes);
            int imageSize = BitConverter.ToInt32(imageSizeBytes, 0);
            Console.WriteLine($"이미지 크기 수신: {imageSize} 바이트");

            // 4. 이미지 데이터 수신
            byte[] imageBytes = new byte[imageSize];
            int totalReceived = 0;
            while (totalReceived < imageSize)
            {
                int received = serverSocket.Receive(imageBytes, totalReceived, imageSize - totalReceived, SocketFlags.None);
                if (received == 0) break;
                totalReceived += received;
            }

            // 5. 이미지 저장
            string savePath = @"C:\DestinationFolder\received_image.jpg";
            File.WriteAllBytes(savePath, imageBytes);
            Console.WriteLine($"이미지 저장 완료: {savePath}");

            // 6. 서버에 응답 전송
            byte[] responseBuffer = Encoding.UTF8.GetBytes("이미지 수신 완료");
            serverSocket.Send(responseBuffer);
            Console.WriteLine("서버에 응답 전송 완료");

            // 7. 소켓 종료
            serverSocket.Close();
            Console.WriteLine("클라이언트 종료");
        }

        /// <summary>
        /// 클라이언트 덧셈 보내기 예제
        /// </summary>
        /// <param name="args"></param>
        static void PlusSocketClient(string[] args)
        {

            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 127.0.0.1 => 무조건 자기 자신
            // IPAddress.Parse("127.0.0.1") == IPAddress.Loopback
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4000);
            serverSocket.Connect(serverEndPoint);

            byte[] buffer;

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

        /// <summary>
        /// 제이슨 파일의 형식 데이터 보내기 클라이언트
        /// </summary>
        /// <param name="args"></param>
        static void JsonSocketClient(string[] args)
        {

            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4000);
            serverSocket.Connect(serverEndPoint);

            byte[] buffer;

            // 제이슨 파일의 형식 데이터 보내기 클라이언트
            MessageDataClient data = new MessageDataClient("안녕하세요");
            string g = JsonConvert.SerializeObject(data);
            buffer = Encoding.UTF8.GetBytes(g);
            int SendLength = serverSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);

            byte[] buffer2 = new byte[1024];
            serverSocket.Receive(buffer2);
            Console.WriteLine(Encoding.UTF8.GetString(buffer2));

            serverSocket.Close();
        }
    }
}
