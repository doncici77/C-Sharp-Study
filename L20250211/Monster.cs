using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250211
{
    public class Monster
    {
        public Monster() 
        {
            Console.WriteLine("몬스터 생성자");
        }
        ~Monster() 
        {
            Console.WriteLine("몬스터 소멸자");
        }
        public int hp = 100;
        public int gold = 0;
        public void Attack()
        {

        }
        public virtual void Move()
        {
            Console.WriteLine("몬스터가 걷는다.");
        }
        public void Die()
        {

        }
    }
}
