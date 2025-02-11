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
        protected int hp = 100;
        /*public int HP
        { 
            get 
            { 
                return hp; // 매개변수가 없을때
            }
            set 
            { 
                hp = value; // 매개변수가 있을때
            }
        }*/

        public int HP
        {
            get;
            set;
        }

        protected int gold = 0;

        public int GetHP()
        {
            return hp;
        }
        public void SetHP(int value)
        {
            if(value >= 0)
            {
                hp = value;
            }
        }
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
