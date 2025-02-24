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
            isTrigger = true;
        }

        public override void Update()
        {
            if(Input.GetKeyDown(ConsoleKey.A) || Input.GetKeyDown(ConsoleKey.LeftArrow)) // a를 눌렀을때
            {
                if(!PredictCollision(X - 1, Y))
                {
                    X--;
                }

            }
            else if(Input.GetKeyDown(ConsoleKey.D) || Input.GetKeyDown(ConsoleKey.RightArrow))
            {
                if (!PredictCollision(X + 1, Y))
                {
                    X++;
                }
            }
            else if (Input.GetKeyDown(ConsoleKey.W) || Input.GetKeyDown(ConsoleKey.UpArrow))
            {
                if (!PredictCollision(X, Y - 1))
                {
                    Y--;
                }
            }
            else if (Input.GetKeyDown(ConsoleKey.S) || Input.GetKeyDown(ConsoleKey.DownArrow))
            {
                if (!PredictCollision(X, Y + 1))
                {
                    Y++;
                }
            }
        }

    }
}
