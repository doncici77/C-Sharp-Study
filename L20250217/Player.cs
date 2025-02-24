using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250217
{
    public class Player : GameObject
    {
        public Player(int inX, int inY, char inShape)
        {
            X = inX;
            Y = inY;
            Shape = inShape;
            orderlayer = 4;
        }

        public override void Update()
        {
            if(Input.GetKeyDown(ConsoleKey.A) || Input.GetKeyDown(ConsoleKey.LeftArrow)) // a를 눌렀을때
            {
                if(X > 1)
                {
                    X--;
                }
            }
            else if(Input.GetKeyDown(ConsoleKey.D) || Input.GetKeyDown(ConsoleKey.RightArrow))
            {
                X++;
            }
            else if (Input.GetKeyDown(ConsoleKey.W) || Input.GetKeyDown(ConsoleKey.UpArrow))
            {
                if (Y > 1)
                {
                    Y--;
                }
            }
            else if (Input.GetKeyDown(ConsoleKey.S) || Input.GetKeyDown(ConsoleKey.DownArrow))
            {
                Y++;
            }
        }

    }
}
