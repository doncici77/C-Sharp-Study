using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250218
{
    public class GameObject
    {
        public int x;
        public int y;
        public char shape;

        public virtual void Collide()
        {

        }

        public virtual void Update()
        {

        }

        public void Dead()
        {

        }

        public virtual bool IsCollide()
        {
            return false;
        }

        public virtual void Rander()
        {
            Console.SetCursorPosition(x, y); //콘솔상의 커서의 위치를 x, y로 변경.
            Console.WriteLine(shape);
        }
    }
}
