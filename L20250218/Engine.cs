using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250218
{
    public class Engine
    {
        private Engine() { }

        static Engine instance; // 싱글톤 패턴

        public static Engine Instance // 싱글톤 패턴은 하나의 객체만 생성하고 그 객체를 여러 클래스에서 공유하는 패턴
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

        public World world;

        public bool isRunning = true;

        public void Load()
        {
            string[] scene =
            {
                "**********",
                "*P       *",
                "*        *",
                "*        *",
                "*        *",
                "*    M   *",
                "*        *",
                "*        *",
                "*       G*",
                "**********"
            };

            world = new World(); // 월드 객체 생성

            for (int y = 0; y < scene.Length; y++)
            {
                for (int x = 0; x < scene[y].Length; x++)
                {
                    if (scene[y][x] == 'P')
                    {
                        Player player = new Player(x, y, scene[y][x]); // Player 객체 생성
                        world.Instanciate(player); // 만든거 월드 클래스에 등록
                    }
                    else if (scene[y][x] == 'M')
                    {
                        Monster monster = new Monster(x, y, scene[y][x]); // Monster 객체 생성
                        world.Instanciate(monster); // 만든거 월드 클래스에 등록
                    }
                    else if (scene[y][x] == 'G')
                    {
                        Goal goal = new Goal(x, y, scene[y][x]); // Goal 객체 생성
                        world.Instanciate(goal); // 만든거 월드 클래스에 등록
                    }
                    else if(scene[y][x] == '*')
                    {
                        Wall wall = new Wall(x, y, scene[y][x]); // Wall 객체 생성
                        world.Instanciate(wall); // 만든거 월드 클래스에 등록
                    }
                    else if (scene[y][x] == ' ')
                    {
                        Floor floor = new Floor(x, y, scene[y][x]); // Floor 객체 생성
                        world.Instanciate(floor); // 만든거 월드 클래스에 등록
                    }
                }
            }
        }

        public void ProcessInput() // 데이터 입력
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
            while(isRunning)
            {
                ProcessInput();
                Update();
                Rander();
            }
        }

        public void GameOver()
        {
            Console.Clear();
            Console.WriteLine("GameOver");
            isRunning = false;
        }
    }
}
