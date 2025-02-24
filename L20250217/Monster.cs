﻿using System;
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
            orderlayer = 5;
            isTrigger = true;
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
                    if (!PredictCollision(X - 1, Y))
                    {
                        X--;
                    }
                    break;

                case 1:
                    if (!PredictCollision(X + 1, Y))
                    {
                        X++;
                    }
                    break;

                case 2:
                    if (!PredictCollision(X, Y - 1))
                    {
                        Y--;
                    }
                    break;

                case 3:
                    if (!PredictCollision(X, Y + 1))
                    {
                        Y++;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
