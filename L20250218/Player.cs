using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250218
{
    public class Player : GameObject
    {
        public Player(int inX, int inY, char inShape)
        {
            x = inX;
            y = inY;
            shape = inShape;
        }

        public override void Update()
        {
            GameEnd();
            Move();
        }

        public void Move()
        {
            if (Input.GetDownKey(ConsoleKey.D) || Input.GetDownKey(ConsoleKey.RightArrow)) // 오른쪽
            {
                if(x < 8)
                {
                    x++;
                }
            }
            else if (Input.GetDownKey(ConsoleKey.A) || Input.GetDownKey(ConsoleKey.LeftArrow)) // 왼쪽
            {
                if(x > 1)
                {
                    x--;
                }
            }
            else if (Input.GetDownKey(ConsoleKey.S) || Input.GetDownKey(ConsoleKey.DownArrow)) // 아래
            {
                if(y < 8)
                {
                    y++;
                }
            }
            else if (Input.GetDownKey(ConsoleKey.W) || Input.GetDownKey(ConsoleKey.UpArrow)) // 위
            {
                if(y > 1)
                {
                    y--;
                }
            }
        }

        public void MovePosition(int xP, int yP)
        {

        }

        public override void Rander()
        {
            GameEnd();
            Console.SetCursorPosition(x, y); //콘솔상의 커서의 위치를 x, y로 변경.
            Console.WriteLine(shape);
        }

        public void GameEnd()
        {
            if(x == 8 && y == 8)
            {
                Console.Clear();
                Console.WriteLine("Game Clear!");
            }
        }
    }
}
