using System.Diagnostics;

namespace L20250211
{
    // 게임 // 플레이어 // 몬스터
    // 플레이어 // 공격 // 이동 // hp // 골드 // 죽음
    // 몬스터
    // 고블린 // 공격 // 걸어서 이동 // hp // 죽음
    // 슬라임 // 공격 // 미끄러져 이동 // hp // 죽음
    // 멧돼지 // 공격 // 뛰어서 이동 // hp // 죽음

    public class 부모
    {
        public int money;
    }

    public class 자식 : 부모
    {
        private int money;
    }

    public class 리모콘
    {
        public void 리모콘만져보기()
        {
            반도체때리기();
        }

        protected void 반도체때리기()
        {

        }
    }

    internal class Program
    {
        public static void Sample()
        {
            Goblin goblin = new Goblin();

        }

        static void Main(string[] args)
        {
            /*Game game = new Game();
            Console.WriteLine("1");

            Player player = new Player();
            Console.WriteLine("2");
            Player[] players = new Player[10];
            Console.WriteLine("3");
            players[0] = new Player();

            Sample();
            GC.Collect();
            Console.ReadLine();

            Player players2 = new Player(100,100);
            Console.WriteLine(players2.hp);*/

            /*Player player = new Player();

            Random rand = new Random();

            int goblinCount = rand.Next(0, 3);
            Goblin[] goblins = new Goblin[goblinCount];
            if (goblinCount > 0)
            {
                for (int i = 0; i < goblins.Length; i++)
                {
                    goblins[i] = new Goblin();
                }
            }

            int slimeCount = rand.Next(1, 5);
            Slime[] slimes = new Slime[slimeCount];
            for (int i = 0; i < slimes.Length; i++)
            {
                slimes[i] = new Slime();
            }

            int wildBoarCount = rand.Next(1, 3);
            WildBoar[] wildBoars = new WildBoar[wildBoarCount];
            for (int i = 0;i < wildBoars.Length; i++)
            {
                wildBoars[i] = new WildBoar();
            }

            while (true)
            {
                //Input();
                Console.ReadKey();
                Console.Clear();

                //Update();
                player.Move();
                for (int i = 0; i < goblins.Length; i++)
                {
                    goblins[i].Move();
                }
                for (int i = 0; i < slimes.Length; i++)
                {
                    slimes[i].Move();
                }
                for (int i = 0; i < wildBoars.Length; i++)
                {
                    wildBoars[i].Move();
                }

                //Rendar();
            }*/

            /*Monster goblin = new Goblin();
            goblin.Move();
            Goblin goblin2 = new Goblin();
            goblin2.Move();
            WildBoar wildBoar = new WildBoar();*/

            /*// C# , Managed language
            // C, C++, Unmanaged language
            Monster[] monsters = new Monster[2];
            monsters[0] = new Goblin();
            monsters[1] = new WildBoar();

            // 다형성, virtual, override
            monsters[0].Move();
            monsters[1].Move();*/

            자식 본체 = new 자식();
            본체.money = 10;

            리모콘 부수기 = new 리모콘();
            부수기.리모콘만져보기();
        }
    }
}
