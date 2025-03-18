﻿using System.Net.Sockets;
using System.Net;
using System.Text;
using System;
using Newtonsoft.Json;
using System.Reflection;

public class MessageDataServer
{
    public MessageDataServer(string inMessage)
    {
        message = inMessage;
    }

    public string message;
}

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket listensocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, 4000);

            listensocket.Bind(listenEndPoint);

            listensocket.Listen(10);

            bool isRunning = true;
            while (isRunning)
            {
                Socket clientSocket = listensocket.Accept();

                byte[] buffer = new byte[1024];
                int RecvLength = clientSocket.Receive(buffer);
                if (RecvLength <= 0)
                {

                    isRunning = false;
                }


                int SendLength = clientSocket.Send(buffer);
                if(SendLength <= 0)
                {
                    isRunning = false;
                }

                clientSocket.Close();
            }

            listensocket.Close();
        }


        /// <summary>
        /// 소켓 더하기 연산 서버
        /// </summary>
        /// <param name="args"></param>
        static void PlusSocketServer(string[] args)
        {
            // 새로운 소켓을 생성 (IPv4 주소 체계, TCP 프로토콜 사용)
            Socket listensocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 서버가 사용할 IP 주소와 포트 번호를 지정 (모든 네트워크 인터페이스에서 포트 4000으로 연결을 받음)
            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, 4000);

            // 소켓을 특정 IP 주소와 포트에 바인딩(해당 IP와 포트에서 클라이언트의 연결 요청을 받을 준비)
            listensocket.Bind(listenEndPoint);

            // 소켓을 수신 대기 상태로 설정 (최대 10개의 클라이언트 연결 요청을 대기열에서 보관)
            listensocket.Listen(10);

            bool isRunning = true;
            while (isRunning)
            {
                // 동기, 블록킹
                // 클라이언트의 연결 요청을 수락 (연결이 들어올 때까지 동기적으로 대기)
                Socket clientSocket = listensocket.Accept();

                byte[] buffer = new byte[1024];
                // 클라이언트로부터 데이터를 수신하고, buffer 배열에 저장
                int RecvLength = clientSocket.Receive(buffer);
                if (RecvLength <= 0)
                {
                    // 내쪽에서 행함
                    // close == 0
                    // error < 0

                    isRunning = false;
                }

                // 정수 덧셈 서버 받기
                // 받을 연산자
                char op = '+';

                // 가져온 숫자 두개 + 기준으로 나누기
                string[] numText = Encoding.UTF8.GetString(buffer).Split(op, '\0');

                Console.WriteLine(Encoding.UTF8.GetString(buffer));
                Console.WriteLine("서버가 받음");

                // 문자열 정수화
                int a = int.Parse(numText[0]);
                int b = int.Parse(numText[1]);

                // 덧셈값 버퍼에 인코딩
                buffer = Encoding.UTF8.GetBytes((a + b).ToString());

                // 수신한 데이터를 그대로 클라이언트에게 다시 전송 (에코 기능)
                int SendLength = clientSocket.Send(buffer);
                if (SendLength <= 0)
                {
                    // 상대방이 행함
                    // close == 0
                    // error < 0

                    isRunning = false;
                }

                // 클라이언트 소켓 연결 종료
                clientSocket.Close();
            }

            listensocket.Close();
        }

        /// <summary>
        /// 제이슨 형식의 값 예제 서버
        /// </summary>
        /// <param name="args"></param>
        static void JsonSocketServer(string[] args)
        {
            Socket listensocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, 4000);

            listensocket.Bind(listenEndPoint);

            listensocket.Listen(10);

            bool isRunning = true;
            while (isRunning)
            {
                Socket clientSocket = listensocket.Accept();

                byte[] buffer = new byte[1024];
                int RecvLength = clientSocket.Receive(buffer);
                if (RecvLength <= 0)
                {
                    isRunning = false;
                }

                // 제이슨 형식의 값 예제 서버
                // 제이슨 형식으로 잘 들어봤는지 체크
                Console.WriteLine(Encoding.UTF8.GetString(buffer));

                // 잘 들어왔는디 비교할 값 세팅
                MessageDataServer checkMessageData = new MessageDataServer("안녕하세요");
                string jsonMessage = JsonConvert.SerializeObject(checkMessageData);

                // 값 받아오기
                MessageDataServer getMessageData = new MessageDataServer(Encoding.UTF8.GetString(buffer));

                // 가져온 값이 맞는지 비교후 반환할 값 세팅
                string g;
                if (getMessageData.message.CompareTo(jsonMessage) == 0)
                {
                    getMessageData.message = "반가워요";
                    g = JsonConvert.SerializeObject(getMessageData);
                }
                else
                {
                    getMessageData.message = "error";
                    g = JsonConvert.SerializeObject(getMessageData);
                }
                buffer = Encoding.UTF8.GetBytes(g);


                int SendLength = clientSocket.Send(buffer);
                if (SendLength <= 0)
                {
                    isRunning = false;
                }

                clientSocket.Close();
            }

            listensocket.Close();
        }
    }
}
