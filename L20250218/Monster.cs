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

        /*public override bool IsCollide()
        {
            foreach (GameObject gameObject in Engine.Instance.world.gameObject)
            {
                if (gameObject.x == x && gameObject.y == y)
                {
                    if (gameObject is Player)
                    {
                        Engine.Instance.GameOver();
                    }

                    else if (gameObject is Wall)
                    {
                        return true;
                    }
                }
            }
            return false;
        }*/

        public override bool IsCollide()
        {
            GameObject[] gameObjects = Engine.Instance.world.gameObject;

            for (int i = 0; i < gameObjects.Length; i++)  // 일반 for문 사용
            {
                GameObject gameObject = gameObjects[i];

                // 디버깅용
                /*string a = gameObject.shape.ToString();
                string a2 = gameObjects[i].shape.ToString();
                string a4 = gameObjects[i].GetType().ToString();
                string a5 = gameObject.GetType().ToString();
                string a3 = $"{typeof(Wall)}";*/

                if (gameObject.x == this.x && gameObject.y == this.y)  // 위치 비교
                {
                    // Player인지 확인 (is 연산자 대신 타입 체크)
                    if (gameObject.GetType() == typeof(Player))
                    {
                        Engine.Instance.GameOver();
                        return true;  // 게임 오버 시 즉시 true 반환
                    }

                    // Wall인지 확인
                    if (gameObject.GetType() == typeof(Wall))
                    {
                        return true;
                    }
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
