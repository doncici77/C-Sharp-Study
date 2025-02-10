namespace L20250210
{
    /*class World
    {
        int wall; // 벽
        int bottom; // 바닥
        void NoPass() // 지나갈수 없음
        {

        }
        int player; // 플레이어
        int monseter; // 몬스터
        Position target; // 목적지
        bool exist; // 존재 여부
        void Move() // 캐릭터가 움직임
        {

        }
        void GameOver() // 게임이 종료됨
        {

        }
    }*/
    class Pixel
    {

        // 생성자 // 클래스에 무존건 있어야함. // 자동으로 생성되긴함
        public Pixel()
        {
            x = 0; 
            y = 0;
            r = 255; 
            g = 255; 
            b = 255;
            Console.WriteLine("생성자 호출");
        }

        // 소멸자 // 클래스에 무존건 있어야함. // 자동으로 생성되긴함
        ~Pixel()
        {
            Console.WriteLine("소멸자 호출");
        }

        public int x;
        public int y;
        public int r;
        public int g;
        public int b;
    }
    class Apple
        {
            public Apple() 
            {
                count++;
            }

            public enum EColor
            {
                None = 0,
                Red = 1,
                Yellow = 2,
                Green = 3,
                Blue = 4
            }

            public EColor color;
            public bool taste;
            public int shape;
            public int hp = 100;

            public void CanEat()
            {
                hp -= 10;
                Console.WriteLine($"남은 체력은 {hp}");

            }

            public void Move(Position target)
            {

            }

            public static void Die()
            {
                Console.WriteLine("죽었다.");
            }

            public static int count = 0;
        }

    struct Position
    {
        public int x; 
        public int y;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            /*Position[] positions = new Position[2];
            positions[0].y = 12;
            positions[0].x = 12;

            int[] numbers = new int[10];
            numbers[0] = 12;

            Apple[] apple = new Apple[3]; // stack 참조 변수 heap을 가르킴
            apple[0] = new Apple(); // heap 사과 모양 자료를 잡음 // heap apple 형태 메모리 공간 확보

            for (int i = 0; i < 3; i++)
            {
                apple[i] = new Apple();
            }

            apple[0].color = Apple.EColor.Yellow; // Yellow
            apple[1].color = Apple.EColor.Yellow; // Yellow
            apple[2].color = Apple.EColor.Yellow; // Yellow

            Apple apple1 = new Apple();
            apple1.color = Apple.EColor.Yellow; // Green

            Apple apple2 = new Apple();
            apple2.color = Apple.EColor.Green; // Green

            Apple apple3 = new Apple();
            apple3.color = Apple.EColor.Red; // Red

            apple[0].CanEat();
            apple[0].Move(positions[0]);*/

            /*Pixel[] pixels = new Pixel[15];
            for(int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = new Pixel();
            }
            pixels[0].x = 0;
            pixels[0].y = 0;
            pixels[0].r = 165;
            pixels[0].g = 55;
            pixels[0].b = 128;

            pixels[1].x = 0;
            pixels[1].y = 1;
            pixels[1].r = 133;
            pixels[1].g = 28;
            pixels[1].b = 182;

            Console.WriteLine($"{pixels[1].g}, {pixels[0].r}");*/

            World world = new World();
            world.player.Move();
            world.goal.IsFinish(world.player);
        }
    }
}
