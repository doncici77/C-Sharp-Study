namespace L20250217_02
{
    class Monster
    {
        public virtual void Move()
        {
            Console.WriteLine("이동한다.");
        }
    }

    class Slime : Monster
    {
        public override void Move()
        {
            Console.WriteLine("미끄러진다.");
        }
        public void Sticky()
        {
            Console.WriteLine("끈적거린다.");
        }
    }

    class Goblin : Monster
    {
        public override void Move()
        {
            Console.WriteLine("미끄러진다.");
        }
    }

    internal class Program
    {
        // Orange is a Fruit
        static void Main(string[] args)
        {
            Monster[] monster = new Monster[2];
            monster[0] = new Slime();
            monster[1] = new Goblin();

            // 다운캐스팅, 동적변환
            Slime? s = monster[0] as Slime; // 추천안함
            s.Sticky();

            for(int i = 0; i < 2; i++)
            {
                if(monster[i] is Goblin)
                {
                    Console.WriteLine("난 고블린이다.");
                }

                if (monster[i] is Slime)
                {
                    Console.WriteLine("난 슬라임이다.");
                }
            }
        }
    }
}
