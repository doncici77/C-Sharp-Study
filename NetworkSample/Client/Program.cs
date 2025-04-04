﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

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
        // 정수형 숫자
        //short //htons
        //int,  //htonl
        //long  //htonll
        //[1][2]
        static Socket clientSocket;

        static void SendPacket(Socket toSocket, string message)
        {
            byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
            ushort length = (ushort)IPAddress.HostToNetworkOrder((short)messageBuffer.Length);

            byte[] headerBuffer = BitConverter.GetBytes(length);

            byte[] packetBuffer = new byte[headerBuffer.Length + messageBuffer.Length];
            Buffer.BlockCopy(headerBuffer, 0, packetBuffer, 0, headerBuffer.Length);
            Buffer.BlockCopy(messageBuffer, 0, packetBuffer, headerBuffer.Length, messageBuffer.Length);

            int SendLength = toSocket.Send(packetBuffer, packetBuffer.Length, SocketFlags.None);

        }

        static void RecvPacket(Socket toSocket, out string jsonString)
        {
            byte[] lengthBuffer = new byte[2];

            int RecvLength = clientSocket.Receive(lengthBuffer, 2, SocketFlags.None);
            ushort length = BitConverter.ToUInt16(lengthBuffer, 0);
            length = (ushort)IPAddress.NetworkToHostOrder((short)length);
            byte[] recvBuffer = new byte[4096];
            RecvLength = clientSocket.Receive(recvBuffer, length, SocketFlags.None);

            jsonString = Encoding.UTF8.GetString(recvBuffer);
        }

        static void Main(string[] args)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4000);

            clientSocket.Connect(listenEndPoint);

            JObject result = new JObject();
            result.Add("code", "Login");
            result.Add("id", "htk008kr");
            result.Add("password", "5678");
            SendPacket(clientSocket, result.ToString());

            //result.Add("code", "Signup");
            //result.Add("id", "robot");
            //result.Add("password", "1234");
            //result.Add("name", "로봇");
            //result.Add("email", "robot@a.com");
            //SendPacket(clientSocket, result.ToString());

            string JsonString;
            RecvPacket(clientSocket, out JsonString);

            Console.WriteLine(JsonString);


            clientSocket.Close();
        }

        #region 쓰레드 클라이언트 형태 구조
        // 쓰레드 클라이언트 형태 구조
        static void ChatThread(string[] args)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4000);

            clientSocket.Connect(listenEndPoint);

            Thread chatInputThread = new Thread(new ThreadStart(ChatInput));
            Thread recvThread = new Thread(new ThreadStart(RecvThread));
            chatInputThread.IsBackground = true;
            recvThread.IsBackground = true;
            chatInputThread.Start();
            recvThread.Start();

            chatInputThread.Join();
            recvThread.Join();

            clientSocket.Close();
        }

        static void ChatInput()
        {
            while (true)
            {
                string InputChat;
                Console.Write("채팅 : ");
                InputChat = Console.ReadLine();

                string jsonString = "{\"id\" : \"익명\",  \"message\" : \"" + InputChat + ".\"}";
                byte[] message = Encoding.UTF8.GetBytes(jsonString);
                ushort length = (ushort)message.Length;

                //길이  자료
                //[][] [][][][][][][][]
                byte[] lengthBuffer = new byte[2];
                lengthBuffer = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)length));
                Console.WriteLine("lengthBuffer : " + lengthBuffer.Length);

                byte[] buffer = new byte[2 + length];

                Buffer.BlockCopy(lengthBuffer, 0, buffer, 0, 2);
                Buffer.BlockCopy(message, 0, buffer, 2, length);

                int SendLength = clientSocket.Send(buffer, buffer.Length, SocketFlags.None);
            }
        }

        static void RecvThread()
        {
            while (true)
            {
                byte[] lengthBuffer = new byte[2];

                int RecvLength = clientSocket.Receive(lengthBuffer, 2, SocketFlags.None);
                ushort length = BitConverter.ToUInt16(lengthBuffer, 0);
                length = (ushort)IPAddress.NetworkToHostOrder((short)length);

                byte[] recvBuffer = new byte[4096];
                RecvLength = clientSocket.Receive(recvBuffer, length, SocketFlags.None);

                string JsonString = Encoding.UTF8.GetString(recvBuffer);

                Console.WriteLine(JsonString);

                Thread.Sleep(100);
            }

        }
        #endregion

        /// <summary>
        /// 이미지 받고 저장 클라이언트 수업버전
        /// </summary>
        /// <param name="args"></param>
        static void ImageSocketClient(string[] args)
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4000);

            #region TCP 클라인어트를 판단하는 코드
            serverSocket.Connect(serverEndPoint);
            #endregion

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
