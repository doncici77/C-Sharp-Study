using System.Net.Sockets;
using System.Net;
using System.Text;
using System;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
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
                if(RecvLength <= 0)
                {
                    // 내쪽에서 행함
                    // close == 0
                    // error < 0

                    isRunning = false;
                }

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
                if(SendLength <= 0)
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
    }
}
