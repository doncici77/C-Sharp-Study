using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250211
{
    public class Player
    {
        public Player()
        {
            Console.WriteLine("플레이어 생성자");
        }

        public Player(int hp, int gold)
        {
            this.hp = hp;
            this.gold = gold;
            Console.WriteLine("플레이어 생성자2");
        }

        ~Player()
        {
            Console.WriteLine("플레이어 소멸자");
        }
        public int hp = 100;
        public int gold = 0;
        public void Attack()
        {

        }
        public void Move()
        {
            Console.WriteLine("플레이어가 움직임");
        }
        public void Die()
        {

        }
    }
}
