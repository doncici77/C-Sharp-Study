using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250217
{
    public class Monster : GameObject
    {
        public Monster(int inX, int inY, char inShape)
        {
            X = inX;
            Y = inY;
            Shape = inShape;
        }

        public Random random = new Random();

        public override void Update()
        {
            Move();
        }

        public void Move()
        {
            int count = random.Next(0, 4);
            
            switch(count)
            {
                case 0:
                    if(X > 0)
                    {
                        X--;
                    }
                    break;

                case 1:
                    X++;
                    break;

                case 2:
                    if (X > 0)
                    {
                        Y--;
                    }
                    break;

                case 3:
                    Y++;
                    break;

                default:
                    break;
            }
        }
    }
}
