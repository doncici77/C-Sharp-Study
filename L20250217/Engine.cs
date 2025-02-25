using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250217
{
    public class Engine
    {
        private Engine()
        {

        }

        static protected Engine instance;

        // 더블 버퍼링
        static public char[ , ] backBuffer = new char[20, 40];
        static public char[ , ] frontBuffer = new char[20, 40];

        public static Engine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Engine();
                }

                return instance;
            }
        }

        protected bool isRunning = true; // 게임이 돌아가는 동안 계속 돌아가게 하기 위해 사용되는 변수

        protected ConsoleKeyInfo keyInfo; // 키보드 입력을 받기 위한 변수

        public World world; // World 객체 생성

        public void Load(string filename)
        {
            /*string tempScene = "";
            byte[] buffer = new byte[1024];
            FileStream fs = new FileStream("level01.map", FileMode.Open);

            fs.Seek(0, SeekOrigin.End);
            long fileSize = fs.Position;
            Console.WriteLine(fileSize);

            fs.Seek(0, SeekOrigin.Begin);
            int readCount = fs.Read(buffer, 0, (int)fileSize);
            tempScene = Encoding.UTF8.GetString(buffer);
            tempScene = tempScene.Replace("\0", "");
            string[] scene = tempScene.Split("\r\n");

            fs.Close();*/

            List<string> scene = new List<string>();

            StreamReader sr = new StreamReader(filename);
            while(!sr.EndOfStream)
            {
                scene.Add(sr.ReadLine());
            }
            sr.Close();


            world = new World(); // world 객체 생성

            for (int y = 0; y < scene.Count; y++)
            {
                for(int x = 0; x < scene[y].Length; x++)
                {
                    if(scene[y][x] == '*')
                    {
                        Wall wall = new Wall(x, y, scene[y][x]); // wall 객체 생성
                        world.Instanciate(wall); // 만든거 등록
                    }
                    else if(scene[y][x] == ' ')
                    {
                        //Floor floor = new Floor(x, y, scene[y][x]);
                        //world.Instanciate(floor);// 만든거 등록
                    }
                    else if(scene[y][x] == 'P')
                    {
                        Player player = new Player(x, y, scene[y][x]); 
                        world.Instanciate(player);
                    }
                    else if(scene[y][x] == 'M')
                    {
                        Monster monster = new Monster(x, y, scene[y][x]);
                        world.Instanciate(monster);
                    }
                    else if(scene[y][x] == 'G')
                    {
                        Goal goal = new Goal(x, y, scene[y][x]);
                        world.Instanciate(goal);
                    }
                    Floor floor = new Floor(x, y, ' ');
                    world.Instanciate(floor);
                }
            }
            // loading cpmplete
            // sort
            world.Sort();
        }

        public void ProcessInput()
        {
            Input.Process();
        }

        public void Update()
        {
            world.Update();
        }

        public void Rander()
        {
            // IO 제일 느려, 모니터 출력, 메모리 
            // Console.Clear();

            world.Rander();

            //메모리에 있는걸 한방에 붙여줘
            //back <-> front (flip)
            for (int Y = 0; Y < 20; Y++)
            {
                for(int X = 0; X < 40; X++)
                {
                    if (frontBuffer[Y, X] != Engine.backBuffer[Y, X])
                    {
                        frontBuffer[Y, X] = backBuffer[Y, X];
                        Console.SetCursorPosition(X, Y);
                        Console.Write(frontBuffer[Y, X]);
                    }
                }
            }
        }

        public void Run()
        {
            // double fps = 1.0 / Time.deltaTime.TotalMilliseconds; // FPS 계산법
            float frameTime = 1000.0f / 60.0f;
            float elpaseTime = 0.0f;

            Console.CursorVisible = false;
            while (isRunning)
            {
                Time.Update();
                //if (elpaseTime >= frameTime)
                //{
                    ProcessInput();
                    Update();
                    Rander();
                    Input.ClearInput();
                    elpaseTime = 0;
                //}
                //else
                //{
                //    elpaseTime += Time.deltaTime;
                //}
            }
        }
    }
}
