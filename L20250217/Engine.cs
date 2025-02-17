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

        public void Load()
        {
            string[] scene = {
                "**********",
                "*P       *",
                "*        *",
                "*        *",
                "*        *",
                "*   M    *",
                "*        *",
                "*        *",
                "*       G*",
                "**********"
            };

            world = new World(); // world 객체 생성

            for (int y = 0; y < scene.Length; y++)
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
                        Floor floor = new Floor(x, y, scene[y][x]);
                        world.Instanciate(floor);// 만든거 등록
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

                }
            }
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
            Console.Clear();
            world.Rander();
        }

        public void Run()
        {
            while (isRunning)
            {
                ProcessInput();
                Update();
                Rander();
            }
        }

    }
}
