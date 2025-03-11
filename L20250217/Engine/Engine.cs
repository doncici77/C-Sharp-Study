using SDL2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
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
        static public char[,] backBuffer = new char[20, 40];
        static public char[,] frontBuffer = new char[20, 40];

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

        public nint myWindow;
        public nint myRenderer;
        public SDL.SDL_Event myEvent; // 메세지 처리(사용자 처리가 추가 구조를 바꿈)

        public bool Init()
        {
            // 엔진 초기화
            if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) < 0) // 초기화
            {
                Console.WriteLine("Fail init");
                return false;
            }

            // 설정 파일 읽어오기

            // 창 만들기
            myWindow = SDL.SDL_CreateWindow(
                "Game",
                100, 100,
                640, 480,
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN); // 윈도우 생성

            // 붓
            myRenderer = SDL.SDL_CreateRenderer(myWindow, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC | SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE);

            return true;
        }

        public bool Quit()
        {
            // 종료
            SDL.SDL_DestroyRenderer(myRenderer);

            SDL.SDL_DestroyWindow(myWindow);

            SDL.SDL_Quit();

            return true;
        }

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
            while (!sr.EndOfStream)
            {
                scene.Add(sr.ReadLine());
            }
            sr.Close();

            world = new World(); // world 객체 생성

            for (int y = 0; y < scene.Count; y++)
            {
                for (int x = 0; x < scene[y].Length; x++)
                {
                    if (scene[y][x] == '*')
                    {
                        GameObject wall = new GameObject();
                        wall.Name = "Wall";
                        wall.transform.X = x;
                        wall.transform.Y = y;

                        SpriteRenderer spriteRenderer = wall.AddComponent(new SpriteRenderer());
                        spriteRenderer.LoadBmp("wall.bmp");
                        spriteRenderer.orderlayer = 1;

                        spriteRenderer.Shape = '*';

                        world.Instanciate(wall); // 만든거 등록
                    }
                    else if (scene[y][x] == ' ')
                    {

                    }
                    else if (scene[y][x] == 'P')
                    {
                        //Player player = new Player(x, y, scene[y][x]);
                        //world.Instanciate(player);
                        GameObject player = new GameObject();
                        player.Name = "Player";
                        player.transform.X = x;
                        player.transform.Y = y;

                        player.AddComponent(new PlayerController());
                        SpriteRenderer spriteRenderer = player.AddComponent(new SpriteRenderer());
                        spriteRenderer.colorKey.r = 255;
                        spriteRenderer.colorKey.g = 0;
                        spriteRenderer.colorKey.b = 255;
                        spriteRenderer.colorKey.a = 255;
                        spriteRenderer.LoadBmp("player.bmp", true);
                        spriteRenderer.preocessTime = 150.0f;
                        spriteRenderer.maxCellCountX = 5;
                        spriteRenderer.orderlayer = 3;

                        spriteRenderer.Shape = 'P';

                        world.Instanciate(player);

                    }
                    else if (scene[y][x] == 'M')
                    {
                        GameObject monster = new GameObject();
                        monster.Name = "Monster";
                        monster.transform.X = x;
                        monster.transform.Y = y;

                        SpriteRenderer spriteRenderer = monster.AddComponent(new SpriteRenderer());
                        spriteRenderer.colorKey.r = 255;
                        spriteRenderer.colorKey.g = 255;
                        spriteRenderer.colorKey.b = 255;
                        spriteRenderer.colorKey.a = 255;
                        spriteRenderer.LoadBmp("monster.bmp");
                        spriteRenderer.orderlayer = 4;

                        spriteRenderer.Shape = 'M';

                        world.Instanciate(monster);
                    }
                    else if (scene[y][x] == 'G')
                    {
                        GameObject goal = new GameObject();
                        goal.Name = "Goal";
                        goal.transform.X = x;
                        goal.transform.Y = y;

                        SpriteRenderer spriteRenderer = goal.AddComponent(new SpriteRenderer());
                        spriteRenderer.colorKey.r = 255;
                        spriteRenderer.colorKey.g = 255;
                        spriteRenderer.colorKey.b = 255;
                        spriteRenderer.colorKey.a = 255;
                        spriteRenderer.LoadBmp("goal.bmp");
                        spriteRenderer.orderlayer = 2;

                        spriteRenderer.Shape = 'G';

                        world.Instanciate(goal);
                    }

                    if (true)
                    {
                        GameObject floor = new GameObject();
                        floor.Name = "Floor";
                        floor.transform.X = x;
                        floor.transform.Y = y;

                        SpriteRenderer spriteRenderer = floor.AddComponent(new SpriteRenderer());
                        spriteRenderer.LoadBmp("floor.bmp");
                        spriteRenderer.orderlayer = 0;

                        spriteRenderer.Shape = ' ';

                        world.Instanciate(floor);
                    }
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

            SDL.SDL_SetRenderDrawColor(myRenderer, 0, 51, 102, 0);
            SDL.SDL_RenderClear(myRenderer);

            world.Rander();

            //메모리에 있는걸 한방에 붙여줘
            //back <-> front (flip)
            for (int Y = 0; Y < 20; Y++)
            {
                for (int X = 0; X < 40; X++)
                {
                    if (frontBuffer[Y, X] != backBuffer[Y, X])
                    {
                        frontBuffer[Y, X] = backBuffer[Y, X];
                        Console.SetCursorPosition(X, Y);
                        Console.Write(frontBuffer[Y, X]);
                    }
                }
            }

            SDL.SDL_RenderPresent(myRenderer);
        }

        public void Run()
        {
            // double fps = 1.0 / Time.deltaTime.TotalMilliseconds; // FPS 계산법
            Console.CursorVisible = false;

            while (isRunning)
            {
                SDL.SDL_PollEvent(out myEvent);

                Time.Update();

                switch (myEvent.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        isRunning = false;
                        break;
                }

                Update();
                Rander();
            }
        }
    }
}
