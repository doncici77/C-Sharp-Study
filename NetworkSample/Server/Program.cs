using System.Net.Sockets;
using System.Net;
using System.Text;
using System;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Data.SqlClient;
using MySqlConnector;
using System.Net.NetworkInformation;

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
    class Message
    {
        public string message;
    }

    class Program
    {
        static Socket listenSocket;

        static List<Socket> clientSockets = new List<Socket>();
        //static List<Thread> threadManager = new List<Thread>();

        static Object _lock = new Object();

        static void AcceptThread()
        {
            while (true)
            {
                Socket clientSocket = listenSocket.Accept();

                lock (_lock) // 이거하는 동안 다른 쓰레드 중단
                {
                    clientSockets.Add(clientSocket);
                }
                Console.WriteLine("Connect client : " + clientSocket.RemoteEndPoint);

                // ParameterizedThreadStart : 델리게이트에서 이름만 바뀐 느낌
                Thread workThread = new Thread(new ParameterizedThreadStart(WorkThread));
                workThread.IsBackground = true;
                workThread.Start(clientSocket);
                //threadManager.Add(workThread);
            }
        }

        static void WorkThread(Object clientObjectSocket)
        {

            Socket clientSocket = clientObjectSocket as Socket;

            while (true)
            {
                try
                {
                    byte[] headerBuffer = new byte[2];
                    int RecvLength = clientSocket.Receive(headerBuffer, 2, SocketFlags.None);
                    if (RecvLength > 0)
                    {
                        short packetlength = BitConverter.ToInt16(headerBuffer, 0);
                        packetlength = IPAddress.NetworkToHostOrder(packetlength);

                        byte[] dataBuffer = new byte[4096];
                        RecvLength = clientSocket.Receive(dataBuffer, packetlength, SocketFlags.None);
                        string JsonString = Encoding.UTF8.GetString(dataBuffer);
                        Console.WriteLine(JsonString);

                        string connectionString = "server=localhost;user=root;database=membership;password=0321";
                        MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

                        JObject clientData = JObject.Parse(JsonString);
                        string code = clientData.Value<string>("code");
                        try
                        {
                            if (code.CompareTo("Login") == 0)
                            {
                                // login 로그인
                                string userId = clientData.Value<string>("id");
                                string userPassword = clientData.Value<string>("password"); 

                                mySqlConnection.Open();
                                MySqlCommand mySqlCommand = new MySqlCommand();
                                mySqlCommand.Connection = mySqlConnection;

                                mySqlCommand.CommandText = "select * from users where user_id = @user_id and user_password = @user_password";
                                mySqlCommand.Prepare();
                                mySqlCommand.Parameters.AddWithValue("@user_id", userId);
                                mySqlCommand.Parameters.AddWithValue("@user_password", userPassword);

                                MySqlDataReader dataReader = mySqlCommand.ExecuteReader();
                                if(dataReader.Read())
                                {
                                    // 로그인 성공 로직
                                }
                                else
                                {
                                    // 로그인 실패 로직
                                }
                            }
                            else if (code.CompareTo("SignUp") == 0)
                            {
                                // 회원가입
                                string userId = clientData.Value<string>("id");
                                string userPassword = clientData.Value<string>("password");
                                string name = clientData.Value<string>("name");
                                string email = clientData.Value<string>("email");

                                mySqlConnection.Open();
                                MySqlCommand mySqlCommand2 = new MySqlCommand();
                                mySqlCommand2.Connection = mySqlConnection;

                                mySqlCommand2.CommandText = "insert into users (user_id, user_password, name, email) values (@user_id, @user_password, @name, @email)";
                                mySqlCommand2.Prepare();
                                mySqlCommand2.Parameters.AddWithValue("@user_id", userId);
                                mySqlCommand2.Parameters.AddWithValue("@user_password", userPassword);
                                mySqlCommand2.Parameters.AddWithValue("@name", name);
                                mySqlCommand2.Parameters.AddWithValue("@email", email);

                                mySqlCommand2.ExecuteNonQuery();


                                // 가입 성공 로직
                            }
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        finally
                        {
                            mySqlConnection.Close();
                        }

                        /*string message = "{ \"message\" : \"" + clientData.Value<String>("message") + "\"}";
                        byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
                        ushort length = (ushort)IPAddress.HostToNetworkOrder((short)messageBuffer.Length);

                        headerBuffer = BitConverter.GetBytes(length);

                        byte[] packetBuffer = new byte[headerBuffer.Length + messageBuffer.Length];
                        Buffer.BlockCopy(headerBuffer, 0, packetBuffer, 0, headerBuffer.Length);
                        Buffer.BlockCopy(messageBuffer, 0, packetBuffer, headerBuffer.Length, messageBuffer.Length);
                        lock (_lock)
                        {
                            foreach (Socket sendSocket in clientSockets)
                            {
                                int SendLength = sendSocket.Send(packetBuffer, packetBuffer.Length, SocketFlags.None);
                            }
                        }*/
                    }
                    else
                    {
                        string message = "{ \"message\" : \" Disconnect : " + clientSocket.RemoteEndPoint + " \"}";
                        byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
                        ushort length = (ushort)IPAddress.HostToNetworkOrder((short)messageBuffer.Length);

                        headerBuffer = BitConverter.GetBytes(length);

                        byte[] packetBuffer = new byte[headerBuffer.Length + messageBuffer.Length];
                        Buffer.BlockCopy(headerBuffer, 0, packetBuffer, 0, headerBuffer.Length);
                        Buffer.BlockCopy(messageBuffer, 0, packetBuffer, headerBuffer.Length, messageBuffer.Length);

                        clientSocket.Close();
                        lock (_lock)
                        {
                            clientSockets.Remove(clientSocket);

                            foreach (Socket sendSocket in clientSockets)
                            {
                                int SendLength = sendSocket.Send(packetBuffer, packetBuffer.Length, SocketFlags.None);
                            }
                        }

                        return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error 낸 놈 : {e.Message} {clientSocket.RemoteEndPoint}");

                    string message = "{ \"message\" : \" Disconnect : " + clientSocket.RemoteEndPoint + " \"}";
                    byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
                    ushort length = (ushort)IPAddress.HostToNetworkOrder((short)messageBuffer.Length);

                    byte[] headerBuffer = new byte[2];

                    headerBuffer = BitConverter.GetBytes(length);

                    byte[] packetBuffer = new byte[headerBuffer.Length + messageBuffer.Length];
                    Buffer.BlockCopy(headerBuffer, 0, packetBuffer, 0, headerBuffer.Length);
                    Buffer.BlockCopy(messageBuffer, 0, packetBuffer, headerBuffer.Length, messageBuffer.Length);

                    clientSocket.Close();
                    lock (_lock)
                    {
                        clientSockets.Remove(clientSocket);

                        foreach (Socket sendSocket in clientSockets)
                        {
                            int SendLength = sendSocket.Send(packetBuffer, packetBuffer.Length, SocketFlags.None);
                        }
                    }

                    return;
                }
            }
        }


        static void Main(string[] args)
        {
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, 4000);

            listenSocket.Bind(listenEndPoint);

            listenSocket.Listen(10);

            Thread acceptThread = new Thread(new ThreadStart(AcceptThread));
            acceptThread.IsBackground = true;
            acceptThread.Start();

            acceptThread.Join();

            listenSocket.Close();
        }

        /// <summary>
        /// 기본적인 간단한 스래트를 이용한 채팅 소켓 서버 형태
        /// </summary>
        /// <param name="args"></param>
        static void SocketChatFirst(string[] args)
        {
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, 4000);

            listenSocket.Bind(listenEndPoint);

            listenSocket.Listen(10);

            List<Socket> clientSockets = new List<Socket>();
            List<Socket> checkRead = new List<Socket>(); // 감시형 소켓

            while (true)
            {
                checkRead.Clear(); // 비우고
                checkRead = new List<Socket>(clientSockets); //복사 // 서로 같은 주소를 가르키지 않게 생성자로 새로 초기화 해줌
                checkRead.Add(listenSocket); // 감시

                // Polling
                Socket.Select(checkRead, null, null, -1); // 멀티플렉싱 함수

                foreach (Socket findSocket in checkRead)
                {
                    if (findSocket == listenSocket)
                    {
                        Socket clientSocket = listenSocket.Accept();
                        clientSockets.Add(clientSocket);
                        Console.WriteLine("Connect client : " + clientSocket.RemoteEndPoint);
                    }
                    else // recvbyte > 0
                    {
                        try
                        {
                            //[][] [][][][][][]

                            //패킷 길이 받기(header)
                            byte[] headerBuffer = new byte[2];
                            int RecvLength = findSocket.Receive(headerBuffer, 2, SocketFlags.None);

                            if (RecvLength > 0)
                            {
                                short packetlength = BitConverter.ToInt16(headerBuffer, 0); // short(16비트 정수)로 변환
                                                                                            // 바이트 오더(빅 엔디안)를 호스트 바이트 오더(리틀 엔디안)로 변환
                                packetlength = IPAddress.NetworkToHostOrder(packetlength);

                                //[][][][][]
                                //실제 패킷(header 길이 만큼)
                                byte[] dataBuffer = new byte[4096];
                                RecvLength = findSocket.Receive(dataBuffer, packetlength, SocketFlags.None);
                                string JsonString = Encoding.UTF8.GetString(dataBuffer);
                                Console.WriteLine(JsonString);

                                JObject clientData = JObject.Parse(JsonString);

                                string message = "{ \"message\" : \"" + clientData.Value<String>("message") + "\"}";

                                //Custom 패킷 만들기
                                //다시 전송 메세지
                                byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
                                ushort length = (ushort)IPAddress.HostToNetworkOrder((short)messageBuffer.Length);

                                //길이  자료
                                //[][] [][][][][][][][]
                                headerBuffer = BitConverter.GetBytes(length);

                                //[][][][][][][][][][][]
                                byte[] packetBuffer = new byte[headerBuffer.Length + messageBuffer.Length];

                                Buffer.BlockCopy(headerBuffer, 0, packetBuffer, 0, headerBuffer.Length);
                                Buffer.BlockCopy(messageBuffer, 0, packetBuffer, headerBuffer.Length, messageBuffer.Length);

                                foreach (Socket sendSocket in clientSockets)
                                {
                                    int SendLength = findSocket.Send(packetBuffer, packetBuffer.Length, SocketFlags.None);
                                }
                            }
                            else
                            {
                                findSocket.Close();
                                clientSockets.Remove(findSocket);

                                string message = "{ \"message\" : \" Disconnect : " + findSocket.RemoteEndPoint + "\"}";

                                //Custom 패킷 만들기
                                //다시 전송 메세지
                                byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
                                ushort length = (ushort)IPAddress.HostToNetworkOrder((short)messageBuffer.Length);

                                //길이  자료
                                //[][] [][][][][][][][]
                                headerBuffer = BitConverter.GetBytes(length);

                                //[][][][][][][][][][][]
                                byte[] packetBuffer = new byte[headerBuffer.Length + messageBuffer.Length];

                                Buffer.BlockCopy(headerBuffer, 0, packetBuffer, 0, headerBuffer.Length);
                                Buffer.BlockCopy(messageBuffer, 0, packetBuffer, headerBuffer.Length, messageBuffer.Length);

                                foreach (Socket sendSocket in clientSockets)
                                {
                                    int SendLength = findSocket.Send(packetBuffer, packetBuffer.Length, SocketFlags.None);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Error 낸 놈 : {e.Message} {findSocket.RemoteEndPoint}");

                            string message = "{ \"message\" : \" Disconnect : " + findSocket.RemoteEndPoint + " \"}";
                            byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
                            ushort length = (ushort)IPAddress.HostToNetworkOrder((short)messageBuffer.Length);

                            byte[] headerBuffer = new byte[2];

                            headerBuffer = BitConverter.GetBytes(length);

                            byte[] packetBuffer = new byte[headerBuffer.Length + messageBuffer.Length];
                            Buffer.BlockCopy(headerBuffer, 0, packetBuffer, 0, headerBuffer.Length);
                            Buffer.BlockCopy(messageBuffer, 0, packetBuffer, headerBuffer.Length, messageBuffer.Length);

                            findSocket.Close();
                            clientSockets.Remove(findSocket);

                            foreach (Socket sendSocket in clientSockets)
                            {
                                int SendLength = sendSocket.Send(packetBuffer, packetBuffer.Length, SocketFlags.None);
                            }
                        }
                    }
                }

                // Sever 작업
                {
                    Console.WriteLine("서버 작업");
                }
            }

            listenSocket.Close();
        }

        /// <summary>
        /// 소켓 이미지파일 전송 수업버전 서버
        /// </summary>
        /// <param name="args"></param>
        static void ImageSocketServer(string[] args)
        {
            Socket listensocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, 4000);
            listensocket.Bind(listenEndPoint);

            #region TCP 서버인지 판단하는 코드
            listensocket.Listen(10);
            Socket clientSocket = listensocket.Accept();
            #endregion

            #region 이미지 파일 열고 보내기
            FileStream fsInput = new FileStream("1.webp", FileMode.Open);
            byte[] buffer = new byte[4096];
            int ReadSize = 0;

            do
            {
                ReadSize = fsInput.Read(buffer, 0, buffer.Length); // 지금까지 읽은 값 반환
                int SendSize = clientSocket.Send(buffer, ReadSize, SocketFlags.None);

            } while (ReadSize > 0);

            fsInput.Close();
            #endregion

            clientSocket.Close();
            listensocket.Close();
        }

        /// <summary>
        /// 소켓 이미지파일 전송 GPT버전 서버
        /// </summary>
        /// <param name="args"></param>
        static void ImageSocketServerGPTver(string[] args)
        {
            // 1. 소켓 생성 (TCP 스트림 소켓)
            Socket listensocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 2. 서버의 IP와 포트 설정 후 바인딩 (모든 네트워크 인터페이스에서 4000번 포트 사용)
            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, 4000);
            listensocket.Bind(listenEndPoint);

            // 3. 클라이언트 접속 대기 (최대 10개 대기 가능)
            listensocket.Listen(10);
            Console.WriteLine("서버 시작됨. 클라이언트 연결 대기 중...");

            bool isRunning = true;
            while (isRunning)
            {
                // 4. 클라이언트 연결 수락
                Socket clientSocket = listensocket.Accept();
                Console.WriteLine("클라이언트 연결됨!");

                // 5. 전송할 이미지 파일 경로 설정
                string imagePath = @"C:\Users\lms29\Documents\GitHub\C-Sharp-Study\NetworkSample\Server\test.png";
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine("이미지 파일이 존재하지 않습니다.");
                    clientSocket.Close();
                    continue;
                }

                // 6. 이미지 파일을 바이트 배열로 읽기
                byte[] imageBytes = File.ReadAllBytes(imagePath);

                // 7. 이미지 크기(4바이트) 전송
                byte[] imageSize = BitConverter.GetBytes(imageBytes.Length);
                clientSocket.Send(imageSize);
                Console.WriteLine("이미지 크기 전송 완료: " + imageBytes.Length + " 바이트");

                // 8. 이미지 데이터 전송
                clientSocket.Send(imageBytes);
                Console.WriteLine("이미지 데이터 전송 완료");

                // 9. 클라이언트 소켓 종료
                clientSocket.Close();
                Console.WriteLine("클라이언트 연결 종료");
            }

            // 10. 서버 종료
            listensocket.Close();
            Console.WriteLine("서버 종료");
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
