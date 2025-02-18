using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250218
{
    public class Monster : GameObject
    {
        public Monster(int inX, int inY, char inShape)
        {
            x = inX;
            y = inY;
            shape = inShape;
        }

        Random rand = new Random();

        public override void Update()
        {
            Move();
            IsCollide();
        }

        public override bool IsCollide()
        {
            foreach (GameObject gameObject in Engine.Instance.world.gameObject)
            {
                if (gameObject.x == x && gameObject.y == y)
                {
                    if (gameObject is Player)
                    {
                        Engine.Instance.GameOver();
                    }

                    /*else if (gameObject is Wall)
                    {
                        return true;
                    }*/
                }
            }
            return false;
        }

        public void Move()
        {
            int pos = rand.Next(0, 4);
            if (pos == 0)
            {
                if (x < 8)
                {
                    x++;
                }
            }
            else if (pos == 1)
            {
                if (x > 1)
                {
                    x--;
                }
            }
            else if (pos == 2)
            {
                if (y < 8)
                {
                    y++;
                }
            }
            else if (pos == 3)
            {
                if (y > 1)
                {
                    y--;
                }
            }
        }
    }
}
